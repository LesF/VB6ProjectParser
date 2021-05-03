
namespace ProjectParser
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.BtnSelectProject = new System.Windows.Forms.Button();
			this.TextProjectFile = new System.Windows.Forms.TextBox();
			this.BtnParseProject = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.TextTargetDir = new System.Windows.Forms.TextBox();
			this.BtnTargetDir = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.BtnLoadFiles = new System.Windows.Forms.Button();
			this.BtnConvert = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "VB6 Project File";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// BtnSelectProject
			// 
			this.BtnSelectProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnSelectProject.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.BtnSelectProject.Location = new System.Drawing.Point(1029, 12);
			this.BtnSelectProject.Name = "BtnSelectProject";
			this.BtnSelectProject.Size = new System.Drawing.Size(32, 23);
			this.BtnSelectProject.TabIndex = 2;
			this.BtnSelectProject.Text = "...";
			this.BtnSelectProject.UseVisualStyleBackColor = true;
			this.BtnSelectProject.Click += new System.EventHandler(this.BtnSelectProject_Click);
			// 
			// TextProjectFile
			// 
			this.TextProjectFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TextProjectFile.Location = new System.Drawing.Point(106, 12);
			this.TextProjectFile.Name = "TextProjectFile";
			this.TextProjectFile.Size = new System.Drawing.Size(917, 23);
			this.TextProjectFile.TabIndex = 1;
			// 
			// BtnParseProject
			// 
			this.BtnParseProject.Location = new System.Drawing.Point(12, 73);
			this.BtnParseProject.Name = "BtnParseProject";
			this.BtnParseProject.Size = new System.Drawing.Size(101, 23);
			this.BtnParseProject.TabIndex = 3;
			this.BtnParseProject.Text = "Parse Project";
			this.BtnParseProject.UseVisualStyleBackColor = true;
			this.BtnParseProject.Click += new System.EventHandler(this.BtnParseProject_Click);
			// 
			// listView1
			// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView1.BackColor = System.Drawing.Color.SeaShell;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader2});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(12, 102);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(1049, 481);
			this.listView1.TabIndex = 4;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "File";
			this.columnHeader1.Width = 190;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Type";
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Name";
			this.columnHeader4.Width = 190;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Summary";
			this.columnHeader2.Width = 600;
			// 
			// TextTargetDir
			// 
			this.TextTargetDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TextTargetDir.Location = new System.Drawing.Point(106, 41);
			this.TextTargetDir.Name = "TextTargetDir";
			this.TextTargetDir.Size = new System.Drawing.Size(917, 23);
			this.TextTargetDir.TabIndex = 6;
			// 
			// BtnTargetDir
			// 
			this.BtnTargetDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnTargetDir.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.BtnTargetDir.Location = new System.Drawing.Point(1029, 41);
			this.BtnTargetDir.Name = "BtnTargetDir";
			this.BtnTargetDir.Size = new System.Drawing.Size(32, 23);
			this.BtnTargetDir.TabIndex = 7;
			this.BtnTargetDir.Text = "...";
			this.BtnTargetDir.UseVisualStyleBackColor = true;
			this.BtnTargetDir.Click += new System.EventHandler(this.BtnTargetDir_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(90, 15);
			this.label2.TabIndex = 5;
			this.label2.Text = "Target Directory";
			// 
			// BtnLoadFiles
			// 
			this.BtnLoadFiles.Location = new System.Drawing.Point(119, 73);
			this.BtnLoadFiles.Name = "BtnLoadFiles";
			this.BtnLoadFiles.Size = new System.Drawing.Size(101, 23);
			this.BtnLoadFiles.TabIndex = 8;
			this.BtnLoadFiles.Text = "Scan Files";
			this.BtnLoadFiles.UseVisualStyleBackColor = true;
			this.BtnLoadFiles.Click += new System.EventHandler(this.BtnLoadFiles_Click);
			// 
			// BtnConvert
			// 
			this.BtnConvert.Location = new System.Drawing.Point(226, 73);
			this.BtnConvert.Name = "BtnConvert";
			this.BtnConvert.Size = new System.Drawing.Size(75, 23);
			this.BtnConvert.TabIndex = 9;
			this.BtnConvert.Text = "Convert";
			this.BtnConvert.UseVisualStyleBackColor = true;
			this.BtnConvert.Click += new System.EventHandler(this.BtnConvert_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1073, 595);
			this.Controls.Add(this.BtnConvert);
			this.Controls.Add(this.BtnLoadFiles);
			this.Controls.Add(this.TextTargetDir);
			this.Controls.Add(this.BtnTargetDir);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.BtnParseProject);
			this.Controls.Add(this.TextProjectFile);
			this.Controls.Add(this.BtnSelectProject);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "VB6 Project File";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Button BtnSelectProject;
		private System.Windows.Forms.TextBox TextProjectFile;
		private System.Windows.Forms.Button BtnParseProject;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.TextBox TextTargetDir;
		private System.Windows.Forms.Button BtnTargetDir;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.Button BtnLoadFiles;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Button BtnConvert;
	}
}

