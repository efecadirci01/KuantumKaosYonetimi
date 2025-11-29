using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuantumKaosYonetimi
{
    public abstract class KuantumNesnesi
    {
        public string ID { get; set; }
        public int TehlikeSeviyesi { get; set; } // 1-10 arası

        // Encapsulation field
        private int stabilite;

        public int Stabilite
        {
            get { return stabilite; }
            set
            {
                // Kural: 0 veya altına düşerse oyun biter (Exception fırlat)
                if (value <= 0)
                {
                    stabilite = 0;
                    throw new KuantumCokusu(ID);
                }

                // Kural: 100'den büyük olamaz
                if (value > 100)
                {
                    stabilite = 100;
                }
                else
                {
                    stabilite = value;
                }
            }
        }

        public KuantumNesnesi(string id, int tehlike)
        {
            ID = id;
            TehlikeSeviyesi = tehlike;
            // Başlangıçta tam stabil olsun (veya rastgele atanabilir)
            stabilite = 100;
        }

        // Abstract Metot: Herkes kendi analiz yöntemini yazmak zorunda
        public abstract void AnalizEt();

        public string DurumBilgisi()
        {
            return $"{ID} - Stabilite: %{Stabilite}   Tehlike: {TehlikeSeviyesi}/10   Tür: {this.GetType().Name}";
        }
    }
}
