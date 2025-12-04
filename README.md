Bu proje, Görsel Programlama dersi ödevi için hazırladığım, Türkiye'deki resmi tatilleri gösteren bir konsol uygulamasıdır.

Projenin amacı, internet üzerindeki bir kaynaktan yani API'den verileri canlı olarak çekip, bizim istediğimiz şekilde filtreleyip ekrana yazdırmaktır.

Çalışma prensibi şöyledir :

1. Bağlantı Kurma :
Program açılır açılmaz "HttpClient" dediğimiz aracı kullanarak internete bağlanıyor. Nager.Date isimli servise gidip 2023, 2024 ve 2025 yıllarının verilerini istiyor.

2. Veriyi Dönüştürme :
İnternetten gelen veri JSON formatında karışık bir düzende geliyor. Ben bu veriyi alıp, kodun içinde oluşturduğum "Holiday" ismindeki sınıfa dönüştürdüm. Yani o karışık yazıyı, programın anlayacağı nesnelere çevirdim.

3. Menü Sistemi :
Kullanıcının rahat işlem yapabilmesi için bir menü yaptım. Burada Switch-Case yapısı kullandım. Yani kullanıcı 1'e basarsa şuraya git, 2'ye basarsa buraya git şeklinde yönlendirdim.

4. Arama ve Listeleme:
Listeleme yaparken "foreach" döngüsü ile tüm tatil listesini tek tek geziyorum.
- Tarih ararken: Kullanıcının girdiği tarih , listedeki tatillerin tarihiyle eşleşiyor mu diye "If" koşuluyla kontrol ediyorum.
- İsim ararken: Girilen kelime tatil isminin içinde geçiyor mu diye bakıyorum (Contains).

Programın Özellikleri:

- Program açılınca verileri otomatik olarak hafızaya alır.
- İstediğiniz yılı seçip sadece o yılın tatillerini görebilirsiniz.
- Tarih girerek o günün tatil olup olmadığını sorgulayabilirsiniz.
- Tatil ismine göre arama yapabilirsiniz.
- İnternet kesikse veya hata olursa program kapanmaz, hata mesajı verir (Try-Catch yapısından kaynaklı).
  
Projeyi Visual Studio 2022 ile açıp F5 tuşuna basarak çalıştırabilirsiniz. Ekstra bir ayar yapmaya gerek yoktur.

Hazırlayan:
Sümeyye Çekiç - 20230108003 - BIP2
