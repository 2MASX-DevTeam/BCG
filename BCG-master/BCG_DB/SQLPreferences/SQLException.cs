namespace MultilanguageTools.SQLPreferences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data;
    using System.Configuration;
    using System.Data.SqlClient;

    public class SQLException : System.Exception
    {
        #region "Members Fields"
        private SqlException m_sqlException;
        #endregion

        public SQLException()
        {
            m_sqlException = null;
        }

        public SQLException(String Message)
            :
            base(Message)
        {
            m_sqlException = null;
        }

        public SQLException(SqlException sqlException)
        {
            m_sqlException = sqlException;
        }

        public SQLException(String Message, Exception InnerException)
            :
            base(Message, InnerException)
        {
            m_sqlException = null;
        }

        private String GenericMessageException()
        {
            String strMessage = "";
            for (int nCount = 0; nCount < m_sqlException.Errors.Count; nCount++)
            {
                strMessage += "Index #" + nCount + "\n" +
                    "Message: " + m_sqlException.Errors[nCount].Message + "\n" +
                    "LineNumber: " + m_sqlException.Errors[nCount].LineNumber + "\n" +
                    "Source: " + m_sqlException.Errors[nCount].Source + "\n" +
                    "Procedure: " + m_sqlException.Errors[nCount].Procedure + "\n";
            }

            return strMessage;
        }

        public override String Message
        {
            get
            {
                if (m_sqlException != null)
                    return GenericMessageException();

                return base.Message;
            }
        }

        public override string ToString()
        {
            // TODO:  Add SQLException.ToString implementation
            if (m_sqlException != null)
                return GenericMessageException();

            return Message;
        }

        public override Exception GetBaseException()
        {
            // TODO:  Add SQLException.GetBaseException implementation
            return base.GetBaseException();
        }
    }
}
