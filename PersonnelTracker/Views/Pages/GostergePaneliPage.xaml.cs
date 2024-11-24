using PersonnelTracker.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace PersonnelTracker.Views.Pages
{
    /// <summary>
    /// Interaction logic for PersonelIslemleriPage.xaml
    /// </summary>
    public partial class GostergePaneliPage : INavigableView<GostergePaneliViewModel>
    {
        public GostergePaneliViewModel ViewModel { get; }

        public GostergePaneliPage(GostergePaneliViewModel viewModel)
        {
            ViewModel = viewModel;

            DataContext = viewModel;

            InitializeComponent();
        }
    }
}