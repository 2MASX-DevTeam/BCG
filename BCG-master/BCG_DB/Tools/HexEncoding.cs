using System;
using System.Text;

namespace Tools
{
	/// <summary>
	/// Summary description for HexEncoding.
	/// </summary>
	public class HexEncoding
	{
		public HexEncoding()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public static Int32 GetByteCount(String hexString)
		{
			Int32 numHexChars = 0;
			char c;
			// remove all none A-F, 0-9, characters
			for (Int32 i=0; i<hexString.Length; i++)
			{
				c = hexString[i];
				if (IsHexDigit(c))
					numHexChars++;
			}
			// if odd number of characters, discard last character
			if (numHexChars % 2 != 0)
			{
				numHexChars--;
			}
			return numHexChars / 2; // 2 characters per byte
		}
		/// <summary>
		/// Creates a byte array from the hexadecimal String. Each two characters are combined
		/// to create one byte. First two hexadecimal characters become first byte in returned array.
		/// Non-hexadecimal characters are ignored. 
		/// </summary>
		/// <param name="hexString">String to convert to byte array</param>
		/// <param name="discarded">number of characters in String ignored</param>
		/// <returns>byte array, in the same left-to-right order as the hexString</returns>
		public static byte[] GetBytes(String hexString, out Int32 discarded)
		{
			discarded = 0;
			String newString = String.Empty;
			char ch;
			// remove all none A-F, 0-9, characters
			for ( Int32 i=0; i < hexString.Length; i++ )
			{
				ch = hexString[i];
				if (IsHexDigit(ch))
					newString += ch;
				else
					discarded++;
			}
			// if odd number of characters, discard last character
			if ( newString.Length % 2 != 0 )
			{
				discarded++;
				newString = newString.Substring( 0, newString.Length-1 );
			}

			Int32 byteLength = newString.Length / 2;
			byte[] bytes = new byte[byteLength];
			String hex;
			Int32 j = 0;
			for (Int32 i=0; i<bytes.Length; i++)
			{
				hex = new String(new Char[] {newString[j], newString[j+1]});
				bytes[i] = HexToByte(hex);
				j = j+2;
			}
			return bytes;
		}
		public static String ToString(byte[] bytes)
		{
			String hexString = "";
			for (Int32 i=0; i<bytes.Length; i++)
			{
				hexString += bytes[i].ToString("X2");
			}
			return hexString;
		}
		/// <summary>
		/// Determines if given String is in proper hexadecimal String format
		/// </summary>
		/// <param name="hexString"></param>
		/// <returns></returns>
		public static Boolean InHexFormat(String hexString)
		{
			Boolean hexFormat = true;

			foreach (char digit in hexString)
			{
				if (!IsHexDigit(digit))
				{
					hexFormat = false;
					break;
				}
			}
			return hexFormat;
		}

		/// <summary>
		/// Returns true is c is a hexadecimal digit (A-F, a-f, 0-9)
		/// </summary>
		/// <param name="c">Character to test</param>
		/// <returns>true if hex digit, false if not</returns>
		public static Boolean IsHexDigit(Char c)
		{
			Int32 numChar;
			Int32 numA = Convert.ToInt32('A');
			Int32 num1 = Convert.ToInt32('0');
			c = Char.ToUpper(c);
			numChar = Convert.ToInt32(c);
			if (numChar >= numA && numChar < (numA + 6))
				return true;
			if (numChar >= num1 && numChar < (num1 + 10))
				return true;
			return false;
		}
		/// <summary>
		/// Converts 1 or 2 character String into equivalant byte value
		/// </summary>
		/// <param name="hex">1 or 2 character String</param>
		/// <returns>byte</returns>
		private static byte HexToByte(String hex)
		{
			if (hex.Length > 2 || hex.Length <= 0)
				throw new ArgumentException("hex must be 1 or 2 characters in length");
			byte newByte = byte.Parse(hex, System.Globalization.NumberStyles.HexNumber);
			return newByte;
		}


	}
}
