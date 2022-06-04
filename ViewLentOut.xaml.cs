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
    /// Interaction logic for ViewLentOut.xaml
    /// </summary>
    public partial class ViewLentOut : UserControl
    {
        public ViewLentOut()
        {
            InitializeComponent();
            FillDataGrid(); //call FillDataGrid() at startup
        }

        private void FillDataGrid()
        {
            const string errorBoxTitle = "Error(s)";

            try
            {
                //Connecting to database
                string connectString = Properties.Settings.Default.connect_string;
                SqlConnection cn = new SqlConnection(connectString);
                cn.Open();

                //Filling the grid
                string selectionQuery = "SELECT * FROM Lent";
                SqlCommand command = new SqlCommand(selectionQuery, cn);
                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable("Lent");
                sda.Fill(dt);

                //Formatting DataGrid: column names (don't know better ways of formatting the DataGrid and...
                //got too lazy to research more)
                dt.Columns[0].ColumnName = "Name";
                dt.Columns[1].ColumnName = "Employee ID";
                dt.Columns[2].ColumnName = "Description";
                dt.Columns[3].ColumnName = "Phone Number";
                gridLent.ItemsSource = dt.DefaultView;
            }
            //Catch any errors
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), errorBoxTitle);
            }
        }

        //Formatting DataGrid: numbering the rows
        private void gridLent_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
