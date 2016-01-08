using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;

namespace Tools
{
    [Serializable]
    class HTTPURLSerializer : ISerializable
    {
        #region Member Variable
        HttpContext _HttpContext = null;
        const String _SerializerDataCookie = "SerializerDataCookie";
        #endregion

        public HTTPURLSerializer(HttpContext _HttpContext)
        {
            this._HttpContext = _HttpContext;
        }

        protected HTTPURLSerializer(SerializationInfo info, StreamingContext context)
        {
           /*
            this.Data = (ListDictionary)info.GetValue("Data", this.Data.GetType());
            this.Checksum = info.GetValue("Checksum", this.Checksum.GetType()).ToString();
           */
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            /*
            info.AddValue("Data", this.Data);
            info.AddValue("Checksum", this.Checksum);
            */
        }

        public String Serialize()
        {
            /*
            String _SerializeData = base.SerializeData();
            if (_HttpContext != null)
            {
                _HttpContext.Response.Cookies[_SerializerDataCookie].Value = _SerializeData;
                _HttpContext.Response.Cookies[_SerializerDataCookie].Path = "/";
                _HttpContext.Response.Cookies[_SerializerDataCookie].Expires = DateTime.Now.AddMinutes(2);

                _HttpContext.Session[GenericNumber()] = _SerializeData;
            }

            return _SerializeData;
            */
            return String.Empty;
        }

        public URLSerializer Deserialize(String Parameters)
        {
            /*
            if (_HttpContext.Session[Parameters] != null || _HttpContext.Request.Cookies[_SerializerDataCookie] != null)
            {
                String sessionData = String.Empty;
                if (_HttpContext.Session[Parameters] != null)
                {
                    sessionData = _HttpContext.Session[Parameters].ToString();
                    _HttpContext.Session.Remove(Parameters);

                    _HttpContext.Response.Cookies[_SerializerDataCookie].Value = sessionData;
                    _HttpContext.Response.Cookies[_SerializerDataCookie].Path = "/";
                    _HttpContext.Response.Cookies[_SerializerDataCookie].Expires = DateTime.Now.AddMinutes(5);
                }
                else if (_HttpContext.Request.Cookies[_SerializerDataCookie] != null)
                    sessionData = _HttpContext.Request.Cookies[_SerializerDataCookie].Value;

                return URLSerializer.DeserializeData(sessionData);
            }
            */
            return null;

        }
    }
}
