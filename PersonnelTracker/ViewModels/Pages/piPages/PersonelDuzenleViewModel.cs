using PersonnelTracker.Models.Data;
using PersonnelTracker.Services;
using Wpf.Ui;
using Wpf.Ui.Extensions;

namespace PersonnelTracker.ViewModels.Pages.piPages
{
    public partial class PersonelDuzenleViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly PersonelService _personelService;
        private readonly PersonelIslemleriViewModel _personelIslemleriViewModel;
        private readonly ISnackbarService _snackbarService;
        private readonly IContentDialogService _contentDialogService;

        #region Personel Düzenleme Propertyleri

        private Personel? _selectedPersonel;
        public Personel? SelectedPersonel
        {
            get => _selectedPersonel;
            set
            {
                _selectedPersonel = value;
                OnPropertyChanged();
            }
        }

        private string? _headerContent;
        public string? HeaderContent
        {
            get => _headerContent;
            set => SetProperty(ref _headerContent, value);
        }

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

        private int _departmanId;
        public int DepartmanId
        {
            get => _departmanId;
            set => SetProperty(ref _departmanId, value); // SetProperty ile değişikliği bildir
        }

        private int _pozisyonId;
        public int PozisyonId
        {
            get => _pozisyonId;
            set => SetProperty(ref _pozisyonId, value); // SetProperty ile değişikliği bildir
        }

        private string _cinsiyet;
        public string Cinsiyet
        {
            get => _cinsiyet;
            set => SetProperty(ref _cinsiyet, value); // SetProperty ile değişikliği bildir
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
                if (_selectedDepartman != value)
                {
                    SetProperty(ref _selectedDepartman, value);

                    // Departman değiştiğinde pozisyonları güncelle (beklenerek)
                    if (value != null)
                    {
                        // Asenkron metot çağrısını beklemek için Task kullanıldı
                        Task.Run(async () => await OnDepartmanChanged(value));
                    }
                }
            }
        }

        private Pozisyon? _selectedPozisyon;
        public Pozisyon? SelectedPozisyon
        {
            get => _selectedPozisyon;
            set => SetProperty(ref _selectedPozisyon, value);
        }

        private string? _selectedCinsiyet; // Varsayılan erkek seçili
        public string? SelectedCinsiyet
        {
            get => _selectedCinsiyet;
            set => SetProperty(ref _selectedCinsiyet, value);
        }
        #endregion


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public PersonelDuzenleViewModel(
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            INavigationService navigationService,
            PersonelService personelService,
            PersonelIslemleriViewModel personelIslemleriViewModel,
            ISnackbarService snackbarService,
            IContentDialogService contentDialogService)
        {
            _navigationService = navigationService;
            _personelService = personelService;
            _personelIslemleriViewModel = personelIslemleriViewModel;
            _snackbarService = snackbarService;
            _contentDialogService = contentDialogService;

        }

        // SelectedDepartman değiştiğinde pozisyonları yenileyen kod:
        private async Task OnDepartmanChanged(Departman departman)
        {
            if (departman != null)
            {
                // Pozisyonları departman id'sine göre yükle
                var pozisyonlar = await _personelService.GetPozisyonlarByDepartmanAsync(departman.DepartmanId);

                // Pozisyonlar koleksiyonunu güncelle
                Pozisyonlar = new ObservableCollection<Pozisyon>(pozisyonlar);

                // Pozisyonlar yüklendikten sonra doğru pozisyonu seç
                if (SelectedPersonel != null && Pozisyonlar != null)
                {
                    var selectedPozisyonId = SelectedPersonel.PozisyonId;
                    SelectedPozisyon = Pozisyonlar.FirstOrDefault(p => p.PozisyonId == selectedPozisyonId);
                }
            }
        }

        public async Task InitializeAsync()
        {
            SelectedPersonel = _personelIslemleriViewModel.SelectedPersonel;

            if (SelectedPersonel != null)
            {
                // Personel bilgilerini ilgili property'lere aktar
                HeaderContent = SelectedPersonel.AdSoyad;
                Ad = SelectedPersonel.Ad;
                Soyad = SelectedPersonel.Soyad;
                Tckn = SelectedPersonel.Tckn;
                Telefon = SelectedPersonel.Telefon;
                Eposta = SelectedPersonel.Eposta;
                Iban = SelectedPersonel.Iban;
                DogumTarihi = SelectedPersonel.DogumTarihi;
                IseGirisTarihi = SelectedPersonel.IseGirisTarihi;
                SabitMaas = SelectedPersonel.SabitMaas;

                SelectedCinsiyet = SelectedPersonel.Cinsiyet ?? "Erkek";

                // Departmanları yükle
                Departmanlar = new ObservableCollection<Departman>(await _personelService.GetDepartmanlarAsync());
                SelectedDepartman = Departmanlar.FirstOrDefault(d => d.DepartmanId == SelectedPersonel.DepartmanId);

                // Pozisyonları departmana göre yükle
                if (SelectedDepartman != null)
                {
                    Pozisyonlar = new ObservableCollection<Pozisyon>(await _personelService.GetPozisyonlarByDepartmanAsync(SelectedDepartman.DepartmanId));

                    // Pozisyon yüklendikten sonra seçili pozisyonu ata
                    SelectedPozisyon = Pozisyonlar.FirstOrDefault(p => p.PozisyonId == SelectedPersonel.PozisyonId);
                }
            }
            else
            {
                _snackbarService.Show("Personel bulunamadı", "Hata", ControlAppearance.Danger);
            }
        }

        public ICommand UpdatePersonelCommand => new RelayCommand(async () => await UpdatePersonelAsync());
        private async Task UpdatePersonelAsync()
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
            // Mevcut personeli almak için SelectedPersonel kullanıyoruz
            if (SelectedPersonel == null) return; // Eğer personel seçilmemişse işlem yapma

            var personel = new Personel
            {
                PersonelId = SelectedPersonel.PersonelId, // Seçili personel id'si
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

            // Personel güncelleme işlemi
            var result = await _personelService.UpdatePersonelAsync(personel);

            if (result)
            {
                _personelIslemleriViewModel.SelectedPersonel = null;

                await _personelIslemleriViewModel.LoadPersonelsAsync();

                _navigationService.GoBack();

                _snackbarService.Show(
                    $"Personelin bilgileri başarıyla güncellendi",
                    "Personel başarıyla sisteme kaydedildi.",
                    ControlAppearance.Success,
                    new SymbolIcon(SymbolRegular.CheckmarkCircle24),
                    TimeSpan.FromSeconds(3)
                );
            }
            else
            {
                _snackbarService.Show("Personel kaydedilemedi", "Hata", ControlAppearance.Danger);
            }
        }

        public ICommand NavigateBackCommand => new RelayCommand(async () => await NavigateBackAsync());
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
