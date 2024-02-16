using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lobi_Uygulaması
{
    class UserFundedBalance
    {
        private static UserFundedBalance _instance;
        public static UserFundedBalance Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UserFundedBalance();
                return _instance;
            }
        }
        public int yuklenenbakiye;
    }
}
