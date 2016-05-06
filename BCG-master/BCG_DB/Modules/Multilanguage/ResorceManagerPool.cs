namespace MultilanguageTools.Modules.Multilanguage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Configuration;
    using System.Threading;
    using System.Data;
    using System.Web.SessionState;
    using System.Collections;
    using SQLPreferences;

    public class ResorceManagerPool
    {
        #region Members Variables
        private DataSet dsPhrases;
        private Mutex SharedResorceManagerPool = null;
        private static System.Timers.Timer aTimer;
        private Thread thDeleteByLivePhrase;
        protected static ResorceManagerPool _ResorceManagerPool = null;
        #endregion

        #region Properties

        public DataSet ListPhrases
        {
            get { return dsPhrases; }
        }

        #endregion

        public ResorceManagerPool()
        {
            SharedResorceManagerPool = new Mutex(false);

            aTimer = new System.Timers.Timer(60000);
            aTimer.Elapsed += new System.Timers.ElapsedEventHandler(aTimer_Elapsed);

            dsPhrases = new DataSet();
            DataTable tblPhrases = new DataTable("tblPhrases");

            tblPhrases.Columns.Add("IdResource", typeof(Int32));
            tblPhrases.Columns.Add("IdLanguage", typeof(int));

            tblPhrases.Columns.Add("Description", typeof(String));
            tblPhrases.Columns.Add("DateTimeLive", typeof(DateTime));

            tblPhrases.PrimaryKey = new DataColumn[] { tblPhrases.Columns["IdResource"], tblPhrases.Columns["IdLanguage"] };
            dsPhrases.Tables.Add(tblPhrases);

            _ResorceManagerPool = this;
        }

        public Boolean StartThread()
        {
            thDeleteByLivePhrase = new Thread(new ThreadStart(ThreadDeleteByLivePhrase));
            thDeleteByLivePhrase.Start();

            return true;
        }

        public void StopThread()
        {
            try
            {
                aTimer.Enabled = false;
                GC.SuppressFinalize(aTimer);
                thDeleteByLivePhrase.Abort();
            }
            catch (ArgumentNullException Ex)
            {
                Global.ErrorMessage(Ex, System.Web.HttpContext.Current);
                return;
            }
            catch (ThreadAbortException Ex)
            {
                Global.ErrorMessage(Ex, System.Web.HttpContext.Current);
                return;
            }
        }

        public Boolean SelectPhrase(Int32 iIdResource, int IdLanguage, ref String strPhrase)
        {
            SharedResorceManagerPool.WaitOne();

            try
            {
                DataRow[] RowPhrases = dsPhrases.Tables["tblPhrases"].Select(String.Format("IdResource = {0} AND IdLanguage = {1}", iIdResource, IdLanguage));
                if (RowPhrases.Length > 0)
                {
                    strPhrase = RowPhrases[0]["Description"].ToString();
                    RowPhrases[0]["DateTimeLive"] = DateTime.UtcNow.ToLocalTime();
                }
                else
                {
                    if (GetPhrase(iIdResource, false, ref strPhrase))
                    {
                        DataRow RowPhrase = dsPhrases.Tables["tblPhrases"].NewRow();

                        RowPhrase["IdResource"] = iIdResource;
                        RowPhrase["IdLanguage"] = IdLanguage;
                        RowPhrase["Description"] = strPhrase;
                        RowPhrase["DateTimeLive"] = DateTime.UtcNow.ToLocalTime();

                        dsPhrases.Tables["tblPhrases"].Rows.Add(RowPhrase);
                    }
                    else
                        return false;
                }
            }
            catch (Exception Ex)
            {
                Global.ErrorMessage(Ex, System.Web.HttpContext.Current);
                return false;
            }
            finally
            {
                SharedResorceManagerPool.ReleaseMutex();
            }

            return true;
        }

        public static void ThreadDeleteByLivePhrase()
        {
            aTimer.Enabled = true;
            GC.KeepAlive(aTimer);
        }

        protected Boolean DeleteByLivePhrase()
        {
            SharedResorceManagerPool.WaitOne();

            try
            {
                SQLDatabase DBConnection = new SQLDatabase(ConfigurationManager.ConnectionStrings["dbConnectionString"].ToString());
                if (DBConnection == null)
                    return false;

                DBConnection.Connect();

                ResorceManager rsMenager = new ResorceManager(DBConnection);
                if (rsMenager == null)
                    return false;

                DataSet dsLanguages = null;
                rsMenager.ListAllLanguages(out dsLanguages);
                DBConnection.Disconnect();

                DateTime IsLiveTime = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(2)).ToLocalTime();

                Int32 IdLanguage = 0;
                DataRow[] RowPhrases;
                foreach (DataRow row in dsLanguages.Tables["Languages"].Rows)
                {
                    IdLanguage = Convert.ToInt32(row["IdLanguage"]);

                    RowPhrases = dsPhrases.Tables["tblPhrases"].Select(String.Format("IdLanguage = {0} AND DateTimeLive <= #{1:s}#", IdLanguage, IsLiveTime));
                    foreach (DataRow RowPhrase in RowPhrases)
                        RowPhrase.Delete();

                    dsPhrases.Tables["tblPhrases"].AcceptChanges();
                }

            }
            catch (Exception Ex)
            {
                Global.ErrorMessage(Ex, System.Web.HttpContext.Current);
                return false;
            }
            finally
            {
                SharedResorceManagerPool.ReleaseMutex();
            }

            return true;
        }

        public Boolean SelectStaticPhrase(Int32 iIdResource, ref String strPhrase)
        {
            return GetPhrase(iIdResource, true, ref strPhrase);
        }

        public Boolean GetPhrase(Int32 i32IdResource, Boolean blnIsStatic, ref String strRetVal)
        {
            try
            {
                SQLDatabase DBConnection = new SQLDatabase(ConfigurationManager.ConnectionStrings["dbConnectionString"].ToString());
                if (DBConnection == null)
                    return false;

                DBConnection.Connect();

                ResorceManager Function = new ResorceManager(DBConnection, System.Web.HttpContext.Current.Session);
                if (Function == null)
                    return false; ;

                if (blnIsStatic)
                    Function.SelectStaticPhrase(i32IdResource, ref strRetVal);
                else
                    Function.SelectPhrase(i32IdResource, ref strRetVal);

                DBConnection.Disconnect();

                if (strRetVal == String.Empty)
                {
                    SessionLanguage Language = (SessionLanguage)System.Web.HttpContext.Current.Session["SessionLanguage"];

                    String strError = String.Format("Missing data for phrase witch IdResource = {0} and IdLanguage = {1}!", i32IdResource, Language.GetLanguage());
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

        public String GetPhrase(Int32 i32IdResource, Boolean blnIsStatic, ref Boolean blnRetVal)
        {
            String strRetVal = String.Empty;

            try
            {
                SQLDatabase DBConnection = new SQLDatabase(ConfigurationManager.ConnectionStrings["dbConnectionString"].ToString());
                if (DBConnection == null)
                {
                    blnRetVal = false;
                    return String.Empty;
                }

                DBConnection.Connect();

                ResorceManager Function = new ResorceManager(DBConnection, System.Web.HttpContext.Current.Session);
                if (Function == null)
                {
                    blnRetVal = false;
                    return String.Empty;
                }

                if (blnIsStatic)
                    Function.SelectStaticPhrase(i32IdResource, ref strRetVal);
                else
                    Function.SelectPhrase(i32IdResource, ref strRetVal);

                DBConnection.Disconnect();

                if (strRetVal == String.Empty)
                {
                    SessionLanguage Language = (SessionLanguage)System.Web.HttpContext.Current.Session["SessionLanguage"];

                    String strError = String.Format("Missing data for phrase witch IdResource = {0} and IdLanguage = {1}!", i32IdResource, Language.GetLanguage());
                    blnRetVal = false;

                    Global.ErrorMessage(strError, System.Web.HttpContext.Current);
                    return "No data for this phrase!";
                }
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

        public Boolean DeletePhraseByResource(Int32 IdResource)
        {
            SharedResorceManagerPool.WaitOne();
            try
            {
                DataRow[] Rows = dsPhrases.Tables["tblPhrases"].Select(String.Format("IdResource = {0}", IdResource));

                if (Rows.Length > 0)
                {
                    foreach (DataRow Row in Rows)
                        Row.Delete();

                    dsPhrases.AcceptChanges();
                }
                return true;
            }
            catch (Exception Ex)
            {
                Global.ErrorMessage(Ex, System.Web.HttpContext.Current);
                return false;
            }
            finally
            {
                SharedResorceManagerPool.ReleaseMutex();
            }
        }

        private void aTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _ResorceManagerPool.DeleteByLivePhrase();
        }
    }
}
