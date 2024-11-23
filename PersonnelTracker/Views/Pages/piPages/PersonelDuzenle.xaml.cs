using PersonnelTracker.ViewModels.Pages;
using PersonnelTracker.ViewModels.Pages.piPages;
using System.Windows.Navigation;

namespace PersonnelTracker.Views.Pages.piPages
{
    public partial class PersonelDuzenle : INavigableView<PersonelDuzenleViewModel>
    {
        public PersonelDuzenleViewModel ViewModel { get; }
        public PersonelDuzenle(PersonelDuzenleViewModel viewModel, PersonelIslemleriViewModel personelIslemleriViewModel)
        {
            ViewModel = viewModel;
            DataContext = viewModel;

            // PersonelIslemleriViewModel'den selectedPersonel'i al
            if (personelIslemleriViewModel.SelectedPersonel != null)
            {
                // SelectedPersonel bilgilerini ViewModel'e aktar
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                ViewModel.InitializeAsync();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }

            InitializeComponent();

        }

    }
}
