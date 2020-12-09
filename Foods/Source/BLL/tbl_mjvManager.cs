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
    public class tbl_mjvManager
    {

        private tbl_mjv Mjv;

        public tbl_mjvManager(tbl_mjv _Mjv)
        {
            Mjv = _Mjv;
        }

        private string GetKey(ISession _iSession)
        {
            string uniqueKey = null;
            ISession session = _iSession;
            try
            {
                session.BeginTransaction();
                string queryString = "select max(cast(mjv_id as int)) from tbl_mjv";

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
            if (Mjv == null)
            {
                return;
            }
            ISession session = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                ITransaction transaction = session.BeginTransaction();

                if (string.IsNullOrEmpty(Mjv.mjv_id))
                { Mjv.mjv_id = GetKey(session); }


                session.SaveOrUpdate(Mjv);
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
                session.Delete(Mjv);
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

        public static List<tbl_mjv> GetList(string Name)
        {
            ISession session = null;
            List<tbl_mjv> objectsList = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                objectsList = (List<tbl_mjv>)session.CreateCriteria(typeof(tbl_mjv)).List<tbl_mjv>();
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

        public static DataTable GetMjvList()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = " select Mjv_sono,ven_nam,CONVERT(VARCHAR(10), Mjv_dat, 105)  as [Mjv_dat],tbl_mjv.CreatedBy,tbl_mjv.CreatedAt,mjv_id,tbl_mjv.ven_id from tbl_mjv  " +
                    " inner join t_ven on tbl_mjv.ven_id = t_ven.ven_id and tbl_mjv.ISActive = 1  order by tbl_mjv.mjv_id desc ";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("Mjv_sono");                   
                    dT_.Columns.Add("ven_nam");
                    dT_.Columns.Add("Mjv_dat");
                    dT_.Columns.Add("CreatedBy");
                    dT_.Columns.Add("CreatedAt");
                    dT_.Columns.Add("mjv_id");
                    dT_.Columns.Add("ven_id");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["Mjv_sono"] = row_[0];                    
                    dR_["ven_nam"] = row_[1];
                    dR_["Mjv_dat"] = row_[2];
                    dR_["CreatedBy"] = row_[3];
                    dR_["CreatedAt"] = row_[4];
                    dR_["mjv_id"] = row_[5];
                    dR_["ven_id"] = row_[6];

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


        public static DataTable GetBRVList()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = "select mjv_id,mjv_sono,CONVERT(VARCHAR(10), mjv_dat, 105)  as [mjv_dat],createdby, CreatedAt  from tbl_mjv where ISActive = '1' and mjv_Vchtyp = 'BRV'";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("mjv_id");
                    dT_.Columns.Add("mjv_sono");
                    dT_.Columns.Add("mjv_dat");
                    dT_.Columns.Add("createdby");
                    dT_.Columns.Add("CreatedAt");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["mjv_id"] = row_[0];
                    dR_["mjv_sono"] = row_[1];
                    dR_["Mjv_dat"] = row_[2];
                    dR_["createdby"] = row_[3];
                    dR_["CreatedAt"] = row_[4];

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

        public static DataTable GetCPVList()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = "select mjv_id,mjv_sono,CONVERT(VARCHAR(10), mjv_dat, 105)  as [mjv_dat],createdby, CreatedAt  from tbl_mjv where ISActive = '1' and mjv_Vchtyp = 'CPV'";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("mjv_id");
                    dT_.Columns.Add("mjv_sono");
                    dT_.Columns.Add("mjv_dat");
                    dT_.Columns.Add("createdby");
                    dT_.Columns.Add("CreatedAt");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["mjv_id"] = row_[0];
                    dR_["mjv_sono"] = row_[1];
                    dR_["Mjv_dat"] = row_[2];
                    dR_["createdby"] = row_[3];
                    dR_["CreatedAt"] = row_[4];

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

        public static DataTable GetCRVList()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = "select mjv_id,mjv_sono,CONVERT(VARCHAR(10), mjv_dat, 105)  as [mjv_dat],createdby, CreatedAt  from tbl_mjv where ISActive = '1' and mjv_Vchtyp = 'CRV'";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("mjv_id");
                    dT_.Columns.Add("mjv_sono");
                    dT_.Columns.Add("mjv_dat");
                    dT_.Columns.Add("createdby");
                    dT_.Columns.Add("CreatedAt");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["mjv_id"] = row_[0];
                    dR_["mjv_sono"] = row_[1];
                    dR_["Mjv_dat"] = row_[2];
                    dR_["createdby"] = row_[3];
                    dR_["CreatedAt"] = row_[4];

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

        public static DataTable GetBPVList()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = "select mjv_id,mjv_sono,CONVERT(VARCHAR(10), mjv_dat, 105)  as [mjv_dat],createdby, CreatedAt  from tbl_mjv where ISActive = '1' and mjv_Vchtyp = 'BPV'";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("mjv_id");
                    dT_.Columns.Add("mjv_sono");
                    dT_.Columns.Add("mjv_dat");
                    dT_.Columns.Add("createdby");
                    dT_.Columns.Add("CreatedAt");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["mjv_id"] = row_[0];
                    dR_["mjv_sono"] = row_[1];
                    dR_["Mjv_dat"] = row_[2];
                    dR_["createdby"] = row_[3];
                    dR_["CreatedAt"] = row_[4];

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

        public static DataTable GetJVList()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = "select mjv_id,mjv_sono,CONVERT(VARCHAR(10), mjv_dat, 105)  as [mjv_dat],createdby, CreatedAt  from tbl_mjv where ISActive = '1' and mjv_Vchtyp = 'JV'";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("mjv_id");
                    dT_.Columns.Add("mjv_sono");
                    dT_.Columns.Add("mjv_dat");
                    dT_.Columns.Add("createdby");
                    dT_.Columns.Add("CreatedAt");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["mjv_id"] = row_[0];
                    dR_["mjv_sono"] = row_[1];
                    dR_["Mjv_dat"] = row_[2];
                    dR_["createdby"] = row_[3];
                    dR_["CreatedAt"] = row_[4];

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