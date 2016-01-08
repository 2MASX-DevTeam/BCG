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
    public class SQLFillParameter : System.Object
    {
        public SQLFillParameter()
        {
        }

        public Boolean FillParameter(SqlParameter Parameter, String Value)
        {
            if (Parameter == null)
                throw new NullReferenceException();
#if ( DEBUG )
            if (Parameter.SqlDbType != SqlDbType.NVarChar && Parameter.SqlDbType != SqlDbType.VarChar && Parameter.SqlDbType != SqlDbType.Char && Parameter.SqlDbType != SqlDbType.NText && Parameter.SqlDbType != SqlDbType.Xml)
                throw new InvalidCastException("Fill Parameter String Type!");
#endif

            Parameter.Value = (Value == String.Empty) ? DBNull.Value : (Object)Value;

            return true;
        }

        public Boolean FillParameter(SqlParameter Parameter, Int32 Value)
        {
            if (Parameter == null)
                throw new NullReferenceException();
#if ( DEBUG )
            if (Parameter.SqlDbType != SqlDbType.Int && Parameter.SqlDbType != SqlDbType.SmallInt && Parameter.SqlDbType != SqlDbType.TinyInt)
                throw new InvalidCastException("Fill Parameter Int Type!");
#endif
            Parameter.Value = (Object)Value;

            return true;
        }

        public Boolean FillParameter(SqlParameter Parameter, DateTime Value)
        {
            if (Parameter == null)
                throw new NullReferenceException();
#if ( DEBUG )
            if (Parameter.SqlDbType != SqlDbType.DateTime)
                throw new InvalidCastException("Fill Parameter DateTime Type!");
#endif
            Parameter.Value = (Value <= DateTime.MinValue) ? DBNull.Value : (Object)Value;

            return true;
        }

        public Boolean FillParameter(SqlParameter Parameter, Boolean Value)
        {
            if (Parameter == null)
                throw new NullReferenceException();
#if ( DEBUG )
            if (Parameter.SqlDbType != SqlDbType.Bit)
                throw new InvalidCastException("Fill Parameter Boolean Type!");
#endif
            Parameter.Value = (Object)Value;

            return true;
        }

        public Boolean FillParameter(SqlParameter Parameter, Decimal Value)
        {
            if (Parameter == null)
                throw new NullReferenceException();
#if ( DEBUG )
            if (Parameter.SqlDbType != SqlDbType.Decimal && Parameter.SqlDbType != SqlDbType.Money)
                throw new InvalidCastException("Fill Parameter Decimal Type!");
#endif
            Parameter.Value = (Object)Value;

            return true;
        }

        public Boolean FillParameter(SqlParameter Parameter, Double Value)
        {
            if (Parameter == null)
                throw new NullReferenceException();
#if ( DEBUG )
            if (Parameter.SqlDbType != SqlDbType.Real)
                throw new InvalidCastException("Fill Parameter Real Type!");
#endif
            Parameter.Value = (Object)Value;

            return true;
        }

        public Boolean FillParameter(SqlParameter Parameter, Byte[] Value)
        {
            if (Parameter == null || Value == null)
                throw new NullReferenceException();
#if ( DEBUG )
            if (Parameter.SqlDbType != SqlDbType.Image && Parameter.SqlDbType != SqlDbType.Text && Parameter.SqlDbType != SqlDbType.VarBinary)
                throw new InvalidCastException("Fill Parameter Image Type!");
#endif
            Parameter.Value = (Object)Value;

            return true;
        }

        public Boolean FillParameter(SqlParameter Parameter, Guid Value)
        {
            if (Parameter == null || Value == Guid.Empty)
                throw new NullReferenceException();
#if ( DEBUG )
            if (Parameter.SqlDbType != SqlDbType.Image && Parameter.SqlDbType != SqlDbType.UniqueIdentifier)
                throw new InvalidCastException("Fill Parameter UniqueIdentifier Type!");
#endif
            Parameter.Value = (Object)Value;

            return true;
        }

        public Boolean FillParameter(SqlParameter Parameter, TextBox Control, Type CastType)
        {
            if (Parameter == null || Control == null)
                throw new NullReferenceException();

            if (Control.Text.Trim() != String.Empty)
                Parameter.Value = Convert.ChangeType(Control.Text.Trim(), CastType);
            else
                return FillParameter(Parameter);

            return true;
        }

        public Boolean FillParameter(SqlParameter Parameter, DropDownList Control, Type CastType, Object IgnoreValue)
        {
            if (Parameter == null || Control == null)
                throw new NullReferenceException();

            if (Control.SelectedValue != String.Empty &&
                !Convert.ChangeType(Control.SelectedValue, CastType).Equals(Convert.ChangeType(IgnoreValue, CastType)))
                Parameter.Value = Convert.ChangeType(Control.SelectedValue, CastType);
            else
                return FillParameter(Parameter);

            return true;
        }

        public Boolean FillParameter(SqlParameter Parameter)
        {
            Parameter.Value = DBNull.Value;
            return true;
        }
    }
}
