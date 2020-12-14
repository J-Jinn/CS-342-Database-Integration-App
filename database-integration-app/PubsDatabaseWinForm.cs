using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

/// <summary>
/// Pubs Database WinForm Application for Front-End/Back-End Integration.
/// 
/// Note: BACKUP the Pubs Database before making changes to it.
/// </summary>
namespace database_integration_app
{
    public partial class PubsDatabaseWinForm : Form
    {
        public PubsDatabaseWinForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This class defines an Author object from the Pubs Database Authors table.
        /// </summary>
        public class Author
        {
            /// <summary>
            /// Constructor for the Author class.
            /// </summary>
            /// <param name="au_id">Author ID</param>
            /// <param name="au_fname">Author First Name</param>
            /// <param name="au_lname">Author Last Name</param>
            public Author(string au_id, string au_fname, string au_lname)
            {
                AuthorID = au_id;
                AuthorFirstName = au_fname;
                AuthorLastName = au_lname;
            }
            /// <summary>
            /// Accessors/Mutator methods.
            /// </summary>
            public string AuthorID { get; set; }
            public string AuthorFirstName { get; set; }
            public string AuthorLastName { get; set; }
        }
        // List of Author objects.
        List<Author> AuthorsList = new List<Author>();

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Function defines the action taken upon clicking the button to load the list of authors in the Pubs Database Authors table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetAuthorTitleInfoButton_Click(object sender, EventArgs e)
        {
            String connString = "Data Source=localhost;" +
                 "Initial Catalog=pubs;Integrated Security=true ";

            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            // Refer to view rather than using direct SQL query statements.
            string queryString = "SELECT * FROM [vAuthors];";

            SqlDataAdapter adapter = new SqlDataAdapter(queryString, conn);

            DataSet authors = new DataSet();

            adapter.Fill(authors, "vAuthors");

            // Populate our List with Authors upon loading all authors in the Authors table.
            foreach (DataRow row in authors.Tables["vAuthors"].Rows)
            {
                Author MyAuthor = new Author($"{row["au_id"]}", $"{row["au_fname"]}", $"{row["au_lname"]}");
                AuthorTitleInfoListBox.Items.Add($"Author: ID:{MyAuthor.AuthorID}, Name:{MyAuthor.AuthorFirstName} {MyAuthor.AuthorLastName}");
                AuthorsList.Add(new Author($"{row["au_id"]}", $"{row["au_fname"]}", $"{row["au_lname"]}"));
            }
            conn.Close();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Function defines the action taken upon clicking the button to update a author's personal information in the Authors Table of the Pubs Database.
        ///  Important Note: The specific author is defined by the selected author in the AuthorTitleInfo ListBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateAuthorTitleInfoButton_Click(object sender, EventArgs e)
        {
            bool debug = true;
            try
            {
                // Get selected item index.
                int SelectedItemIndex = AuthorTitleInfoListBox.SelectedIndex;
                // Get selected author based on item index.
                Author SelectedAuthor = AuthorsList[SelectedItemIndex];

                string authorID = $"{SelectedAuthor.AuthorID}";
                string newData = NewDataTextBox.Text.ToString();
                if (debug)
                {
                    Console.WriteLine($"Author ID: {authorID}");
                    Console.WriteLine($"Author's New Data: {newData}");
                }
                SqlConnection conn = new SqlConnection("Data Source=localhost;" +
                                 "Initial Catalog=pubs;Integrated Security=true ");

                if (authorID.Length == 0)
                {
                    string message = $"Please select an author first before attempting to update any information!";
                    MessageBox.Show(message);
                    return;
                }
                // Call the stored procedure.
                RunAuthorTitlesInfoPStoredProcedure(conn, authorID);
            }
            catch (Exception error)
            {
                if (error.Message.Contains("Index"));
                {
                    string message = $"Please select an author first before attempting to update any information!";
                    MessageBox.Show(message);
                    return;
                }
                Console.WriteLine("Error: " + error.Message);
            }
        }
                      
        /// <summary>
        /// Function defines the action taken when the text changes in the NewDataTextBox for updating the data of the selected author.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewDataTextBox_TextChanged(object sender, EventArgs e)
        {
           // Do something.
        }

        /// <summary>
        /// Function defines the action taken when the user selects a different item in the drop-down list for the author table attributes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectAuthorTableFieldToUpdateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Do something.
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Function defines calling a stored procedure for updating a author's personal address information.
        /// </summary>
        /// <param name="DbConn"></param>
        /// <param name="tId"></param>
        /// <param name="iQty"></param>
        public void RunAuthorTitlesInfoPStoredProcedure(SqlConnection DbConn, string id)
        {
            string column_name = $"";
            // Get user input from TextBox that specifies new value to change author's attribute to.
            string data_value = NewDataTextBox.Text;

            // Determine the column name to use in Authors Table based on selected index of ComboBox drop-down list.
            int caseSwitch = SelectAuthorTableFieldToUpdateComboBox.SelectedIndex;
            switch (caseSwitch)
            {
                case 0:
                    column_name = "au_fname";
                    if (data_value.Length > 20)
                    {
                        string message = $"First name must be 20 characters or less!";
                        MessageBox.Show(message);
                        return;
                    }
                    break;
                case 1:
                    column_name = "au_lname";
                    if (data_value.Length > 40)
                    {
                        string message = $"Last name must be 40 characters or less!";
                        MessageBox.Show(message);
                        return;
                    }
                    break;
                case 2:
                    column_name = "phone";
                    Regex rx = new Regex(@"^[0-9]{3}[\s]{1}[0-9]{3}-[0-9]{4}$", RegexOptions.Compiled);
                    bool result = Regex.IsMatch(data_value, rx.ToString());
                    if (data_value.Length != 12  || result == false)
                    {
                        string message = $"Phone number must be in the format: 'XXX XXX-XXXX where X is a digit 0-9'!";
                        MessageBox.Show(message);
                        return;
                    }
                    break;
                case 3:
                    column_name = "address";
                    if (data_value.Length > 40)
                    {
                        string message = $"Address must be 40 characters or less!";
                        MessageBox.Show(message);
                        return;
                    }
                    break;
                case 4:
                    column_name = "city";
                    if (data_value.Length > 20)
                    {
                        string message = $"City name must be 20 characters or less!";
                        MessageBox.Show(message);
                        return;
                    }
                    break;
                case 5:
                    column_name = "state";
                    if (data_value.Length != 2)
                    {
                        // TODO: Utillize specific list of valid state codes.
                        string message = $"State code must be in the format: 'XX'!";
                        MessageBox.Show(message);
                        return;
                    }
                    break;
                case 6:
                    column_name = "zip";
                    Regex rx2 = new Regex(@"^[0-9]{5}$", RegexOptions.Compiled);
                    bool result2 = Regex.IsMatch(data_value, rx2.ToString());
                    if (data_value.Length != 5 || result2 == false)
                    {
                        string message = $"Zip Code must be 5 characters and contain only the digits 0-9!";
                        MessageBox.Show(message);
                        return;
                    }
                    break;
                case 7:
                    column_name = "contract";
                    bool check = int.TryParse(data_value, out int value);
                    if ((value != 0 && value!= 1) || check == false)
                    {
                        string message = $"Contract status be either '1' or '0'!";
                        MessageBox.Show(message);
                        return;
                    }
                    break;
                default:
                    string message2 = $"Please select an attribute to update!";
                    MessageBox.Show(message2);
                    Console.WriteLine("Default case");
                    break;
            }

            SqlCommand rSproc = new SqlCommand("[proc_ainfo]", DbConn);
            rSproc.CommandType = CommandType.StoredProcedure;

            // Return value.
            SqlParameter spReturn = new SqlParameter();
            spReturn.ParameterName = "@RETURNVALUE";
            spReturn.SqlDbType = System.Data.SqlDbType.Int;
            spReturn.Direction = ParameterDirection.ReturnValue;
            rSproc.Parameters.Add(spReturn);

            // Input parameter - specify which author.
            SqlParameter authorIDParam = new SqlParameter();
            authorIDParam.ParameterName = "@AuthorID";
            authorIDParam.SqlDbType = SqlDbType.VarChar;
            authorIDParam.Size = 12;
            authorIDParam.Direction = ParameterDirection.Input;
            authorIDParam.Value = id;
            rSproc.Parameters.Add(authorIDParam);

            // Input parameter - specify the column name to change the data value for.
            SqlParameter columnNameParam = new SqlParameter();
            columnNameParam.ParameterName = "@AttributeToChange";
            columnNameParam.SqlDbType = SqlDbType.VarChar;
            columnNameParam.Direction = ParameterDirection.Input;
            columnNameParam.Value = column_name;
            rSproc.Parameters.Add(columnNameParam);

            // Input parameter - specify the data value to change to.
            SqlParameter authorNewDataParam = new SqlParameter();
            authorNewDataParam.ParameterName = "@AuthorNewData";
            authorNewDataParam.SqlDbType = SqlDbType.VarChar;
            authorNewDataParam.Direction = ParameterDirection.Input;
            authorNewDataParam.Value = data_value;
            rSproc.Parameters.Add(authorNewDataParam);

            DbConn.Open();
            try
            {
                // Write value of store procedure return value to console to check success/failure.
                rSproc.ExecuteNonQuery();
                Console.WriteLine("Stored procedure return Value: " + rSproc.Parameters[0].Value.ToString());

                // Check if the update was succesful.
                if (rSproc.Parameters[0].Value.ToString() == "0")
                {
                    // Get the index of the selected author in the Authors table.
                    int SelectedItemIndex = AuthorTitleInfoListBox.SelectedIndex;

                    // Get the selected author based on item index value.
                    Author SelectedAuthor = AuthorsList[SelectedItemIndex];

                    string authorName = $"{SelectedAuthor.AuthorFirstName} {SelectedAuthor.AuthorLastName}";

                    string message = $"Sucessfully updated Pubs database!\n Selected Author: {authorName}\n The value for field \"{column_name}\" was changed to \"{data_value}\"";
                    MessageBox.Show(message);
                }
                else
                {
                    string message = $"Failed to update Pubs database!";
                    MessageBox.Show(message);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            DbConn.Close();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Function defines the action taken upon selecting an item in the ListBox that displays the author's ID and full name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AuthorTitleInfoListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear text each time we select a new author.
            AuthorTitleInfoTextBox.Clear();

            // Get selected item index.
            int SelectedItemIndex = AuthorTitleInfoListBox.SelectedIndex;
            // Get selected author based on item index.
            Author SelectedAuthor = AuthorsList[SelectedItemIndex];

            //////////////////////////////////////////////////////////////////////////////////

            String connString = "Data Source=localhost;" +
            "Initial Catalog=pubs;Integrated Security=true ";

            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            string queryString =
                $"SELECT * FROM vAuthorTitles " +
                $"WHERE au_id = '{SelectedAuthor.AuthorID}'";

            SqlDataAdapter adapter = new SqlDataAdapter(queryString, conn);
            DataSet titles = new DataSet();

            adapter.Fill(titles, "titles");

            // Display works by selected author, if any.
            if (titles.Tables[0].Rows.Count == 0)
            {
                AuthorTitleInfoTextBox.AppendText($"No publications were found under the author selected.\r\n\r\n");
            }
            else
            {
                foreach (DataRow row in titles.Tables["titles"].Rows)
                {
                    if (!(row["price"].Equals(System.DBNull.Value)))
                    {
                        double price = Convert.ToDouble(row["price"]);
                        string title = $"Book Title: {row["title"]}. Publication Date: {row["pubdate"]}. Price: ${price}";
                        AuthorTitleInfoTextBox.AppendText($"{title}\r\n\r\n");
                    }
                    else
                    {
                        string title = $"Book Title: {row["title"]}. Publication Date: {row["pubdate"]}. Price: NA";
                        AuthorTitleInfoTextBox.AppendText($"{title}\r\n\r\n");
                    }
                }
            }
            conn.Close();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Function defines the action taken upon the text changing in the TextBox listing all authors in the Authors table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AuthorTitleInfoTextBox_TextChanged(object sender, EventArgs e)
        {
            // Do something if text in the TextBox changes.
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Function defines the action taken when the WinForms app Forms is initially initialized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PubsDatabaseForm_Load(object sender, EventArgs e)
        {
            // Do something to Form upon initialization.
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Function defines the action taken upon clicking on the Update label.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateLabel_Click(object sender, EventArgs e)
        {
            // Label.
        }

        /// <summary>
        /// Function defines the action taken upon clicking on the SelectAuthorTableFieldToUpdate label.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectAuthorTableFieldToUpdateLabel_Click(object sender, EventArgs e)
        {
            // Label.
        }

        /// <summary>
        /// Function defines the action taken upon clicking on the TitlesWrittenBySelectedAuthor label.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TitlesWrittenBySelectedAuthorLabel_Click(object sender, EventArgs e)
        {
            // Label.
        }

        /// <summary>
        /// Function defines the action taken upon clicking on the ListOfAuthors label.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListOfAuthorsLabel_Click(object sender, EventArgs e)
        {
            // Label.
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Function defines the action taken upon clicking the ExitProgram Button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitProgramButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
