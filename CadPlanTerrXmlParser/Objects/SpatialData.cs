using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CadPlanTerrXmlParser.Objects
{
    class SpatialData
    {
        public List<Tuple<double, double>> ordinates;
        public string skId;

        public string orbNmb;
        public string numGeo;
        public string geoZacrep;
        public Tuple<string, string> opred;
        public string delta;
        public XElement xml;

        public SpatialData() { ordinates = new List<Tuple<double, double>>(); }

        public override string ToString()
        {
            string str = "Пространственные данные: ";
            for (int i = 0; i < ordinates.Count; i++)
            {
                str += "\r\nТочка: " + (i + 1).ToString();
                str += "\r\nКоординаты: (" + ordinates[i].Item1 + " ; " + ordinates[i].Item2 + ")";
            }

            return str;
        }
    }
}
