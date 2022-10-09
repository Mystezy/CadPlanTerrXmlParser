using CadPlanTerrXmlParser.Objects;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace CadPlanTerrXmlParser.WorkWithXML
{
    class SaveXMLfile
    {
        public string FilePath { get; set; }

        private bool savedStatus;
        public SaveXMLfile()
        {
            savedStatus = false;
        }
        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "Cadastral objects";
            saveFileDialog.DefaultExt = ".xml";
            saveFileDialog.Filter = "XML-File | *.xml";
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                return true;
            }
            return false;
        }

        public void SaveElements(List<List<string>> selectedNodes, DataObjects DataSet)
        {

            try
            {
                XDocument xdoc = new XDocument();
                xdoc.Add(new XElement("Objects"));
                var doc = xdoc.Element("Objects");
                foreach (var el in selectedNodes)
                {
                    foreach(var elem in el)
                    {
                        if (DataSet.GetTypeObject(elem) == "Parcel")
                        {
                            if (doc.Element("Parcels") == null)
                                doc.Add(new XElement("Parcels"));
                            var parcel = DataSet.Parcels[elem].xml;
                            parcel.Name = "Parcel";
                            doc.Element("Parcels").Add(parcel);

                        }
                        else if (DataSet.GetTypeObject(elem) == "ObjectRealty")
                        {
                            if (doc.Element("ObjectsRealty") == null)
                                doc.Add(new XElement("ObjectsRealty"));
                            var objectRealty = DataSet.ObjectsRealty[elem].xml;
                            objectRealty.Name = "ObjectRealty";
                            doc.Element("ObjectsRealty").Add(objectRealty);
                        }
                        else if (DataSet.GetTypeObject(elem) == "Spatial")
                        {
                            if (doc.Element("Spatials") == null)
                                doc.Add(new XElement("Spatials"));
                            var spatial = DataSet.Spatials[elem].xml;
                            spatial.Name = "SpatialData";
                            doc.Element("Spatials").Add(spatial);
                        }
                        else if (DataSet.GetTypeObject(elem) == "Bound")
                        {
                            if (doc.Element("Bounds") == null)
                                doc.Add(new XElement("Bounds"));
                            var bound = DataSet.Bounds[elem].xml;
                            bound.Name = "Bound";
                            doc.Element("Bounds").Add(bound);
                        }
                        else
                        {
                            if (doc.Element("Zones") == null)
                                doc.Add(new XElement("Zones"));
                            var zone = DataSet.Zones[elem].xml;
                            zone.Name = "Zone";
                            doc.Element("Zones").Add(zone);
                        }
                    }
                }

                xdoc.Save(FilePath);

                savedStatus = true;
            }
            catch
            {
                savedStatus = false;
            }
            StatusMesage();
        }

        private void StatusMesage()
        {
            if (savedStatus)
                MessageBox.Show("Файл успешно сохранен", "Сохранение",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("Ошибка. Не удалось сохранить файл",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
