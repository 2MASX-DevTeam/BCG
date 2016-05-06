namespace MultilanguageTools.SQLPreferences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Collections;

    public class SQLParameters : System.Collections.CollectionBase
    {
        private const String returnValue = "@RETURN_VALUE";
        public SQLParameters()
        {
            List.Clear();
            SqlParameter ReturnParam = new SqlParameter(returnValue, SqlDbType.Int);
            ReturnParam.Direction = ParameterDirection.ReturnValue;
            ReturnParam.Value = DBNull.Value;
            List.Add(ReturnParam);
        }

        public SQLParameters(SqlDbType TypeReturnValue)
        {
            List.Clear();
            SqlParameter ReturnParam = new SqlParameter(returnValue, TypeReturnValue);
            ReturnParam.Direction = ParameterDirection.ReturnValue;
            ReturnParam.Value = DBNull.Value;
            List.Add(ReturnParam);
        }

        public SqlParameter this[String name]
        {
            get
            {
                if (name == String.Empty)
                    throw new ArgumentException();

                SqlParameter RetunParameter = FindByName(name);
                if (RetunParameter == null)
                    return null;

                return RetunParameter;
            }
        }

        public SqlParameter this[Int32 Index]
        {
            get
            {
                SqlParameter RetunParameter = (SqlParameter)List[Index];
                if (RetunParameter == null)
                    throw new NullReferenceException();

                return RetunParameter;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();

                List[Index] = value;
            }
        }

        public int Add(SqlParameter value)
        {
            return (List.Add(value));
        }

        public int IndexOf(SqlParameter value)
        {
            return (List.IndexOf(value));
        }

        public void Insert(int index, SqlParameter value)
        {
            List.Insert(index, value);
        }

        public void Remove(SqlParameter value)
        {
            List.Remove(value);
        }

        public SqlParameter FindByName(String ParameterName)
        {
            IEnumerator Enumerator = null;

            try
            {
                Enumerator = List.GetEnumerator();
                while (Enumerator.MoveNext())
                {
                    SqlParameter Parameter = (SqlParameter)Enumerator.Current;
                    if (Parameter.ParameterName == ParameterName)
                        return Parameter;
                }

                return null;
            }
            catch (InvalidOperationException Ex)
            {
                throw new SQLException("", Ex);
            }
            catch (NullReferenceException Ex)
            {
                throw new SQLException("", Ex);
            }
            finally
            {
                Enumerator.Reset();
            }
        }

        protected Boolean FillParameter(String Name, Object Value)
        {
            if (Name == String.Empty || Convert.IsDBNull(Value))
                return false;

            SqlParameter ValidParameter = FindByName(Name);
            if (ValidParameter == null)
                return false;

            /*			if (  Value is ValidParameter.DbType )
                            throw new System.Exception("Bind DbType isn't equal in parameter !!!");
            */
            ValidParameter.Value = Value;

            return true;
        }

        public Boolean FillParametersIn(SqlCommand Command)
        {
            if (Command == null)
                throw new SQLException("Null Referance Exception", new NullReferenceException());

            SqlCommandBuilder.DeriveParameters(Command);

            SqlParameterCollection ParameterCollection = Command.Parameters;
            if (ParameterCollection == null)
                throw new SQLException("Null Referance Exception", new NullReferenceException());

            try
            {
                for (int Index = 0; Index < ParameterCollection.Count; Index++)
                {
                    SqlParameter Parameter = ParameterCollection[Index];
                    SqlParameter ValidParameter = FindByName(Parameter.ParameterName);

                    if (ValidParameter == null)
                    {
                        Parameter.Value = DBNull.Value;
                        continue;
                    }

                    if (Parameter.SqlDbType.GetType() != ValidParameter.SqlDbType.GetType())
                        throw new System.Exception("SqlDbType isn't equal in parameters !!!");

                    ParameterCollection[ParameterCollection.IndexOf(Parameter)] = ValidParameter;
                }
            }
            catch (NullReferenceException Ex)
            {
                throw new SQLException("Null Referance Exception", Ex);
            }

            return true;
        }

        public Object ReturnValue()
        {
            SqlParameter RetunParameter = FindByName(returnValue);
            if (RetunParameter == null)
                return null;

            return RetunParameter.Value;
        }

        public bool Contains(SqlParameter value)
        {
            // If value is not of type SqlParameter, this will return false.
            return (List.Contains(value));
        }

        protected override void OnInsert(int index, Object value)
        {
            // Insert additional code to be run only when inserting values.
        }

        protected override void OnRemove(int index, Object value)
        {
            // Insert additional code to be run only when removing values.
        }

        protected override void OnSet(int index, Object oldValue, Object newValue)
        {
            // Insert additional code to be run only when setting values.
        }

        protected override void OnValidate(Object value)
        {
            if (Type.ReferenceEquals(value, new SqlParameter()))
                throw new ArgumentException("value must be of type SqlParameter.", "value");
        }
    }
}
