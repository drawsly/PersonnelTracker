using PersonnelTracker.Services;

namespace PersonnelTracker.ViewModels.Pages
{
    public partial class OrganizasyonIslemleriViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly GostergePaneliViewModel _gostergePaneliViewModel;
        private readonly ISnackbarService _snackbarService;
        private readonly IContentDialogService _contentDialogService;

        public OrganizasyonIslemleriViewModel(
            INavigationService navigationService,
            GostergePaneliViewModel gostergePaneliViewModel,
            ISnackbarService snackbarService,
            IContentDialogService contentDialogService)
        {
            _navigationService = navigationService;
            _gostergePaneliViewModel = gostergePaneliViewModel;
            _snackbarService = snackbarService;
            _contentDialogService = contentDialogService;

        }
    }
}
