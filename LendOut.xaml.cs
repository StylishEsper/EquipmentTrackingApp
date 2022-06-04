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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace NETD3202_Lab2
{
    /// <summary>
    /// Interaction logic for LendOut.xaml
    /// </summary>
    public partial class LendOut : UserControl
    {
        public LendOut()
        {
            InitializeComponent();
        }

        private void addToDatabase(object sender, RoutedEventArgs e)
        {
            //Variables
            int ID; //for tryparsing ID input
            const string errorBoxTitle = "Error(s)";
            const string success = "Data successfully added to the database.";
            const string successBoxTitle = "Success";
            string error = String.Empty; //for error display

            //Name validation
            if (txtName.Text == String.Empty) //checking if name is empty
            {
                error += "Name cannot be empty.";
            }

            //ID validation
            if (!int.TryParse(txtID.Text, out ID)) //if txtID.Text is not an integer value
            {
                error += "\nEnter a whole number for ID.";
            }

            //Connecting to database
            string connectString = Properties.Settings.Default.connect_string;
            SqlConnection cn = new SqlConnection(connectString);
            cn.Open();

            //Checking if ID already exists in the database
            string insertQuery = "SELECT [empID] FROM Lent";
            SqlCommand command = new SqlCommand(insertQuery, cn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int i = 0;
                if (ID.ToString() == reader[i].ToString())
                {
                    error += "\nThis ID already exists.";
                }
                i++;
            }
            cn.Close();

            //Description validation
            if (txtDesc.Text == String.Empty) //checking if description is empty
            {
                error += "\nDescription cannot be empty.";
            }

            //Phone validation
            if (txtPhone.Text == String.Empty) //checking if phone number is empty
            {
                error += "\nPhone number cannot be empty.";
            }

            //If input errors found, show error message
            if (error != string.Empty)
            {
                MessageBox.Show(error, errorBoxTitle);
            }
            //Else, try to add the data to the database
            else
            {
                try
                {
                    //Connecting to database
                    connectString = Properties.Settings.Default.connect_string;
                    cn = new SqlConnection(connectString);
                    cn.Open();

                    //Executing the insert query
                    insertQuery = "INSERT INTO Lent ([name], [empID], [desc], [phone]) VALUES('" + txtName.Text + "', '" + txtID.Text + "', '" + txtDesc.Text + "', '" + txtPhone.Text + "')";
                    command = new SqlCommand(insertQuery, cn);
                    command.ExecuteNonQuery();
                    cn.Close();

                    //If no errors were caught, show success message
                    MessageBox.Show(success, successBoxTitle);

                    //Empty all text boxes
                    txtName.Text = string.Empty;
                    txtID.Text = string.Empty;
                    txtDesc.Text = string.Empty;
                    txtPhone.Text = string.Empty;
                }
                //Catch any other errors
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
