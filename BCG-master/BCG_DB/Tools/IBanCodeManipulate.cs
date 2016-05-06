 using System;

namespace Tools
{
	/// <summary>
	/// Summary description for IBanCodeManipulate.
	/// </summary>
	public class IBanCodeManipulate
	{
		#region "Program Variable"
		#endregion

		public IBanCodeManipulate()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		protected Int32 CalculateMod97 ( String strBufferCalculate )
		{
			if( strBufferCalculate == String.Empty )
				throw new ArgumentException("Error in Argument strBufferCalculate !");
			
			String szPartTmp = String.Empty;
			Int32 nInputLength = strBufferCalculate.Length, nProcessed = 0;
			Int32 nResult = 0x00, nAmount = 9;

			while ( nProcessed < nInputLength ) 
			{
				if ( nProcessed + nAmount > nInputLength )
					nAmount = nInputLength - nProcessed;

				szPartTmp += strBufferCalculate.Substring( nProcessed, nAmount );
				nResult = Convert.ToInt32( szPartTmp ) % 97;
				szPartTmp = nResult.ToString();
				nProcessed += nAmount;
				nAmount = 7;
			}
	
			return nResult;
		}

		protected String TransformIBAN( String strIBANAccount )
		{
			String strBuffer = String.Empty; 
			Char[] arrIBANCode = strIBANAccount.ToCharArray();

			try
			{
				for ( Int32 Index = 0; Index < strIBANAccount.Length; Index++ )
				{
					if ( Char.IsDigit( arrIBANCode[Index] ) )
						strBuffer += arrIBANCode[Index];
					else
						strBuffer += ( arrIBANCode[Index] - 'A' + 10 );
				}
			}
			catch( Exception /*Ex*/ )
			{
				return String.Empty;
			}

			return strBuffer;
		}

		protected Boolean CheckIBANString( String strIBANAccount, String strExpectedSymbols, Int32 nExpectedLength, ref String strDescription )
		{
			if( strExpectedSymbols == String.Empty || nExpectedLength <= 0 )
				return false;

			if ( strIBANAccount.Length != nExpectedLength )
			{
				strDescription = String.Format("The IBAN Code must be {0} digits long\n", nExpectedLength );
				return false;
			}
			
			if( strIBANAccount.Substring( 0, 2 ) != strExpectedSymbols )
			{
				strDescription = "The IBAN Code goto with BG\n";
				return false;
			}

			Char[] arrIBANCode = strIBANAccount.ToCharArray();
			for ( Int32 Index = 0; Index < arrIBANCode.Length; ++Index )
			{
				if ( ! Char.IsDigit( arrIBANCode[Index] ) && ! ( Char.IsLetter( arrIBANCode[Index] ) && Char.IsUpper( arrIBANCode[Index] ) ) )
				{
					strDescription = String.Format("The IBAN Code contents have a {0} char\n", arrIBANCode[Index] );
					return false;
				}
			}

			return true;
		}

		public Int32 CalculateIBanCode( String strIBANCode, String ExpectedSymbols, Int32 ExpectedLength )
		{
			String strDescription = String.Empty;
			if( ! CheckIBANString( strIBANCode, ExpectedSymbols, ExpectedLength, ref strDescription ) )
				throw new ArgumentException( strDescription );

			strIBANCode += strIBANCode.Substring( 0, 4 );
			strIBANCode.Remove( 0, 4 );

			strIBANCode = TransformIBAN( strIBANCode );
			if( strIBANCode == String.Empty )
				return 0;

			return 98 - CalculateMod97 ( strIBANCode );
		}
		
		public Int32 CalculateIBanCode( String strIBANCode )
		{
			String strDescription = String.Empty;
			if( ! CheckIBANString( strIBANCode, "BG", 22, ref strDescription ) )
				throw new ArgumentException( strDescription );

			strIBANCode += strIBANCode.Substring( 0, 4 );
			strIBANCode.Remove( 0, 4 );

			strIBANCode = TransformIBAN( strIBANCode );
			if( strIBANCode == String.Empty )
				return 0;

			return 98 - CalculateMod97 ( strIBANCode );
		}

		public Boolean CheckIBanCode( String strIBANCode, String ExpectedSymbols, Int32 ExpectedLength )
		{
			String strDescription = String.Empty;
			if( ! CheckIBANString( strIBANCode, ExpectedSymbols, ExpectedLength, ref strDescription ) )
				throw new ArgumentException( strDescription );

			strIBANCode += strIBANCode.Substring( 0, 4 );
			strIBANCode = strIBANCode.Remove( 0, 4 );

			strIBANCode = TransformIBAN( strIBANCode );
			if( strIBANCode == String.Empty )
				return false;

			return ( CalculateMod97 ( strIBANCode ) == 1 )?true:false;
		}
		
		public Boolean CheckIBanCode( String strIBANCode )
		{
			String strDescription = String.Empty;
			if( ! CheckIBANString( strIBANCode, "BG", 22, ref strDescription ) )
				throw new ArgumentException( strDescription );

			strIBANCode += strIBANCode.Substring( 0, 4 );
			strIBANCode = strIBANCode.Remove( 0, 4 );

			strIBANCode = TransformIBAN( strIBANCode );
			if( strIBANCode == String.Empty )
				return false;

			return ( CalculateMod97 ( strIBANCode ) == 1 )?true:false;
		}
	}
}
