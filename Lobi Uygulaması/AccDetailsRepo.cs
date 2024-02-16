using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lobi_Uygulaması
{
    class AccDetailsRepo
    {
        private static AccDetailsRepo _instance;
        public static AccDetailsRepo Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AccDetailsRepo();
                    return _instance;
            }
        }
        public string ogrenciad, ogrencisoyad, kartno;
        public int ogrencino;
    }
}
