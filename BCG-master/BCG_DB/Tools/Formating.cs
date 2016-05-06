using System;
using System.IO;
using System.Text;

namespace Tools
{
	/// <summary>
	/// Summary description for Formating.
	/// </summary>
	public class Formating
	{
		public Formating()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		static public String FormatCurrency( Decimal FormatValue )
		{
			return String.Format("{0:C}", FormatValue );
		}
		
		static public String FormatDecimal( Decimal FormatValue )
		{
			// Thread.CurrentThread.CurrentUICulture;
			return String.Format("{0:N0}", FormatValue );
		}
	}
	 
	public class EncodeStringWriter : StringWriter
	{
		#region "Member Fields"
		String Encode = String.Empty;
		#endregion

		public EncodeStringWriter()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	
		public EncodeStringWriter( String Encode )
		{
			this.Encode = Encode;
		}

		public override System.Text.Encoding Encoding
		{
			get { return ( Encode != String.Empty )?Encoding.GetEncoding( Encode ):base.Encoding; }
		}
	}
}
