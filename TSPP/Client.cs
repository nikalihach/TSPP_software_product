using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPP
{
    public class Client : general_information //Клієнт
    {
        public Client(string name_client, string address_client, uint order)
        {
            name_client = removing_extra_spaces(name_client);
            address_client = removing_extra_spaces(address_client);
            name_client = fix_the_registry(name_client);
            address_client = fix_the_registry(address_client);
            this.name = name_client;
            this.order = order;
            this.address = address_client;
        }

        public uint order; //потреби споживача

        ~Client()
        {
        }
    }

    


}
