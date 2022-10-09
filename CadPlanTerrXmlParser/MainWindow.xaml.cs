using CadPlanTerrXmlParser.WorkWithXML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CadPlanTerrXmlParser.Objects;
using CadPlanTerrXmlParser.ViewModel;
using System.Xml.Serialization;
using System.Xml;

namespace CadPlanTerrXmlParser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataObjects DataSet;
        public MainWindow()
        {
            DataSet = new DataObjects();
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            DataSet = ParseXML.ParsingData();
            treeElementsView.ItemsSource = TreeViewModel.SetTree("Objects", DataSet);
        }

        /// <summary>
        /// Вывод содержимого узла при двоймно клике
        /// </summary>
        /// <param name="sender">Label</param>
        /// <param name="e"></param>
        private void ObjectElement_DoubleClickSelected(object sender, RoutedEventArgs e)
        {
            var tvItem = (Label)sender;
            try
            {
                DataTextBox.Text = DataSet.GetObjectById(tvItem.Content.ToString()).ToString();
                DataTextBox.ScrollToHome();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Для отображения информации необходимо выбрать один объект, а не группу",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Получение выбранных узлов из дерева
        /// </summary>
        /// <returns></returns>
        private List<List<string>> getSelectedNodes()
        {
            var treeEnum = treeElementsView.ItemsSource.GetEnumerator();
            treeEnum.MoveNext();

            var rootTree = treeEnum.Current as TreeViewModel;
            var selectedNodes = TreeViewModel.GetSelectedChildElements(rootTree);

            return selectedNodes;
        }

        /// <summary>
        /// Сохрание выбранных узлов из дерева
        /// </summary>
        /// <param name="sender">Label</param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedNodes = getSelectedNodes();

            SaveXMLfile saved = new SaveXMLfile();
            if (selectedNodes.Count > 0 && saved.SaveFileDialog())
            {
                saved.SaveElements(selectedNodes, DataSet);
            }

        }
    }
}
