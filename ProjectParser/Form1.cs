using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectParser
{
	public partial class Form1 : Form
	{
		private ProjectCollection mProject;

		public Form1()
		{
			InitializeComponent();

			// TODO save and restore previously used path names
			TextProjectFile.Text = @"C:\Projects\HealthViews\DEV\Current\HVClinician\ComponentsSource\VB6\Data\dbGalenILAB\dbGalenILAB.vbp";
			TextTargetDir.Text = @"C:\Projects\HealthViews\DEV\Current\HVClinician\ComponentsSource\Converted";
		}

		private void BtnSelectProject_Click(object sender, EventArgs e)
		{
			openFileDialog1.FileName = TextProjectFile.Text;
			openFileDialog1.Filter = "VB6 Project (*.vbp)|*.vbp|All files (*.*)|*.*";
			openFileDialog1.FilterIndex = 0;
			openFileDialog1.DefaultExt = ".vbp";
			openFileDialog1.Title = "Select VB6 Project";
			openFileDialog1.Multiselect = false;
			if (TextProjectFile.Text.Length == 0)
				openFileDialog1.InitialDirectory = @"C:\Projects\HealthViews\DEV\Current\HVClinician\ComponentsSource\VB6";
			if (openFileDialog1.ShowDialog(this).Equals(DialogResult.OK))
			{
				TextProjectFile.Text = openFileDialog1.FileName;
				LoadProject();
			}
		}

		private void BtnTargetDir_Click(object sender, EventArgs e)
		{
			folderBrowserDialog1.Description = "Conversion Target Directory";
			if (TextTargetDir.Text.Length > 0)
				folderBrowserDialog1.SelectedPath = TextTargetDir.Text;
			else
				folderBrowserDialog1.SelectedPath = @"C:\Projects\HealthViews\DEV\Current\HVClinician\ComponentsSource";
			
			if (folderBrowserDialog1.ShowDialog(this).Equals(DialogResult.OK))
				TextTargetDir.Text = folderBrowserDialog1.SelectedPath;
		}

		private void BtnParseProject_Click(object sender, EventArgs e)
		{
			LoadProject();
		}

		private void BtnLoadFiles_Click(object sender, EventArgs e)
		{
			// TODO clear previously loaded project, if button was clicked a second time
			ScanProjectFiles();
		}

		private void BtnConvert_Click(object sender, EventArgs e)
		{
			// TODO
		}

		private void LoadProject()
		{
			listView1.Items.Clear();
			string projectPath = TextProjectFile.Text.Trim();
			if (projectPath.Length == 0)
				return;

			mProject = new ProjectCollection(projectPath);
			if (mProject.VBPPath.Length > 0)
			{
				// Project was loaded - display summary
				ListViewItem itm = listView1.Items.Add(new ListViewItem(new string[] { 
					mProject.ExeName32.Replace("\"",""),
					"project",
					mProject.Name.Replace("\"",""),
					$"Classes: {mProject.ClassNames.Keys.Count}, Modules: {mProject.ModuleNames.Keys.Count}"
				}));
				itm.BackColor = Color.BlanchedAlmond;
			}
			else
			{
				listView1.Items.Add(new ListViewItem("Project could not be loaded"));
				return;
			}

			// Show summary of modules and classes

			foreach (string m in mProject.ModuleNames.Keys)
			{
				listView1.Items.Add(new ListViewItem(new string[] { 
					m,
					"module",
					mProject.ModuleNames[m],
					""
				}));
			}

			foreach (string c in mProject.ClassNames.Keys)
			{
				listView1.Items.Add(new ListViewItem(new string[] {
					c,
					"class",
					mProject.ClassNames[c],
					""
				}));
			}
		}

		private void ScanProjectFiles()
		{
			foreach (ListViewItem itm in listView1.Items)
			{
				itm.BackColor = Color.MistyRose;
				listView1.Update();
				if (itm.SubItems[1].Text.Equals("module") || itm.SubItems[1].Text.Equals("class"))
					itm.SubItems[3].Text = ScanFile (itm.SubItems[2].Text, itm.SubItems[0].Text, itm.SubItems[1].Text);
				itm.BackColor = Color.SeaShell;
				listView1.Update();
			}
			Console.WriteLine($"Classes: {mProject.Classes.Keys.Count}");
		}

		private string ScanFile(string PublicName, string SourceFile, string FileType)
		{
			string sourcePath = Path.Combine(mProject.VBPPath, SourceFile);
			if (!File.Exists(sourcePath))
				return $"Not found ({sourcePath})";

			VB6File codeFile = new VB6File(sourcePath);
			if (codeFile.RawSource == null || codeFile.RawSource.Length == 0)
				return "Unable to parse class file";

			if (FileType.Equals("class"))
				mProject.Classes.Add(SourceFile, codeFile);
			else
				mProject.Modules.Add(SourceFile, codeFile);

			return $"Props/vars: {codeFile.Declarations.Keys.Count}, Methods: {codeFile.Methods.Keys.Count}";
		}

	}
}
