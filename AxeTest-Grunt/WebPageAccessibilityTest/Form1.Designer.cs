namespace TDWebPageListGenerator
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.protocolEntryUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.outputPath = new System.Windows.Forms.TextBox();
            this.pathBrowseButton = new System.Windows.Forms.Button();
            this.generatePageList = new System.Windows.Forms.Button();
            this.webPageExportingStatusText = new System.Windows.Forms.TextBox();
            this.getProtocols = new System.Windows.Forms.Button();
            this.protocolListImportingStatusText = new System.Windows.Forms.TextBox();
            this.tocItemlList = new System.Windows.Forms.CheckedListBox();
            this.selectAll = new System.Windows.Forms.CheckBox();
            this.itemCountText = new System.Windows.Forms.TextBox();
            this.protocolNameText = new System.Windows.Forms.TextBox();
            this.importURLList = new System.Windows.Forms.Button();
            this.testPages = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.testRuleGridView = new System.Windows.Forms.DataGridView();
            this.testStatus = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.testRuleGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // protocolEntryUrl
            // 
            this.protocolEntryUrl.Location = new System.Drawing.Point(184, 24);
            this.protocolEntryUrl.Multiline = true;
            this.protocolEntryUrl.Name = "protocolEntryUrl";
            this.protocolEntryUrl.Size = new System.Drawing.Size(267, 40);
            this.protocolEntryUrl.TabIndex = 0;
            this.protocolEntryUrl.TextChanged += new System.EventHandler(this.protocolEntryUrl_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Input the Url of entry page";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-1, 346);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Output location";
            // 
            // outputPath
            // 
            this.outputPath.Location = new System.Drawing.Point(184, 346);
            this.outputPath.Multiline = true;
            this.outputPath.Name = "outputPath";
            this.outputPath.Size = new System.Drawing.Size(325, 41);
            this.outputPath.TabIndex = 3;
            // 
            // pathBrowseButton
            // 
            this.pathBrowseButton.Location = new System.Drawing.Point(3, 362);
            this.pathBrowseButton.Name = "pathBrowseButton";
            this.pathBrowseButton.Size = new System.Drawing.Size(75, 25);
            this.pathBrowseButton.TabIndex = 4;
            this.pathBrowseButton.Text = "Browse";
            this.pathBrowseButton.UseVisualStyleBackColor = true;
            this.pathBrowseButton.Click += new System.EventHandler(this.pathBrowseButton_Click);
            // 
            // generatePageList
            // 
            this.generatePageList.Enabled = false;
            this.generatePageList.Location = new System.Drawing.Point(2, 416);
            this.generatePageList.Name = "generatePageList";
            this.generatePageList.Size = new System.Drawing.Size(99, 20);
            this.generatePageList.TabIndex = 5;
            this.generatePageList.Text = "Generate URL list";
            this.generatePageList.UseVisualStyleBackColor = true;
            this.generatePageList.Click += new System.EventHandler(this.generatePageList_Click);
            // 
            // webPageExportingStatusText
            // 
            this.webPageExportingStatusText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.webPageExportingStatusText.Enabled = false;
            this.webPageExportingStatusText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.webPageExportingStatusText.ForeColor = System.Drawing.Color.DarkGreen;
            this.webPageExportingStatusText.Location = new System.Drawing.Point(184, 423);
            this.webPageExportingStatusText.Name = "webPageExportingStatusText";
            this.webPageExportingStatusText.Size = new System.Drawing.Size(272, 13);
            this.webPageExportingStatusText.TabIndex = 6;
            // 
            // getProtocols
            // 
            this.getProtocols.Location = new System.Drawing.Point(3, 90);
            this.getProtocols.Name = "getProtocols";
            this.getProtocols.Size = new System.Drawing.Size(109, 23);
            this.getProtocols.TabIndex = 9;
            this.getProtocols.Text = "Get TOC item list";
            this.getProtocols.UseVisualStyleBackColor = true;
            this.getProtocols.Click += new System.EventHandler(this.getTOCItems_Click);
            // 
            // protocolListImportingStatusText
            // 
            this.protocolListImportingStatusText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.protocolListImportingStatusText.Enabled = false;
            this.protocolListImportingStatusText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.protocolListImportingStatusText.ForeColor = System.Drawing.Color.DarkGreen;
            this.protocolListImportingStatusText.Location = new System.Drawing.Point(3, 126);
            this.protocolListImportingStatusText.Name = "protocolListImportingStatusText";
            this.protocolListImportingStatusText.Size = new System.Drawing.Size(176, 13);
            this.protocolListImportingStatusText.TabIndex = 10;
            // 
            // tocItemlList
            // 
            this.tocItemlList.CheckOnClick = true;
            this.tocItemlList.ColumnWidth = 200;
            this.tocItemlList.FormattingEnabled = true;
            this.tocItemlList.Location = new System.Drawing.Point(184, 90);
            this.tocItemlList.MultiColumn = true;
            this.tocItemlList.Name = "tocItemlList";
            this.tocItemlList.Size = new System.Drawing.Size(546, 214);
            this.tocItemlList.TabIndex = 11;
            this.tocItemlList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.tocItemList_ItemCheck);
            // 
            // selectAll
            // 
            this.selectAll.AutoSize = true;
            this.selectAll.Location = new System.Drawing.Point(2, 287);
            this.selectAll.Name = "selectAll";
            this.selectAll.Size = new System.Drawing.Size(65, 17);
            this.selectAll.TabIndex = 12;
            this.selectAll.Text = "selectAll";
            this.selectAll.UseVisualStyleBackColor = true;
            this.selectAll.CheckStateChanged += new System.EventHandler(this.selectAll_CheckStateChanged);
            // 
            // itemCountText
            // 
            this.itemCountText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.itemCountText.Enabled = false;
            this.itemCountText.Location = new System.Drawing.Point(2, 261);
            this.itemCountText.Name = "itemCountText";
            this.itemCountText.Size = new System.Drawing.Size(100, 13);
            this.itemCountText.TabIndex = 13;
            // 
            // protocolNameText
            // 
            this.protocolNameText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.protocolNameText.Enabled = false;
            this.protocolNameText.Location = new System.Drawing.Point(2, 159);
            this.protocolNameText.Multiline = true;
            this.protocolNameText.Name = "protocolNameText";
            this.protocolNameText.Size = new System.Drawing.Size(176, 88);
            this.protocolNameText.TabIndex = 14;
            // 
            // importURLList
            // 
            this.importURLList.Location = new System.Drawing.Point(3, 311);
            this.importURLList.Name = "importURLList";
            this.importURLList.Size = new System.Drawing.Size(98, 23);
            this.importURLList.TabIndex = 16;
            this.importURLList.Text = "Import URL list";
            this.importURLList.UseVisualStyleBackColor = true;
            this.importURLList.Click += new System.EventHandler(this.importURLList_Click);
            // 
            // testPages
            // 
            this.testPages.Enabled = false;
            this.testPages.Location = new System.Drawing.Point(3, 495);
            this.testPages.Name = "testPages";
            this.testPages.Size = new System.Drawing.Size(75, 23);
            this.testPages.TabIndex = 17;
            this.testPages.Text = "Start to test";
            this.testPages.UseVisualStyleBackColor = true;
            this.testPages.Click += new System.EventHandler(this.testPages_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 450);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "Configure test rules";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.configureRules_Click);
            // 
            // testRuleGridView
            // 
            this.testRuleGridView.AllowUserToAddRows = false;
            this.testRuleGridView.AllowUserToDeleteRows = false;
            this.testRuleGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.testRuleGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.testRuleGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.testRuleGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.testRuleGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.testRuleGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.testRuleGridView.Location = new System.Drawing.Point(174, 450);
            this.testRuleGridView.Name = "testRuleGridView";
            this.testRuleGridView.Size = new System.Drawing.Size(556, 150);
            this.testRuleGridView.TabIndex = 19;
            this.testRuleGridView.Visible = false;
            // 
            // testStatus
            // 
            this.testStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.testStatus.Enabled = false;
            this.testStatus.Location = new System.Drawing.Point(3, 534);
            this.testStatus.Name = "testStatus";
            this.testStatus.Size = new System.Drawing.Size(155, 13);
            this.testStatus.TabIndex = 20;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(742, 603);
            this.Controls.Add(this.testStatus);
            this.Controls.Add(this.testRuleGridView);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.testPages);
            this.Controls.Add(this.importURLList);
            this.Controls.Add(this.protocolNameText);
            this.Controls.Add(this.itemCountText);
            this.Controls.Add(this.selectAll);
            this.Controls.Add(this.tocItemlList);
            this.Controls.Add(this.protocolListImportingStatusText);
            this.Controls.Add(this.getProtocols);
            this.Controls.Add(this.webPageExportingStatusText);
            this.Controls.Add(this.generatePageList);
            this.Controls.Add(this.pathBrowseButton);
            this.Controls.Add(this.outputPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.protocolEntryUrl);
            this.Name = "Form1";
            this.Text = "WebPageAccessibilityTest";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.testRuleGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox protocolEntryUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox outputPath;
        private System.Windows.Forms.Button pathBrowseButton;
        private System.Windows.Forms.Button generatePageList;
        private System.Windows.Forms.TextBox webPageExportingStatusText;
        private System.Windows.Forms.Button getProtocols;
        private System.Windows.Forms.TextBox protocolListImportingStatusText;
        private System.Windows.Forms.CheckedListBox tocItemlList;
        private System.Windows.Forms.CheckBox selectAll;
        private System.Windows.Forms.TextBox itemCountText;
        private System.Windows.Forms.TextBox protocolNameText;
        private System.Windows.Forms.Button importURLList;
        private System.Windows.Forms.Button testPages;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView testRuleGridView;
        private System.Windows.Forms.TextBox testStatus;
    }
}

