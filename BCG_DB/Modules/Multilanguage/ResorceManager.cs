namespace MultilanguageTools.Modules.Multilanguage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.SessionState;
    using SQLPreferences;


    public class ResorceManager : DBFunction
    {
        protected SessionLanguage Language;

        public ResorceManager(HttpSessionState Session)
        {
            Language = (SessionLanguage)Session["SessionLanguage"];
            if (Language == null)
                throw new NullReferenceException("Not valid instance SESION LANGUAGE !");
        }

        public ResorceManager(SQLDatabase conDatabase, HttpSessionState Session)
            :
            base(conDatabase)
        {
            Language = (SessionLanguage)Session["SessionLanguage"];
            if (Language == null)
                throw new NullReferenceException("Not valid instance SESION LANGUAGE !");
        }

        public ResorceManager(SQLDatabase conDatabase)
            :
            base(conDatabase)
        {
        }

        /*--------------------------
         *	PROCEDURE procSEL_Phrase
         *-------------------------- 
         *	@IdLanguage int
         *	@IdResource int
         */

        public Boolean SelectPhrase(int iIdResource, ref String strPhrase)
        {
            SQLParameters Parameters = new SQLParameters();
            SQLFillParameter FillParameter = new SQLFillParameter();

            SqlParameter Parameter = new SqlParameter("@IdLanguage", SqlDbType.Int);
            FillParameter.FillParameter(Parameter, Language.GetLanguage());
            Parameters.Add(Parameter);

            Parameter = new SqlParameter("@IdResource", SqlDbType.Int);
            FillParameter.FillParameter(Parameter, iIdResource);
            Parameters.Add(Parameter);

            SqlCommand CommandSelectPhrase = new SqlCommand("procSEL_Phrase", Database.Connection);
            if (CommandSelectPhrase == null)
                return false;

            try
            {
                CommandSelectPhrase.CommandType = CommandType.StoredProcedure;
                Parameters.FillParametersIn(CommandSelectPhrase);

                SqlDataReader AdapterSelectPhrase = CommandSelectPhrase.ExecuteReader();

                if (!AdapterSelectPhrase.HasRows)
                    return false;

                AdapterSelectPhrase.Read();

                strPhrase = AdapterSelectPhrase["Context"].ToString();
            }
            catch (SqlException Ex)
            {
                throw new SQLException("Exception in 'SelectPhrase' function!", Ex);
            }
            catch (FormatException Ex)
            {
                throw new SQLException("Exception in 'SelectPhrase' function!", Ex);
            }
            return true;
        }

        /*--------------------------
         *	PROCEDURE procSEL_Phrase
         *-------------------------- 
         *	@IdLanguage int
         *	@IdResource int
         */

        public Boolean SelectPhraseByLanguage(int iIdResource, int IdLanguage, ref String strPhrase)
        {
            SQLParameters Parameters = new SQLParameters();
            SQLFillParameter FillParameter = new SQLFillParameter();

            SqlParameter Parameter = new SqlParameter("@IdLanguage", SqlDbType.Int);
            FillParameter.FillParameter(Parameter, IdLanguage);
            Parameters.Add(Parameter);

            Parameter = new SqlParameter("@IdResource", SqlDbType.Int);
            FillParameter.FillParameter(Parameter, iIdResource);
            Parameters.Add(Parameter);

            SqlCommand CommandSelectPhrase = new SqlCommand("procSEL_Phrase", Database.Connection);
            if (CommandSelectPhrase == null)
                return false;

            try
            {
                CommandSelectPhrase.CommandType = CommandType.StoredProcedure;
                Parameters.FillParametersIn(CommandSelectPhrase);

                SqlDataReader AdapterSelectPhrase = CommandSelectPhrase.ExecuteReader();

                if (!AdapterSelectPhrase.HasRows)
                    return false;

                AdapterSelectPhrase.Read();

                strPhrase = AdapterSelectPhrase["Context"].ToString();
            }
            catch (SqlException Ex)
            {
                throw new SQLException("Exception in 'SelectPhraseByLanguage' function!", Ex);
            }
            catch (FormatException Ex)
            {
                throw new SQLException("Exception in 'SelectPhraseByLanguage' function!", Ex);
            }
            return true;
        }

        /*-------------------------------------------
         *	PROCEDURE procSEL_DescriptionByIdResource
         *------------------------------------------- 
         *	@IdResource int
         */

        public Boolean SelectDescription(int iIdResource, ref String strDescription)
        {

            SQLParameters Parameters = new SQLParameters();
            SQLFillParameter FillParameter = new SQLFillParameter();

            SqlParameter Parameter = new SqlParameter("@IdLanguage", SqlDbType.Int);
            FillParameter.FillParameter(Parameter, Language.GetLanguage());
            Parameters.Add(Parameter);

            Parameter = new SqlParameter("@IdResource", SqlDbType.Int);
            FillParameter.FillParameter(Parameter, iIdResource);
            Parameters.Add(Parameter);


            SqlCommand CommandSelectDescription = new SqlCommand("procSEL_DescriptionByIdResource", Database.Connection);
            if (CommandSelectDescription == null)
                return false;

            try
            {
                CommandSelectDescription.CommandType = CommandType.StoredProcedure;
                Parameters.FillParametersIn(CommandSelectDescription);

                SqlDataReader AdapterSelectDescription = CommandSelectDescription.ExecuteReader();


                if (!AdapterSelectDescription.HasRows)
                    return false;

                AdapterSelectDescription.Read();

                strDescription = AdapterSelectDescription["Description"].ToString();
                AdapterSelectDescription.Close();
            }
            catch (Exception Ex)
            {
                throw new SQLException("Exception in 'SelectDescription' function!", Ex);
            }

            return true;
        }

        /*-------------------------------------
        *	PROCEDURE procSEL_Language
        *--------------------------------------
        *	@IdLanguage int
        */
        public Boolean SelectLanguage(int iIdLanguage, out DataSet dsLanguage)
        {

            SQLParameters Parameters = new SQLParameters();
            SQLFillParameter FillParameter = new SQLFillParameter();

            SqlParameter Parameter = new SqlParameter("@IdLanguage", SqlDbType.Int);
            FillParameter.FillParameter(Parameter, iIdLanguage);
            Parameters.Add(Parameter);

            dsLanguage = new DataSet();

            SqlCommand CommandSelectLanguage = new SqlCommand("procSEL_Language", Database.Connection);
            if (CommandSelectLanguage == null)
                return false;

            try
            {
                CommandSelectLanguage.CommandType = CommandType.StoredProcedure;
                Parameters.FillParametersIn(CommandSelectLanguage);

                SqlDataAdapter AdapterSelectLanguage = new SqlDataAdapter();

                AdapterSelectLanguage.SelectCommand = CommandSelectLanguage;
                AdapterSelectLanguage.Fill(dsLanguage, "tblLanguages");
            }
            catch (SqlException Ex)
            {
                throw new SQLException("Exception in 'SelectLanguage' function!", Ex);
            }
            catch (FormatException Ex)
            {
                throw new SQLException("Exception in 'SelectLanguage' function!", Ex);
            }
            return true;
        }

        /*-------------------------------------
        *	PROCEDURE procSEL_StaticContext
        *--------------------------------------
        *	@IdStatic int
        *	@IdLanguage int
        */
        public Boolean SelectStaticContext(int iIdStatic, int iIdLanguage, out DataSet dsText)
        {

            SQLParameters Parameters = new SQLParameters();
            SQLFillParameter FillParameter = new SQLFillParameter();

            SqlParameter Parameter = new SqlParameter("@IdLanguage", SqlDbType.Int);
            FillParameter.FillParameter(Parameter, iIdLanguage);
            Parameters.Add(Parameter);

            Parameter = new SqlParameter("@IdStatic", SqlDbType.Int);
            FillParameter.FillParameter(Parameter, iIdStatic);
            Parameters.Add(Parameter);

            dsText = new DataSet();

            SqlCommand CommandSelectText = new SqlCommand("procSEL_StaticContext", Database.Connection);
            if (CommandSelectText == null)
                return false;

            try
            {
                CommandSelectText.CommandType = CommandType.StoredProcedure;
                Parameters.FillParametersIn(CommandSelectText);

                SqlDataAdapter AdapterSelectText = new SqlDataAdapter();

                AdapterSelectText.SelectCommand = CommandSelectText;
                AdapterSelectText.Fill(dsText, "tblStaticText");
            }
            catch (SqlException Ex)
            {
                throw new SQLException("Exception in 'SelectStaticText' function!", Ex);
            }
            catch (FormatException Ex)
            {
                throw new SQLException("Exception in 'SelectStaticText' function!", Ex);
            }
            return true;
        }

        public Boolean SelectStaticPhrase(int iIdResource, ref String strPhrase)
        {
            DataSet dsContext = null;
            SelectStaticContext(iIdResource, Language.GetLanguage(), out dsContext);

            if (dsContext.Tables["tblStaticText"].Rows.Count <= 0)
                return false;

            strPhrase = dsContext.Tables["tblStaticText"].Rows[0]["Context"].ToString();
            return true;
        }

        public Boolean SelectStaticPhraseByLanguage(int iIdResource, int IdLanguage, ref String strPhrase)
        {
            DataSet dsContext = null;
            SelectStaticContext(iIdResource, IdLanguage, out dsContext);

            if (dsContext.Tables["tblStaticText"].Rows.Count <= 0)
                return false;

            strPhrase = dsContext.Tables["tblStaticText"].Rows[0]["Context"].ToString();
            return true;
        }

        /*---------------------------------------
        *	PROCEDURE procLIS_PhrasesByIdResource
        *----------------------------------------
        *	@IdResource int
        */
        public Boolean ListPhrasesByIdResource(int nIdResource, out DataSet dsLanguage)
        {
            dsLanguage = new DataSet();

            SqlCommand CommandSelectPhrases = new SqlCommand("procLIS_PhrasesByIdResource", Database.Connection);
            if (CommandSelectPhrases == null)
                return false;

            try
            {
                SQLParameters Parameters = new SQLParameters();
                SQLFillParameter FillParameter = new SQLFillParameter();

                SqlParameter Parameter = new SqlParameter("@IdResource", SqlDbType.Int);
                FillParameter.FillParameter(Parameter, nIdResource);
                Parameters.Add(Parameter);

                CommandSelectPhrases.CommandType = CommandType.StoredProcedure;
                Parameters.FillParametersIn(CommandSelectPhrases);

                SqlDataAdapter AdapterSelectPhrases = new SqlDataAdapter();
                AdapterSelectPhrases.SelectCommand = CommandSelectPhrases;

                AdapterSelectPhrases.Fill(dsLanguage, "tblPhrases");
            }
            catch (SqlException Ex)
            {
                throw new SQLException("Exception in 'SelectPhrasesByIdRes' function!", Ex);
            }
            catch (FormatException Ex)
            {
                throw new SQLException("Exception in 'SelectPhrasesByIdRes' function!", Ex);
            }
            return true;
        }


        /*-------------------------------------------------
        *	PROCEDURE procSEL_ExcludedLanguagesByIdResource
        *--------------------------------------------------
        *	@IdResource int,
        *	@IsStaticResource as bit
        */
        public Boolean SelectExcludedLanguagesByIdRes(int nIdResource, Boolean IsStaticResource, out DataSet dsLanguage)
        {
            dsLanguage = new DataSet();

            SqlCommand CommandSelectExcludedLanguages = new SqlCommand("procSEL_ExcludedLanguagesByIdResource", Database.Connection);
            if (CommandSelectExcludedLanguages == null)
                return false;

            try
            {
                SQLParameters Parameters = new SQLParameters();
                SQLFillParameter FillParameter = new SQLFillParameter();

                SqlParameter Parameter = new SqlParameter("@IdResource", SqlDbType.Int);
                FillParameter.FillParameter(Parameter, nIdResource);
                Parameters.Add(Parameter);

                Parameter = new SqlParameter("@IsStaticResource", SqlDbType.Bit);
                FillParameter.FillParameter(Parameter, IsStaticResource);
                Parameters.Add(Parameter);

                CommandSelectExcludedLanguages.CommandType = CommandType.StoredProcedure;
                Parameters.FillParametersIn(CommandSelectExcludedLanguages);

                SqlDataAdapter AdapterSelectExcludedLanguages = new SqlDataAdapter();
                AdapterSelectExcludedLanguages.SelectCommand = CommandSelectExcludedLanguages;

                AdapterSelectExcludedLanguages.Fill(dsLanguage, "tblExLanguages");
            }
            catch (SqlException Ex)
            {
                throw new SQLException("Exception in 'SelectExcludedLanguagesByIdRes' function!", Ex);
            }
            catch (FormatException Ex)
            {
                throw new SQLException("Exception in 'SelectExcludedLanguagesByIdRes' function!", Ex);
            }
            return true;
        }


        /*--------------------------------
         *	PROCEDURE procLIS_AllLanguages
         *--------------------------------
         *	
         */
        public Boolean ListAllLanguages(out DataSet dsLanguages)
        {

            dsLanguages = new DataSet();

            SQLParameters Parameters = new SQLParameters();

            SqlCommand CommandSelectAllLanguages = new SqlCommand("procLIS_AllLanguages", Database.Connection);
            if (CommandSelectAllLanguages == null)
                return false;

            try
            {
                CommandSelectAllLanguages.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter AdapterSelectAllLanguages = new SqlDataAdapter();

                AdapterSelectAllLanguages.SelectCommand = CommandSelectAllLanguages;

                dsLanguages.DataSetName = "Languages";

                AdapterSelectAllLanguages.Fill(dsLanguages, "Languages");
            }
            catch (SqlException Ex)
            {
                throw new SQLException("Exception in 'SelectAllLanguages' function!", Ex);
            }
            catch (FormatException Ex)
            {
                throw new SQLException("Exception in 'SelectAllLanguages' function!", Ex);
            }
            return true;
        }

        /*[dbo].[procLIS_ActiveLanguages]
         */
        public Boolean ListActiveLanguages(out DataSet dsLanguages)
        {
            dsLanguages = new DataSet();

            SQLParameters Parameters = new SQLParameters();

            SqlCommand CommandSelectAllLanguages = new SqlCommand("procLIS_ActiveLanguages", Database.Connection);
            if (CommandSelectAllLanguages == null)
                return false;

            try
            {
                CommandSelectAllLanguages.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter AdapterSelectAllLanguages = new SqlDataAdapter();

                AdapterSelectAllLanguages.SelectCommand = CommandSelectAllLanguages;

                dsLanguages.DataSetName = "Languages";

                AdapterSelectAllLanguages.Fill(dsLanguages, "Languages");
            }
            catch (SqlException Ex)
            {
                throw new SQLException("Exception in 'ListActiveLanguages' function!", Ex);
            }
            catch (FormatException Ex)
            {
                throw new SQLException("Exception in 'ListActiveLanguages' function!", Ex);
            }
            return true;
        }

        /*----------------------------
         *	PROCEDURE procUPD_Language
         *----------------------------
         *	
         *  @IdLanguage
         *	@IsActive bit	
         *	@Picture image
         *  @Culture nvarchar (20)
         *  @UserName nvarchar(50)
         */

        public Boolean UpdateLanguage(SQLParameters Parameters, ref Int32 ReturnValue)
        {

            SqlCommand CommandUpdateLanguage = new SqlCommand("procUPD_Language", Database.Connection);
            if (CommandUpdateLanguage == null)
                return false;

            try
            {
                CommandUpdateLanguage.CommandType = CommandType.StoredProcedure;
                Parameters.FillParametersIn(CommandUpdateLanguage);

                CommandUpdateLanguage.ExecuteNonQuery();
                ReturnValue = (Int32)Parameters.ReturnValue();
            }
            catch (SqlException Ex)
            {
                throw new SQLException("Exception in 'UpdateLanguage' function", Ex);
            }

            return true;
        }


        /*----------------------------
         *	PROCEDURE procINS_Language
         *----------------------------
         *	@Languages nvarchar(50)
         *	@Initials nvarchar(15)
         *	@IsActive bit	
         *	@Picture image
         *  @UserName nvarchar(50)
         */

        public Boolean InsertLanguage(SQLParameters Parameters, ref Int32 ReturnValue)
        {
            SqlCommand CommandInsertLanguage = new SqlCommand("procINS_Language", Database.Connection);
            if (CommandInsertLanguage == null)
                return false;

            try
            {
                CommandInsertLanguage.CommandType = CommandType.StoredProcedure;
                Parameters.FillParametersIn(CommandInsertLanguage);

                CommandInsertLanguage.ExecuteNonQuery();
                ReturnValue = (Int32)Parameters.ReturnValue();
            }
            catch (SqlException Ex)
            {
                throw new SQLException("Exception in 'InsertLanguage' function", Ex);
            }

            return true;
        }



        /*-----------------------------------
         *	PROCEDURE procINS_LanguageContext
         *-----------------------------------	
         *	@IdDefLanguage int
         *	@IdNewLanguage int
         *	@Context nvarchar (500)
         *	@UserName nvarchar (50)		 
         */
        public Boolean InsertLanguageContext(SQLParameters Parameters, ref Int32 ReturnValue)
        {
            SqlCommand CommandInsertLanguageContext = new SqlCommand("procINS_LanguageContext", Database.Connection);
            if (CommandInsertLanguageContext == null)
                return false;

            try
            {
                CommandInsertLanguageContext.CommandType = CommandType.StoredProcedure;
                Parameters.FillParametersIn(CommandInsertLanguageContext);

                CommandInsertLanguageContext.ExecuteNonQuery();
                ReturnValue = (Int32)Parameters.ReturnValue();
            }
            catch (SqlException Ex)
            {
                throw new SQLException("Exception in 'InsertLanguageContext' function", Ex);
            }

            return true;
        }



        /*-----------------------------
         *	PROCEDURE procINS_DefPhrase
         *-----------------------------	
         *	@IdResource int,
         *	@IdNewLanguage int,
         *	@Context nvarchar (500),
         *	@UserName nvarchar (50)
         */
        public Boolean InsertDefaultPhrase(SQLParameters Parameters, ref Int32 ReturnValue)
        {
            SqlCommand CommandInsertDefaultPhrase = new SqlCommand("procINS_DefPhrase", Database.Connection);
            if (CommandInsertDefaultPhrase == null)
                return false;

            try
            {
                CommandInsertDefaultPhrase.CommandType = CommandType.StoredProcedure;
                Parameters.FillParametersIn(CommandInsertDefaultPhrase);

                CommandInsertDefaultPhrase.ExecuteNonQuery();
                ReturnValue = (Int32)Parameters.ReturnValue();
            }
            catch (SqlException Ex)
            {
                throw new SQLException("Exception in 'InsertDefaultPhrase' function", Ex);
            }

            return true;
        }


        /*---------------------------------
         *	PROCEDURE procLIS_SearchContext
         *---------------------------------
            @IdResource int,
            @IdLanguage int,
            @Context nvarchar(500)
         */
        public Boolean ListSearchContext(SQLParameters Parameters, out DataSet dsSearchContext)
        {
            dsSearchContext = new DataSet();

            SqlCommand CommandListSearchContext = new SqlCommand("procLIS_SearchContext", Database.Connection);
            if (CommandListSearchContext == null)
                return false;

            try
            {
                CommandListSearchContext.CommandType = CommandType.StoredProcedure;
                Parameters.FillParametersIn(CommandListSearchContext);

                SqlDataAdapter AdapterListSearchContext = new SqlDataAdapter();
                AdapterListSearchContext.SelectCommand = CommandListSearchContext;

                AdapterListSearchContext.Fill(dsSearchContext, "tblPhrases");
            }
            catch (SqlException Ex)
            {
                throw new SQLException("Error in 'ListSearchContext' function", Ex);
            }

            return true;
        }

        /*---------------------------------
         *	PROCEDURE procLIS_SearchStaticText
         *---------------------------------
         @IdResource int,
         @IdLanguage int,
         @Context ntext,
         @Description nvarchar 300
         */
        public Boolean ListSearchStaticText(SQLParameters Parameters, out DataSet dsSearchStaticText)
        {
            dsSearchStaticText = new DataSet();

            SqlCommand CommandListSearchStaticText = new SqlCommand("procLIS_SearchStaticText", Database.Connection);
            if (CommandListSearchStaticText == null)
                return false;

            try
            {
                CommandListSearchStaticText.CommandType = CommandType.StoredProcedure;
                Parameters.FillParametersIn(CommandListSearchStaticText);

                SqlDataAdapter AdapterListSearchStaticText = new SqlDataAdapter();
                AdapterListSearchStaticText.SelectCommand = CommandListSearchStaticText;

                AdapterListSearchStaticText.Fill(dsSearchStaticText, "tblStaticText");
            }
            catch (SqlException Ex)
            {
                throw new SQLException("Error in 'ListSearchStaticText' function", Ex);
            }

            return true;
        }



        /*-----------------------------------
         *	PROCEDURE procUPD_LanguageContext
         *-----------------------------------	
         *	@Context nvarchar (500)
         *	@IdContext int
         *  @UserName nvarchar(50)
         */
        public Boolean UpdateLanguageContext(String strContext, int iIdContext, String UserName, ref Int32 nReturnValue)
        {
            SqlCommand CommandUpdateLanguageContext = new SqlCommand("procUPD_LanguageContext", Database.Connection);
            if (CommandUpdateLanguageContext == null)
                return false;

            try
            {
                SQLParameters Parameters = new SQLParameters();
                SQLFillParameter FillParameter = new SQLFillParameter();

                SqlParameter Parameter = new SqlParameter("@IdContext", SqlDbType.Int);
                FillParameter.FillParameter(Parameter, iIdContext);
                Parameters.Add(Parameter);

                Parameter = new SqlParameter("@Context", SqlDbType.NVarChar, 500);
                FillParameter.FillParameter(Parameter, strContext);
                Parameters.Add(Parameter);

                Parameter = new SqlParameter("@UserName", SqlDbType.NVarChar, 50);
                FillParameter.FillParameter(Parameter, UserName);
                Parameters.Add(Parameter);

                CommandUpdateLanguageContext.CommandType = CommandType.StoredProcedure;
                Parameters.FillParametersIn(CommandUpdateLanguageContext);

                CommandUpdateLanguageContext.ExecuteNonQuery();

                nReturnValue = (Int32)Parameters.ReturnValue();
            }
            catch (SqlException Ex)
            {
                nReturnValue = 0;
                throw new SQLException("Error in 'UpdateLanguageContext' function", Ex);
            }

            return true;
        }

        /*---------------------------------------
         *	PROCEDURE procUPD_ResourceDescription
         *---------------------------------------	
         *	@IdResource int
         *	@Description nvarchar (100)
         */
        public Boolean UpdateResourceDescription(int iIdResource, String strDescription, ref Int32 nReturnValue)
        {
            SqlCommand CommandUpdateResourceDescription = new SqlCommand("procUPD_ResourceDescription", Database.Connection);
            if (CommandUpdateResourceDescription == null)
                return false;

            try
            {
                SQLParameters Parameters = new SQLParameters();
                SQLFillParameter FillParameter = new SQLFillParameter();

                SqlParameter Parameter = new SqlParameter("@Description", SqlDbType.NVarChar, 100);
                FillParameter.FillParameter(Parameter, strDescription);
                Parameters.Add(Parameter);

                Parameter = new SqlParameter("@IdResource", SqlDbType.Int);
                FillParameter.FillParameter(Parameter, iIdResource);
                Parameters.Add(Parameter);

                Parameter = new SqlParameter("@IdLanguage", SqlDbType.Int);
                FillParameter.FillParameter(Parameter, Language.GetLanguage());
                Parameters.Add(Parameter);

                CommandUpdateResourceDescription.CommandType = CommandType.StoredProcedure;
                Parameters.FillParametersIn(CommandUpdateResourceDescription);

                CommandUpdateResourceDescription.ExecuteNonQuery();

                nReturnValue = (Int32)Parameters.ReturnValue();
            }
            catch (SqlException Ex)
            {
                nReturnValue = 0;
                throw new SQLException("Error in 'UpdateResourceDescription' function", Ex);
            }

            return true;
        }




        /*-----------------------------------------
        *	PROCEDURE procUPD_LanguageStaticContext
        *------------------------------------------
        *	@IdLanguage int
        *	@IdStatic int	
        *	@StaticName nvarchar (300)	
        *	@Context ntext
        *	@UserName nvarchar(50)
        *	
        */
        public Boolean UpdateLanguageStaticContext(SQLParameters Parameters, ref Int32 ReturnValue)
        {
            SqlCommand CommandUpdateLanguageStaticContext = new SqlCommand("procUPD_LanguageStaticContext", Database.Connection);
            if (CommandUpdateLanguageStaticContext == null)
                return false;

            try
            {
                CommandUpdateLanguageStaticContext.CommandType = CommandType.StoredProcedure;
                Parameters.FillParametersIn(CommandUpdateLanguageStaticContext);

                CommandUpdateLanguageStaticContext.ExecuteNonQuery();
                ReturnValue = (Int32)Parameters.ReturnValue();
            }
            catch (SqlException Ex)
            {
                throw new SQLException("Exception in 'UpdateLanguageStaticContext' function", Ex);
            }

            return true;
        }

        /*-----------------------------------------
        *	PROCEDURE procINS_LanguageStaticContext
        *------------------------------------------
            @IdStatic int = null,
            @IdLanguage int,
            @StaticName nvarchar (300),
            @UserName nvarchar (50)		
        */
        public Boolean InsertLanguageStaticContext(SQLParameters Parameters, ref Int32 ReturnValue)
        {
            SqlCommand CommandInsertLanguageStaticContext = new SqlCommand("procINS_LanguageStaticContext", Database.Connection);
            if (CommandInsertLanguageStaticContext == null)
                return false;

            try
            {
                CommandInsertLanguageStaticContext.CommandType = CommandType.StoredProcedure;
                Parameters.FillParametersIn(CommandInsertLanguageStaticContext);

                CommandInsertLanguageStaticContext.ExecuteNonQuery();
                ReturnValue = (Int32)Parameters.ReturnValue();
            }
            catch (SqlException Ex)
            {
                throw new SQLException("Exception in 'InsertLanguageStaticContext' function", Ex);
            }

            return true;
        }

        /*-----------------------------------------
        *	PROCEDURE procUPD_LanguageStaticName
        *------------------------------------------
	
            @IdLanguage int,
            @IdStatic int,
            @StaticName nvarchar(300),
            @UserName nvarchar(50)		
        */
        public Boolean UpdateLanguageStaticName(SQLParameters Parameters)
        {
            SqlCommand CommandUpdateLanguageStaticName = new SqlCommand("procUPD_LanguageStaticName", Database.Connection);
            if (CommandUpdateLanguageStaticName == null)
                return false;

            try
            {
                CommandUpdateLanguageStaticName.CommandType = CommandType.StoredProcedure;
                Parameters.FillParametersIn(CommandUpdateLanguageStaticName);

                CommandUpdateLanguageStaticName.ExecuteNonQuery();

            }
            catch (SqlException Ex)
            {
                throw new SQLException("Exception in 'UpdateLanguageStaticContext' function", Ex);
            }

            return true;
        }
    }
}
