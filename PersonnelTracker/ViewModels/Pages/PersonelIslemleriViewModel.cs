using PersonnelTracker.Models.Data;
using PersonnelTracker.Services;
using PersonnelTracker.ViewModels.Pages.piPages;
using PersonnelTracker.Views.Pages.piPages;
using Wpf.Ui.Extensions;

namespace PersonnelTracker.ViewModels.Pages
{
    public partial class PersonelIslemleriViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly PersonelService _personelService;
        private readonly GostergePaneliViewModel _gostergePaneliViewModel;
        private readonly ISnackbarService _snackbarService;
        private readonly IContentDialogService _contentDialogService;

        public bool CanNavigateToEdit => SelectedPersonel != null;

        private ObservableCollection<Personel> _allPersonels = new ObservableCollection<Personel>();
        private ObservableCollection<Personel> _filteredPersonels = new ObservableCollection<Personel>();
        public ObservableCollection<Personel> FilteredPersonels
        {
            get => _filteredPersonels;
            set
            {
                _filteredPersonels = value;
                OnPropertyChanged();
            }
        }

        private Personel? _selectedPersonel;
        public Personel? SelectedPersonel
        {
            get => _selectedPersonel;
            set
            {
                SetProperty(ref _selectedPersonel, value);
                SilPersonelCommand.NotifyCanExecuteChanged(); // Silme komutunu güncelle
                NavigateForwardCommand.NotifyCanExecuteChanged(); // Navigasyon komutunu güncelle
                OnPropertyChanged(nameof(CanNavigateToEdit)); // Düzenleme butonunu aktif hale getir
            }
        }

        // SearchQuery ile yapılan her değişiklikle filtreleme işlemi yapılacak
        private string _searchQuery = string.Empty; // Varsayılan değer

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                SetProperty(ref _searchQuery, value);
                FilterPersonels(value); // Arama sorgusunu işleyin
            }
        }

        public PersonelIslemleriViewModel(
            INavigationService navigationService,
            PersonelService personelService,
            GostergePaneliViewModel gostergePaneliViewModel,
            ISnackbarService snackbarService,
            IContentDialogService contentDialogService)
        {
            _navigationService = navigationService;
            _personelService = personelService;
            _gostergePaneliViewModel = gostergePaneliViewModel;
            _snackbarService = snackbarService;
            _contentDialogService = contentDialogService;

            Task.Run(async () => await LoadPersonelsAsync());
        }

        [RelayCommand(CanExecute = nameof(CanSilPersonel))]
        private async Task SilPersonelAsync()
        {
            if (SelectedPersonel == null)
                return;

            // Onay dialogu
            ContentDialogResult result = await _contentDialogService.ShowSimpleDialogAsync(
                new SimpleContentDialogCreateOptions()
                {
                    Title = "Personel Silme Onayı",
                    Content = $"{SelectedPersonel.Ad} {SelectedPersonel.Soyad} isimli personeli silmek istediğinizden emin misiniz? Bu işlem geri alınamaz.",
                    PrimaryButtonText = "Evet",
                    CloseButtonText = "Hayır",
                }
            );

            if (result != ContentDialogResult.Primary)
                return; // Kullanıcı hayır dedi, işlemi sonlandır

            // Silme işlemi
            bool isDeleted = await _personelService.DeletePersonelByIdAsync(SelectedPersonel.PersonelId);

            if (isDeleted)
            {
                _snackbarService.Show(
                    $"{SelectedPersonel.Ad} {SelectedPersonel.Soyad} isimli personel başarıyla silindi.",
                    "Personel başarıyla sistemden kaldırıldı.",
                    ControlAppearance.Success,
                    new SymbolIcon(SymbolRegular.CheckmarkCircle24),
                    TimeSpan.FromSeconds(3)
                );

                _allPersonels.Remove(SelectedPersonel);
                FilteredPersonels.Remove(SelectedPersonel);
                await _gostergePaneliViewModel.UpdateStatisticsAsync();

                SelectedPersonel = null; // Seçili personeli sıfırla
            }
            else
            {
                _snackbarService.Show(
                    "Silme Başarısız",
                    "Personel silinirken bir hata oluştu.",
                    ControlAppearance.Danger,
                    new SymbolIcon(SymbolRegular.ErrorCircle24),
                    TimeSpan.FromSeconds(3)
                );
            }
        }

        private bool CanSilPersonel() => SelectedPersonel != null;


        // Personelleri yükle ve filtreleme işlemini başlat
        public async Task LoadPersonelsAsync()
        {
            var personelList = await _personelService.GetAllPersonelsAsync();
            _allPersonels = new ObservableCollection<Personel>(personelList);
            FilteredPersonels = new ObservableCollection<Personel>(_allPersonels);
        }
        public void AddPersonelToList(Personel yeniPersonel)
        {
            // Personel eklenirse listeyi güncelle
            _filteredPersonels.Add(yeniPersonel);
        }

        // Arama sorgusuna göre filtreleme yap
        public void FilterPersonels(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                // Arama sorgusu boşsa tüm personelleri göster
                FilteredPersonels = new ObservableCollection<Personel>(_allPersonels);
            }
            else
            {
                var filtered = _personelService.FilterPersonels(query, _allPersonels.ToList());
                FilteredPersonels = new ObservableCollection<Personel>(filtered);
            }
        }

        // Sayfalar arası navigasyon
        [RelayCommand]
        private void NavigateForward(Type pageType)
        {
            if (pageType == typeof(PersonelEkle))
            {
                _navigationService.NavigateWithHierarchy(pageType);
            }
            else if (SelectedPersonel != null)
            {
                _navigationService.NavigateWithHierarchy(pageType);
            }
        }

    }
}