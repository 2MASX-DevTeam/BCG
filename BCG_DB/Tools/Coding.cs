using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Tools
{
	/// <summary>
	/// Object implement coding and encoding string
	/// </summary>
	public class Coding
	{
		protected String SecKeyIni; 
		protected String SecKeySec;

        #region Constructors

        public Coding( String SecKeyIni, String SecKeySec )
		{
			if( SecKeyIni == String.Empty || SecKeySec == String.Empty )
				throw new ArgumentException("You have empty Security Key!");
			
			this.SecKeyIni = SecKeyIni;
			this.SecKeySec = SecKeySec;
        }

        #endregion

        public String MD5Encrypt( String PlainText )
		{
			try
			{
				// First we need to convert the string into bytes, which
				// means using a text encoder.
				Encoder encUnicodetoByte = System.Text.Encoding.Unicode.GetEncoder();

				// Create a buffer large enough to hold the string
				byte[] unicodeText = new byte[ PlainText.Length * 2];
				encUnicodetoByte.GetBytes( PlainText.ToCharArray(), 0, PlainText.Length, unicodeText, 0, true );

				// Now that we have a byte array we can ask the CSP to hash it
				MD5 CryptServ = new MD5CryptoServiceProvider();
				byte[] Result = CryptServ.ComputeHash( unicodeText );

				// Build the final string by converting each byte
				// into hex and appending it to a StringBuilder
				StringBuilder sb = new StringBuilder();
				for (int i=0; i < Result.Length; i++ ) 
					sb.Append( Result[i].ToString()); 

				return sb.ToString();
			}
			catch ( System.IO.IOException Ex ) 
			{
				throw Ex;
			}
		}

		protected Int32 GetBytes( String PlainText )
		{
			return Encoding.ASCII.GetByteCount( PlainText );
        }

        protected Int32 GetBytesUnicode(String PlainText)
        {
            return Encoding.UTF8.GetByteCount(PlainText);
        }

        #region Rijndael Managed Encrypt ASCII

        public String RijndaelManagedEncrypt( String PlainText )
		{
			RijndaelManaged EncryptRijndaelManaged = new RijndaelManaged();
			
			try
			{
				ASCIIEncoding encASCII = (ASCIIEncoding)Encoding.ASCII;

				byte[] byteSecKeyIni = new byte[ GetBytes( SecKeyIni ) ];
				byte[] byteSecKeySec = new byte[ GetBytes( SecKeySec ) ];
				byte[] bytePlainText = new byte[ GetBytes( PlainText ) ];

				encASCII.GetBytes( SecKeyIni.ToCharArray(), 0, SecKeyIni.Length, byteSecKeyIni, 0 );
				encASCII.GetBytes( SecKeySec.ToCharArray(), 0, SecKeySec.Length, byteSecKeySec, 0 );
				encASCII.GetBytes( PlainText.ToCharArray(), 0, PlainText.Length, bytePlainText, 0 );

				ICryptoTransform Encryptor = EncryptRijndaelManaged.CreateEncryptor( byteSecKeyIni, byteSecKeySec );
				
				//Encrypt the data.
				MemoryStream msEncrypt = new MemoryStream();
				CryptoStream csEncrypt = new CryptoStream( msEncrypt, Encryptor, CryptoStreamMode.Write );
				csEncrypt.Write( bytePlainText, 0, bytePlainText.Length );
				csEncrypt.FlushFinalBlock();

				byte[] byteEncryptText = msEncrypt.ToArray();
				msEncrypt.Close();
				csEncrypt.Close();

				String EncryptText = Convert.ToBase64String( byteEncryptText );

				return EncryptText.Trim();
			}
			catch ( Exception Ex ) 
			{
				throw Ex;
			}
		}

		public String RijndaelManagedDecrypt( String EncryptText )
		{
			RijndaelManaged EncryptRijndaelManaged = new RijndaelManaged();
			
			try
			{
				ASCIIEncoding encASCII = (ASCIIEncoding)Encoding.ASCII;

				byte[] byteSecKeyIni = new byte[ GetBytes( SecKeyIni ) ];
				byte[] byteSecKeySec = new byte[ GetBytes( SecKeySec ) ];
				byte[] byteEncryptedText = Convert.FromBase64String( EncryptText );

				encASCII.GetBytes( SecKeyIni.ToCharArray(), 0, SecKeyIni.Length, byteSecKeyIni, 0 );
				encASCII.GetBytes( SecKeySec.ToCharArray(), 0, SecKeySec.Length, byteSecKeySec, 0 );

				ICryptoTransform Decryptor = EncryptRijndaelManaged.CreateDecryptor( byteSecKeyIni, byteSecKeySec );
				
				//Decrypt the data.
				MemoryStream msDecrypt = new MemoryStream( byteEncryptedText );
				CryptoStream csDecrypt = new CryptoStream( msDecrypt, Decryptor, CryptoStreamMode.Read );
				
				byte[] bytePlainText = new byte[byteEncryptedText.Length];
				Int32 readSize = csDecrypt.Read( bytePlainText, 0, bytePlainText.Length );

				String strDecryptData = encASCII.GetString( bytePlainText );

				msDecrypt.Close();
				csDecrypt.Close();

				strDecryptData = strDecryptData.Trim('\0');

				return strDecryptData;
			}
			catch ( Exception Ex ) 
			{
				throw Ex;
			}
        }

        #endregion

        #region Rijndael Managed Encrypt ASCII byte array

        public byte[] RijndaelManagedEncrypt(byte[] bytePlainText)
        {
            RijndaelManaged EncryptRijndaelManaged = new RijndaelManaged();
            EncryptRijndaelManaged.Padding = PaddingMode.Zeros;

            try
            {
                ASCIIEncoding encASCII = (ASCIIEncoding)Encoding.ASCII;

                byte[] byteSecKeyIni = new byte[GetBytes(SecKeyIni)];
                byte[] byteSecKeySec = new byte[GetBytes(SecKeySec)];
                //byte[] bytePlainText = new byte[GetBytes(PlainText)];

                encASCII.GetBytes(SecKeyIni.ToCharArray(), 0, SecKeyIni.Length, byteSecKeyIni, 0);
                encASCII.GetBytes(SecKeySec.ToCharArray(), 0, SecKeySec.Length, byteSecKeySec, 0);
                //encASCII.GetBytes(PlainText.ToCharArray(), 0, PlainText.Length, bytePlainText, 0);

                ICryptoTransform Encryptor = EncryptRijndaelManaged.CreateEncryptor(byteSecKeyIni, byteSecKeySec);

                //Encrypt the data.
                MemoryStream msEncrypt = new MemoryStream();
                CryptoStream csEncrypt = new CryptoStream(msEncrypt, Encryptor, CryptoStreamMode.Write);
                csEncrypt.Write(bytePlainText, 0, bytePlainText.Length);
                csEncrypt.FlushFinalBlock();

                byte[] byteEncryptText = msEncrypt.ToArray();
                msEncrypt.Close();
                csEncrypt.Close();

                //String EncryptText = Convert.ToBase64String(byteEncryptText);

                return byteEncryptText;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public byte[] RijndaelManagedDecrypt(byte[] byteEncryptedText)
        {
            RijndaelManaged EncryptRijndaelManaged = new RijndaelManaged();
            EncryptRijndaelManaged.Padding = PaddingMode.Zeros;

            try
            {
                ASCIIEncoding encASCII = (ASCIIEncoding)Encoding.ASCII;

                byte[] byteSecKeyIni = new byte[GetBytes(SecKeyIni)];
                byte[] byteSecKeySec = new byte[GetBytes(SecKeySec)];
                //byte[] byteEncryptedText = Convert.FromBase64String(EncryptText);

                encASCII.GetBytes(SecKeyIni.ToCharArray(), 0, SecKeyIni.Length, byteSecKeyIni, 0);
                encASCII.GetBytes(SecKeySec.ToCharArray(), 0, SecKeySec.Length, byteSecKeySec, 0);

                ICryptoTransform Decryptor = EncryptRijndaelManaged.CreateDecryptor(byteSecKeyIni, byteSecKeySec);

                //Decrypt the data.
                MemoryStream msDecrypt = new MemoryStream(byteEncryptedText);
                CryptoStream csDecrypt = new CryptoStream(msDecrypt, Decryptor, CryptoStreamMode.Read);

                byte[] bytePlainText = new byte[byteEncryptedText.Length];
                Int32 readSize = csDecrypt.Read(bytePlainText, 0, bytePlainText.Length);

                //String strDecryptData = encASCII.GetString(bytePlainText);

                msDecrypt.Close();
                csDecrypt.Close();

                //strDecryptData = strDecryptData.Trim('\0');

                return bytePlainText;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion

        #region Rijndael Managed Encrypt Unicode

        public String RijndaelManagedEncryptUnicode(String PlainText)
        {
            RijndaelManaged EncryptRijndaelManaged = new RijndaelManaged();

            try
            {
                UTF8Encoding encUNICODE = (UTF8Encoding)Encoding.UTF8;                

                byte[] byteSecKeyIni = new byte[GetBytesUnicode(SecKeyIni)];
                byte[] byteSecKeySec = new byte[GetBytesUnicode(SecKeySec)];
                byte[] bytePlainText = new byte[GetBytesUnicode(PlainText)];

                encUNICODE.GetBytes(SecKeyIni.ToCharArray(), 0, SecKeyIni.Length, byteSecKeyIni, 0);
                encUNICODE.GetBytes(SecKeySec.ToCharArray(), 0, SecKeySec.Length, byteSecKeySec, 0);
                encUNICODE.GetBytes(PlainText.ToCharArray(), 0, PlainText.Length, bytePlainText, 0);

                ICryptoTransform Encryptor = EncryptRijndaelManaged.CreateEncryptor(byteSecKeyIni, byteSecKeySec);

                //Encrypt the data.
                MemoryStream msEncrypt = new MemoryStream();
                CryptoStream csEncrypt = new CryptoStream(msEncrypt, Encryptor, CryptoStreamMode.Write);
                csEncrypt.Write(bytePlainText, 0, bytePlainText.Length);
                csEncrypt.FlushFinalBlock();

                byte[] byteEncryptText = msEncrypt.ToArray();
                msEncrypt.Close();
                csEncrypt.Close();

                String EncryptText = Convert.ToBase64String(byteEncryptText);

                return EncryptText.Trim();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public String RijndaelManagedDecryptUnicode(String EncryptText)
        {
            RijndaelManaged EncryptRijndaelManaged = new RijndaelManaged();

            try
            {
                UTF8Encoding encUNICODE = (UTF8Encoding)Encoding.UTF8;

                byte[] byteSecKeyIni = new byte[GetBytesUnicode(SecKeyIni)];
                byte[] byteSecKeySec = new byte[GetBytesUnicode(SecKeySec)];
                byte[] byteEncryptedText = Convert.FromBase64String(EncryptText);

                encUNICODE.GetBytes(SecKeyIni.ToCharArray(), 0, SecKeyIni.Length, byteSecKeyIni, 0);
                encUNICODE.GetBytes(SecKeySec.ToCharArray(), 0, SecKeySec.Length, byteSecKeySec, 0);

                ICryptoTransform Decryptor = EncryptRijndaelManaged.CreateDecryptor(byteSecKeyIni, byteSecKeySec);

                //Decrypt the data.
                MemoryStream msDecrypt = new MemoryStream(byteEncryptedText);
                CryptoStream csDecrypt = new CryptoStream(msDecrypt, Decryptor, CryptoStreamMode.Read);

                byte[] bytePlainText = new byte[byteEncryptedText.Length];
                Int32 readSize = csDecrypt.Read(bytePlainText, 0, bytePlainText.Length);

                String strDecryptData = encUNICODE.GetString(bytePlainText);

                msDecrypt.Close();
                csDecrypt.Close();

                strDecryptData = strDecryptData.Trim('\0');

                return strDecryptData;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion

        public static string GenericPassword( String Template )
		{
			String l = "qwertyuiopasdfghjklzxcvbnm";
			String n = "0123456789";
			String p = ",.?<>;:@/!\"%^&*()-+.=_";
			String L = "QWERTYUIOPASDFGHJKLZXCVBNM";
			String c = "qwrtypsdfghjklzxcvbnm";
			String C = "QWRTYPSDFGHJKLZXCVBNM";
			String v = "aeiou";
			String V = "AEIOU";
			String a = "qwertyuiopasdfghjkl.zxcvbnmQWERTYUIOPASD.FGHJKLZXCVBNM,.?<>;:.@/!\"%^&*()-+=_012345.6789";

			Int32 lenTempalte = Template.Length;
			String newPassword = String.Empty;

			Random randPassword = new Random();
			for ( Int32 Index = 0; Index < lenTempalte; Index++ ) 
			{
				if ( Template[Index] == 'l') { newPassword += l[randPassword.Next(0, l.Length)];}
				if ( Template[Index] == 'L') { newPassword += L[randPassword.Next(0, L.Length)];}
				if ( Template[Index] == 'c') { newPassword += c[randPassword.Next(0, c.Length)];}
				if ( Template[Index] == 'C') { newPassword += C[randPassword.Next(0, C.Length)];}
				if ( Template[Index] == 'v') { newPassword += v[randPassword.Next(0, v.Length)];}
				if ( Template[Index] == 'V') { newPassword += V[randPassword.Next(0, V.Length)];}
				if ( Template[Index] == 'n') { newPassword += n[randPassword.Next(0, n.Length)];}
				if ( Template[Index] == 'p') { newPassword += p[randPassword.Next(0, p.Length)];}
				if ( Template[Index] == 'a') { newPassword += a[randPassword.Next(0, a.Length)];}
				newPassword +=  Template[Index];
			}
			
			return newPassword;
		}

		public static string GenericRandomNumbers( Int32 CountNumbers )
		{
			String Numbers = "0123456789";
			String genNumbers = String.Empty;

			Random randNumbers = new Random();
			for ( Int32 Index = 0; Index < CountNumbers; Index++ ) 
				genNumbers += Numbers[randNumbers.Next(0, Numbers.Length)];
			
			return genNumbers;
		}
	}
}
