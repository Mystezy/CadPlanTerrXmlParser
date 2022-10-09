using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Linq;
using CadPlanTerrXmlParser.Objects;
using Zone = CadPlanTerrXmlParser.Objects.Zone;

namespace CadPlanTerrXmlParser.WorkWithXML
{
    class ParseXML
    {
        static public DataObjects ParsingData()
        {
            var dataSet = new DataObjects();
            var fileXML = "..\\..\\24_21_1003001_2017-05-29_kpt11.xml";
            XDocument docXML = XDocument.Load(fileXML);
            var data = docXML.Element("extract_cadastral_plan_territory").Element("cadastral_blocks").Element("cadastral_block");

            dataSet.Parcels = ParsingParcels(data.Element("record_data").Element("base_data"));
            dataSet.ObjectsRealty = ParsingObjectRealty(data.Element("record_data").Element("base_data"));
            var Spatials = ParsingSpatial(data.Element("spatial_data").Element("entity_spatial"));
            dataSet.Spatials = new Dictionary<string, SpatialData> { { Spatials.skId, Spatials } };
            dataSet.Bounds = ParsingBound(data.Element("municipal_boundaries"));
            dataSet.Zones = ParsingZone(data.Element("zones_and_territories_boundaries"));

            return dataSet;
        }

        static public Dictionary<string, Parcel> ParsingParcels(XElement xdoc)
        {
            var resList = new Dictionary<string, Parcel>();
            foreach (var construction in xdoc.Element("land_records").Elements("land_record"))
            {
                
                var parcel = new Parcel();
                var dataSection = construction.Element("object").Element("common_data");

                parcel.xml = construction;

                parcel.cadastralNumber = dataSection.Element("cad_number").Value;
                parcel.type = new Tuple<string, string>(dataSection.Element("type").Element("code").Value, 
                    dataSection.Element("type").Element("value").Value);

                if (construction.Element("object").Element("subtype") != null)
                {
                    parcel.subtype = new Tuple<string, string>(construction.Element("object").Element("subtype").Element("code").Value, 
                        construction.Element("object").Element("subtype").Element("value").Value);
                }

                if (construction.Element("params").Element("category") != null)
                {
                    parcel.category = new Tuple<string, string>(construction.Element("params").Element("category").Element("type").Element("code").Value,
                        construction.Element("params").Element("category").Element("type").Element("value").Value);
                }

                if (construction.Element("params").Element("permitted_use") != null)
                {
                    var use = construction.Element("params").Element("permitted_use").Element("permitted_use_established");
                    parcel.documentUse = use.Element("by_document").Value;
                    parcel.trueUse = use.Element("land_use") != null ?
                        use.Element("land_use").Element("value").Value : null;
                }

                parcel.area = new Tuple<string, string>(construction.Element("params").Element("area").Element("value").Value,
                    construction.Element("params").Element("area").Element("inaccuracy") != null ? construction.Element("params").Element("area").Element("inaccuracy").Value : null);


                var adrSection = construction.Element("address_location").Element("address");
                parcel.address = ParsingAddress(adrSection);

                parcel.boundMark = construction.Element("address_location").Element("rel_position").Element("in_boundaries_mark").Value;

                parcel.cost = construction.Element("cost") != null ? construction.Element("cost").Element("value").Value : null;

                if (construction.Element("contours_location") != null)
                {
                    var sptEl = construction.Element("contours_location").Element("contours").Element("contour").Element("entity_spatial");
                    parcel.spatial = ParsingSpatial(sptEl);
                }
                resList.Add(dataSection.Element("cad_number").Value, parcel);
            }
            return resList;
        }

        static private Address ParsingAddress(XElement adress)
        {
            var resAdress = new Address();
            var levelSection = adress.Element("address_fias").Element("level_settlement");

            resAdress.okato = levelSection.Element("okato").Value;
            resAdress.kladr = levelSection.Element("kladr").Value;

            resAdress.region = new Tuple<string, string>(levelSection.Element("region").Element("code").Value,
                levelSection.Element("region").Element("value").Value);

            if (levelSection.Element("district") != null)
            {
                resAdress.district = new Tuple<string, string>(levelSection.Element("district").Element("type_district").Value,
                    levelSection.Element("district").Element("name_district").Value);
            }

            if (levelSection.Element("locality") != null)
            {
                resAdress.locality = new Tuple<string, string>(levelSection.Element("locality").Element("type_locality").Value,
                    levelSection.Element("locality").Element("name_locality").Value);
            }

            if (adress.Element("address_fias").Element("street") != null)
            {
                resAdress.street = new Tuple<string, string>(adress.Element("address_fias").Element("street").Element("type_street").Value,
                    adress.Element("address_fias").Element("street").Element("name_street").Value);
                if (adress.Element("address_fias").Element("level1") != null)
                    resAdress.level = adress.Element("address_fias").Element("level1").Element("name_level1").Value;
            }


            if (adress.Element("address_fias").Element("detailed_level") != null)
            {
                var detal = adress.Element("address_fias").Element("detailed_level");
                if (detal.Element("street") != null)
                {
                    resAdress.street = new Tuple<string, string>(detal.Element("street").Element("type_street").Value,
                        detal.Element("street").Element("name_street").Value);
                    resAdress.level = detal.Element("level1") != null ? detal.Element("level1").Element("name_level1").Value : null;
                }
                resAdress.other = detal.Element("other") != null ? detal.Element("other").Value : null;
            }
            resAdress.readableAddress = adress.Element("readable_address").Value;
            return resAdress;
        }

        static public Dictionary<string, ObjectRealty> ParsingObjectRealty(XElement xdoc)
        {
            var resList = new Dictionary<string, ObjectRealty>();

            foreach (var buildEl in xdoc.Element("build_records").Elements("build_record"))
            {
                var build = new ObjectRealty();
                var dataSection = buildEl.Element("object").Element("common_data");

                build.xml = buildEl;

                build.cadastralNumber = dataSection.Element("cad_number").Value;
                build.type = new Tuple<string, string>(dataSection.Element("type").Element("code").Value,
                    dataSection.Element("type").Element("value").Value);
                build.area = buildEl.Element("params").Element("area").Value;
                build.purpose = new Tuple<string, string>(buildEl.Element("params").Element("purpose").Element("code").Value,
                    buildEl.Element("params").Element("purpose").Element("value").Value);

                //тип адреса
                if (buildEl.Element("address_location").Element("address_type") != null)
                {
                    build.addressType = new Tuple<string, string>(buildEl.Element("address_location").Element("address_type").Element("code").Value,
                        buildEl.Element("address_location").Element("address_type").Element("value").Value);
                }
                //адрес
                var adrSection = buildEl.Element("address_location").Element("address");
                build.address = ParsingAddress(adrSection);

                //локация адреса
                var locationSection = buildEl.Element("address_location").Element("location");
                build.okatoLocation = locationSection.Element("okato").Value;

                build.region = new Tuple<string, string>(locationSection.Element("region").Element("code").Value,
                    locationSection.Element("region").Element("value").Value);

                build.posDescription = locationSection.Element("position_description") != null ?
                     locationSection.Element("position_description").Value : null; ;


                build.cost = buildEl.Element("cost") != null ?
                    buildEl.Element("cost").Element("value").Value : null;

                resList.Add(dataSection.Element("cad_number").Value, build);
            }

            foreach (var constructionEl in xdoc.Element("construction_records").Elements("construction_record"))
            {
                var construction = new ObjectRealty();

                construction.xml = constructionEl;

                var dataSection = constructionEl.Element("object").Element("common_data");
                construction.cadastralNumber = dataSection.Element("cad_number").Value;
                construction.type = new Tuple<string, string>(dataSection.Element("type").Element("code").Value,
                   dataSection.Element("type").Element("value").Value);

                construction.purpose = new Tuple<string, string>(null, constructionEl.Element("params").Element("purpose").Value);

                var adrSection = constructionEl.Element("address_location").Element("address");
                construction.address = ParsingAddress(adrSection);
                resList.Add(dataSection.Element("cad_number").Value, construction);
            }
            return resList;
        }

        static public Dictionary<string, Bound> ParsingBound(XElement xdoc)
        {
            var resList = new Dictionary<string, Bound>();
            foreach (var boundEl in xdoc.Elements("municipal_boundary_record"))
            {
                var bound = new Bound();

                bound.xml = boundEl;

                bound.registrationDate = boundEl.Element("record_info").Element("registration_date").Value;

                var xombSection = boundEl.Element("b_object_municipal_boundary").Element("b_object");
                bound.registrationNumber = xombSection.Element("reg_numb_border").Value;

                bound.type = new Tuple<string, string>(xombSection.Element("type_boundary").Element("code").Value,
                    xombSection.Element("type_boundary").Element("value").Value);

                var sptEl = boundEl.Element("b_contours_location").Element("contours").Element("contour").Element("entity_spatial");
                bound.spatial = ParsingSpatial(sptEl);
                resList.Add(bound.registrationNumber, bound);
            }
            return resList;
        }

        static public Dictionary<string, Zone> ParsingZone(XElement xdoc)
        {
            var resList = new Dictionary<string, Zone>();
            foreach (var zoneEl in xdoc.Elements("zones_and_territories_record"))
            {
                var zone = new Zone();

                zone.xml = zoneEl;

                zone.registrationDate = zoneEl.Element("record_info").Element("registration_date").Value;

                var xombSection = zoneEl.Element("b_object_zones_and_territories").Element("b_object");
                zone.registrationNumber = xombSection.Element("reg_numb_border").Value;

                zone.typeBound = new Tuple<string, string>(xombSection.Element("type_boundary").Element("code").Value,
                    xombSection.Element("type_boundary").Element("value").Value);

                zone.typeZone = new Tuple<string, string>(zoneEl.Element("b_object_zones_and_territories").Element("type_zone").Element("code").Value,
                    zoneEl.Element("b_object_zones_and_territories").Element("type_zone").Element("value").Value);

                zone.number = zoneEl.Element("b_object_zones_and_territories").Element("number") != null ?
                    zoneEl.Element("b_object_zones_and_territories").Element("number").Value : null;

                var sptEl = zoneEl.Element("b_contours_location").Element("contours").Element("contour").Element("entity_spatial");
                zone.spatial = ParsingSpatial(sptEl);
                resList.Add(zone.registrationNumber, zone);
            }
            return resList;
        }

        static public SpatialData ParsingSpatial(XElement xdoc)
        {

            var spatial = new SpatialData();

            spatial.xml = xdoc;

            spatial.skId = xdoc.Element("sk_id").Value;
            foreach (var spat in xdoc.Element("spatials_elements").Element("spatial_element").Element("ordinates").Elements("ordinate"))
            {
                var x = double.Parse(spat.Element("x").Value.Replace('.', ','));
                var y = double.Parse(spat.Element("y").Value.Replace('.', ','));

                spatial.ordinates.Add(new Tuple<double, double> (x, y));

                spatial.orbNmb = spat.Element("ord_nmb") != null ? spat.Element("ord_nmb").Value : null;
                spatial.numGeo = spat.Element("num_geopoint") != null ? spat.Element("num_geopoint").Value : null;
                spatial.geoZacrep = spat.Element("geopoint_zacrep") != null ? spat.Element("geopoint_zacrep").Value : null;

                if (spat.Element("geopoint_opred") != null)
                {
                    spatial.opred = new Tuple<string, string>(spat.Element("geopoint_opred").Element("code").Value,
                        spat.Element("geopoint_opred").Element("value").Value);
                }
                spatial.delta = spat.Element("delta_geopoint") != null ? spat.Element("delta_geopoint").Value : null;
            }
            return spatial;
        }
    }
}
