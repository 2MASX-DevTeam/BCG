using System;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;

namespace Tools
{
	#region Classes
	
	public sealed class ValidateTools
	{
		private ValidateTools()
		{
			return;
		}
				
		public static Boolean LocalValidate( params BaseValidator[] ValidatorList )
		{
			Int32 index = 0;
			Boolean blnReturnValue = true;
			
			for( index = 0; index < ValidatorList.Length; index++)
				ValidatorList[index].Validate();
				
			for( index = 0; index < ValidatorList.Length; index++)
				blnReturnValue = blnReturnValue & ValidatorList[index].IsValid;
						
			return blnReturnValue;
		}		
	}


    public sealed class ValidateEGN
    {
        public ValidateEGN()
        {
        }

        static public Boolean IsEgnValid( String EGN )
        {
            // проверяваме дали е въведена няква стойност
            if (String.IsNullOrEmpty(EGN)) return false;

            // проверяваме дали низът се състои точно от 10 символа
            if (EGN.Length != 10) { return false; }

            // проверяваме дали низът се състои само от цифри
            Double num;

            if (!Double.TryParse(EGN, out num)) { return false; }

            // низът е проверен, слователно пристъпваме към главната задача
            int[] Weights = { 2, 4, 8, 5, 10, 9, 7, 3, 6 };
            String Date = string.Empty;

            // първите две цифри указват годината на раждане
            Int16 Year = Int16.Parse(EGN.Substring(0, 2));

            // вторите две цифри указват месеца
            Int16 Month = Int16.Parse(EGN.Substring(2, 2));

            // третите две цифри указват деня
            Int16 Day = Int16.Parse(EGN.Substring(4, 2));

            DateTime validDateTime;
            if (Month > 40)
                // Date = String.Format("{0}/{1}/{2}", Day, (Month - 40),(Year + 2000) );
                validDateTime = new DateTime( (Year + 2000), (Month - 40), Day );
            else if (Month > 20)
                // Date = String.Format("{0}/{1}/{2}", Day, (Month - 20), (Year + 1800) );
                validDateTime = new DateTime( (Year + 1800), (Month - 20), Day );
            else
                // Date = String.Format("{0}/{1}/{2}", Day, Month, (Year + 1900) );
                validDateTime = new DateTime( (Year + 1900), Month, Day );

            // DateTime validDateTime = DateTimeFormat.DateParse(Date, "bg-BG");
            if (validDateTime.Day != Day) 
                return false;

            Int32 CheckSum = Int32.Parse(EGN.Substring(9, 1));
            Int32 EGNSum = 0;
            Int32 ValidCheckSum = 0;

            for (Int16 Index = 0; Index < 9; Index++)
                EGNSum += int.Parse( EGN.Substring(Index, 1)) * Weights[Index];

            ValidCheckSum = EGNSum % 11;

            if (ValidCheckSum == 10) { ValidCheckSum = 0; }

            return ( CheckSum == ValidCheckSum)?true:false;
        }
    }

    public sealed class ValidateEIK
    {
        
        public ValidateEIK()
        {
        }

        static public Boolean IsEIKValid(String EIK)
        {
            if (EIK == String.Empty)
                return false;

            EIK = EIK.Replace("/&#092;s+/", "");
            if (EIK.Length == 9 || EIK.Length == 13)
            {
                Int32 Sum = 0;
                for (int i = 0; i < 8; i++)
                    Sum += Convert.ToInt16(EIK[i].ToString()) * (i + 1);

                Int32 NewValue = Sum % 11;
                if (NewValue == 10)
                {
                    Sum = 0;
                    for (int i = 0; i < 8; i++)
                        Sum += Convert.ToInt16(EIK[i].ToString()) * (i + 3);

                    NewValue = Sum % 11;
                    if (NewValue == 10)
                        NewValue = 0;
                }
                if (NewValue == Convert.ToInt16(EIK[8].ToString()))
                {
                    if (EIK.Length == 9)
                        return true;
                    else
                    {
                        Sum = Convert.ToInt16(EIK[8].ToString()) * 2 + Convert.ToInt16(EIK[9].ToString()) * 7 + Convert.ToInt16(EIK[10].ToString()) * 3 + Convert.ToInt16(EIK[11].ToString()) * 5;
                        NewValue = Sum % 11;
                        if (NewValue == 10)
                        {
                            Sum = Convert.ToInt16(EIK[8].ToString()) * 4 + Convert.ToInt16(EIK[9].ToString()) * 9 + Convert.ToInt16(EIK[10].ToString()) * 5 + Convert.ToInt16(EIK[11].ToString()) * 7;
                            NewValue = Sum % 11;
                            if (NewValue == 10)
                                NewValue = 0;
                        }
                        if (NewValue == Convert.ToInt16(EIK[12].ToString()))
                            return true;
                        else
                            return false;

                    }

                }
                else
                    return false;
            }
            else
                return false;
        }
    }
	
	#endregion	
}
