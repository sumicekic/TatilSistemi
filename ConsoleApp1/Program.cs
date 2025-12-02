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
            Console.WriteLine("Sistem başlatılıyor...");

            // veri çekme metodunu çağırıyorum
            await VerileriCek();

            Console.WriteLine("Veriler hafızaya alındı. Devam etmek için Enter'a bas.");
            Console.ReadLine();
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