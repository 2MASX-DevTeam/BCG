using System;
using System.Globalization;
using System.Threading;

namespace Tools
{
	/// <summary>
	/// 
	/// </summary>
	public class DateTimeFormat : System.Object
	{
		#region "Member Fields"
		const Int32 TimeOffset = 6;
		#endregion
		
		public DateTimeFormat()
		{
		}

        //static public String FormatDateLong( DateTime FormatDateTime )
        //{
        //    try
        //    {
        //        return FormatDateTime.ToString("MMMMMMMMMM dd, yyyy");
        //    }
        //    catch (NullReferenceException)
        //    {
        //        return "########";
        //    }
        //}
		
		static public String FormatDate( Object obj )
		{
			try
			{
				if ( obj != DBNull.Value )
				{
					DateTime FormatDateTime = (DateTime)obj;
					// return FormatDateTime.ToString("dd/MM/yyyy");
					return FormatDateTime.ToShortDateString();
				}

				return "-";
			}
			catch ( NullReferenceException )
			{
				return "########";
			}
		}

        static public String FormatDate(DateTime FormatDateTime, String Format )
        {
            try
            {
                return FormatDateTime.ToString(Format, DateTimeFormatInfo.InvariantInfo );
            }
            catch (NullReferenceException)
            {
                return "########";
            }
        }
        
        static public String FormatDateFull(Object obj)
		{
			try
			{
				if ( obj != DBNull.Value )
				{     
					return String.Format( "{0:dd.MM.yyyy} г.", obj );
				}

				return "-";
			}
			catch ( NullReferenceException )
			{
				return "########";
			}
		}

		static public String FormatDateFull( DateTime dateTime )
		{
			try
			{
				if( ! Convert.IsDBNull( dateTime ))
				{     
					return String.Format( "{0:dd.MM.yyyy} г.", dateTime );
				}

				return "-";
			}
			catch ( NullReferenceException )
			{
				return "########";
			}
		}

        static public String FormatDateHour(Object obj)
        {
            try
            {
                if (obj != DBNull.Value)
                {
                    DateTime FormatDateTime = (DateTime)obj;
                    String strHour = String.Empty;
                    String strMinutes = String.Empty;

                    if (FormatDateTime.Hour > 0 || FormatDateTime.Minute > 0)
                    {
                        if (FormatDateTime.Hour < 10)
                            strHour = String.Format("0{0}", FormatDateTime.Hour);
                        else
                            strHour = FormatDateTime.Hour.ToString();

                        if (FormatDateTime.Minute < 10)
                            strMinutes = String.Format("0{0}", FormatDateTime.Minute);
                        else
                            strMinutes = FormatDateTime.Minute.ToString();
                    }

                    if (strHour != String.Empty || strMinutes != String.Empty)
                        return String.Format("{0} {1}:{2}", FormatDateTime.ToShortDateString(), strHour, strMinutes);
                    else
                        return FormatDateTime.ToShortDateString();
                }

                return "-";
            }
            catch (NullReferenceException)
            {
                return "########";
            }
        }

		public virtual DateTime CurrentTime()
		{
			return DateTime.Now;
		}
		
		public override String ToString()
		{
			return CurrentTime().ToString("yyyyMMddHHmmssfff");
		}

		/// <summary>
		/// Връща референция спрямо днешната дата и час като отчита зададената часова разлика
		/// </summary>
		/// <param name="byteTimeOffset">
		///	Часовата разлика, която се добавя към текущата времева референция. Може да бъде и отрицателно число
		/// </param>
		/// <returns>
		/// Низ, представляващ времева референция.
		/// </returns>
		public String ToString( Byte byteTimeOffset )
		{
			DateTime Local = CurrentTime().AddHours( byteTimeOffset );
			return Local.ToString("yyyyMMddHHmmssfff");
		}

		public static DateTime DateParse( String Date )
		{
			try
			{
				IFormatProvider Culture = new CultureInfo( Thread.CurrentThread.CurrentCulture.Name, true);
				return DateTime.Parse( Date, Culture, DateTimeStyles.NoCurrentDateDefault);
			}
			catch ( FormatException ) 
			{
				return DateTime.MinValue;
			}
			catch ( ArgumentNullException ) 
			{
				return DateTime.MinValue;
			}
		}

		public static DateTime DateParse( String Date, String strCulture )
		{
			try
			{
				IFormatProvider Culture = new CultureInfo( strCulture );				

				return DateTime.Parse( Date, Culture, DateTimeStyles.NoCurrentDateDefault );
			}
			catch ( FormatException ) 
			{				
				return DateTime.MinValue;
			}
			catch ( ArgumentNullException ) 
			{
				return DateTime.MinValue;
			}
		}
		
		public static String LoadDatePicker( DateTime dtmToEval )
		{
			String strDay = dtmToEval.Day.ToString();
			String strMonth = dtmToEval.Month.ToString();
			String strYear = dtmToEval.Year.ToString();
			
			return String.Format( "{0}/{1}/{2}", strDay, strMonth, strYear );
		}
 
	}
}
