using System;
using System.Collections.Generic;
using System.Linq;

namespace KuantumKaosYonetimi
{
    // ---------------------------------------------------------
    // D. ÖZEL HATA YÖNETİMİ (CUSTOM EXCEPTION)
    // ---------------------------------------------------------
    

    // ---------------------------------------------------------
    // B. ARAYÜZ (INTERFACE SEGREGATION)
    // ---------------------------------------------------------
    // Sadece tehlikeli nesneler soğutulabilir.
    

    // ---------------------------------------------------------
    // A. TEMEL YAPI (ABSTRACT CLASS & ENCAPSULATION)
    // ---------------------------------------------------------
    

    // ---------------------------------------------------------
    // C. NESNE ÇEŞİTLERİ (INHERITANCE & POLYMORPHISM)
    // ---------------------------------------------------------

    // 1. Veri Paketi (Güvenli)
   

    // 2. Karanlık Madde (Tehlikeli - IKritik)
    

    // 3. Anti Madde (Çok Tehlikeli - IKritik)
    

    // ---------------------------------------------------------
    // MAIN LOOP (OYUN DÖNGÜSÜ)
    // ---------------------------------------------------------
    class Program
    {
        static List<KuantumNesnesi> envanter = new List<KuantumNesnesi>();
        static Random rnd = new Random();
        static int sayac = 1; // ID üretmek için

        static void Main(string[] args)
        {
            Console.Title = "OMEGA SEKTÖRÜ - KUANTUM AMBARI";

            while (true)
            {
                // Global Exception Handling (Game Over Mekaniği)
                try
                {
                    EkraniCiz();
                    string secim = Console.ReadLine();
                    

                    switch (secim)
                    {
                        case "1":
                            YeniNesneEkle();
                            break;
                        case "2":
                            Listele();
                            break;
                        case "3":
                            AnalizIslemi();
                            break;
                        case "4":
                            SogutmaIslemi();
                            break;
                        case "5":
                            Console.WriteLine("Vardiya sonlandırılıyor... Çıkış yapılıyor.");
                            return; // Programdan çık
                        default:
                            Console.WriteLine("Geçersiz işlem!");
                            break;
                    }

                    Console.WriteLine("\nDevam etmek için bir tuşa basın...");
                    Console.ReadKey();
                }
                catch (KuantumCokusu ex)
                {
                    // GAME OVER EKRANI
                    Console.Clear();
                    
                    Console.WriteLine("     SİSTEM ÇÖKTÜ! TAHLİYE BAŞLATILIYOR...        ");
                    
                    Console.WriteLine($"\nSEBEP: {ex.Message}");
                    Console.WriteLine("Simülasyon Bitti (GAME OVER)");
                    
                    Console.ReadLine();
                    Environment.Exit(0); // Uygulamayı tamamen kapat
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Beklenmedik bir hata: {ex.Message}");
                }
            }
        }

        // YARDIMCI METOTLAR

        static void EkraniCiz()
        {
            Console.Clear();
            
            Console.WriteLine("KUANTUM AMBARI KONTROL PANELİ  \n ");
          
            Console.WriteLine("1. Yeni Nesne Ekle (Rastgele)");
            Console.WriteLine("2. Tüm Envanteri Listele");
            Console.WriteLine("3. Nesneyi Analiz Et ");
            Console.WriteLine("4. Acil Durum Soğutması Yap ");
            Console.WriteLine("5. Çıkış");
            Console.Write("Seçiminiz: ");
        }

        static void YeniNesneEkle()
        {
            int zar = rnd.Next(1, 4); // 1, 2 veya 3
            string yeniId = "NESNE-" + sayac.ToString("000");
            KuantumNesnesi yeniNesne = null;

            switch (zar)
            {
                case 1:
                    yeniNesne = new VeriPaketi(yeniId);
                    Console.WriteLine("Depoya 'Veri Paketi' giriş yaptı.");
                    break;

                case 2:
                    yeniNesne = new KaranlikMadde(yeniId);  
                    
                    Console.WriteLine("DİKKAT: Depoya 'Karanlık Madde' alındı!");                   
                    break;

                case 3:
                    yeniNesne = new AntiMadde(yeniId);                  
                    Console.WriteLine("ALARM: Depoya 'Anti-Madde' kabul edildi!!");
                    break;
            }

            envanter.Add(yeniNesne);
            sayac++;
        }

        static void Listele()
        {
            if (envanter.Count == 0)
            {
                Console.WriteLine("Depo boş");
                return;
            }

            Console.WriteLine("    GÜNCEL ENVANTER DURUMU    ");
            foreach (var item in envanter)
            {
                // Polymorphism: Hepsi kendi bilgisini düzgün verir
                Console.WriteLine(item.DurumBilgisi());
            }
        }

        static void AnalizIslemi()
        {
            Listele();
            if (envanter.Count == 0) return;

            Console.Write("\nAnaliz edilecek nesne ID'si: ");
            string id = Console.ReadLine();

            // LINQ ile nesneyi bul
            var nesne = envanter.FirstOrDefault(x => x.ID == id);

            if (nesne != null)
            {
                // Polymorphism: Hangi tipse onun AnalizEt'i çalışır
                // Eğer stabilite 0 altına düşerse, setter Exception fırlatır
                // ve Main'deki catch bloğu yakalar.
                nesne.AnalizEt();
            }
            else
            {
                Console.WriteLine("Bu ID ile bir nesne bulunamadı.");
            }
        }

        static void SogutmaIslemi()
        {
            Listele();
            if (envanter.Count == 0) return;

            Console.Write("\nSoğutulacak nesne ID'si: ");
            string id = Console.ReadLine();

            var nesne = envanter.FirstOrDefault(x => x.ID == id);

            if (nesne != null)
            {
                // Type Checking (IS kontrolü)
                if (nesne is IKritik kritikNesne)
                {
                    // Eğer IKritik ise soğutma yapabiliriz
                    kritikNesne.AcilDurumSogutmasi();
                }
                else
                {
                    // Değilse hata ver
                    
                    Console.WriteLine("HATA: Bu nesne (Veri Paketi) soğutulamaz! Sadece tehlikeli maddeler soğutulur.");
                   
                }
            }
            else
            {
                Console.WriteLine("Bu ID ile bir nesne bulunamadı.");
            }
        }
    }
}