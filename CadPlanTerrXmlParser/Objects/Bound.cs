using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CadPlanTerrXmlParser.Objects
{
    class Bound
    {
        public string registrationDate;
        public string registrationNumber;
        public Tuple<string, string> type;
        public SpatialData spatial;
        public XElement xml;
        public Bound() { }

        public override string ToString()
        {
            string str = "Муниципальные границы"
                + "\r\n\rРегистрационный номер: " + registrationNumber
                + "\r\nДата регистации: " + registrationDate
                + "\r\nТип границы: " + type.Item1 + " " + type.Item2
                + "\r\n\n" + spatial;
            return str;
        }
    }
}
