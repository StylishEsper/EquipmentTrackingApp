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
using System.Data;
using System.Data.SqlClient;

namespace NETD3202_Lab2
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : UserControl
    {
        public Search()
        {
            InitializeComponent();
        }

        //When search button is clicked
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            int ID;
            const string errorBoxTitle = "Error(s)";
            string error = string.Empty;

            //Employee ID Validation
            if (!int.TryParse(txtEmployeeID.Text, out ID)) //if txtEmployeeID.Text is not an integer value
            {
                error = "Enter a whole number for employee ID.";
            }

            //If input errors found, show error message
            if (error != string.Empty)
            {
                MessageBox.Show(error, errorBoxTitle);
            }
            //Else, display the data on the DataGrid
            else
            {
                try
                {
                    //Connecting to database
                    string connectString = Properties.Settings.Default.connect_string;
                    SqlConnection cn = new SqlConnection(connectString);
                    cn.Open();

                    //Filling the grid
                    string selectionQuery = "SELECT * FROM Lent WHERE empID = " + ID; //finding a matching ID
                    SqlCommand command = new SqlCommand(selectionQuery, cn);
                    SqlDataAdapter sda = new SqlDataAdapter(command);
                    DataTable dt = new DataTable("Search");
                    sda.Fill(dt);

                    //Formatting DataGrid: column names
                    dt.Columns[0].ColumnName = "Name";
                    dt.Columns[1].ColumnName = "Employee ID";
                    dt.Columns[2].ColumnName = "Description";
                    dt.Columns[3].ColumnName = "Phone Number";
                    gridSearch.ItemsSource = dt.DefaultView;

                    //If no matching ID found in the database (no rows loaded)
                    if (gridSearch.Items.Count == 0)
                    {
                        error = "Entered employee ID does not exist.";
                        MessageBox.Show(error, errorBoxTitle);
                    }
                }
                //Catch any other errors
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), errorBoxTitle);
                }
            }
        }

        //Formatting DataGrid: numbering the rows
        private void gridSearch_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }

}
