namespace MultilanguageTools.Modules
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Web;
    using System.Web.SessionState;
    using System.Security.Principal;
    using System.Web.Security;
    using System.Net.Mail;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Data;
    using System.Text;
    using System.Threading;
    using MultilanguageTools.SQLPreferences;
    using System.Net.Mime;
    using Multilanguage;
    public class Global : System.Web.HttpApplication
    {
        #region Member variables

        private System.ComponentModel.IContainer components = null;
        public static ResorceManagerPool _ResorceManagerPool = null;

        public static readonly Int16 PageSize = InitPageSize();
        private const Int16 DefaultPageSize = 12;

        public static Int16 PaginationSize = InitPaginationSize();
        private const Int16 DefaultPaginationSize = 3;

        #endregion

        public Global()
        {
            InitializeComponent();
        }

        private static Int16 InitPageSize()
        {
            try
            {
                String _PageSize = ConfigurationManager.AppSettings["PageSize"];
                if (_PageSize != String.Empty)
                    return Convert.ToInt16(ConfigurationManager.AppSettings["PageSize"]);
                else
                    return DefaultPageSize;
            }
            catch (Exception)
            {
                return DefaultPageSize;
            }
        }

        private static Int16 InitPaginationSize()
        {
            try
            {
                String _PaginationSize = ConfigurationManager.AppSettings["PaginationSize"];
                if (_PaginationSize != String.Empty)
                    return Convert.ToInt16(_PaginationSize);
                else
                    return DefaultPaginationSize;
            }
            catch (Exception)
            {
                return DefaultPaginationSize;
            }
        }

        protected void Application_Start(Object sender, EventArgs e)
        {
            _ResorceManagerPool = new ResorceManagerPool();
            _ResorceManagerPool.StartThread();
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            Session["SessionLanguage"] = new MultiLanguageSession(Request);
            Session["LoginUserName"] = String.Empty;
            Session["culture"] = String.Empty;
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {

        }

        protected void Application_EndRequest(Object sender, EventArgs e)
        {

        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            string strBody;
            strBody = "Error:  ";
            strBody += Server.GetLastError().InnerException.ToString() + "\n\n";
            strBody += Request.Url.ToString() + "\n\n";
            strBody += Request.RawUrl + "\n\n";
            strBody += Convert.ToString(Context.User.Identity.IsAuthenticated) + "\n\n";
            strBody += Context.User.Identity.Name;

            ErrorMessage(strBody, Context);
        }

        protected void Session_End(Object sender, EventArgs e)
        {

        }

        protected void Application_End(Object sender, EventArgs e)
        {
            _ResorceManagerPool.StopThread();
        }

        protected String SelectUserCulture(Int32 IdLanguage)
        {
            String UserCulture = String.Empty;
            try
            {
                SQLDatabase DBConnection = new SQLDatabase(ConfigurationManager.ConnectionStrings["dbConnectionString"].ToString());
                DBConnection.Connect();

                ResorceManager ResManager = new ResorceManager(DBConnection);
                DataSet dsLanguage = null;
                ResManager.SelectLanguage(IdLanguage, out dsLanguage);

                UserCulture = dsLanguage.Tables["tblLanguages"].Rows[0]["Culture"].ToString();
                DBConnection.Disconnect();
            }
            catch (Exception Ex)
            {
                Global.ErrorMessage(Ex, Context);
                return "en-US";
            }

            return UserCulture;
        }

        static public void SendMail(String From, String To, String ToDesc, String Subject, String Body, HttpContext contextObject)
        {
#if (!DEBUG)
            try
            {
                MailMessage _MailMessage = new MailMessage();
                SmtpClient _SmtpClient = new SmtpClient(ConfigurationManager.AppSettings["MailHost"]);

                _MailMessage.Sender = new MailAddress(From);

                _MailMessage.Subject = Subject;

                _MailMessage.Body = Body;

                _MailMessage.BodyEncoding = Encoding.UTF8;

                _MailMessage.To.Add(To);

                _MailMessage.From = new MailAddress(From);

                _MailMessage.IsBodyHtml = true;

                _SmtpClient.Send(_MailMessage);
            }
            catch (System.Runtime.InteropServices.COMException Ex)
            {
                if (contextObject != null)
                {
                    contextObject.Trace.Warn(Body + Ex.Message);
                }
            }
            catch (Exception Ex)
            {
                if (contextObject != null)
                {
                    contextObject.Trace.Warn(Body + Ex.Message);
                }
            }
#endif
        }

        static public void SendMail(String From, String To, String ToDesc, String Subject, String Body, String strContent, HttpContext contextObject)
        {
            try
            {

                MailMessage _MailMessage = new MailMessage();
                SmtpClient _SmtpClient = new SmtpClient(ConfigurationManager.AppSettings["MailHost"]);

                _MailMessage.Sender = new MailAddress(From);
                _MailMessage.Subject = Subject;

                _MailMessage.To.Add(To);
                _MailMessage.From = new MailAddress(From);

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(Body, Encoding.UTF8, MediaTypeNames.Text.Html);
                AlternateView TextsView = AlternateView.CreateAlternateViewFromString(strContent, Encoding.UTF8, MediaTypeNames.Text.Plain);

                _MailMessage.AlternateViews.Add(TextsView);
                _MailMessage.AlternateViews.Add(htmlView);

                _MailMessage.IsBodyHtml = true;
                _SmtpClient.Send(_MailMessage);
            }
            catch (System.Runtime.InteropServices.COMException Ex)
            {
                if (contextObject != null)
                {
                    contextObject.Trace.Warn(Body + Ex.Message);
                }
            }
            catch (Exception Ex)
            {
                if (contextObject != null)
                {
                    contextObject.Trace.Warn(Body + Ex.Message);
                }
            }

        }

        static public void ErrorMessage(string strError, HttpContext contextObject)
        {
            SendMail(ConfigurationManager.AppSettings["EmailWebApplication"], ConfigurationManager.AppSettings["EmailWarningBreak"], String.Empty, "BISOFT Application Error", strError, contextObject);
        }

        static public void ErrorMessage(Exception Ex, HttpContext contextObject)
        {
            String Exception = String.Format("<B><U>Exception</U></B><BR/><BR/><B>Type: </B>{0}<BR/><BR/><B>Message: </B>{1}<BR/><BR/><B>Stack trace: </B>{2}<BR/><BR/><B>Target site: </B>{3}<BR/><BR/><B>Environment user: </B>{4}", Ex.GetType(), Ex.Message, Ex.StackTrace, Ex.TargetSite, Environment.UserName);

            while (Ex.InnerException != null)
            {
                String InnerException = String.Format("<BR/><BR/><BR/><BR/><B><U>Inner exception</U></B><BR/><BR/><B>Type: </B>{0}<BR/><BR/><B>Message: </B>{1}<BR/><BR/><B>Stack trace: </B>{2}<BR/><BR/><B>Target site: </B>{3}", Ex.InnerException.GetType(), Ex.InnerException.Message, Ex.InnerException.StackTrace, Ex.InnerException.TargetSite);
                Exception += InnerException;
                Ex = Ex.InnerException;
            }

            ErrorMessage(Exception, contextObject);
        }

        #region Web Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
        }
        #endregion
    }
}
