using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuantumKaosYonetimi
{
    public class VeriPaketi : KuantumNesnesi
    {
        public VeriPaketi(string id) : base(id, 1) { } // Tehlike seviyesi düşük

        public override void AnalizEt()
        {
            // Kural: Stabilite 5 birim düşer
            Stabilite -= 5;
            Console.WriteLine($"{ID}: Veri içeriği okundu. (Stabilite -5)");
        }
    }
}
