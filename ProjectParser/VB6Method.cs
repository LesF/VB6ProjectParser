using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectParser
{
	class VB6Method
	{
		public string MethodName { get; set; }
		public bool IsFunction { get; set; }
		public VB6Declaration ReturnValue { get; set; }
		public VB6Declaration[] Arguments { get; set; }		// TODO need to be able to indicate optional args and default values as part of VB6Declaration
		public string[] RawMethod { get; internal set; }
		public BlockType CodeBlockType { get; set; }

		public VB6Method() { }
		public VB6Method(string RawSource)
		{
			ParseMethod(RawSource);
		}

		public bool ParseMethod(string rawSource)
		{
			RawMethod = rawSource.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
			// TODO parse arguments and return type

			return false;
		}
	}
}
