using PersonnelTracker.ViewModels.Pages;

namespace PersonnelTracker.Views.Pages
{
    /// <summary>
    /// Interaction logic for PersonelIslemleri.xaml
    /// </summary>
    public partial class PersonelIslemleri : INavigableView<PersonelIslemleriViewModel>
    {
        public PersonelIslemleriViewModel ViewModel { get; }

        public PersonelIslemleri(PersonelIslemleriViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = viewModel;

            InitializeComponent();

        }
    }
}
