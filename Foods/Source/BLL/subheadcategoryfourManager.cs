using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Foods;

using NHibernate;
using NHibernate.Criterion;

namespace Foods
{
    public class subheadcategoryfourManager
    {
         private subheadcategoryfour subheadcategoryfour;

         public subheadcategoryfourManager(subheadcategoryfour _subheadcategoryfour)
        {
            subheadcategoryfour = _subheadcategoryfour;
        }

        private string GetKey(ISession _iSession)
        {
            string uniqueKey = null;
            ISession session = _iSession;
            try
            {
                session.BeginTransaction();
                string queryString = "select max(cast(subheadcategoryfourID as int)) from subheadcategoryfour";

                IQuery query = session.CreateQuery(queryString);
               // .SetParameter("pCmCode", _cmCode);
                IList resultsList = query.List();

                if (resultsList == null)
                {
                    uniqueKey = "1";
                }
                else
                {
                    if (resultsList[0] == null)
                    {
                        uniqueKey = "1";
                    }
                    else
                    {
                        uniqueKey = (Int32.Parse(resultsList[0].ToString()) + 1).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return uniqueKey;
        }

        public void Save()
        {
            if (subheadcategoryfour == null)
            {
                return;
            }
            ISession session = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                ITransaction transaction = session.BeginTransaction();

                if (string.IsNullOrEmpty(subheadcategoryfour.subheadcategoryfourID))
                { subheadcategoryfour.subheadcategoryfourID = GetKey(session); }

                //if (string.IsNullOrEmpty(city.CityID))
                //{ city.CityID = GetKey(session); }

                session.SaveOrUpdate(subheadcategoryfour);
                transaction.Commit();
          
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
        }

        public void Delete()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                ITransaction transaction = session.BeginTransaction();
                session.Delete(subheadcategoryfour);
                transaction.Commit();
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
        }

        public static List<subheadcategoryfour> GetList(string Name)
        {
            ISession session = null;
            List<subheadcategoryfour> objectsList = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                objectsList = (List<subheadcategoryfour>)session.CreateCriteria(typeof(subheadcategoryfour)).List<subheadcategoryfour>();
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
            return objectsList;
        }
//        SELECT SubHeadCategoriesGeneratedID, SubHeadCategoriesName FROM SubHeadCategories where SubHeadGeneratedID ='" + DLLCategoriesfourSubAccountName.SelectedValue + "'"
        public static DataTable GetHeadFour(string CategoriesfourSubAccountName)
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = "SELECT SubHeadCategoriesGeneratedID, SubHeadCategoriesName FROM SubHeadCategories where SubHeadGeneratedID ='" + CategoriesfourSubAccountName + "'";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("SubHeadCategoriesGeneratedID");
                    dT_.Columns.Add("SubHeadCategoriesName");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();

                    dR_["SubHeadCategoriesGeneratedID"] = row_[0];
                    dR_["SubHeadCategoriesName"] = row_[1];

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
        public static DataTable GetHeadFourList()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = "select * from subheadcategoryfour where subheadcategoryfourName != 'Del'";

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

        public static DataTable GetHeadFou(string CategoriesfourSubAccount)
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = "SELECT * FROM subheadcategoryfour where subheadcategoryfourName ='" + CategoriesfourSubAccount + "'";

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
    }
}