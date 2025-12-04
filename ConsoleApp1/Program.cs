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
        static void YilSec()
        {
            Console.Write("Hangi yılı görmek istersin (2023, 2024, 2025): ");
            string girilenYil = Console.ReadLine();

            Console.WriteLine("\n--- " + girilenYil + " Yılı Tatilleri ---");

            // listeyi tek tek geziyorum
            foreach (var tatil in tumTatiller)
            {
                // eğer tatilin tarihi girilen yılla başlıyorsa ekrana yaz
                if (tatil.date.StartsWith(girilenYil))
                {
                    Console.WriteLine(tatil.date + " : " + tatil.localName);
                }
            }
        }

        static void TarihAra()
        {
            Console.Write("Tarih gir (gg-aa şeklinde, örn: 29-10): ");
            string girilen = Console.ReadLine();

            // tarih formatı doğru mu diye kontrol ediyorum (uzunluğu 5 olmalı)
            if (girilen.Length == 5)
            {
                // Kullanıcı 29-10 giriyor ama API'de tarih yyyy-aa-gg (2024-10-29)
                // O yüzden tersten kontrol edeceğiz: -10-29 şeklinde aratıyorum
                string aranacak = "-" + girilen.Substring(3, 2) + "-" + girilen.Substring(0, 2);

                bool bulunduMu = false;

                foreach (var tatil in tumTatiller)
                {
                    // tarih bu şekilde bitiyor mu?
                    if (tatil.date.EndsWith(aranacak))
                    {
                        Console.WriteLine("Bulundu: " + tatil.date + " -> " + tatil.localName);
                        bulunduMu = true;
                    }
                }

                // eğer hiç bulamadıysa uyarı ver
                if (bulunduMu == false)
                {
                    Console.WriteLine("Bu tarihte resmi tatil yokmuş.");
                }
            }
            else
            {
                Console.WriteLine("Hatalı yazdın, lütfen gg-aa şeklinde yaz.");
            }
        }

        static void IsimAra()
        {
            Console.Write("Aramak istediğin tatilin adı ne: ");
            // küçük harfe çeviriyorum ki büyük-küçük harf sorunu olmasın
            string aranan = Console.ReadLine().ToLower();

            Console.WriteLine("\n--- Arama Sonuçları ---");

            foreach (var tatil in tumTatiller)
            {
                // tatil isminin içinde aranan kelime geçiyor mu?
                if (tatil.localName.ToLower().Contains(aranan))
                {
                    Console.WriteLine(tatil.date + " -> " + tatil.localName);
                }
            }
        }

        static void HepsiniGoster()
        {
            Console.WriteLine("\n--- 2023-2025 Tüm Tatiller ---");
            // döngüyle hepsini alt alta yazdır
            foreach (var t in tumTatiller)
            {
                Console.WriteLine(t.date + "\t" + t.localName);
            }
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