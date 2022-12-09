using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPP
{
   
    class Provider
    {
        public Provider(String name, String address, int supply, double cost) //Поставщик
        {
            this.name = name;
            this.address = address;
            this.supply = supply;
            this.cost = cost;
        }

       public String name;
       public  String address;
       public int supply; //Запасы
       public double cost; //Стоимость товара

        ~Provider()
        {

        }
    }
}
