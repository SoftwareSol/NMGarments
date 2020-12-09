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
    public class DPurchaseManager
    {
        private DPurchase dPureMng;

        public DPurchaseManager(DPurchase _dPureMng)
        {
            dPureMng = _dPureMng;
        }

        private string GetKey(ISession _iSession)
        {
            string uniqueKey = null;
            ISession session = _iSession;
            try
            {
                session.BeginTransaction();
                string queryString = "select max(cast(DPurID as int)) from DPurchase";

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
            if (dPureMng == null)
            {
                return;
            }
            ISession session = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                ITransaction transaction = session.BeginTransaction();

                if (string.IsNullOrEmpty(dPureMng.DPurID))
                { dPureMng.DPurID = GetKey(session); }

                if (string.IsNullOrEmpty(dPureMng.DPurID))
                { dPureMng.DPurID = GetKey(session); }

                session.SaveOrUpdate(dPureMng);
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
                session.Delete(dPureMng);
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

        public static List<DPurchaseManager> GetList(string Name)
        {
            ISession session = null;
            List<DPurchaseManager> objectsList = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                objectsList = (List<DPurchaseManager>)session.CreateCriteria(typeof(DPurchaseManager)).List<DPurchaseManager>();
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
        
        public static DataTable GetCityList()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = "select * from DPurchase";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("CityID");
                    dT_.Columns.Add("City_");
                    
                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["CityID"] = row_[0];
                    dR_["City_"] = row_[1];
                  
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