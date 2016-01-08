namespace MultilanguageTools.SQLPreferences
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class DBFunction : System.Object, System.ICloneable, System.IComparable
    {
        protected String _LastError;
        protected SQLDatabase _Database;

        public DBFunction()
        {
            _Database = null;
        }

        public DBFunction(SQLDatabase conDatabase)
        {
            Database = conDatabase;
        }

        virtual public Object Clone()
        {
            return this;
        }

        virtual public int CompareTo(Object ObjCompare)
        {
            if (ObjCompare is DBFunction)
            {
                DBFunction tempObj = (DBFunction)ObjCompare;
                return 0;
            }

            throw new ArgumentException("Object is not a DBFunction");
        }

        public SQLDatabase Database
        {
            get { return _Database; }
            set
            {
                if (value == null)
                    new NullReferenceException("Database object isn't valid");

                if (value.Connection == null || value.Connection.State != ConnectionState.Open)
                    value.Connect();

                _Database = value;
            }
        }

        public String LastError
        {
            get { return _LastError; }
        }
    }
}
