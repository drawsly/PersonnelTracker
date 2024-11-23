using PersonnelTracker.Models.Data;
using PersonnelTracker.Services;
using Wpf.Ui;
using Wpf.Ui.Extensions;

namespace PersonnelTracker.ViewModels.Pages.piPages
{
    public partial class PersonelEkleViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly PersonelService _personelService;
        private readonly PersonelIslemleriViewModel _personelIslemleriViewModel;
        private readonly GostergePaneliViewModel _gostergePaneliViewModel;
        private readonly ISnackbarService _snackbarService;
        private readonly IContentDialogService _contentDialogService;

        #region Personel Ekle Propertyleri 
        private string? _tckn;
        public string? Tckn
        {
            get => _tckn;
            set => SetProperty(ref _tckn, value);
        }

        private string? _ad;
        public string? Ad
        {
            get => _ad;
            set => SetProperty(ref _ad, value);
        }

        private string? _soyad;
        public string? Soyad
        {
            get => _soyad;
            set => SetProperty(ref _soyad, value);
        }

        private string? _telefon;
        public string? Telefon
        {
            get => _telefon;
            set => SetProperty(ref _telefon, value);
        }

        private string? _eposta;
        public string? Eposta
        {
            get => _eposta;
            set => SetProperty(ref _eposta, value);
        }

        private string? _iban;
        public string? Iban
        {
            get => _iban;
            set => SetProperty(ref _iban, value);
        }

        private DateOnly _dogumTarihi;
        public DateOnly DogumTarihi
        {
            get => _dogumTarihi;
            set => SetProperty(ref _dogumTarihi, value);
        }

        private DateOnly _iseGirisTarihi;
        public DateOnly IseGirisTarihi
        {
            get => _iseGirisTarihi;
            set => SetProperty(ref _iseGirisTarihi, value);
        }

        private decimal _sabitMaas;
        public decimal SabitMaas
        {
            get => _sabitMaas;
            set => SetProperty(ref _sabitMaas, value); // SetProperty ile değişikliği bildir
        }

        private ObservableCollection<Departman> _departmanlar = new ObservableCollection<Departman>();
        public ObservableCollection<Departman> Departmanlar
        {
            get => _departmanlar;
            set => SetProperty(ref _departmanlar, value);
        }

        private ObservableCollection<Pozisyon> _pozisyonlar = new ObservableCollection<Pozisyon>();
        public ObservableCollection<Pozisyon> Pozisyonlar
        {
            get => _pozisyonlar;
            set => SetProperty(ref _pozisyonlar, value);
        }

        private Departman? _selectedDepartman;
        public Departman? SelectedDepartman
        {
            get => _selectedDepartman;
            set
            {
                SetProperty(ref _selectedDepartman, value);
                LoadPozisyonlarAsync().ConfigureAwait(false); ; // Seçilen departmana göre pozisyonları yükler
            }
        }

        private Pozisyon? _selectedPozisyon;
        public Pozisyon? SelectedPozisyon
        {
            get => _selectedPozisyon;
            set => SetProperty(ref _selectedPozisyon, value);
        }

        private string? _selectedCinsiyet = "E"; // Varsayılan erkek seçili
        public string? SelectedCinsiyet
        {
            get => _selectedCinsiyet;
            set => SetProperty(ref _selectedCinsiyet, value);
        }

        #endregion

        // ReSharper disable once ConvertToPrimaryConstructor
        public PersonelEkleViewModel(
            INavigationService navigationService,
            PersonelService personelService,
            PersonelIslemleriViewModel personelIslemleriViewModel,
            GostergePaneliViewModel gostergePaneliViewModel,
            ISnackbarService snackbarService,
            IContentDialogService contentDialogService)
        {
            _navigationService = navigationService;
            _personelService = personelService;
            _personelIslemleriViewModel = personelIslemleriViewModel;
            _gostergePaneliViewModel = gostergePaneliViewModel;
            _snackbarService = snackbarService;
            _contentDialogService = contentDialogService;
        }

        public async Task InitializeAsync()
        {
            await LoadDepartmanlarAsync();
        }

        public ICommand EklePersonelCommand => new RelayCommand(async () => await EklePersonelAsync());
        private async Task EklePersonelAsync()
        {
            // Eksik alanları kontrol et
            if (string.IsNullOrWhiteSpace(Tckn) ||
                string.IsNullOrWhiteSpace(Ad) ||
                string.IsNullOrWhiteSpace(Soyad) ||
                SelectedDepartman == null ||
                SelectedPozisyon == null ||
                IseGirisTarihi == DateOnly.MinValue ||
                // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                SelectedCinsiyet == null ||
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                SabitMaas == null)
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            {
                _snackbarService.Show(
                    "Eksik Bilgiler",
                    "Tüm gerekli alanları doldurduğunuzdan emin olun.",
                    ControlAppearance.Caution,
                    new SymbolIcon(SymbolRegular.Warning24),
                    TimeSpan.FromSeconds(3)
                );
                return;
            }

            // Yeni personeli oluşturuyoruz
            var yeniPersonel = new Personel
            {
                Ad = Ad,
                Soyad = Soyad,
                Tckn = Tckn,
                Telefon = Telefon,
                Eposta = Eposta,
                Iban = Iban,
                DogumTarihi = DogumTarihi,
                IseGirisTarihi = IseGirisTarihi,
                SabitMaas = SabitMaas,
                DepartmanId = SelectedDepartman.DepartmanId,
                PozisyonId = SelectedPozisyon.PozisyonId,
                Cinsiyet = SelectedCinsiyet
            };

            bool result = await _personelService.AddPersonelAsync(yeniPersonel); // Personel ekle

            if (result)
            {
                // Personel başarılı bir şekilde eklenirse, istatistikleri güncelle
                await _gostergePaneliViewModel.UpdateStatisticsAsync();

                await _personelIslemleriViewModel.LoadPersonelsAsync();

                _navigationService.GoBack();
                _snackbarService.Show(
                    $"{Ad} {Soyad} isimli personel başarıyla eklendi.",
                    "Personel başarıyla sisteme kaydedildi.",
                    ControlAppearance.Success,
                    new SymbolIcon(SymbolRegular.CheckmarkCircle24),
                    TimeSpan.FromSeconds(3)
                );
            }
        }

        private async Task LoadDepartmanlarAsync()
        {
            var departmanlar = await _personelService.GetDepartmanlarAsync();
            Departmanlar = new ObservableCollection<Departman>(departmanlar);
        }

        private async Task LoadPozisyonlarAsync()
        {
            if (SelectedDepartman == null) return;

            var pozisyonlar = await _personelService.GetPozisyonlarByDepartmanAsync(SelectedDepartman.DepartmanId);
            Pozisyonlar = new ObservableCollection<Pozisyon>(pozisyonlar);
        }

        public void ChangeDate()
        {
            DogumTarihi = DateOnly.FromDateTime(DateTime.Now);
            IseGirisTarihi = DateOnly.FromDateTime(DateTime.Now);
        }

        public ICommand NavigateBack => new RelayCommand(async () => await NavigateBackAsync());
        private async Task NavigateBackAsync()
        {
            ContentDialogResult result = await _contentDialogService.ShowSimpleDialogAsync(
                new SimpleContentDialogCreateOptions()
                {
                    Title = "Gerçekten vazgeçmek istiyor musunuz?",
                    Content = "Kaydedilmemiş değişiklikler kaybolacak. Geri gitmek istediğinizden emin misiniz?",
                    PrimaryButtonText = "Vazgeç",
                    CloseButtonText = "Düzenlemeye Devam Et",
                }
            );

            if (result == ContentDialogResult.Primary)
            {
                _navigationService.GoBack();
            }
        }
    }
}
