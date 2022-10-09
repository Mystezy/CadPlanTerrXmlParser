using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CadPlanTerrXmlParser.Objects
{
    class ObjectRealty
    {
        public string cadastralNumber;
        public Tuple<string, string> type;
        public string area;
        public Tuple<string, string> purpose;
        public Tuple<string, string> addressType;
        public Address address;
        public string okatoLocation;
        public Tuple<string, string> region;
        public string posDescription;
        public string cost;
        public XElement xml;

        public override string ToString()
        {
            string str = "Постройка"
                + "\r\n\rКадастровый номер: " + cadastralNumber
                + "\r\nПредназначение: " + purpose.Item1
                + "\r\nТип конструкции: " + type.Item1 + " " + type.Item2
                + "\r\nПлощадь: " + area
                + "\r\n\rАдрес: " + address;

            if (okatoLocation != null) str += "\r\nOkato локации: " + okatoLocation;
            if (region.Item1 != null) str += "\r\nРегион локации: " + region.Item1 + "" + region.Item2;
            if (posDescription != null) str += "\r\nПозиция локации: " + posDescription;
            if (cost != null) str += "\r\nЦена: " + cost;
            return str;
        }
    }
}
