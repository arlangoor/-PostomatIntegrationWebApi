using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using PostomatIntegration.DAL.Interfaces;

namespace PostomatIntegration.DAL.Entities
{
	[Table("Postomats")]
	public class Postomat:IPostomat, IEqualityComparer
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[MaxLength(8)]
		[Required]
		public string Number { get; set; }
		public string Address { get; set; }
		public bool Status { get; set; }

		public new bool Equals(object x, object y)
		{
			var xPostomat = x as Postomat;
			var yPostomat = y as Postomat;

			if (xPostomat is null && yPostomat is null)
				return true;

			if (xPostomat is null || yPostomat is null)
				return false;

			if (xPostomat.Number != yPostomat.Number)
				return false;

			if (xPostomat.Address != yPostomat.Address)
				return false;

			if (xPostomat.Status != yPostomat.Status)
				return false;

			return true;
		}

		public int GetHashCode(object obj)
		{
			var postomat = obj as Postomat;
			
			if (postomat == null)
				return -1;

			return postomat.Id;
		}
		public static bool operator ==(Postomat x, Postomat y)
		=>new Postomat().Equals(x, y);
		public static bool operator !=(Postomat x, Postomat y)
		=> !new Postomat().Equals(x, y);
	}
}
