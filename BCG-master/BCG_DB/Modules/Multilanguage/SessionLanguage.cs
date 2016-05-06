namespace MultilanguageTools.Modules.Multilanguage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Configuration;

    /// <summary>
    /// Summary description for SessionLanguage.
    /// </summary>
    /// 
    public class SessionLanguage
    {
        virtual public int GetLanguage()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["defaultLanguage"]);
        }
    }
}
