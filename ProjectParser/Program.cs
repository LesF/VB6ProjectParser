using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectParser
{
	/// <summary>
	/// Types of VB6 code blocks or lines will attempt to identify.
	/// General is the default if other types could not be identified.
	/// </summary>
	public enum BlockType
	{
		General,
		Comment,
		Sub,
		Function,
		Enum,
		Constant,
		Variable
	}

	static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
