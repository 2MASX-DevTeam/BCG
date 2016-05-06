using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Security.Cryptography;
using System.Reflection;
using System.Reflection.Emit;



using System.Web;

namespace Tools
{
    [Serializable]
    public class URLSerializer : ISerializable
    {
        private ListDictionary hashData = new ListDictionary();
        private String checksum = String.Empty;

        public ListDictionary Data
        {
            get { return hashData; }
            set { hashData = value; }
        }

        public String Checksum
        {
            get { return checksum; }
            set { checksum = value; }
        }

        public void Add(object Key, object Value)
        {
            hashData[Key.ToString()] = Value.ToString();
        }

        public String SerializeData()
        {
            byte[] byteArray = null;

            try
            {
                MemoryStream memoryStream = new MemoryStream();
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(memoryStream, this.Data);

                this.Checksum = GetMD5Hash(memoryStream);
                memoryStream.Close();

                MemoryStream memoryStreamData = new MemoryStream();
                bin.Serialize(memoryStreamData, this);
                byteArray = memoryStreamData.ToArray();
                memoryStreamData.Close();

                return HexEncoding.ToString(byteArray);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public URLSerializer DeserializeData(String Parameters)
        {
            Int32 discarded = 0;
            byte[] arrayData = HexEncoding.GetBytes(Parameters, out discarded);

            try
            {
                MemoryStream ms = new MemoryStream(arrayData);
                BinaryFormatter bin = new BinaryFormatter();
                bin.Binder = new AllowAllAssemblyVersionsDeserializationBinder();

                object obj = bin.Deserialize(ms);

                URLSerializer oData = new URLSerializer();
                oData = (URLSerializer)obj;

                String strChecksum = oData.Checksum;

                MemoryStream msData = new MemoryStream();
                bin.Serialize(msData, oData.Data);

                String strChecksumChecked = GetMD5Hash(msData);
                msData.Close();
                ms.Close();
               
                if (strChecksum != strChecksumChecked)
                    return null;
                else
                    return oData;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected String GetMD5Hash(MemoryStream ms)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(ms);

            return ByteToHexStr(retVal);
        }

        protected String ByteToHexStr(byte[] _data)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < _data.Length; i++)
            {
                sb.Append(_data[i].ToString("X2"));
            }

            return sb.ToString();
        }

        public URLSerializer()
        {
        }

        protected URLSerializer(SerializationInfo info, StreamingContext context)
        {
            this.Data = (ListDictionary)info.GetValue("Data", this.Data.GetType());
            this.Checksum = info.GetValue("Checksum", this.Checksum.GetType()).ToString();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Data", this.Data);
            info.AddValue("Checksum", this.Checksum);
        }

        public String GenericNumber()
        {
            return string.Format("{0:x}", DateTime.Now.Ticks);
        }

        sealed class AllowAllAssemblyVersionsDeserializationBinder : SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName)
            {
                Type typeToDeserialize = null;
                String currentAssembly = Assembly.GetExecutingAssembly().FullName;
                // In this case we are always using the current assembly 
                assemblyName = currentAssembly;
                // Get the type using the typeName and assemblyName  
                typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));

                return typeToDeserialize;
            }
        }

    }
}
