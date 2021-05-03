using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectParser
{
	class VB6Declaration
	{
		public string RawDeclaration { get; internal set; }
		public string DataType { get; set; }
		public string VarName { get; set; }

		public VB6Declaration(){ }
		public VB6Declaration(string SourceDeclaration)
		{
			ParseDeclaration(SourceDeclaration);
		}

		public bool ParseDeclaration(string SourceDeclaration)
		{
			RawDeclaration = SourceDeclaration;
			// TODO find VB6 type and scope, determine C# data type
			return false;
		}
	}
}
