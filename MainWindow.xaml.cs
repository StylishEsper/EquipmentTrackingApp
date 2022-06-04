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

namespace NETD3202_Lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            //Added this because I thought starting it up on a blank page felt strange
            Control controlLendOut = new LendOut();
            this.SettingsContentPanel.Children.Add(controlLendOut);
        }

        //Runs when a selection in ListView is changed
        private void SettingsListViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = e.Source as ListView;

            if (listView != null) 
            {
                SettingsContentPanel.Children.Clear(); //clear the current child to avoid overlap

                //If Lend Out tab is selected, display LendOut.xaml
                if (listView.SelectedItem.Equals(lviLendOut))
                {
                    Control controlLendOut = new LendOut();
                    this.SettingsContentPanel.Children.Add(controlLendOut);
                }

                //If View Lent Out tab is selected, display ViewLentOut.xaml
                if (listView.SelectedItem.Equals(lviViewLent))
                {
                    Control controlViewLent = new ViewLentOut();
                    this.SettingsContentPanel.Children.Add(controlViewLent);
                }

                //If Serach tab is selected, display Search.xaml
                if (listView.SelectedItem.Equals(lviSearch))
                {
                    Control controlSearch = new Search();
                    this.SettingsContentPanel.Children.Add(controlSearch);
                }
            }
        }
    }
}
