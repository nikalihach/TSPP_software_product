using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPP
{
    class Client //Замовник
    {
        public Client (String name, string address, int order)
        {
            this.name = name;
            this.order = order;
            this.address = address;
        }

        public String name;
        public String address;
        public int order; //потреби споживача

        ~Client()
        {
        }
    }
}
