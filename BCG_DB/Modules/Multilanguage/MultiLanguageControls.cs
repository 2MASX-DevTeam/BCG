namespace MultilanguageTools.Modules.Multilanguage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using MultilanguageTools.SQLPreferences;
    #region HtmlMultiLanguageButton
    public class HtmlMultiLanguageButton : HtmlButton, IMultiLanguage
    {
        #region Properties
        private int IdResource;

        public int Resource
        {
            get { return IdResource; }
            set { IdResource = value; }
        }

        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            if (IdResource != 0)
            {
                string Context = String.Empty;
                try
                {
                    SessionLanguage Language = (SessionLanguage)System.Web.HttpContext.Current.Session["SessionLanguage"];
                    Global._ResorceManagerPool.SelectPhrase(IdResource, Language.GetLanguage(), ref Context);
                }
                catch (SQLException ex)
                {
                    Context = ex.Message;
                }

                base.InnerText = Context;
            }

            base.Render(writer);
        }
    }
    #endregion

    #region MultiLanguageButton
    public class MultiLanguageButton : Button, IMultiLanguage
    {
        #region Properties
        private int IdResource;

        public int Resource
        {
            get { return IdResource; }
            set { IdResource = value; }
        }

        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            if (IdResource != 0)
            {
                string Context = String.Empty;
                try
                {
                    SessionLanguage Language = (SessionLanguage)System.Web.HttpContext.Current.Session["SessionLanguage"];
                    Global._ResorceManagerPool.SelectPhrase(IdResource, Language.GetLanguage(), ref Context);
                }
                catch (SQLException ex)
                {
                    Context = ex.Message;
                }

                base.Text = Context;
            }

            base.Render(writer);
        }

    }
    #endregion

    #region MultiLanguageImageButton
    public class MultiLanguageImageButton : ImageButton, IMultiLanguage
    {
        #region Properties
        private int IdResource;

        public int Resource
        {
            get { return IdResource; }
            set { IdResource = value; }
        }

        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            if (IdResource != 0)
            {
                string Context = String.Empty;
                try
                {
                    SessionLanguage Language = (SessionLanguage)System.Web.HttpContext.Current.Session["SessionLanguage"];
                    Global._ResorceManagerPool.SelectPhrase(IdResource, Language.GetLanguage(), ref Context);
                }
                catch (SQLException ex)
                {
                    Context = ex.Message;
                }

                base.AlternateText = Context;
            }

            base.Render(writer);
        }
    }
    #endregion

    #region MultiLanguageTextBox
    public class MultiLanguageTextBox : TextBox, IMultiLanguage
    {
        #region Properties
        private int IdResource;

        public int Resource
        {
            get { return IdResource; }
            set { IdResource = value; }
        }

        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            if (IdResource != 0)
            {
                string Context = String.Empty;
                try
                {
                    SessionLanguage Language = (SessionLanguage)System.Web.HttpContext.Current.Session["SessionLanguage"];
                    Global._ResorceManagerPool.SelectPhrase(IdResource, Language.GetLanguage(), ref Context);
                }
                catch (SQLException ex)
                {
                    Context = ex.Message;
                }

                base.Text = Context;
            }

            base.Render(writer);
        }
    }
    #endregion

    #region MultiLanguageLiteral
    public class MultiLanguageLiteral : Literal, IMultiLanguage
    {
        #region Properties

        private int IdResource;
        public int Resource
        {
            get { return IdResource; }
            set { IdResource = value; }
        }

        private bool _IsStatic;
        public bool IsStatic
        {
            get { return _IsStatic; }
            set { _IsStatic = value; }
        }

        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            if (IdResource != 0)
            {
                string Context = String.Empty;
                try
                {
                    SessionLanguage Language = (SessionLanguage)System.Web.HttpContext.Current.Session["SessionLanguage"];

                    if (!IsStatic)
                        Global._ResorceManagerPool.SelectPhrase(IdResource, Language.GetLanguage(), ref Context);
                    else
                        Global._ResorceManagerPool.SelectStaticPhrase(IdResource, ref Context);
                }
                catch (SQLException ex)
                {
                    Context = ex.Message;
                }

                base.Text = Context;
            }

            base.Render(writer);

        }
    }
    #endregion

    #region MultiLanguageCheckBox
    public class MultiLanguageCheckBox : CheckBox, IMultiLanguage
    {
        #region Properties
        private int IdResource;

        public int Resource
        {
            get { return IdResource; }
            set { IdResource = value; }
        }

        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            if (IdResource != 0)
            {
                string Context = String.Empty;
                try
                {
                    SessionLanguage Language = (SessionLanguage)System.Web.HttpContext.Current.Session["SessionLanguage"];
                    Global._ResorceManagerPool.SelectPhrase(IdResource, Language.GetLanguage(), ref Context);
                }
                catch (SQLException ex)
                {
                    Context = ex.Message;
                }

                base.Text = Context;
            }

            base.Render(writer);
        }
    }
    #endregion

    #region MultiLanguageRequiredFieldValidator
    public class MultiLanguageRequiredFieldValidator : RequiredFieldValidator, IMultiLanguage
    {
        #region Properties
        private int IdResource;

        public int Resource
        {
            get { return IdResource; }
            set { IdResource = value; }
        }

        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            if (IdResource != 0)
            {
                string Context = String.Empty;

                try
                {
                    SessionLanguage Language = (SessionLanguage)System.Web.HttpContext.Current.Session["SessionLanguage"];
                    Global._ResorceManagerPool.SelectPhrase(IdResource, Language.GetLanguage(), ref Context);
                }
                catch (SQLException ex)
                {
                    Context = ex.Message;
                }

                base.ErrorMessage = Context;
            }

            base.Render(writer);
        }
    }
    #endregion

    #region MultiLanguageRangeValidator
    public class MultiLanguageRangeValidator : RangeValidator, IMultiLanguage
    {
        #region Properties
        private int IdResource;

        public int Resource
        {
            get { return IdResource; }
            set { IdResource = value; }
        }

        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            if (IdResource != 0)
            {
                string Context = String.Empty;

                try
                {
                    SessionLanguage Language = (SessionLanguage)System.Web.HttpContext.Current.Session["SessionLanguage"];
                    Global._ResorceManagerPool.SelectPhrase(IdResource, Language.GetLanguage(), ref Context);
                }
                catch (SQLException ex)
                {
                    Context = ex.Message;
                }

                base.ErrorMessage = String.Format(Context, base.MaximumValue);
            }

            base.Render(writer);
        }
    }
    #endregion

    #region MultiLanguageRegularExpressionValidator
    public class MultiLanguageRegularExpressionValidator : RegularExpressionValidator, IMultiLanguage
    {
        #region Properties
        private int IdResource;
        private bool Line = false;

        public Boolean NewLine
        {
            get { return Line; }
            set { Line = value; }
        }

        public int Resource
        {
            get { return IdResource; }
            set { IdResource = value; }
        }

        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            if (IdResource != 0)
            {
                string Context = String.Empty;
                try
                {
                    SessionLanguage Language = (SessionLanguage)System.Web.HttpContext.Current.Session["SessionLanguage"];
                    Global._ResorceManagerPool.SelectPhrase(IdResource, Language.GetLanguage(), ref Context);
                }
                catch (SQLException ex)
                {
                    Context = ex.Message;
                }

                base.ErrorMessage = Context;
            }

            base.Render(writer);
        }
    }
    #endregion

    #region MultiLanguageCompareValidator
    public class MultiLanguageCompareValidator : CompareValidator, IMultiLanguage
    {
        #region Properties
        private int IdResource;
        private bool Line = false;

        public Boolean NewLine
        {
            get { return Line; }
            set { Line = value; }
        }

        public int Resource
        {
            get { return IdResource; }
            set { IdResource = value; }
        }

        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            if (IdResource != 0)
            {
                string Context = String.Empty;
                try
                {
                    SessionLanguage Language = (SessionLanguage)System.Web.HttpContext.Current.Session["SessionLanguage"];
                    Global._ResorceManagerPool.SelectPhrase(IdResource, Language.GetLanguage(), ref Context);
                }
                catch (SQLException ex)
                {
                    Context = ex.Message;
                }

                base.ErrorMessage = Context;
            }

            base.Render(writer);
        }
    }
    #endregion

    #region MultiLanguageCustomValidator
    public class MultiLanguageCustomValidator : CustomValidator, IMultiLanguage
    {
        #region Properties
        private int IdResource;
        private bool Line = false;

        public Boolean NewLine
        {
            get { return Line; }
            set { Line = value; }
        }

        public int Resource
        {
            get { return IdResource; }
            set { IdResource = value; }
        }

        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            if (IdResource != 0)
            {
                string Context = String.Empty;
                try
                {
                    SessionLanguage Language = (SessionLanguage)System.Web.HttpContext.Current.Session["SessionLanguage"];
                    Global._ResorceManagerPool.SelectPhrase(IdResource, Language.GetLanguage(), ref Context);
                }
                catch (SQLException ex)
                {
                    Context = ex.Message;
                }

                base.ErrorMessage = Context;
            }

            base.Render(writer);
        }
    }
    #endregion

    #region MultiLanguageLabel
    public class MultiLanguageLabel : Label, IMultiLanguage
    {
        #region Properties
        private int IdResource;

        public int Resource
        {
            get { return IdResource; }
            set { IdResource = value; }
        }

        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            if (IdResource != 0)
            {
                string Context = String.Empty;

                try
                {
                    SessionLanguage Language = (SessionLanguage)System.Web.HttpContext.Current.Session["SessionLanguage"];
                    Global._ResorceManagerPool.SelectPhrase(IdResource, Language.GetLanguage(), ref Context);
                }
                catch (SQLException Ex)
                {
                    Context = Ex.Message;
                }

                base.Text = Context;
            }

            base.Render(writer);
        }
    }
    #endregion

    #region MultiLanguageLinkButton
    public class MultiLanguageLinkButton : LinkButton, IMultiLanguage
    {
        #region Properties
        private int IdResource;

        public int Resource
        {
            get { return IdResource; }
            set { IdResource = value; }
        }

        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            if (IdResource != 0)
            {
                string Context = String.Empty;

                try
                {
                    SessionLanguage Language = (SessionLanguage)System.Web.HttpContext.Current.Session["SessionLanguage"];
                    Global._ResorceManagerPool.SelectPhrase(IdResource, Language.GetLanguage(), ref Context);
                }
                catch (SQLException ex)
                {
                    Context = ex.Message;
                }

                base.Text = Context;
            }

            base.Render(writer);
        }
    }
    #endregion

    #region MultiLanguageHyperlink
    public class MultiLanguageHyperlink : HyperLink, IMultiLanguage
    {
        #region Properties
        private int IdResource;

        public int Resource
        {
            get { return IdResource; }
            set { IdResource = value; }
        }

        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            if (IdResource != 0)
            {
                string Context = String.Empty;

                try
                {
                    SessionLanguage Language = (SessionLanguage)System.Web.HttpContext.Current.Session["SessionLanguage"];
                    Global._ResorceManagerPool.SelectPhrase(IdResource, Language.GetLanguage(), ref Context);
                }
                catch (SQLException ex)
                {
                    Context = ex.Message;
                }

                base.Text = Context;
            }

            base.Render(writer);
        }
    }
    #endregion

    #region MultiLanguageRadioButton
    public class MultiLanguageRadioButton : RadioButton, IMultiLanguage
    {
        #region Properties
        private int IdResource;

        public int Resource
        {
            get { return IdResource; }
            set { IdResource = value; }
        }

        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            if (IdResource != 0)
            {
                string Context = String.Empty;

                try
                {
                    SessionLanguage Language = (SessionLanguage)System.Web.HttpContext.Current.Session["SessionLanguage"];
                    Global._ResorceManagerPool.SelectPhrase(IdResource, Language.GetLanguage(), ref Context);
                }
                catch (SQLException ex)
                {
                    Context = ex.Message;
                }

                base.Text = Context;
            }

            base.Render(writer);
        }
    }
    #endregion

    #region MultiLanguageRadioButtonList
    public class Conversion : Object
    {
        static public String ConvertToString(Int32[] arrIntResource)
        {
            String[] arrString = new String[arrIntResource.Length];
            for (Int32 Index = 0; Index < arrIntResource.Length; arrString[Index] = arrIntResource[Index++].ToString()) ;

            return String.Join(":", arrString);
        }

        static public Int32[] ConvertToInt32(String arrStringResource)
        {
            String[] arrString = arrStringResource.Split(new Char[] { ':' });
            Int32[] arrInt32 = new Int32[arrString.Length];
            for (Int32 Index = 0; Index < arrString.Length; arrInt32[Index] = Convert.ToInt32(arrString[Index++])) ;

            return arrInt32;
        }
    }

    public class MultiLanguageRadioButtonList : RadioButtonList, IMultiLanguage
    {
        #region Properties
        private Int32[] IdResource;

        public Int32 Resource
        { get { return -1; } set { throw new NotImplementedException(); } }

        public String ListResource
        {
            get { return Conversion.ConvertToString(IdResource); }
            set
            {
                IdResource = Conversion.ConvertToInt32(value);
                if (IdResource.Length < Items.Count)
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            if (IdResource.Length == Items.Count)
            {

                String Context = String.Empty;
                SessionLanguage Language = (SessionLanguage)System.Web.HttpContext.Current.Session["SessionLanguage"];
                int IdLanguage = Language.GetLanguage();

                try
                {
                    foreach (ListItem Item in Items)
                    {
                        Global._ResorceManagerPool.SelectPhrase(Convert.ToInt32(Item.Attributes["ResourceId"]), IdLanguage, ref Context);
                        Item.Text = Context;
                    }
                }
                catch (SQLException ex)
                {
                    Context = ex.Message;
                }
            }

            base.Render(writer);
        }
    }
    #endregion

    #region MultiLanguageDropDownList

    public class MultiLanguageDropDownList : DropDownList, IMultiLanguage
    {
        #region Properties
        private Int32[] IdResource;

        public Int32 Resource
        { get { return -1; } set { throw new NotImplementedException(); } }

        public String ListResource
        {
            get { return Conversion.ConvertToString(IdResource); }
            set
            {
                IdResource = Conversion.ConvertToInt32(value);
                if (IdResource.Length < Items.Count)
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            if (IdResource.Length == Items.Count)
            {

                String Context = String.Empty;
                SessionLanguage Language = (SessionLanguage)System.Web.HttpContext.Current.Session["SessionLanguage"];
                int IdLanguage = Language.GetLanguage();

                try
                {
                    foreach (ListItem Item in Items)
                    {
                        Global._ResorceManagerPool.SelectPhrase(Convert.ToInt32(Item.Attributes["ResourceId"]), IdLanguage, ref Context);
                        Item.Text = Context;
                    }
                }
                catch (SQLException ex)
                {
                    Context = ex.Message;
                }
            }

            base.Render(writer);
        }
    }
    #endregion
}
