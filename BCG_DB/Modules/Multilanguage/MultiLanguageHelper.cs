
namespace MultilanguageTools.Modules.Multilanguage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using SQLPreferences;
    using System.Configuration;

    public class MultiLanguageHelper
    {
        #region Constructors

        private MultiLanguageHelper()
        {
            return;
        }

        #endregion

        #region Static Methods

        public static Boolean GetPhrase(Int32 i32IdResource, Boolean blnIsStatic, ref String strRetVal)
        {
            try
            {
                SessionLanguage Language = (SessionLanguage)System.Web.HttpContext.Current.Session["SessionLanguage"];

                if (blnIsStatic)
                    Global._ResorceManagerPool.SelectStaticPhrase(i32IdResource, ref strRetVal);
                else
                    Global._ResorceManagerPool.SelectPhrase(i32IdResource, Language.GetLanguage(), ref strRetVal);

            }
            catch (SQLException Ex)
            {
                Global.ErrorMessage(Ex, System.Web.HttpContext.Current);
                return false;
            }
            catch (NullReferenceException Ex)
            {
                Global.ErrorMessage(Ex, System.Web.HttpContext.Current);
                return false;
            }

            return true;
        }

        public static String GetPhrase(Int32 i32IdResource, Boolean blnIsStatic, ref Boolean blnRetVal)
        {
            String strRetVal = String.Empty;

            try
            {
                SessionLanguage Language = (SessionLanguage)System.Web.HttpContext.Current.Session["SessionLanguage"];

                if (blnIsStatic)
                    Global._ResorceManagerPool.SelectStaticPhrase(i32IdResource, ref strRetVal);
                else
                    Global._ResorceManagerPool.SelectPhrase(i32IdResource, Language.GetLanguage(), ref strRetVal);

            }
            catch (SQLException Ex)
            {
                Global.ErrorMessage(Ex, System.Web.HttpContext.Current);
                blnRetVal = false;
                return Ex.Message;
            }
            catch (NullReferenceException Ex)
            {
                Global.ErrorMessage(Ex, System.Web.HttpContext.Current);
                blnRetVal = false;
                return Ex.Message;
            }

            return strRetVal;
        }

        public static Boolean GetPhrase(Int32 i32IdResource, Byte IdLanguage, Boolean blnIsStatic, ref String strRetVal)
        {
            try
            {
                SQLDatabase DBConnection = new SQLDatabase(ConfigurationManager.ConnectionStrings["dbConnectionString"].ToString());
                if (DBConnection == null)
                    return false;

                DBConnection.Connect();

                ResorceManager Function = new ResorceManager(DBConnection);
                if (Function == null)
                    return false; ;

                if (blnIsStatic)
                    Function.SelectStaticPhraseByLanguage(i32IdResource, IdLanguage, ref strRetVal);
                else
                    Function.SelectPhraseByLanguage(i32IdResource, IdLanguage, ref strRetVal);

                DBConnection.Disconnect();

                if (strRetVal == String.Empty)
                {
                    String strError = String.Format("Missing data for phrase witch IdResource = {0} and IdLanguage = {1}!", i32IdResource, IdLanguage);
                    strRetVal = "No data for this phrase!";

                    Global.ErrorMessage(strError, System.Web.HttpContext.Current);
                    return false;
                }
            }
            catch (SQLException Ex)
            {
                Global.ErrorMessage(Ex, System.Web.HttpContext.Current);
                return false;
            }
            catch (NullReferenceException Ex)
            {
                Global.ErrorMessage(Ex, System.Web.HttpContext.Current);
                return false;
            }
            return true;
        }

        #endregion
    }
}
