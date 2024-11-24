using PersonnelTracker.ViewModels.Pages;

namespace PersonnelTracker.Views.Pages
{
    /// <summary>
    /// Interaction logic for OrganizasyonIslemleriPage.xaml
    /// </summary>
    public partial class OrganizasyonIslemleriPage : INavigableView<OrganizasyonIslemleriViewModel>
    {
        public OrganizasyonIslemleriViewModel ViewModel { get; }

        public OrganizasyonIslemleriPage(OrganizasyonIslemleriViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = viewModel;

            InitializeComponent();
        }
    }
}
