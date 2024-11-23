using PersonnelTracker.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace PersonnelTracker.Services
{
    public class PersonelService(AppDbContext context)
    {
        // Personel sil
        public async Task<bool> DeletePersonelByIdAsync(int personelId)
        {
            try
            {
                if (context.Personels != null)
                {
                    var personel = await context.Personels.FindAsync(personelId);
                    if (personel == null)
                        return false;

                    context.Personels.Remove(personel);
                }

                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Personel güncelle
        public async Task<bool> UpdatePersonelAsync(Personel personel)
        {
            try
            {
                if (context.Personels == null)
                    return false;

                var existingPersonel = await context.Personels.FindAsync(personel.PersonelId);
                if (existingPersonel == null)
                    return false;

                // Mevcut personeli güncelle
                existingPersonel.Ad = personel.Ad;
                existingPersonel.Soyad = personel.Soyad;
                existingPersonel.Tckn = personel.Tckn;
                existingPersonel.Telefon = personel.Telefon;
                existingPersonel.Eposta = personel.Eposta;
                existingPersonel.Iban = personel.Iban;
                existingPersonel.DogumTarihi = personel.DogumTarihi;
                existingPersonel.IseGirisTarihi = personel.IseGirisTarihi;
                existingPersonel.SabitMaas = personel.SabitMaas;
                existingPersonel.DepartmanId = personel.DepartmanId;
                existingPersonel.PozisyonId = personel.PozisyonId;
                existingPersonel.Cinsiyet = personel.Cinsiyet;

                // Değişiklikleri kaydet
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Yeni personel ekle
        public async Task<bool> AddPersonelAsync(Personel yeniPersonel)
        {
            try
            {
                // Personeli ekleyip, veritabanına kaydediyoruz
                if (context.Personels != null) await context.Personels.AddAsync(yeniPersonel);
                await context.SaveChangesAsync(); // Async olarak kaydediyoruz
                return true;
            }
            catch (Exception)
            {
                return false; // Hata durumunda false döndürüyoruz
            }
        }

        // Arama sorgusuna göre personelleri filtrele
        public List<Personel> FilterPersonels(string? query, List<Personel> allPersonels)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return allPersonels;
            }

            return allPersonels.Where(p =>
                    (p.Tckn?.Contains(query, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (p.Ad?.Contains(query, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (p.Soyad?.Contains(query, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (p.Telefon?.Contains(query, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (p.Eposta?.Contains(query, StringComparison.OrdinalIgnoreCase) ?? false))
                .ToList();
        }

        // Tüm personelleri getir
        public async Task<List<Personel>> GetAllPersonelsAsync()
        {
            if (context.Personels == null)
                return new List<Personel>(); // Eğer Personels null ise boş liste döndür

            return await context.Personels
                .Include(p => p.Departman)
                .Include(p => p.Pozisyon)
                .ToListAsync();
        }

        //Personelleri say
        public async Task<int> GetTotalPersonelCountAsync()
        {
            return context.Personels == null ? 0 : await context.Personels.CountAsync();
        }

        // Departmanları yükle
        public async Task<List<Departman>> GetDepartmanlarAsync()
        {
            if (context.Departmen == null)
                return new List<Departman>(); // Boş liste döndür

            return await context.Departmen.ToListAsync();
        }
        public async Task<Personel?> GetPersonelByIdAsync(int personelId)
        {
            if (context.Personels == null)
            {
                // Veritabanı koleksiyonu null ise, uygun şekilde hata fırlatabilir veya null döndürebilir
                return null;
            }

            var personel = await context.Personels.FindAsync(personelId);
            return personel; // Personel bulunamadığında null döner
        }


        // Pozisyonları yükle
        public async Task<List<Pozisyon>> GetPozisyonlarByDepartmanAsync(int departmanId)
        {
            if (context.Pozisyons == null)
                return new List<Pozisyon>(); // Eğer null ise boş liste döndür

            return await context.Pozisyons
                .Where(p => p.DepartmanId == departmanId)
                .ToListAsync();
        }
    }
}
