using Stripe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace EncounterHelper
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CollectionViewSource ViewSource;
        ObservableCollection<Unit> listOfNames = new ObservableCollection<Unit>();
        

        public MainWindow()
        {
            InitializeComponent();
            ViewSource = new CollectionViewSource();
            Sheet.ItemsSource = listOfNames;
            ViewSource.Source = listOfNames;

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBoxValue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextBoxTextAllowed(e.Text);
        }
        private void textBoxValue_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String Text1 = (String)e.DataObject.GetData(typeof(String));
                if (!TextBoxTextAllowed(Text1)) e.CancelCommand();
            }
            else e.CancelCommand();
        }
        private Boolean TextBoxTextAllowed(String Text2)
        {
            return Array.TrueForAll<Char>(Text2.ToCharArray(), delegate (Char c)
            {
                return Char.IsDigit(c) || Char.IsControl(c);
            });
        }

        private void AddMultiple_Click(object sender, RoutedEventArgs e)
        {
            int counter = 0;
            Int32.TryParse(RowQuantity.Text, out counter);
            
            for (int i = 0; i < counter; i++)
            {
                
                Unit theNew = new Unit();
                theNew.Name = NameBox.Text;
                int tmp = 0;
                Int32.TryParse(HPValue.Text, out tmp);
                theNew.HP = tmp;
                tmp = 0;
                Int32.TryParse(ACValue.Text, out tmp);
                theNew.AC = tmp;
                tmp = 0;
                Int32.TryParse(CSValue.Text, out tmp);
                theNew.CustomResource = tmp;
                tmp = 0;
                listOfNames.Add(theNew);

            }

            
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            foreach(Unit unit in listOfNames)
            {
                if(unit.Selected==true)
                {
                    
                    unit.Selected = false;
                    int tmp = 0;
                    Int32.TryParse(HPChange.Text, out tmp);
                    unit.HP = unit.HP + tmp;

                    tmp = 0;
                    Int32.TryParse(ACChange.Text, out tmp);
                    unit.AC = unit.AC + tmp;

                    tmp = 0;
                    Int32.TryParse(CSChange.Text, out tmp);
                    unit.CustomResource = unit.CustomResource + tmp;

                    NameBox.Text = unit.AC.ToString();
                    Sheet.ItemsSource = null;
                    Sheet.ItemsSource = listOfNames;
                }
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            foreach(Unit unit in listOfNames)
            {
                if(unit.Selected==true)
                {
                    unit.Selected = false;
                    Sheet.ItemsSource = null;
                    Sheet.ItemsSource = listOfNames;

                }
            }
        }

        private void Substract_Click(object sender, RoutedEventArgs e)
        {
            foreach (Unit unit in listOfNames)
            {
                if (unit.Selected == true)
                {
                    
                    unit.Selected = false;
                    int tmp = 0;
                    Int32.TryParse(HPChange.Text, out tmp);
                    unit.HP = unit.HP - tmp;

                    tmp = 0;
                    Int32.TryParse(ACChange.Text, out tmp);
                    unit.AC = unit.AC - tmp;

                    tmp = 0;
                    Int32.TryParse(CSChange.Text, out tmp);
                    unit.CustomResource = unit.CustomResource - tmp;

                    NameBox.Text = unit.AC.ToString();
                    Sheet.ItemsSource = null;
                    Sheet.ItemsSource = listOfNames;
                }
            }
        }

        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (Unit unit in listOfNames)
            {
                if (unit.Selected == false)
                {
                    unit.Selected = true;
                    Sheet.ItemsSource = null;
                    Sheet.ItemsSource = listOfNames;

                }
            }
        }

    }
}