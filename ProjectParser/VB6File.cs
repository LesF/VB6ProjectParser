using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectParser
{
	class VB6File
	{
		public string PublicName { get; internal set; }
		public string SourceFile { get; internal set; }
		public string FileType { get; set; }
		public string[] RawSource { get; internal set; }
		public Dictionary<string, VB6Method> Methods { get; internal set; }
		public Dictionary<string, VB6Declaration> Declarations { get; internal set; }

		public VB6File(){ }
		public VB6File(string SourcePath)
		{
			ScanProjectFile(SourcePath);
		}

		public bool ScanProjectFile(string SourcePath)
		{
			Methods = new Dictionary<string, VB6Method>();
			Declarations = new Dictionary<string, VB6Declaration>();
			FileInfo fi = new FileInfo(SourcePath);
			if (!fi.Exists)
				return false;
			SourceFile = SourcePath;
			string rawCode = fi.OpenText().ReadToEnd();
			RawSource = rawCode.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
			VB6Line lineParser;
			string inStructure = "";
			string structureName = "";
			List<string> block = new List<string>();

			/* If the line ends with a continuation character, hold that line
			 * value and prefix it to the next line.  We want to parse the
			 * entire statement as a single line.  This mainly concerns multi-line
			 * method declarations.
			 */
			string continueLine = "";
			string thisLine = "";
			foreach (string line in RawSource)
			{
				if (continueLine.Length > 0)
				{
					thisLine = (continueLine + " " + line).Trim();
					continueLine = "";
				}
				else
				{
					thisLine = line.Trim();
				}
				if (thisLine.EndsWith("_"))
				{
					continueLine = thisLine.Substring(0, thisLine.Length).Trim();
					continue;
				}

				if (thisLine.StartsWith("Attribute"))
				{
					if (thisLine.StartsWith("Attribute VB_Name"))
					{
						PublicName = thisLine.Split(" ")[3].Replace("\"", "");
					}
					// else - any other attributes we are interested in?  Methods can have a description attribute
				}
				else
				{
					lineParser = new VB6Line(thisLine);
					switch (lineParser.LineType)
					{
						case VB6Line.CodeType.Sub:
							inStructure = "sub";
							structureName = lineParser.BlockName;
							break;
						case VB6Line.CodeType.Function:
							inStructure = "function";
							structureName = lineParser.BlockName;
							break;
						case VB6Line.CodeType.Enum:
							// not stored currently, but track it as a code block
							inStructure = "enum";
							structureName = lineParser.BlockName;
							break;
						default:
							//
							break;
					}

					if (inStructure.Length > 0)
					{
						block.Add(thisLine);

						if (thisLine.Contains($"end {inStructure}", StringComparison.OrdinalIgnoreCase))
						{
							/* Have reached the end of a block
							 * TODO store the sub,function or enum declaration
							 */
							Methods.Add(structureName, new VB6Method(string.Join("\r\n", block)));
							inStructure = "";
							structureName = "";
							block.Clear();
						}
					}
					else
					{
						if (lineParser.LineType != VB6Line.CodeType.Comment && !string.IsNullOrEmpty(lineParser.BlockName))
						{
							// Store declarations which are found outside of method blocks
							Declarations.Add(lineParser.BlockName, new VB6Declaration(lineParser.RawCode));
						}
					}
				}
			}
			return false;
		}

	}
}
