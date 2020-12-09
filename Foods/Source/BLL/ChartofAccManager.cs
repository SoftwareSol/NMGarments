using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using Foods;
using NHibernate;
using NHibernate.Criterion;

namespace Foods
{
    public class ChartofAccManager
    {

        public static DataTable GetSubHead(string CatSubAcc)
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                //string queryString = "SELECT * FROM SubHead where HeadGeneratedID ='" + CatSubAcc + "' or SubHeadGeneratedID ='" + CatSubAcc + "' and SubHeadName != 'Del' and SubHeadName !='NULL' ";
                
				 string queryString = "SELECT * FROM SubHead where HeadGeneratedID ='" + CatSubAcc + "' and SubHeadName <> 'Del' and SubHeadName <>'NULL'";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("SubHeadID");
                    dT_.Columns.Add("SubHeadName");
                    dT_.Columns.Add("SubHeadGeneratedID");
                    dT_.Columns.Add("HeadGeneratedID");
                    dT_.Columns.Add("CreatedBy");
                    dT_.Columns.Add("CraetedAt");
                    dT_.Columns.Add("SubHeadKey");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();

                    dR_["SubHeadID"] = row_[0];
                    dR_["SubHeadName"] = row_[1];
                    dR_["SubHeadGeneratedID"] = row_[2];
                    dR_["HeadGeneratedID"] = row_[3];
                    dR_["CreatedBy"] = row_[4];
                    dR_["CraetedAt"] = row_[5];
                    dR_["SubHeadKey"] = row_[6];


                    dT_.Rows.Add(row_);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (session != null)
                {
                    NHibernateHelper.CloseSession();
                }
            }
            return dT_;
        }

        public static DataTable GetSubCatHead(string CatSubCatAcc)
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = "SELECT * FROM SubHeadCategories where HeadGeneratedID ='" + CatSubCatAcc + "' or SubHeadGeneratedID ='" + CatSubCatAcc + "' or SubHeadCategoriesGeneratedID ='" + CatSubCatAcc + "'";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("SubHeadCategoriesID");
                    dT_.Columns.Add("ven_id");
                    dT_.Columns.Add("SubHeadCategoriesName");
                    dT_.Columns.Add("SubHeadCategoriesGeneratedID");
                    dT_.Columns.Add("HeadGeneratedID");
                    dT_.Columns.Add("SubHeadGeneratedID");
                    dT_.Columns.Add("CreatedBy");
                    dT_.Columns.Add("CraetedAt");
                    dT_.Columns.Add("SubHeadKey");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();

                    dR_["SubHeadCategoriesID"] = row_[0];
                    dR_["ven_id"] = row_[1];
                    dR_["SubHeadCategoriesName"] = row_[2];
                    dR_["SubHeadCategoriesGeneratedID"] = row_[3];
                    dR_["HeadGeneratedID"] = row_[4];
                    dR_["SubHeadGeneratedID"] = row_[5];
                    dR_["CreatedBy"] = row_[6];
                    dR_["CraetedAt"] = row_[7];
                    dR_["SubHeadKey"] = row_[8];


                    dT_.Rows.Add(row_);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (session != null)
                {
                    NHibernateHelper.CloseSession();
                }
            }
            return dT_;
        }

        public static DataTable GetHeadFour(string CatfourSubAcc)
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = "SELECT * FROM subheadcategoryfour where subheadcategoryfourName not like '%del%' and SubHeadGeneratedID = '" + CatfourSubAcc + "' or HeadGeneratedID ='" + CatfourSubAcc + "' or subheadcategoryfourGeneratedID ='" + CatfourSubAcc + "' or  SubHeadCategoriesGeneratedID ='" + CatfourSubAcc + "'";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("subheadcategoryfourID");
                    dT_.Columns.Add("subheadcategoryfourName");
                    dT_.Columns.Add("subheadcategoryfourGeneratedID");
                    dT_.Columns.Add("HeadGeneratedID");
                    dT_.Columns.Add("SubHeadGeneratedID");
                    dT_.Columns.Add("SubHeadCategoriesGeneratedID");
                    dT_.Columns.Add("CreatedAt");
                    dT_.Columns.Add("CreatedBy");
                    dT_.Columns.Add("SubFourKey");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();

                    dR_["subheadcategoryfourID"] = row_[0];
                    dR_["subheadcategoryfourName"] = row_[1];
                    dR_["subheadcategoryfourGeneratedID"] = row_[2];
                    dR_["HeadGeneratedID"] = row_[3];
                    dR_["SubHeadGeneratedID"] = row_[4];
                    dR_["SubHeadCategoriesGeneratedID"] = row_[5];
                    dR_["CreatedAt"] = row_[6];
                    dR_["CreatedBy"] = row_[7];
                    dR_["SubFourKey"] = row_[8];


                    dT_.Rows.Add(row_);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (session != null)
                {
                    NHibernateHelper.CloseSession();
                }
            }
            return dT_;
        }

        public static DataTable GetHeadFive(string CatfiveSubAcc)
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
//                string queryString = "SELECT * FROM subheadcategoryfive where HeadGeneratedID ='" + CatfiveSubAcc + "'";
                string queryString = "SELECT * FROM subheadcategoryfive where subheadcategoryfiveName not like '%del%' and SubHeadGeneratedID  ='" + CatfiveSubAcc + "' or HeadGeneratedID ='" + CatfiveSubAcc + "' or subheadcategoryfourGeneratedID ='" + CatfiveSubAcc + "' or  SubHeadCategoriesGeneratedID ='" + CatfiveSubAcc + "'or  subheadcategoryfiveGeneratedID ='" + CatfiveSubAcc + "'";


                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("subheadcategoryfiveID");
                    dT_.Columns.Add("subheadcategoryfiveName");
                    dT_.Columns.Add("subheadcategoryfiveGeneratedID");
                    dT_.Columns.Add("HeadGeneratedID");
                    dT_.Columns.Add("SubHeadGeneratedID");
                    dT_.Columns.Add("SubHeadCategoriesGeneratedID");
                    dT_.Columns.Add("subheadcategoryfourGeneratedID");
                    dT_.Columns.Add("CreatedAt");
                    dT_.Columns.Add("CreatedBy");
                    dT_.Columns.Add("SubFiveKey");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();

                    dR_["subheadcategoryfiveID"] = row_[0];
                    dR_["subheadcategoryfiveName"] = row_[1];
                    dR_["subheadcategoryfiveGeneratedID"] = row_[2];
                    dR_["HeadGeneratedID"] = row_[3];
                    dR_["SubHeadGeneratedID"] = row_[4];
                    dR_["SubHeadCategoriesGeneratedID"] = row_[5];
                    dR_["subheadcategoryfourGeneratedID"] = row_[6];
                    dR_["CreatedAt"] = row_[7];
                    dR_["CreatedBy"] = row_[8];
                    dR_["SubFiveKey"] = row_[9];


                    dT_.Rows.Add(row_);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (session != null)
                {
                    NHibernateHelper.CloseSession();
                }
            }
            return dT_;
        }
    }
}