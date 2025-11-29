using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuantumKaosYonetimi
{
    public class AntiMadde : KuantumNesnesi, IKritik
    {
        public AntiMadde(string id) : base(id, 10) { }

        public override void AnalizEt()
        {
            // Kural: Stabilite 25 birim düşer + Uyarı

            Console.WriteLine("UYARI: Evrenin dokusu titriyor...");


            Stabilite -= 25;
            Console.WriteLine($"{ID}: Anti-Madde zorlukla analiz edildi. (Stabilite -25)");
        }

        public void AcilDurumSogutmasi()
        {
            Stabilite += 50;
            Console.WriteLine($"{ID}: Manyetik alanlar güçlendirildi. (Soğutma Başarılı)");
        }
    }
}
