
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
            this.NewAddressTextBox = new System.Windows.Forms.TextBox();
            this.AddressLabel = new System.Windows.Forms.Label();
            this.AuthorTitleInfoTextBox = new System.Windows.Forms.TextBox();
            this.TitlesWrittenBySelectedAuthorLabel = new System.Windows.Forms.Label();
            this.ListOfAuthorsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GetAuthorTitleInfoButton
            // 
            this.GetAuthorTitleInfoButton.Location = new System.Drawing.Point(557, 703);
            this.GetAuthorTitleInfoButton.Name = "GetAuthorTitleInfoButton";
            this.GetAuthorTitleInfoButton.Size = new System.Drawing.Size(130, 45);
            this.GetAuthorTitleInfoButton.TabIndex = 1;
            this.GetAuthorTitleInfoButton.Text = "Get Author Title Info";
            this.GetAuthorTitleInfoButton.UseVisualStyleBackColor = true;
            this.GetAuthorTitleInfoButton.Click += new System.EventHandler(this.GetAuthorTitleInfoButton_Click);
            // 
            // AuthorTitleInfoListBox
            // 
            this.AuthorTitleInfoListBox.ColumnWidth = 80;
            this.AuthorTitleInfoListBox.FormattingEnabled = true;
            this.AuthorTitleInfoListBox.HorizontalExtent = 480;
            this.AuthorTitleInfoListBox.HorizontalScrollbar = true;
            this.AuthorTitleInfoListBox.Location = new System.Drawing.Point(24, 393);
            this.AuthorTitleInfoListBox.Name = "AuthorTitleInfoListBox";
            this.AuthorTitleInfoListBox.ScrollAlwaysVisible = true;
            this.AuthorTitleInfoListBox.Size = new System.Drawing.Size(506, 355);
            this.AuthorTitleInfoListBox.TabIndex = 2;
            this.AuthorTitleInfoListBox.SelectedIndexChanged += new System.EventHandler(this.AuthorTitleInfoListBox_SelectedIndexChanged);
            // 
            // UpdateAuthorTitleInfoButton
            // 
            this.UpdateAuthorTitleInfoButton.Location = new System.Drawing.Point(1014, 101);
            this.UpdateAuthorTitleInfoButton.Name = "UpdateAuthorTitleInfoButton";
            this.UpdateAuthorTitleInfoButton.Size = new System.Drawing.Size(150, 45);
            this.UpdateAuthorTitleInfoButton.TabIndex = 3;
            this.UpdateAuthorTitleInfoButton.Text = "Update Author Title Info";
            this.UpdateAuthorTitleInfoButton.UseVisualStyleBackColor = true;
            this.UpdateAuthorTitleInfoButton.Click += new System.EventHandler(this.UpdateAuthorTitleInfoButton_Click);
            // 
            // NewAddressTextBox
            // 
            this.NewAddressTextBox.Location = new System.Drawing.Point(612, 64);
            this.NewAddressTextBox.Name = "NewAddressTextBox";
            this.NewAddressTextBox.Size = new System.Drawing.Size(552, 20);
            this.NewAddressTextBox.TabIndex = 4;
            this.NewAddressTextBox.Text = "Enter new address here...";
            this.NewAddressTextBox.TextChanged += new System.EventHandler(this.NewAddressTextBox_TextChanged);
            // 
            // AddressLabel
            // 
            this.AddressLabel.AutoSize = true;
            this.AddressLabel.Location = new System.Drawing.Point(609, 37);
            this.AddressLabel.Name = "AddressLabel";
            this.AddressLabel.Size = new System.Drawing.Size(171, 13);
            this.AddressLabel.TabIndex = 5;
            this.AddressLabel.Text = "Enter New Address (x <= 40 chars)";
            this.AddressLabel.Click += new System.EventHandler(this.AddressLabel_Click);
            // 
            // AuthorTitleInfoTextBox
            // 
            this.AuthorTitleInfoTextBox.Location = new System.Drawing.Point(24, 37);
            this.AuthorTitleInfoTextBox.Multiline = true;
            this.AuthorTitleInfoTextBox.Name = "AuthorTitleInfoTextBox";
            this.AuthorTitleInfoTextBox.ReadOnly = true;
            this.AuthorTitleInfoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.AuthorTitleInfoTextBox.Size = new System.Drawing.Size(506, 300);
            this.AuthorTitleInfoTextBox.TabIndex = 6;
            this.AuthorTitleInfoTextBox.TextChanged += new System.EventHandler(this.AuthorTitleInfoTextBox_TextChanged);
            // 
            // TitlesWrittenBySelectedAuthorLabel
            // 
            this.TitlesWrittenBySelectedAuthorLabel.AutoSize = true;
            this.TitlesWrittenBySelectedAuthorLabel.Location = new System.Drawing.Point(21, 9);
            this.TitlesWrittenBySelectedAuthorLabel.Name = "TitlesWrittenBySelectedAuthorLabel";
            this.TitlesWrittenBySelectedAuthorLabel.Size = new System.Drawing.Size(180, 13);
            this.TitlesWrittenBySelectedAuthorLabel.TabIndex = 7;
            this.TitlesWrittenBySelectedAuthorLabel.Text = "Titles Written by the Selected Author";
            this.TitlesWrittenBySelectedAuthorLabel.Click += new System.EventHandler(this.TitlesWrittenBySelectedAuthorLabel_Click);
            // 
            // ListOfAuthorsLabel
            // 
            this.ListOfAuthorsLabel.AutoSize = true;
            this.ListOfAuthorsLabel.Location = new System.Drawing.Point(21, 365);
            this.ListOfAuthorsLabel.Name = "ListOfAuthorsLabel";
            this.ListOfAuthorsLabel.Size = new System.Drawing.Size(161, 13);
            this.ListOfAuthorsLabel.TabIndex = 8;
            this.ListOfAuthorsLabel.Text = "List of Authors in Pubs Database";
            this.ListOfAuthorsLabel.Click += new System.EventHandler(this.ListOfAuthorsLabel_Click);
            // 
            // PubsDatabaseWinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1222, 760);
            this.Controls.Add(this.ListOfAuthorsLabel);
            this.Controls.Add(this.TitlesWrittenBySelectedAuthorLabel);
            this.Controls.Add(this.AuthorTitleInfoTextBox);
            this.Controls.Add(this.AddressLabel);
            this.Controls.Add(this.NewAddressTextBox);
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
        private System.Windows.Forms.TextBox NewAddressTextBox;
        private System.Windows.Forms.Label AddressLabel;
        private System.Windows.Forms.TextBox AuthorTitleInfoTextBox;
        private System.Windows.Forms.Label TitlesWrittenBySelectedAuthorLabel;
        private System.Windows.Forms.Label ListOfAuthorsLabel;
    }
}

