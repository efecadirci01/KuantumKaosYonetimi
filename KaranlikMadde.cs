using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuantumKaosYonetimi
{
    public class KaranlikMadde : KuantumNesnesi, IKritik
    {
        public KaranlikMadde(string id) : base(id, 7) { }

        public override void AnalizEt()
        {
            // Kural: Stabilite 15 birim düşer
            Stabilite -= 15;
            Console.WriteLine($"{ID}: Karanlık madde analizi tamamlandı. Dikkatli ol! (Stabilite -15)");
        }

        public void AcilDurumSogutmasi()
        {
            // Kural: +50 artar, Max 100 (Setter bunu halleder)
            Stabilite += 50;
            Console.WriteLine($"{ID}: Soğutma sıvısı enjekte edildi. Stabilite arttı.");
        }
    }
}
