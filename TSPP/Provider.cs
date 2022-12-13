using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPP
{

    public class Provider : general_information
    {
        public Provider(String name_provider, String address_provider, uint supply, double cost) //Постачальник
        {
            name_provider = removing_extra_spaces(name_provider);
            address_provider = removing_extra_spaces(address_provider);
            name_provider = fix_the_registry(name_provider);
            address_provider = fix_the_registry(address_provider);
            this.name = name_provider;
            this.address = address_provider;
            this.supply = supply;
            this.cost = cost;
        }

        public uint supply; //Запаси
        public double cost; //Вартість товару

        ~Provider()
        {
        }
    }
}
