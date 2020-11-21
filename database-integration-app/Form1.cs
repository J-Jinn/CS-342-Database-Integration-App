﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace database_integration_app
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void GetAuthorTitleInfoButton_Click(object sender, EventArgs e)
        {
            String connString = "Data Source=localhost;" +
                 "Initial Catalog=pubs;Integrated Security=true ";

            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            string queryString = "SELECT * FROM authors;";

            SqlDataAdapter adapter = new SqlDataAdapter(queryString, conn);

            DataSet authors = new DataSet();
            //DataSet titles = new DataSet();

            adapter.Fill(authors, "authors");

            foreach (DataRow row in authors.Tables["authors"].Rows)
            {
                //string info = $"Author: {row["au_fname"]} {row["au_lname"]} Address: {row["address"]} City: {row["city"]} " +
                //    $"State: {row["state"]} Zip Code: {row["zip"]} Book Title: {row["title"]} Publication Date: {row["pubdate"]} Price: {row["price"]}"; 

                string author = $"Author: {row["au_fname"]} {row["au_lname"]}";

                AuthorTitleInfoListBox.Items.Add(author);
            }

            conn.Close();
            Console.WriteLine("\n\nDone...");
        }

        private void UpdateAuthorTitleInfoButton_Click(object sender, EventArgs e)
        {
            //String connString =
            //"Data Source=localhost;" +
            //"Initial Catalog=pubs;Integrated Security=true ";

            //String cmdString = "select au_lname, au_fname from authors";

            //SqlConnection conn = new SqlConnection(connString);

            //conn.Open();

            //SqlCommand cmd = new SqlCommand(cmdString, conn);
            //SqlDataReader r = cmd.ExecuteReader();
            //while (r.Read())
            //{
            //    Console.WriteLine("Name: " +
            //                 r["au_fname"] + " " + r["au_lname"]);
            //}
            //conn.Close();

            //string inTitleId;
            //int iQty;
            //Console.Write("Enter Title ID:");
            //inTitleId = Console.ReadLine();
            //Console.Write("Enter Qty:");
            //iQty = int.Parse(Console.ReadLine());
            //SqlConnection conn = new SqlConnection("Data Source=localhost;" +
            //                 "Initial Catalog=pubs;Integrated Security=true ");
            //runProc(conn, inTitleId, iQty);

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

        public static void runProc(SqlConnection DbConn, string tId, int iQty)
        {
            SqlCommand rSproc = new SqlCommand("prAddToOrder", DbConn);
            rSproc.CommandType = CommandType.StoredProcedure;
            //now create a parameter object for the return value - note it will be an integer
            SqlParameter spReturn = new SqlParameter();
            spReturn.ParameterName = "@return_value";
            spReturn.SqlDbType = System.Data.SqlDbType.Int;
            spReturn.Direction = ParameterDirection.ReturnValue;
            rSproc.Parameters.Add(spReturn);

            //now add the input parameters
            SqlParameter rTParam = new SqlParameter();
            rTParam.ParameterName = "@inTitleId";
            rTParam.SqlDbType = SqlDbType.Char;
            rTParam.Size = 12;
            rTParam.Direction = ParameterDirection.Input;
            rTParam.Value = tId;
            rSproc.Parameters.Add(rTParam);

            SqlParameter rQParam = new SqlParameter();
            rQParam.ParameterName = "@inIncrease";
            rQParam.SqlDbType = SqlDbType.Int;
            rQParam.Direction = ParameterDirection.Input;
            rQParam.Value = iQty;
            rSproc.Parameters.Add(rQParam);

            //Now create the output
            SqlParameter rOParam = new SqlParameter();
            rOParam.ParameterName = "@outTotalAdd";
            rOParam.SqlDbType = SqlDbType.Int;
            rOParam.Direction = ParameterDirection.Output;
            rSproc.Parameters.Add(rOParam);
            DbConn.Open();

            try
            {

                rSproc.ExecuteNonQuery();
                Console.WriteLine("Added: " + rSproc.Parameters[3].Value.ToString());
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            DbConn.Close();

        }

        private void AuthorTitleInfoListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AuthorTitleInfoListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            String connString = "Data Source=localhost;" +
            "Initial Catalog=pubs;Integrated Security=true ";

            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            // Get selected text.
            // Parse text for first name and last name.
            // Send query for book title and other information based on first name and last name.

            string queryString = "SELECT * FROM [vAuthorTitles];";

            SqlDataAdapter adapter = new SqlDataAdapter(queryString, conn);

            //DataSet authors = new DataSet();
            DataSet titles = new DataSet();

            adapter.Fill(titles, "titles");

            foreach (DataRow row in titles.Tables["titles"].Rows)
            {
                //string info = $"Author: {row["au_fname"]} {row["au_lname"]} Address: {row["address"]} City: {row["city"]} " +
                //    $"State: {row["state"]} Zip Code: {row["zip"]} Book Title: {row["title"]} Publication Date: {row["pubdate"]} Price: {row["price"]}"; 

                string title = $"Book Title: {row["title"]} Publication Date: {row["pubdate"]} Price: {row["price"]}";

                AuthorTitleInfoTextBox.AppendText($"{title}\r\n\r\n");
            }

            conn.Close();
            Console.WriteLine("\n\nDone...");
        }

        private void AddressLabel_Click(object sender, EventArgs e)
        {

        }

        private void AuthorTitleInfoTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
