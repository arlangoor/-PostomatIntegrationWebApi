using System;
using System.Collections.Generic;
using System.Text;

namespace PostomatIntegration.DAL.Interfaces
{
	public interface IPostomat
	{
		string Number { get; set; }
		string Address { get; set; }
		bool Status { get; set; }
	}
}
