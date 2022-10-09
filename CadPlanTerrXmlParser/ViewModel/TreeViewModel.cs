using CadPlanTerrXmlParser.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CadPlanTerrXmlParser.ViewModel
{
    class TreeViewModel : INotifyPropertyChanged
    {
        TreeViewModel(string name)
        {
            Name = name;
            Children = new List<TreeViewModel>();
        }

        public string Name { get; private set; }
        public List<TreeViewModel> Children { get; private set; }
        public bool IsInitiallySelected { get; private set; }

        bool? _isChecked = false;
        TreeViewModel _parent;

        public static List<TreeViewModel> SetTree(string topLevelName, DataObjects DataSet)
        {
            List<TreeViewModel> treeView = new List<TreeViewModel>();
            TreeViewModel rootItem = new TreeViewModel(topLevelName);

            treeView.Add(rootItem);

            TreeViewModel parcelItem = new TreeViewModel("Parcels");
            TreeViewModel objectRealtyItem = new TreeViewModel("ObjecstRealty");
            TreeViewModel spatialDataItem = new TreeViewModel("Spatials");
            TreeViewModel boundItem = new TreeViewModel("Boundsы");
            TreeViewModel zoneItem = new TreeViewModel("Zones");

            rootItem.Children.Add(parcelItem);
            foreach (var elemId in DataSet.Parcels.Keys)
                parcelItem.Children.Add(new TreeViewModel(elemId));

            rootItem.Children.Add(objectRealtyItem);
            foreach (var elemId in DataSet.ObjectsRealty.Keys)
                objectRealtyItem.Children.Add(new TreeViewModel(elemId));

            rootItem.Children.Add(spatialDataItem);
            foreach (var elemId in DataSet.Spatials.Keys)
                spatialDataItem.Children.Add(new TreeViewModel(elemId));

            rootItem.Children.Add(boundItem);
            foreach (var elemId in DataSet.Bounds.Keys)
                boundItem.Children.Add(new TreeViewModel(elemId));

            rootItem.Children.Add(zoneItem);
            foreach (var elemId in DataSet.Zones.Keys)
                zoneItem.Children.Add(new TreeViewModel(elemId));

            rootItem.Initialize();

            return treeView;
        }

        void Initialize()
        {
            foreach (TreeViewModel child in Children)
            {
                child._parent = this;
                child.Initialize();
            }
        }

        void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool? IsChecked
        {
            get { return _isChecked; }
            set { SetIsChecked(value, true, true); }
        }

        void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {
            if (value == _isChecked) return;

            _isChecked = value;

            if (updateChildren && _isChecked.HasValue) Children.ForEach(c => c.SetIsChecked(_isChecked, true, false));

            if (updateParent && _parent != null) _parent.VerifyCheckedState();

            NotifyPropertyChanged("IsChecked");
        }

        void VerifyCheckedState()
        {
            bool? state = null;

            for (int i = 0; i < Children.Count; ++i)
            {
                bool? current = Children[i].IsChecked;
                if (i == 0)
                {
                    state = current;
                }
                else if (state != current)
                {
                    state = null;
                    break;
                }
            }

            SetIsChecked(state, false, true);
        }

        public static List<List<string>> GetSelectedChildElements(TreeViewModel TreeViewRoot)
        {
            List<List<string>> selectedGroup = new List<List<string>>();
            foreach (var group in TreeViewRoot.Children)
            {
                List<string> selected = new List<string>();
                foreach (var element in group.Children)
                    if (element.IsChecked == true)
                        selected.Add(element.Name);

                if (selected.Count > 0)
                    selectedGroup.Add(selected);
            }

            return selectedGroup;
        }
    }
}
