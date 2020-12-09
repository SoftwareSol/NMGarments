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
    public class tbl_DSalManager
    {
        private tbl_DSal DSal;

        public tbl_DSalManager(tbl_DSal _DSal)
        {
            DSal = _DSal;
        }

        private string GetKey(ISession _iSession)
        {
            string uniqueKey = null;
            ISession session = _iSession;
            try
            {
                session.BeginTransaction();
                string queryString = "select  max(cast(DSal_id as int)) from tbl_DSal";

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
            if (DSal == null)
            {
                return;
            }
            ISession session = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                ITransaction transaction = session.BeginTransaction();

                if (string.IsNullOrEmpty(DSal.DSal_id))
                { DSal.DSal_id = GetKey(session); }

                session.SaveOrUpdate(DSal);
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
                session.Delete(DSal);
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

        public static List<tbl_DSal> GetList(string Name)
        {
            ISession session = null;
            List<tbl_DSal> objectsList = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                objectsList = (List<tbl_DSal>)session.CreateCriteria(typeof(tbl_DSal)).List<tbl_DSal>();
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
        
        public static DataTable GetDSalList()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = "select DSal_id,DSal_ItmDes from tbl_DSal";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("DSal_id");
                    dT_.Columns.Add("DSal_ItmDes");
                    
                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["DSal_id"] = row_[0];
                    dR_["DSal_ItmDes"] = row_[1];
                  
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