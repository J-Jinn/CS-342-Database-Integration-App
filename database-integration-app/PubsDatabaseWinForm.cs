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

            adapter.Fill(authors, "vAuthorTitles");

            foreach (DataRow row in authors.Tables["vAuthorTitles"].Rows)
            {
                Author MyAuthor = new Author($"{row["au_id"]}", $"{row["au_fname"]}", $"{row["au_lname"]}");
                AuthorTitleInfoListBox.Items.Add($"Author: ID:{MyAuthor.AuthorID}, Name:{MyAuthor.AuthorFirstName} {MyAuthor.AuthorLastName}");
                AuthorsList.Add(new Author($"{row["au_id"]}", $"{row["au_fname"]}", $"{row["au_lname"]}"));
            }
            conn.Close();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Function defines the action taken upon clicking the button to updated a author's personal address information.
        /// 
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
                string newAddress = NewAddressTextBox.Text.ToString();
                if (debug)
                {
                    Console.WriteLine($"Author ID: {authorID}");
                    Console.WriteLine($"Author's New Address: {newAddress}");
                }
                SqlConnection conn = new SqlConnection("Data Source=localhost;" +
                                 "Initial Catalog=pubs;Integrated Security=true ");


                // Call the stored procedure.
                RunAuthorTitlesInfoPStoredProcedure(conn, authorID, authorFirstName, authorLastName, newAddress);
            }
            catch (Exception error)
            {
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

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Function defines a stored procedure for updating a author's personal address information.
        /// </summary>
        /// <param name="DbConn"></param>
        /// <param name="tId"></param>
        /// <param name="iQty"></param>
        public void RunAuthorTitlesInfoPStoredProcedure(SqlConnection DbConn, string id, string first_name, string last_name, string new_address)
        {
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

            // Deprecated.
            SqlParameter authorFirstNameParam = new SqlParameter();
            authorFirstNameParam.ParameterName = "@AuthorFirstName";
            authorFirstNameParam.SqlDbType = SqlDbType.VarChar;
            authorFirstNameParam.Direction = ParameterDirection.Input;
            authorFirstNameParam.Value = first_name;
            rSproc.Parameters.Add(authorFirstNameParam);

            // Deprecated.
            SqlParameter authorLastNameParam = new SqlParameter();
            authorLastNameParam.ParameterName = "@AuthorLastName";
            authorLastNameParam.SqlDbType = SqlDbType.VarChar;
            authorLastNameParam.Direction = ParameterDirection.Input;
            authorLastNameParam.Value = last_name;
            rSproc.Parameters.Add(authorLastNameParam);

            // Input parameter - change author's address to...
            SqlParameter authorNewAddressParam = new SqlParameter();
            authorNewAddressParam.ParameterName = "@AuthorNewAddress";
            authorNewAddressParam.SqlDbType = SqlDbType.VarChar;
            authorNewAddressParam.Direction = ParameterDirection.Input;
            authorNewAddressParam.Value = new_address;
            rSproc.Parameters.Add(authorNewAddressParam);

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
                Console.WriteLine("Added: " + rSproc.Parameters[5].Value.ToString());
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

            // Alter the view's WHERE clause to point to the selected author.
            string queryString =
                $"ALTER VIEW [vAuthorTitles] " +
                $"AS " +
                $"SELECT " +
                $"authors.au_id, authors.au_fname, authors.au_lname, " +
                $"authors.address, authors.city, authors.state, authors.zip, " +
                $"titles.title, titles.pubdate, titles.price " +
                $"FROM authors " +
                $"INNER JOIN titleauthor ON authors.au_id = titleauthor.au_id " +
                $"INNER JOIN titles ON titleauthor.title_id = titles.title_id " +
                $"WHERE authors.au_id = '{SelectedAuthor.AuthorID}'";

            SqlDataAdapter adapter = new SqlDataAdapter(queryString, conn);

            DataSet titles = new DataSet();

            adapter.Fill(titles, "titles");

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
            string queryString2 = $"SELECT * FROM [vAuthorTitles] ";

            SqlDataAdapter adapter2 = new SqlDataAdapter(queryString2, conn);

            DataSet titles2 = new DataSet();

            adapter2.Fill(titles2, "titles2");

            foreach (DataRow row in titles2.Tables["titles2"].Rows)
            {
                string title = $"Book Title: {row["title"]}. Publication Date: {row["pubdate"]}. Price: ${row["price"]}";
                AuthorTitleInfoTextBox.AppendText($"{title}\r\n\r\n");
            }
            conn.Close();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Function defines the action taken upon clicking on the Address label.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddressLabel_Click(object sender, EventArgs e)
        {
            // We won't be doing anything directly with the label.
        }

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
        /// Function controls what happens when the WinForms app Forms is initially initialized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PubsDatabaseForm_Load(object sender, EventArgs e)
        {
            // Do something to Form upon initialization.
            string message = $"Instructions (close MessageBox to load app GUI):\n" +
                $"\n1. Click on the 'Get Author Title Info' button to obtain a list of authors. " +
                $"\n2. Select an author from the list. " +
                $"\n3. The TextBox in the lower-right will display all titles that correspond to the author. " +
                $"\n4. To update an author's address, enter a new address in the 'Enter New Address' TextBox. " +
                $"\n5. Click on the 'Update Address' button to update the author's info. " +
                $"\n6. The address will be updated for the author selected in the ListView of authors. " +
                $"\n7. To view changes, use SQL Server 2018. " +
                $"\n\nTODO: Update aesthetics/functionality of app to be more intuitive.";
            MessageBox.Show(message);

        }

        private void NewAddressTextBox_TextChanged(object sender, EventArgs e)
        {
            // Text Box.
            if (NewAddressTextBox.Text.Count() > 40)
            {
                MessageBox.Show("The address should be less than 40 characters long. Please input a smaller address");
                return;
            }

            else
            {
                string new_address = NewAddressTextBox.Text.ToString();
                MessageBox.Show("The updated address for " + AuthorTitleInfoListBox.SelectedItem.ToString() + " is: " + new_address);
            }
        }

        private void TitlesWrittenBySelectedAuthorLabel_Click(object sender, EventArgs e)
        {
            // Label.
        }

        private void ListOfAuthorsLabel_Click(object sender, EventArgs e)
        {
            // Label.
        }
    }
}
