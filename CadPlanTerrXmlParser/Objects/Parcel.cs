using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace CadPlanTerrXmlParser.Objects
{
    class Parcel
    {
        public Parcel() { }

        public string cadastralNumber;
        public Tuple<string, string> type;
        public Tuple<string, string> subtype;
        public Tuple<string, string> category;
        public string documentUse;
        public string trueUse;
        public Tuple<string, string> area;
        public Address address;
        public string boundMark;
        public string cost;
        public SpatialData spatial;
        public XElement xml;

        public override string ToString()
        {
            string str = "Участок"
                + "\r\n\rКадастровый номер : " + cadastralNumber
                + "\r\nТип участка: " + type.Item1 + " " + type.Item2;
            if (area.Item2 != null) str += "\r\nПодвид участка: " + subtype.Item1 + " " + subtype.Item2;
            if (area.Item1 != null) str += "\r\nПлощадь: " + area.Item1;
            if (area.Item2 != null) str += "\r\nПогрешность: " + area.Item2;
            if (category.Item1 != null) str += "\r\nКатегория: " + category.Item1 + " " + category.Item2;
            if (documentUse != null) str += "\r\nРазрешено использование по документам : " + documentUse;
            if (trueUse != null) str += "\r\nИспользование земли : " + trueUse;
            if (address != null) str += "\r\n\rПолный адрес: " + address;
            if (boundMark != "-") str += "\r\nМетка: " + boundMark;
            str += "\r\nЦена: " + cost;
            if (spatial != null) str += "\r\n\r" + spatial;
            return str;
        }
    }
}
