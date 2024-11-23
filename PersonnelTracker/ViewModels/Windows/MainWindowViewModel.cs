using System.Collections.ObjectModel;
using PersonnelTracker.Views.Pages;
using LiveChartsCore.Drawing;
using Wpf.Ui.Controls;

namespace PersonnelTracker.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "PersonnelTracker";

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new ObservableCollection<object>
    {
        new NavigationViewItem
        {
            Content = "Gösterge Paneli",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Grid24 },
            TargetPageType = typeof(GostergePaneliPage)
        },
        new NavigationViewItem
        {
            Content = "Personel İşlemleri",
            Icon = new SymbolIcon { Symbol = SymbolRegular.PersonEdit24 },
            TargetPageType = typeof(PersonelIslemleri)
        },
        new NavigationViewItem
        {
            Content = "Organizasyon İşlemleri",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Organization24 },
        },
        new NavigationViewItem
        {
            Content = "İzin İşlemleri",
            Icon = new SymbolIcon { Symbol = SymbolRegular.CalendarLtr24 }
        },
        new NavigationViewItem
        {
            Content = "Personel Rapor İşlemleri",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Document24 },
        },
        new NavigationViewItem
        {
            Content = "Maaş Bordrosu İşlemleri",
            Icon = new SymbolIcon { Symbol = SymbolRegular.MoneyCalculator24 }
        }
    };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new ObservableCollection<object>
    {
        new NavigationViewItem
        {
            Content = "Ayarlar",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
            TargetPageType = typeof(AyarlarPage)
        }
    };

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new ObservableCollection<MenuItem>
    {
        new MenuItem { Header = "Gösterge Paneli", Tag = "tray_home" },
        new MenuItem { Header = "Close", Tag = "tray_close" }
    };
    }
}
