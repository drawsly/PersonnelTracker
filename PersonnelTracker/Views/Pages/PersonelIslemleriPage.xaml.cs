using PersonnelTracker.ViewModels.Pages;

namespace PersonnelTracker.Views.Pages
{
    /// <summary>
    /// Interaction logic for PersonelIslemleriPage.xaml
    /// </summary>
    public partial class PersonelIslemleriPage : INavigableView<PersonelIslemleriViewModel>
    {
        public PersonelIslemleriViewModel ViewModel { get; }

        public PersonelIslemleriPage(PersonelIslemleriViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = viewModel;

            InitializeComponent();

        }
    }
}
