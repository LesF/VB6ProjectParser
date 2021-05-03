using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjectParser
{
	class ProjectCollection
	{
		public string VBPPath { get; set; }
		public string Name { get; set; }
		public string Title { get; set; }
		public string ProjectType { get; set; }		// Should be OleDll, we are not interested in other types
		public string Startup { get; set; }
		public string ExeName32 { get; set; }
		public string Path32 { get; set; }
		public string[] References { get; internal set; }

		// (file name, public name) for populating summary view
		public Dictionary<string, string> ClassNames { get; internal set; }
		public Dictionary<string, string> ModuleNames { get; internal set; }

		// (file name, parsed file) for accumulating data during scan, prior to conversion
		public Dictionary<string, VB6File> Classes { get; internal set; }
		public Dictionary<string, VB6File> Modules { get; internal set; }

		public ProjectCollection() { }
		public ProjectCollection(string FromVBP = "")
		{
			if (!string.IsNullOrEmpty(FromVBP))
				ParseVBP(FromVBP);
		}

		public bool ParseVBP(string fromVBP)
		{
			// In case an instance of this class gets re-used, clear properties first
			Name = "";
			ExeName32 = "";
			VBPPath = "";
			Title = "";
			ProjectType = "";
			Startup = "";
			ExeName32 = "";
			Path32 = "";
			References = null;
			Classes = null;
			Modules = null;
			ClassNames = new Dictionary<string, string>();
			ModuleNames = new Dictionary<string, string>();
			Classes = new Dictionary<string, VB6File>();
			Modules = new Dictionary<string, VB6File>();

			if (string.IsNullOrEmpty(fromVBP))
				return false;
			FileInfo vbpInfo = new FileInfo(fromVBP);
			if (!vbpInfo.Exists)
				return false;

			VBPPath = vbpInfo.DirectoryName;
			List<string> refs = new List<string>();

			string[] propValue;
			using (StreamReader sr = vbpInfo.OpenText())
			{
				string line = "";
				while ((line = sr.ReadLine()) != null)
				{
					string[] prop = line.Split("=");
					if (prop.Length > 0 && prop[0].Length > 0)
					{
						switch (prop[0].ToLower())
						{
							case "reference":
								refs.Add(prop[1]);
								break;
							case "class":
								propValue = prop[1].Split(";");
								ClassNames.Add(propValue[1].Trim(), propValue[0].Trim());	// Filename, Classname  - they are not always the same
								break;
							case "module":
								propValue = prop[1].Split(";");
								ModuleNames.Add(propValue[1].Trim(), propValue[0].Trim());
								break;
							case "name":
								Name = prop[1];
								break;
							case "title":
								Title = prop[1];
								break;
							case "startup":
								Startup = prop[1];
								break;
							case "exename32":
								ExeName32 = prop[1];
								break;
							case "path32":
								Path32 = prop[1];
								break;
							case "type":
								ProjectType = prop[1];
								break;
						}
					}
				}
			}
			References = refs.ToArray<string>();
			return true;
		}
	}
}
