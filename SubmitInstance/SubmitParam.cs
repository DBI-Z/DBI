using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubmitInstance
{
	internal class SubmitParam
	{
		public bool Prod { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public int NumberOfPeriods {get; set;}
		public string Filename { get; set; }
	}
}
