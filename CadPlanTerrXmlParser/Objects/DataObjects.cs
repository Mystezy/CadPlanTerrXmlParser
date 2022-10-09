using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CadPlanTerrXmlParser.Objects
{
    class DataObjects
    {
        public DataObjects() { }
        public Dictionary<string, Parcel> Parcels { get; set; }
        public Dictionary<string, ObjectRealty> ObjectsRealty { get; set; }
        public Dictionary<string, SpatialData> Spatials { get; set; }
        public Dictionary<string, Bound> Bounds { get; set; }
        public Dictionary<string, Zone> Zones { get; set; }

        public string GetObjectById(string id)
        {
            if (Parcels.ContainsKey(id))
                return Parcels[id].ToString();
            else if (ObjectsRealty.ContainsKey(id))
                return ObjectsRealty[id].ToString();
            else if (Spatials.ContainsKey(id))
                return Spatials[id].ToString();
            else if (Bounds.ContainsKey(id))
                return Bounds[id].ToString();
            else if (Zones.ContainsKey(id))
                return Zones[id].ToString();
            else return null;
        }

        public string GetTypeObject(string id)
        {
            if (Parcels.ContainsKey(id))
                return "Parcel";
            else if (ObjectsRealty.ContainsKey(id))
                return "ObjectRealty";
            else if (Spatials.ContainsKey(id))
                return "Spatial";
            else if (Bounds.ContainsKey(id))
                return "Bound";
            else if (Zones.ContainsKey(id))
                return "Zone";
            else return null;
        }
    }
}
