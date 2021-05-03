using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectParser
{
	/// <summary>
	/// Code line parser.  Attempts to identify code block headers
	/// (Sub, Function, Enum) and the accessibility scope declarations.
	/// When a sub or function declaration is identified, determine
	/// the method name, arguments and return type.
	/// Notes: 
	/// * This does not handle multi-line statements, suggested action
	/// is to find all lines ending in the continuation character (_)
	/// and join them together before passing the full code line to this
	/// parser.
	/// </summary>
	class VB6Line
	{
		public string RawCode { get; set; }
		public string Scope { get; set; }       // Public, Private, Friend
		public string BlockName { get; set; }   // Name of the Function, Sub or Enum.  Empty for other code lines
		public string DataType { get; set; }    // The VB6 data type, if appropriate - Function return type, Enum data type
		public string[] Args { get; set; }      // Raw argument declarations, for Sub or Function (is this useful?)
		public CodeType LineType { get; internal set; }

		/// <summary>
		/// Types of code line we will attempt to identify.
		/// General is the default if other types could not be identified.
		/// </summary>
		public enum CodeType {
			General,
			Comment,
			Sub,
			Function,
			Enum,
			Constant,
			Variable
		}

		private string[] mAccess = new string[] { "Private", "Public", "Friend" };

		/// <summary>
		/// Constructor must be passed a line of code to parse
		/// </summary>
		/// <param name="CodeLine">Some VB6 code</param>
		public VB6Line(string CodeLine)
		{
			LineType = CodeType.General;
			ParseCodeLine(CodeLine);
		}

		private void ParseCodeLine(string CodeLine)
		{
			CodeLine = CodeLine.Trim();
			RawCode = CodeLine;
			if (CodeLine.Length == 0 || CodeLine.StartsWith("'"))
			{
				// The easiest one...
				LineType = CodeType.Comment;
				return;
			}

			string[] words = CodeLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);
			string[] args = null;
			string returnType = "";

			if (mAccess.Contains(words[0]))
			{
				Scope = words[0];
				// Drop the first word
				words[0] = "";
				CodeLine = string.Join(" ", words).Trim();
				words = CodeLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);
			}

			switch (words[0].ToLower())
			{
				case "sub":
					LineType = CodeType.Sub;
					args = null;
					BlockName = ParseMethodDeclaration(CodeLine, ref args, ref returnType);
					if (BlockName.Length > 0 && args != null)
						Args = args;
					break;

				case "function":
					LineType = CodeType.Function;
					args = null;
					returnType = "";
					BlockName = ParseMethodDeclaration(CodeLine, ref args, ref returnType);
					if (BlockName.Length > 0)
					{
						Args = args;
						DataType = returnType;
					}
					break;

				case "enum":
					LineType = CodeType.Enum;
					BlockName = words[1];
					break;

				case "const":
					/* Example:
					 * Private Const MStr_MODULE_NAME As String = "UTILS"
					 * or something more complex:
					 * Private Const KEY_READ = ((STANDARD_RIGHTS_READ Or KEY_QUERY_VALUE Or KEY_ENUMERATE_SUB_KEYS Or KEY_NOTIFY) And (Not SYNCHRONIZE))
					 * 
					 * TODO determine if there is an inline comment after the value
					 * - if value is not quoted, chop at 'comment
					 * - if value is quoted, chop and 'comment outside of 2nd quote
					 */
					LineType = CodeType.Constant;
					BlockName = words[1];
					if (words.Length > 3 && words[2].Equals("as", StringComparison.OrdinalIgnoreCase))
						DataType = words[3];
					if (words.Length > 5 && words[4].Equals("="))
					{
						int idx = CodeLine.IndexOf(" = ");
						if (idx > 0)
						{
							string theValue = CodeLine.Substring(idx + 2).Trim();
							if (theValue.StartsWith("\""))
							{
								idx = theValue.IndexOf("\"", 1);
								if (idx > 1)
									theValue = theValue.Substring(0, idx);  // Drop anything after the 2nd quote
							}
							else
							{
								idx = theValue.IndexOf("'");
								if (idx > 0)
									theValue = theValue.Substring(0, idx).Trim();
							}
							Args = new string[] { theValue };   // Lets put the constant value in the Args property for now
						}
					}
					break;

				case "dim":
					LineType = CodeType.Variable;
					/* BlockName will have the name of the first or only variable declared in this Dim statement.
					 * Args will contain the full declaration including the type, for each variable declared in the statement.
					 * 
					 * Example, will put 3 values in Args and 'rs' in Blockname:
					 * Dim rs As ADODB.Recordset, cmd As ADODB.Command, cnn As ADODB.Connection
					 */
					BlockName = words[1];
					if (CodeLine.Contains(" as ",StringComparison.OrdinalIgnoreCase) || CodeLine.Contains(","))
					{
						args = null;
						ParseVariables(CodeLine, ref args);
						if (args != null)
							Args = args;
					}
					break;

				default:
					LineType = CodeType.General;
					break;
			}

		}

		private string ParseMethodDeclaration(string MethodDeclaration, ref string[] MethodArgs, ref string ReturnType)
		{
			int idx;
			string name = "";
			ReturnType = "";
			if (MethodDeclaration.StartsWith("function", StringComparison.OrdinalIgnoreCase))
			{
				idx = MethodDeclaration.LastIndexOf(" as ", StringComparison.OrdinalIgnoreCase);
				if (idx > 0)
				{
					ReturnType = MethodDeclaration.Substring(idx + 4).Trim();
					MethodDeclaration = MethodDeclaration.Substring(0, idx).Trim();
					idx = ReturnType.LastIndexOf("'");
					if (idx > 0)
						ReturnType = ReturnType.Substring(0, idx).Trim();   // Remove any trailing comments
				}
			}

			idx = MethodDeclaration.IndexOf(" ");   // 1st gap after sub,function,enum etc.
			if (idx > 0)
			{
				name = MethodDeclaration.Substring(idx).Trim();
				idx = name.IndexOf("(");
				if (idx > 0)
					name = name.Substring(0, idx);
			}

			idx = MethodDeclaration.IndexOf("(");
			if (idx > 0)
			{
				string args = MethodDeclaration.Substring(idx +1);
				idx = args.LastIndexOf(")");
				if (idx > 0)
					args = args.Substring(0, idx).Trim();
				MethodArgs = args.Split(",");
			}

			return name;
		}

		private int ParseVariables(string DimStatement, ref string[] MethodArgs)
		{
			/* How many variations should we try to scrape meaning from?
			 * Dim sTemp As String							- simple
			 * Dim rs As ADODB.Recordset, cmd As ADODB.Command, cnn As ADODB.Connection		- still kind of simple
			 * Dim objConfigHelper, objLookup,rsLookup		- variant types
			 */
			List<string> dimStatements = new List<string>();
			if (DimStatement.StartsWith("dim", StringComparison.OrdinalIgnoreCase))
				DimStatement = DimStatement.Substring(3).Trim();
			int idx = DimStatement.LastIndexOf("'");
			if (idx > 0)
				DimStatement = DimStatement.Substring(0, idx);  // Assume it has an inline comment
			string[] vars = DimStatement.Split(",");
			foreach (string varName in vars)
			{
				string[] bits = varName.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
				if (bits.Length > 1)
					dimStatements.Add(string.Join(" ", bits));
				else
					dimStatements.Add($"{bits[0]} As Variant");
			}
			MethodArgs = dimStatements.ToArray();
			return dimStatements.Count;
		}
	}
}
