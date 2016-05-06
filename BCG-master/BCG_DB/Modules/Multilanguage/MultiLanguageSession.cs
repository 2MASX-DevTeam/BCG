namespace MultilanguageTools.Modules.Multilanguage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Configuration;
    /// <summary>
    /// Summary description for MultiLanguageSession.
    /// </summary>
    public class MultiLanguageSession : SessionLanguage
    {

        private Int32 IdLanguage = 0;

        public Int32 Language
        {
            set { IdLanguage = value; }
            get { return IdLanguage; }
        }

        public MultiLanguageSession()
        {
            Language = Convert.ToInt32(ConfigurationManager.AppSettings["defaultLanguage"]);
        }

        public MultiLanguageSession(HttpRequest Request)
        {
            if (Request.Cookies["Language"] != null && Request.Cookies["Language"].Value != String.Empty)
                Language = Convert.ToInt32(Request.Cookies["Language"].Value);
            else
                Language = Convert.ToInt32(ConfigurationManager.AppSettings["defaultLanguage"]);
        }

        override public int GetLanguage()
        {
            if (IdLanguage == 0)
                return Convert.ToInt32(ConfigurationManager.AppSettings["defaultLanguage"]);
            else
                return IdLanguage;
        }
    }
}
