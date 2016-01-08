namespace MultilanguageTools.SQLPreferences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Reflection;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class SQLFillControl : System.Object
    {
        public SQLFillControl()
        {
        }

        public Boolean FillTextControl(Control _Control, String Data)
        {
            try
            {
                Type typeControl = _Control.GetType();
                PropertyInfo propertyInfo = typeControl.GetProperty("Text", typeof(String));
                if (propertyInfo == null)
                    return false;

                if (Data != "<NULL>")
                    propertyInfo.SetValue(_Control, Data, null);
                else
                    propertyInfo.SetValue(_Control, String.Empty, null);

                return true;
            }
            catch (NullReferenceException Ex)
            {
                throw new SQLException("Null referance Exception in Fill String Data", Ex);
            }
        }

        public Boolean FillTextControl(Control _Control, Boolean Data)
        {
            try
            {
                Type typeControl = _Control.GetType();
                PropertyInfo propertyInfo = typeControl.GetProperty("Text", typeof(String));
                if (propertyInfo == null)
                    return false;

                propertyInfo.SetValue(_Control, (Data) ? "True" : "False", null);

                return true;
            }
            catch (NullReferenceException Ex)
            {
                throw new SQLException("Null referance Exception in Fill String Data", Ex);
            }
        }

        public Boolean FillTextControl(ref TextBox _Control, String Data)
        {
            try
            {
                _Control.Text = (Data == String.Empty) ? String.Empty : Data;
                return true;
            }
            catch (NullReferenceException Ex)
            {
                throw new SQLException("Null referance Exception in Fill String Data", Ex);
            }
        }

        public Boolean FillBoolControl(ref TextBox _Control, Boolean Data)
        {
            try
            {
                _Control.Text = (!Data) ? "False" : "True";
                return true;
            }
            catch (NullReferenceException Ex)
            {
                throw new SQLException("Null referance Exception in Fill Boolean Data", Ex);
            }
        }

        public Boolean FillDropDownList(DropDownList _Control, Object ItemSelect, Type typeObject)
        {
            if (_Control == null)
                return false;

            if (ItemSelect.Equals(DBNull.Value))
                return true;

            _Control.ClearSelection();
            if (typeObject == typeof(Int32) || typeObject == typeof(Byte))
                return SelectListItem(_Control.Items, Convert.ToInt32(ItemSelect));
            else if (typeObject == typeof(String))
                return SelectListItem(_Control.Items, (String)Convert.ChangeType(ItemSelect, typeObject));
            else
                throw new NotImplementedException(String.Format("NotImplemented function for type: {0}", typeObject.ToString()));
        }

        public Boolean FillDropDownList(DropDownList _Control, Byte ItemSelect)
        {
            if (_Control == null)
                return false;

            _Control.ClearSelection();
            return SelectListItem(_Control.Items, ItemSelect);
        }

        public Boolean FillDropDownList(DropDownList _Control, String ItemSelect)
        {
            if (_Control == null)
                return false;

            _Control.ClearSelection();
            return SelectListItem(_Control.Items, ItemSelect);
        }

        public Boolean FillDropDownList(DropDownList _Control, Int32 ItemSelect)
        {
            if (_Control == null)
                return false;

            _Control.ClearSelection();
            return SelectListItem(_Control.Items, ItemSelect);
        }

        public Boolean SelectListItem(ListItemCollection ListCollection, String ItemSelect)
        {
            if (ListCollection == null)
                return false;

            ListItem Item = ListCollection.FindByText(ItemSelect);
            if (Item != null)
            {
                Item.Selected = true;
                return true;
            }

            return false;
        }

        public Boolean SelectListItem(ListItemCollection ListCollection, Int32 ItemSelect)
        {
            if (ListCollection == null)
                return false;

            ListItem Item = ListCollection.FindByValue(ItemSelect.ToString());
            if (Item != null)
            {
                Item.Selected = true;
                return true;
            }

            return false;
        }
    }
}
