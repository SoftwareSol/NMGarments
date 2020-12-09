using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Project;

using NHibernate;
using NHibernate.Criterion;
namespace Foods
{
    public class tbl_ItmPricingManager
    {
        private tbl_ItmPricing tbl_ItmPricing;

        public tbl_ItmPricingManager(tbl_ItmPricing _tbl_ItmPricing)
        {
            tbl_ItmPricing = _tbl_ItmPricing;
        }
        private string GetKey(ISession _iSession)
        {
            string uniqueKey = null;
            ISession session = _iSession;
            try
            {
                session.BeginTransaction();
                string queryString = "select max(cast(ItmPriID as int)) from tbl_ItmPricing";

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
            if (tbl_ItmPricing == null)
            {
                return;
            }
            ISession session = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                ITransaction transaction = session.BeginTransaction();

                if (string.IsNullOrEmpty(tbl_ItmPricing.ItmPriID))
                { tbl_ItmPricing.ItmPriID = GetKey(session); }


                if (string.IsNullOrEmpty(tbl_ItmPricing.ItmPriID))
                {
                    tbl_ItmPricing.ItmPriID = GetKey(session);
                }

                session.SaveOrUpdate(tbl_ItmPricing);
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
                session.Delete(tbl_ItmPricing);
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
        public static List<tbl_ItmPricing> GetList(string Name)
        {
            ISession session = null;
            List<tbl_ItmPricing> objectsList = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                objectsList = (List<tbl_ItmPricing>)session.CreateCriteria(typeof(tbl_ItmPricing)).List<tbl_ItmPricing>();
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
        public static DataTable getPro()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = " select ProductID,ProductName from Products";
                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("ProductID");
                    dT_.Columns.Add("ProductName");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();

                    dR_["ProductID"] = row_[0];
                    dR_["ProductName"] = row_[1];
                  
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

        public static DataTable getCus()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = " select CustomerID,CustomerName from Customers_";
                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("CustomerID");
                    dT_.Columns.Add("CustomerName");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();

                    dR_["CustomerID"] = row_[0];
                    dR_["CustomerName"] = row_[1];

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
        public static DataTable GetItmPricingsList()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = "select * from dbo.tbl_ItmPricing";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("ItmPriID");
                    dT_.Columns.Add("EffDat");
                    dT_.Columns.Add("ProductID");
                    dT_.Columns.Add("CustomerID");
                    dT_.Columns.Add("itmpri_Qty");
                    dT_.Columns.Add("unt_cost");
                    dT_.Columns.Add("cost");
                    dT_.Columns.Add("crtd_by");
                    dT_.Columns.Add("crtd_at");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["ItmPriID"] = row_[0];
                    dR_["EffDat"] = row_[1];
                    dR_["ProductID"] = row_[2];
                    dR_["CustomerID"] = row_[3];
                    dR_["itmpri_Qty"] = row_[4];
                    dR_["unt_cost"] = row_[5];
                    dR_["cost"] = row_[6];
                    dR_["crtd_by"] = row_[7];
                    dR_["crtd_at"] = row_[8];
                    
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