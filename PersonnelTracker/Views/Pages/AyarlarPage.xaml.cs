using PersonnelTracker.ViewModels.Pages;

namespace PersonnelTracker.Views.Pages
{
    public partial class AyarlarPage : INavigableView<AyarlarViewModel>
    {
        public AyarlarViewModel ViewModel { get; }

        public AyarlarPage(AyarlarViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = viewModel;

            InitializeComponent();
        }
    }
}
