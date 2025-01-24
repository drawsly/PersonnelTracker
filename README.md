# PersonnelTracker
Orta ölçekli işletmeler için geliştirilmiş bir **Personel Takip Programı**. Bu program, personel bilgilerini takip etmek, izin ve maaş yönetimi gibi işlemleri kolaylaştırmak amacıyla tasarlanmıştır.

<p align="center">
  <img src="https://img.shields.io/github/contributors/drawsly/PersonnelTracker?color=dark-green">
  <img src="https://img.shields.io/github/forks/drawsly/PersonnelTracker?style=social">
  <img src="https://img.shields.io/github/issues/drawsly/PersonnelTracker">
  <img src="https://img.shields.io/github/license/drawsly/PersonnelTracker">
</p>

## Kullanılan Teknolojiler

- **UI Tasarımı**: [WPF.UI (LepoCo)](https://github.com/lepoco/wpfui) - Modern ve kullanıcı dostu bir tasarım için kullanıldı.
- **Veritabanı İşlemleri**: MySQL ve Entity Framework Core ile ilişkisel veri tabanı yönetimi.
- **Grafiksel İstatistikler**: [LiveCharts2](https://github.com/beto-rodriguez/LiveCharts2) ile veri görselleştirme.
- **Yazılım Mimarisi**: WPF MVVM tasarım deseni ile daha düzenli ve ölçeklenebilir bir yapı sağlandı.

## Özellikler
- **Personel Yönetimi**: Personel ekleme, düzenleme, ve listeleme.
- **Maaş Bordrosu**: Bordro oluşturma ve maaş hesaplama.
- **İzin Yönetimi**: Personel izinlerinin kaydını tutma ve izleme.
- **Departman ve Pozisyon Yönetimi**: Departmanlar ve pozisyonlar için özelleştirilebilir yapı.
- **Gösterge Paneli**: Genel istatistikler ve grafiksel analizler.

## Kurulum

### Gerekli Bağımlılıklar
- .NET 6.0 veya üzeri
- MySQL Server
- Visual Studio 2022 (veya uyumlu bir IDE)

### Adım Adım Kurulum

1. Proje Deposunu klonlayın:
   ```sh
   git clone https://github.com/drawsly/PersonnelTracker.git
   cd PersonnelTracker
2. Projeyi IDE ortamında başlatın:
    * Visual Studio'da **PersonnelTracker.sln** dosyasını açın.
3. Bağlantı Dizisi Ayarı:
    * Properties/App.config dosyasını açın ve kendi MySQL Server bağlantı bilgilerinizi doldurun:
        ```xml
          <connectionStrings>
            <add name="PersonelDatabase" connectionString="server=localhost;database=personel_data;user=root;password="/>
          </connectionStrings>
4. Veritabanını Oluşturun:
    * Package Manager Console (PMC) kullanarak:
        ```sh 
        Update-Database
    * DotNet CLI kullanarak:
        ```sh
        dotnet ef database update
5. Son Olarak
    * Son olarak projeyi başlatın ve test edin iyi geliştirmeler 😊
## Sürüm Geçmişi
* **v1.0.0**
    * İlk tam sürüm.
    * **Özellikler**: Gösterge Paneli, personel işlemleri (ekle,düzenle,sil,listele).


## Meta
**Yazar:** [Enes Yasin DIVRENGI](https://github.com/drawsly)  
**İletişim:** [dr4wsly@gmail.com](mailto:dr4wsly@gmail.com)  
**Dağıtım Lisansı:** GPL v3 Lisansı. Daha fazla bilgi için [LICENSE](https://github.com/drawsly/PersonnelTracker/blob/master/LICENSE) dosyasına göz atabilirsiniz.  


## Katkıda Bulunma
1. Fork oluşturun: https://github.com/drawsly/PersonnelTracker/fork

2. Yeni bir dal (branch) oluşturun:
    ```bash
    git checkout -b feature/example
3. Yaptığınız değişiklikleri commit edin:
    ```bash
    git commit -am 'Bazı hatalar düzeltildi'
4. Dalınızı gönderin:
    ```bash
    git push origin feature/example
5. Bir Pull Request oluşturun!
