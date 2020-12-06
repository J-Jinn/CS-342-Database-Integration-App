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

/// <summary>
/// Pubs Database WinForm Application Integration.
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
            /// <param name="au_id"></param>
            /// <param name="au_fname"></param>
            /// <param name="au_lname"></param>
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
            /*
             * This function defines the button click that populates the ListBox with Author information.
             * */
            String connString = "Data Source=localhost;" +
                 "Initial Catalog=pubs;Integrated Security=true ";

            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            //refer to view rather than dataset
            string queryString = "SELECT * FROM [vAuthors];";

            SqlDataAdapter adapter = new SqlDataAdapter(queryString, conn);

            DataSet authors = new DataSet();

            adapter.Fill(authors, "vAuthors");

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
        /// Function defines the action taken upon clicking the button to update a author's personal information in the Authors Table of the Pubs DB.
        ///  Important Note: Currently, The specific author is defined by the selected author in the AuthorTitleInfo ListBox.
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
                string authorFirstName = $"";
                string authorLastName = $"";
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

            // Deprecated - we don't want to directly update the database with SQL query statements.
            //Console.WriteLine("\n\nUpdating database...");
            ////setup update
            //string stringUpdate = "Update [authors] set state = @state where au_id = @au_id";
            //SqlCommand updateCmd = new SqlCommand(stringUpdate, conn);
            //updateCmd.Parameters.Add("@au_id", SqlDbType.VarChar, 11, "au_id");
            //updateCmd.Parameters.Add("state", SqlDbType.Char, 2, "state");
            //DataTable table = authors.Tables["authors"];
            //adapter.UpdateCommand = updateCmd;
            //adapter.Update(table);
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
        /// Function defines a stored procedure for updating a author's personal address information.
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
                    if (data_value.Length != 12)
                    {
                        string message = $"Phone number must be in the format: 'XXX XXX-XXXX'!";
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
                        string message = $"State code must be in the format: 'XX'!";
                        MessageBox.Show(message);
                        return;
                    }
                    break;
                case 6:
                    column_name = "zip";
                    if (data_value.Length != 5)
                    {
                        string message = $"Zip Code must be 5 characters exactly!";
                        MessageBox.Show(message);
                        return;
                    }
                    break;
                case 7:
                    column_name = "contract";
                    if (data_value != "0" || data_value != "1")
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

            // Output parameters (we don't need one).
            //SqlParameter rOParam = new SqlParameter();
            //rOParam.ParameterName = "@outTotalAdd";
            //rOParam.SqlDbType = SqlDbType.Int;
            //rOParam.Direction = ParameterDirection.Output;
            //rSproc.Parameters.Add(rOParam);

            DbConn.Open();
            try
            {
                // Write values of return value to console to check success/failure.
                rSproc.ExecuteNonQuery();
                Console.WriteLine("Return Value: " + rSproc.Parameters[0].Value.ToString());

                if (rSproc.Parameters[0].Value.ToString() == "0")
                {
                    //Get selected item text
                    int SelectedItem = SelectAuthorTableFieldToUpdateComboBox.SelectedIndex;
                    // Get selected item index.
                    int SelectedItemIndex = AuthorTitleInfoListBox.SelectedIndex;
                    // Get selected author based on item index.
                    Author SelectedAuthor = AuthorsList[SelectedItemIndex];
                    string authorName = $"{SelectedAuthor.AuthorFirstName} {SelectedAuthor.AuthorLastName}";


                    string message = $"Sucessfully updated Pubs database!\n" + $"{authorName}: The value for field '{column_name}' was changed to {data_value}";
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
        /// Function defines the action taken upon selecting an item in the ListBox that outputs the author's ID and full name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AuthorTitleInfoListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear text each time we select a new author.
            AuthorTitleInfoTextBox.Clear();

            // Get selected item text.
            string SelectedItem = AuthorTitleInfoListBox.SelectedItem.ToString();
            // Parse text for ID.
            string[] SelectedItemComponents = SelectedItem.Split(' ');
            // Blah blah blah....skipping parse ID method as it's easier to just use our Author class.

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
                $"SELECT * FROM vATitles " +
                $"WHERE au_id = '{SelectedAuthor.AuthorID}'";

            SqlDataAdapter adapter = new SqlDataAdapter(queryString, conn);

            DataSet titles = new DataSet();

            adapter.Fill(titles, "titles");

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

            // Alter the view's WHERE clause to point to the selected author.
            // This is technically "using" the view we created (views don't support dynamic variable assignment via parameters).
            //string queryString =
            //    $"ALTER VIEW [vAuthorTitles] " +
            //    $"AS " +
            //    $"SELECT " +
            //    $"authors.au_id, authors.au_fname, authors.au_lname, " +
            //    $"authors.address, authors.city, authors.state, authors.zip, " +
            //    $"titles.title, titles.pubdate, titles.price " +
            //    $"FROM authors " +
            //    $"INNER JOIN titleauthor ON authors.au_id = titleauthor.au_id " +
            //    $"INNER JOIN titles ON titleauthor.title_id = titles.title_id " +
            //    $"WHERE authors.au_id = '{SelectedAuthor.AuthorID}'";

            //SqlDataAdapter adapter = new SqlDataAdapter(queryString, conn);

            //DataSet titles = new DataSet();

            //adapter.Fill(titles, "titles");

            //////////////////////////////////////////////////////////////////////////////////

            //// FIXME: Need to change the view itself to set new author ID in WHERE clause (this won't work as is).
            //string queryString = $"SELECT * FROM vAuthorTitles " +
            //   $"WHERE vAuthorTitles.au_id = '{SelectedAuthor.AuthorID}'";

            //// FIXME: Is there a way to use UPDATE view to update the author ID in the WHERE clause of the view (won't function as-is)?
            //string queryString2 = $"UPDATE[vAuthorTitles] " +
            //    $"SET au_id = '{SelectedAuthor.AuthorID}' " +
            //    $"WHERE au_id = '{SelectedAuthor.AuthorID}'";

            //// TODO: Refactor to use a View instead (this will currently work as intended).
            //string queryString2 = $"SELECT * FROM authors " +
            //    $"INNER JOIN titleauthor ON authors.au_id = titleauthor.au_id " +
            //    $"INNER JOIN titles ON titleauthor.title_id = titles.title_id " +
            //    $"WHERE authors.au_id = '{SelectedAuthor.AuthorID}'";

            // This directly refers to the View after altering the View to reflect the currently selected Author ID.
            //string queryString2 = $"SELECT * FROM [vAuthorTitles] ";

            //SqlDataAdapter adapter2 = new SqlDataAdapter(queryString2, conn);

            //DataSet titles2 = new DataSet();

            //adapter2.Fill(titles2, "titles2");

            //foreach (DataRow row in titles2.Tables["titles2"].Rows)
            //{
            //    string title = $"Book Title: {row["title"]}. Publication Date: {row["pubdate"]}. Price: ${row["price"]}";
            //    AuthorTitleInfoTextBox.AppendText($"{title}\r\n\r\n");
            //}
            conn.Close();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Function defines the action taken upon the text changing in the author titles' info TextBox.
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
