using Shop.BuisnesLogic.interfaces;
using Shop.BuisnesLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BuisnesLogic
{
    public class BuisnesLogic
    {

        public ISession GetSessionBL()
        {
            return new SessionBL();
        }

    }
}
