using System;
using System.IO;
using System.Security.Cryptography;

namespace Tools
{
    public class FileCheckSum
    {

        public FileCheckSum()
		{
			//
			// TODO: Add constructor logic here
			//
		}

        public String CalculateCheckSumName(String FileName)
        {
            try
            {
                using (FileStream Stream = File.OpenRead(FileName))
                {
                    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                    Byte[] CheckSum = md5.ComputeHash(Stream);
                    return BitConverter.ToString(CheckSum).Replace("-", String.Empty);
                }
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        public String CalculateCheckSumStream(Stream fileStream)
        {
            try
            {
               MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
               Byte[] CheckSum = md5.ComputeHash(fileStream);
               return BitConverter.ToString(CheckSum).Replace("-", String.Empty);
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        public Boolean CompareCheckSum(String FileName, String CheckSum )
        {
            return (CalculateCheckSumName(FileName) == CheckSum);
        }
    }
}
