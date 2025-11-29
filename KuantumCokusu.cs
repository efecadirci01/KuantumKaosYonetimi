using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuantumKaosYonetimi
{
    public class KuantumCokusu : Exception
    {
        public string PatlayanNesneID { get; }

        public KuantumCokusu(string id)
            : base($"KRİTİK HATA: {id} ID'li nesne stabilitesini kaybetti!")
        {
            PatlayanNesneID = id;
        }
    }
}
