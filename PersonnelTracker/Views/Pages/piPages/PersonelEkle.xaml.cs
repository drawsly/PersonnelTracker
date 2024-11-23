using PersonnelTracker.Models.Data;
using PersonnelTracker.ViewModels.Pages.piPages;

namespace PersonnelTracker.Views.Pages.piPages
{
    public partial class PersonelEkle : INavigableView<PersonelEkleViewModel>
    {
        public PersonelEkleViewModel ViewModel { get; }
        public PersonelEkle(PersonelEkleViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = viewModel;

            InitializeComponent();

            InitializeViewModelAsync();

            // Sayfa yüklendiğinde verileri sıfırla
            Loaded += (sender, e) => ViewModel.ChangeDate();
        }

        private async void InitializeViewModelAsync()
        {
            await ViewModel.InitializeAsync();
        }
    }
}
