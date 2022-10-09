using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CadPlanTerrXmlParser.Objects
{
    class Zone
    {
        public string registrationDate;
        public string registrationNumber;
        public Tuple<string, string> typeBound;
        public Tuple<string, string> typeZone;
        public string number;
        public SpatialData spatial;
        public XElement xml;

        public override string ToString()
        {
            string str = "Зона/территория"
                + "\r\n\rРегистрационный номер: " + registrationNumber
                + "\r\nДата регистации: " + registrationDate
                + "\r\nТип границы: " + typeBound.Item1 + " " + typeBound.Item2
                + "\r\nТип Зоны: " + typeZone.Item1 + " " + typeZone.Item2;
            if (number != null) str += "\r\nНомер: " + number;
            str += "\r\n\r" + spatial;
            return str;
        }
    }
}
