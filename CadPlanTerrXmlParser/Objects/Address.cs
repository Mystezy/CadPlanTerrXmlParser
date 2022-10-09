using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadPlanTerrXmlParser.Objects
{
    class Address
    {
        public Address() { }

        public string okato;
        public string kladr;
        public Tuple<string, string> region;
        public Tuple<string, string> district;
        public Tuple<string, string> locality;
        public Tuple<string, string> street;
        public string level;
        public string other;
        public string readableAddress;

        public override string ToString()
        {
            string str = "\r\nOkato : " + okato + "\r\nKladr: " + kladr;

            if (region.Item2 != null) str += "\r\nРегион: " + region.Item1 + " " + region.Item2;
            if (district.Item2 != null) str += "\r\nРайон: " + district.Item1 + ". " + district.Item2;

            if (locality.Item2 != null) str += "\r\nЛокация: " + locality.Item1 + ". " + locality.Item2;
            if (street.Item2 != null) str += "\r\nУлица: " + street.Item1 + ". " + street.Item2;
            if (level != null) str += "\r\nДом: д. " + level;
            if (other != null) str += "\r\nДополнительно: " + other;
            if (readableAddress != null) str += "\r\nПолный адрес: " + readableAddress;
            return str;
        }
    }
}
