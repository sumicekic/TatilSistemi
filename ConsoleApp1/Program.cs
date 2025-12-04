// kütüphane entegre ediyoruz
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json; 

namespace TatilSistemi
{
    class Program
    {
        // veri saklayacağım liste
        static List<Holiday> tumTatiller = new List<Holiday>();

        static async Task Main(string[] args)
        {
            // önce gidip apiden verileri çeksin
            await VerileriCek();

            Console.WriteLine("\nVeriler geldi. Menüye geçiliyor...");

            // program sürekli dönsün diye true yapıyorum
            bool devam = true;

            while (devam)
            {
                // ekrana menü seçeneklerini yazdırıyorum
                Console.WriteLine("\n===== PublicHolidayTracker =====");
                Console.WriteLine("1. Tatil listesini göster (yıl seçmeli)");
                Console.WriteLine("2. Tarihe göre tatil ara (gg-aa formatı)");
                Console.WriteLine("3. İsme göre tatil ara");
                Console.WriteLine("4. Tüm tatilleri 3 yıl boyunca göster (2023–2025)");
                Console.WriteLine("5. Çıkış");
                Console.Write("Seçiminiz: ");

                string secim = Console.ReadLine();

                // girilen sayıya göre işlem seçimi (switch-case)
                switch (secim)
                {
                    case "1":
                        YilSec(); // yıl seçme metoduna git
                        break;
                    case "2":
                        TarihAra(); // tarih arama metoduna git
                        break;
                    case "3":
                        IsimAra(); // isim arama metoduna git
                        break;
                    case "4":
                        HepsiniGoster(); // hepsini listele
                        break;
                    case "5":
                        devam = false; // döngüyü kır, program kapansın
                        Console.WriteLine("Çıkış yapılıyor...");
                        break;
                    default:
                        Console.WriteLine("Yanlış tuşa bastın, tekrar dene.");
                        break;
                }
            }
        }

        // --- ŞİMDİLİK BOŞ METOTLAR (HATA VERMESİN DİYE) ---
        // bunların içini bir sonraki adımda dolduracağım

        static void YilSec()
        {
            Console.WriteLine(">> Yapım aşamasında...");
        }

        static void TarihAra()
        {
            Console.WriteLine(">> Yapım aşamasında...");
        }

        static void IsimAra()
        {
            Console.WriteLine(">> Yapım aşamasında...");
        }

        static void HepsiniGoster()
        {
            Console.WriteLine(">> Yapım aşamasında...");
        }

        // APIden veri çeken metot
        static async Task VerileriCek()
        {
            // istenen yılları diziye koyduk
            string[] yillar = { "2023", "2024", "2025" };

            using (HttpClient istemci = new HttpClient())
            {
                // her yıl için sırayla işlem yap
                foreach (string yil in yillar)
                {
                    try
                    {
                        Console.WriteLine(yil + " yılı indiriliyor...");

                        // adresi oluştur, veriyi çek
                        string url = $"https://date.nager.at/api/v3/PublicHolidays/{yil}/TR";
                        string jsonVerisi = await istemci.GetStringAsync(url);

                        // veriyi listeye çevirip ekle
                        var liste = JsonSerializer.Deserialize<List<Holiday>>(jsonVerisi);

                        if (liste != null)
                        {
                            tumTatiller.AddRange(liste);
                        }
                    }
                    catch (Exception hata)
                    {
                        Console.WriteLine("Hata oluştu: " + hata.Message);
                    }
                }
            }
        }
    }
    public class Holiday
    {
        public string date { get; set; }        
        public string localName { get; set; }   
        public string name { get; set; }         
        public string countryCode { get; set; } 
        public bool @fixed { get; set; }        
        public bool global { get; set; }        
    }
}