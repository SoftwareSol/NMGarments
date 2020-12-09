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
    public class SubHeadCategoriesManager
    {
        private SubHeadCategories SubHeadCategories;

        public SubHeadCategoriesManager(SubHeadCategories _SubHeadCategories)
        {
            SubHeadCategories = _SubHeadCategories;
        }

        private string GetKey(ISession _iSession)
        {
            string uniqueKey = null;
            ISession session = _iSession;
            try
            {
                session.BeginTransaction();
                string queryString = "select max(cast(SubHeadCategoriesID as int)) from SubHeadCategories";

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
            if (SubHeadCategories == null)
            {
                return;
            }
            ISession session = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                ITransaction transaction = session.BeginTransaction();

                if (string.IsNullOrEmpty(SubHeadCategories.SubHeadCategoriesID))
                { SubHeadCategories.SubHeadCategoriesID = GetKey(session); }

                //if (string.IsNullOrEmpty(city.CityID))
                //{ city.CityID = GetKey(session); }

                session.SaveOrUpdate(SubHeadCategories);
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
                session.Delete(SubHeadCategories);
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

        public static List<SubHeadCategories> GetList(string Name)
        {
            ISession session = null;
            List<SubHeadCategories> objectsList = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                objectsList = (List<SubHeadCategories>)session.CreateCriteria(typeof(SubHeadCategories)).List<SubHeadCategories>();
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


        public static DataTable GetSubHeadCatGrid(string SubHeadCategories)
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string searchSubHeadCategories = "Select SubHeadCategoriesID, SubHeadCategoriesGeneratedID, SubHeadCategoriesName, HeadGeneratedID, SubHeadGeneratedID from SubHeadCategories where SubHeadCategoriesGeneratedID = '" + SubHeadCategories +
                                "' or SubHeadCategoriesName = '" + SubHeadCategories + "' or HeadGeneratedID = '" + SubHeadCategories +
                                "' or SubHeadGeneratedID = '" + SubHeadCategories + "'";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(searchSubHeadCategories);
                objectsList = iQuery.List();
                {

                    dT_.Columns.Add("SubHeadCategoriesID");
                    dT_.Columns.Add("SubHeadCategoriesGeneratedID");
                    dT_.Columns.Add("SubHeadCategoriesName");
                    dT_.Columns.Add("HeadGeneratedID");
                    dT_.Columns.Add("SubHeadGeneratedID");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["SubHeadCategoriesID"] = row_[0];
                    dR_["SubHeadCategoriesGeneratedID"] = row_[1];
                    dR_["SubHeadCategoriesName"] = row_[2];
                    dR_["HeadGeneratedID"] = row_[3];
                    dR_["SubHeadGeneratedID"] = row_[4];

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

        public static DataTable GetSubHeadCatList()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = "select * from SubHeadCategories where SubHeadCategoriesName != 'Del'";

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
                    dT_.Columns.Add("CreatedAt");
                    dT_.Columns.Add("CreatedBy");
                    dT_.Columns.Add("SubCategoriesKey");
                    
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
                    dR_["CreatedAt"] = row_[6];
                    dR_["CreatedBy"] = row_[7];
                    dR_["SubCategoriesKey"] = row_[8];
                  
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