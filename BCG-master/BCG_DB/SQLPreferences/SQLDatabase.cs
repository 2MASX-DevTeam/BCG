namespace MultilanguageTools.SQLPreferences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data;
    using System.Data.SqlClient;

    public class SQLDatabase : System.Object, System.ICloneable, System.IComparable
    {
        #region "Members Fields"

        protected string m_strConnectionString; // Connection String of object. Used for Server Connect
        protected SqlConnection m_SqlConnection;
        protected string m_strLastStringError;

        #endregion

        public SQLDatabase()
        {
            m_strConnectionString = "";
        }

        public SQLDatabase(String strConnectionString)
        {
            m_strConnectionString = strConnectionString;
        }

        public Object Clone()
        {
            return this;
        }

        public int CompareTo(Object ObjCompare)
        {
            if (ObjCompare is SQLDatabase)
            {
                SQLDatabase tempObj = (SQLDatabase)ObjCompare;
                return (m_SqlConnection.GetType() == tempObj.Connection.GetType()) ? 0 : 1;
            }

            throw new ArgumentException("Object is not a SQLDatabase");
        }

        public Boolean Connect()
        {
            if (m_SqlConnection == null)
                m_SqlConnection = new SqlConnection();

            try
            {
                m_SqlConnection.ConnectionString = m_strConnectionString;
                m_SqlConnection.Open();

                if (m_SqlConnection.State != ConnectionState.Open)
                    return false;

                return true;
            }
            catch (SqlException Ex)
            {
                throw new SQLException(Ex);
            }

        }

        public void Disconnect()
        {
            if (m_SqlConnection.State == ConnectionState.Open)
                m_SqlConnection.Close();
        }

        public SqlConnection Connection
        {
            get { return m_SqlConnection; }
        }

        public String LastErrorMessage
        {
            get { return m_strLastStringError; }
        }

        public String StringConnection
        {
            get { return m_strConnectionString; }
            set { m_strConnectionString = value; }
        }
    }
}
