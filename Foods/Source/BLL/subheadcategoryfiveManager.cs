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
    public class subheadcategoryfiveManager
    {
        private subheadcategoryfive subheadcategoryfive;

        public subheadcategoryfiveManager(subheadcategoryfive _subheadcategoryfive)
        {
            subheadcategoryfive = _subheadcategoryfive;
        }

        private string GetKey(ISession _iSession)
        {
            string uniqueKey = null;
            ISession session = _iSession;
            try
            {
                session.BeginTransaction();
                string queryString = "select max(cast(subheadcategoryfiveID as int)) from subheadcategoryfive";

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
            if (subheadcategoryfive == null)
            {
                return;
            }
            ISession session = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                ITransaction transaction = session.BeginTransaction();

                if (string.IsNullOrEmpty(subheadcategoryfive.subheadcategoryfiveID))
                { subheadcategoryfive.subheadcategoryfiveID = GetKey(session); }

                //if (string.IsNullOrEmpty(subheadcategoryfive.subheadcategoryfiveID))
                //{ subheadcategoryfive.subheadcategoryfiveID = GetKey(session); }

                session.SaveOrUpdate(subheadcategoryfive);
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
                session.Delete(subheadcategoryfive);
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

        public static List<subheadcategoryfive> GetList(string Name)
        {
            ISession session = null;
            List<subheadcategoryfive> objectsList = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                objectsList = (List<subheadcategoryfive>)session.CreateCriteria(typeof(subheadcategoryfive)).List<subheadcategoryfive>();
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

        public static DataTable GetHeadFiveList()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = "select * from subheadcategoryfive where subheadcategoryfiveName != 'Del'";

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
                    dT_.Columns.Add("isflag");
                    
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
                    dR_["isflag"] = row_[10];
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

        public static DataTable GetHeadFive(string CatfiveSubAccCatName)
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = "select subheadcategoryfourGeneratedID,  subheadcategoryfourName from subheadcategoryfour where SubHeadCategoriesGeneratedID ='" + CatfiveSubAccCatName + "'";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("subheadcategoryfourGeneratedID");
                    dT_.Columns.Add("subheadcategoryfourName");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();

                    dR_["subheadcategoryfourGeneratedID"] = row_[0];
                    dR_["subheadcategoryfourName"] = row_[1];

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


        public static DataTable GetHeadFiv(string CatfiveSubAcc)
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = "SELECT * FROM subheadcategoryfive where subheadcategoryfiveName ='" + CatfiveSubAcc + "'";

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