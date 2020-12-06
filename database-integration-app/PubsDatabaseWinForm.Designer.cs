
namespace database_integration_app
{
    partial class PubsDatabaseWinForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GetAuthorTitleInfoButton = new System.Windows.Forms.Button();
            this.AuthorTitleInfoListBox = new System.Windows.Forms.ListBox();
            this.UpdateAuthorTitleInfoButton = new System.Windows.Forms.Button();
            this.NewDataTextBox = new System.Windows.Forms.TextBox();
            this.UpdateLabel = new System.Windows.Forms.Label();
            this.AuthorTitleInfoTextBox = new System.Windows.Forms.TextBox();
            this.TitlesWrittenBySelectedAuthorLabel = new System.Windows.Forms.Label();
            this.ListOfAuthorsLabel = new System.Windows.Forms.Label();
            this.SelectAuthorTableFieldToUpdateComboBox = new System.Windows.Forms.ComboBox();
            this.SelectAuthorTableFieldToUpdateLabel = new System.Windows.Forms.Label();
            this.ExitProgramButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GetAuthorTitleInfoButton
            // 
            this.GetAuthorTitleInfoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GetAuthorTitleInfoButton.Location = new System.Drawing.Point(431, 313);
            this.GetAuthorTitleInfoButton.Name = "GetAuthorTitleInfoButton";
            this.GetAuthorTitleInfoButton.Size = new System.Drawing.Size(131, 45);
            this.GetAuthorTitleInfoButton.TabIndex = 1;
            this.GetAuthorTitleInfoButton.Text = "Get List of Authors";
            this.GetAuthorTitleInfoButton.UseVisualStyleBackColor = true;
            this.GetAuthorTitleInfoButton.Click += new System.EventHandler(this.GetAuthorTitleInfoButton_Click);
            // 
            // AuthorTitleInfoListBox
            // 
            this.AuthorTitleInfoListBox.ColumnWidth = 80;
            this.AuthorTitleInfoListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AuthorTitleInfoListBox.FormattingEnabled = true;
            this.AuthorTitleInfoListBox.HorizontalExtent = 480;
            this.AuthorTitleInfoListBox.HorizontalScrollbar = true;
            this.AuthorTitleInfoListBox.ItemHeight = 16;
            this.AuthorTitleInfoListBox.Location = new System.Drawing.Point(24, 30);
            this.AuthorTitleInfoListBox.Name = "AuthorTitleInfoListBox";
            this.AuthorTitleInfoListBox.ScrollAlwaysVisible = true;
            this.AuthorTitleInfoListBox.Size = new System.Drawing.Size(538, 276);
            this.AuthorTitleInfoListBox.TabIndex = 2;
            this.AuthorTitleInfoListBox.SelectedIndexChanged += new System.EventHandler(this.AuthorTitleInfoListBox_SelectedIndexChanged);
            // 
            // UpdateAuthorTitleInfoButton
            // 
            this.UpdateAuthorTitleInfoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateAuthorTitleInfoButton.Location = new System.Drawing.Point(1017, 131);
            this.UpdateAuthorTitleInfoButton.Name = "UpdateAuthorTitleInfoButton";
            this.UpdateAuthorTitleInfoButton.Size = new System.Drawing.Size(150, 45);
            this.UpdateAuthorTitleInfoButton.TabIndex = 3;
            this.UpdateAuthorTitleInfoButton.Text = "Update";
            this.UpdateAuthorTitleInfoButton.UseVisualStyleBackColor = true;
            this.UpdateAuthorTitleInfoButton.Click += new System.EventHandler(this.UpdateAuthorTitleInfoButton_Click);
            // 
            // NewDataTextBox
            // 
            this.NewDataTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewDataTextBox.Location = new System.Drawing.Point(615, 103);
            this.NewDataTextBox.Name = "NewDataTextBox";
            this.NewDataTextBox.Size = new System.Drawing.Size(552, 22);
            this.NewDataTextBox.TabIndex = 4;
            this.NewDataTextBox.Text = "Enter new data here...";
            this.NewDataTextBox.TextChanged += new System.EventHandler(this.NewDataTextBox_TextChanged);
            // 
            // UpdateLabel
            // 
            this.UpdateLabel.AutoSize = true;
            this.UpdateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateLabel.Location = new System.Drawing.Point(609, 73);
            this.UpdateLabel.Name = "UpdateLabel";
            this.UpdateLabel.Size = new System.Drawing.Size(94, 18);
            this.UpdateLabel.TabIndex = 5;
            this.UpdateLabel.Text = "Update Data:";
            this.UpdateLabel.Click += new System.EventHandler(this.UpdateLabel_Click);
            // 
            // AuthorTitleInfoTextBox
            // 
            this.AuthorTitleInfoTextBox.Location = new System.Drawing.Point(24, 376);
            this.AuthorTitleInfoTextBox.Multiline = true;
            this.AuthorTitleInfoTextBox.Name = "AuthorTitleInfoTextBox";
            this.AuthorTitleInfoTextBox.ReadOnly = true;
            this.AuthorTitleInfoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.AuthorTitleInfoTextBox.Size = new System.Drawing.Size(538, 280);
            this.AuthorTitleInfoTextBox.TabIndex = 6;
            this.AuthorTitleInfoTextBox.TextChanged += new System.EventHandler(this.AuthorTitleInfoTextBox_TextChanged);
            // 
            // TitlesWrittenBySelectedAuthorLabel
            // 
            this.TitlesWrittenBySelectedAuthorLabel.AutoSize = true;
            this.TitlesWrittenBySelectedAuthorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitlesWrittenBySelectedAuthorLabel.Location = new System.Drawing.Point(21, 355);
            this.TitlesWrittenBySelectedAuthorLabel.Name = "TitlesWrittenBySelectedAuthorLabel";
            this.TitlesWrittenBySelectedAuthorLabel.Size = new System.Drawing.Size(245, 18);
            this.TitlesWrittenBySelectedAuthorLabel.TabIndex = 7;
            this.TitlesWrittenBySelectedAuthorLabel.Text = "Titles Written by the Selected Author";
            this.TitlesWrittenBySelectedAuthorLabel.Click += new System.EventHandler(this.TitlesWrittenBySelectedAuthorLabel_Click);
            // 
            // ListOfAuthorsLabel
            // 
            this.ListOfAuthorsLabel.AutoSize = true;
            this.ListOfAuthorsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListOfAuthorsLabel.Location = new System.Drawing.Point(21, 9);
            this.ListOfAuthorsLabel.Name = "ListOfAuthorsLabel";
            this.ListOfAuthorsLabel.Size = new System.Drawing.Size(223, 18);
            this.ListOfAuthorsLabel.TabIndex = 8;
            this.ListOfAuthorsLabel.Text = "List of Authors in Pubs Database";
            this.ListOfAuthorsLabel.Click += new System.EventHandler(this.ListOfAuthorsLabel_Click);
            // 
            // SelectAuthorTableFieldToUpdateComboBox
            // 
            this.SelectAuthorTableFieldToUpdateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectAuthorTableFieldToUpdateComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectAuthorTableFieldToUpdateComboBox.FormattingEnabled = true;
            this.SelectAuthorTableFieldToUpdateComboBox.Items.AddRange(new object[] {
            "Author First Name (au_fname)",
            "Author Last Name (au_lname)",
            "Author Phone Number (phone)",
            "Author Address (address)",
            "Author City (city)",
            "Author State (state)",
            "Author Zip Code (zip)",
            "Author Contract Status (contract)"});
            this.SelectAuthorTableFieldToUpdateComboBox.Location = new System.Drawing.Point(615, 30);
            this.SelectAuthorTableFieldToUpdateComboBox.MaxDropDownItems = 20;
            this.SelectAuthorTableFieldToUpdateComboBox.Name = "SelectAuthorTableFieldToUpdateComboBox";
            this.SelectAuthorTableFieldToUpdateComboBox.Size = new System.Drawing.Size(327, 24);
            this.SelectAuthorTableFieldToUpdateComboBox.TabIndex = 9;
            this.SelectAuthorTableFieldToUpdateComboBox.SelectedIndexChanged += new System.EventHandler(this.SelectAuthorTableFieldToUpdateComboBox_SelectedIndexChanged);
            // 
            // SelectAuthorTableFieldToUpdateLabel
            // 
            this.SelectAuthorTableFieldToUpdateLabel.AutoSize = true;
            this.SelectAuthorTableFieldToUpdateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectAuthorTableFieldToUpdateLabel.Location = new System.Drawing.Point(609, 9);
            this.SelectAuthorTableFieldToUpdateLabel.Name = "SelectAuthorTableFieldToUpdateLabel";
            this.SelectAuthorTableFieldToUpdateLabel.Size = new System.Drawing.Size(243, 18);
            this.SelectAuthorTableFieldToUpdateLabel.TabIndex = 10;
            this.SelectAuthorTableFieldToUpdateLabel.Text = "Select Author Table Field to Update:";
            this.SelectAuthorTableFieldToUpdateLabel.Click += new System.EventHandler(this.SelectAuthorTableFieldToUpdateLabel_Click);
            // 
            // ExitProgramButton
            // 
            this.ExitProgramButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitProgramButton.Location = new System.Drawing.Point(1014, 611);
            this.ExitProgramButton.Name = "ExitProgramButton";
            this.ExitProgramButton.Size = new System.Drawing.Size(150, 45);
            this.ExitProgramButton.TabIndex = 11;
            this.ExitProgramButton.Text = "Exit Program";
            this.ExitProgramButton.UseVisualStyleBackColor = true;
            this.ExitProgramButton.Click += new System.EventHandler(this.ExitProgramButton_Click);
            // 
            // PubsDatabaseWinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1222, 678);
            this.Controls.Add(this.ExitProgramButton);
            this.Controls.Add(this.SelectAuthorTableFieldToUpdateLabel);
            this.Controls.Add(this.SelectAuthorTableFieldToUpdateComboBox);
            this.Controls.Add(this.ListOfAuthorsLabel);
            this.Controls.Add(this.TitlesWrittenBySelectedAuthorLabel);
            this.Controls.Add(this.AuthorTitleInfoTextBox);
            this.Controls.Add(this.UpdateLabel);
            this.Controls.Add(this.NewDataTextBox);
            this.Controls.Add(this.UpdateAuthorTitleInfoButton);
            this.Controls.Add(this.AuthorTitleInfoListBox);
            this.Controls.Add(this.GetAuthorTitleInfoButton);
            this.Name = "PubsDatabaseWinForm";
            this.Text = "Pubs Database - Author Titles Info App (Important: BACKUP DATABASE FIRST)";
            this.Load += new System.EventHandler(this.PubsDatabaseForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button GetAuthorTitleInfoButton;
        private System.Windows.Forms.ListBox AuthorTitleInfoListBox;
        private System.Windows.Forms.Button UpdateAuthorTitleInfoButton;
        private System.Windows.Forms.TextBox NewDataTextBox;
        private System.Windows.Forms.Label UpdateLabel;
        private System.Windows.Forms.TextBox AuthorTitleInfoTextBox;
        private System.Windows.Forms.Label TitlesWrittenBySelectedAuthorLabel;
        private System.Windows.Forms.Label ListOfAuthorsLabel;
        private System.Windows.Forms.ComboBox SelectAuthorTableFieldToUpdateComboBox;
        private System.Windows.Forms.Label SelectAuthorTableFieldToUpdateLabel;
        private System.Windows.Forms.Button ExitProgramButton;
    }
}

