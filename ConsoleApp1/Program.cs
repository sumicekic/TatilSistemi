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
        // veirleri saklama
        static List<Holiday> tumTatiller = new List<Holiday>();

        static async Task Main(string[] args)
        {
            Console.WriteLine("Tatil Takip Sistemi Hazırlanıyor...");
            //kod gelecek
            Console.ReadLine();
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