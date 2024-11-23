using Microsoft.EntityFrameworkCore;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using PersonnelTracker.Services;

namespace PersonnelTracker.ViewModels.Pages
{
    public partial class GostergePaneliViewModel : ObservableObject
    {
        private readonly PersonelService _personelService;

        public GostergePaneliViewModel(PersonelService personelService)
        {
            _personelService = personelService;
            _ = LoadStatisticsAsync();
        }

        [ObservableProperty]
        private int _toplamPersonelSayisi;
        private async Task LoadStatisticsAsync()
        {
            ToplamPersonelSayisi = await _personelService.GetTotalPersonelCountAsync(); // Toplam personel sayısını servis ile alıyoruz
        }
        public async Task UpdateStatisticsAsync()
        {
            ToplamPersonelSayisi = await _personelService.GetTotalPersonelCountAsync(); // Personel sayısını güncelliyoruz
        }

        public ISeries[] SeriesCartesian { get; set; } = [
            new LineSeries<TimeSpanPoint>
            {
                Values = [
                    new () { TimeSpan = TimeSpan.FromMilliseconds(1), Value = 10 },
                    new () { TimeSpan = TimeSpan.FromMilliseconds(2), Value = 6 },
                    new () { TimeSpan = TimeSpan.FromMilliseconds(3), Value = 3 },
                    new () { TimeSpan = TimeSpan.FromMilliseconds(4), Value = 12 },
                    new () { TimeSpan = TimeSpan.FromMilliseconds(5), Value = 8 }
                ],
            }
        ];
        public ICartesianAxis[] XAxes { get; set; } = [
            new TimeSpanAxis(TimeSpan.FromMilliseconds(1), date => date.ToString("fff") + "ms")
        ];

        public ISeries[] SeriesPie { get; set; }
            = new ISeries[]
            {
                new PieSeries<double> { Values = new double[] { 2 } },
                new PieSeries<double> { Values = new double[] { 4 } },
                new PieSeries<double> { Values = new double[] { 1 } },
                new PieSeries<double> { Values = new double[] { 4 } },
                new PieSeries<double> { Values = new double[] { 3 } }
            };
    }
}