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
    public class tbl_MProQuotManager
    {
        private tbl_MProQuot MProQuot;

        public tbl_MProQuotManager(tbl_MProQuot _MProQuot)
        {
            MProQuot = _MProQuot;
        }

        private string GetKey(ISession _iSession)
        {
            string uniqueKey = null;
            ISession session = _iSession;
            try
            {
                session.BeginTransaction();
                string queryString = "select max(cast(MProQuot_id as int)) from tbl_MProQuot";

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
            if (MProQuot == null)
            {
                return;
            }
            ISession session = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                ITransaction transaction = session.BeginTransaction();

                if (string.IsNullOrEmpty(MProQuot.MProQuot_id))
                { MProQuot.MProQuot_id = GetKey(session); }


                session.SaveOrUpdate(MProQuot);
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
                session.Delete(MProQuot);
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

        public static List<tbl_MProQuot> GetList(string Name)
        {
            ISession session = null;
            List<tbl_MProQuot> objectsList = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                objectsList = (List<tbl_MProQuot>)session.CreateCriteria(typeof(tbl_MProQuot)).List<tbl_MProQuot>();
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

        public static DataTable BindCust()
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

        public static DataTable BindEmpDDL()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = " select rtrim('[' + CAST(employeeID AS VARCHAR(200)) + ']-' + employeeName ) as [employeeName], employeeID  from tbl_employee";
                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("employeeID");
                    dT_.Columns.Add("employeeName");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();

                    dR_["employeeID"] = row_[0];
                    dR_["employeeName"] = row_[1];

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

        public static DataTable GetQuotationList()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                //string queryString = " select tbl_MProQuot.MProQuot_id,DProQuot_id,MProQuot_dat,MProQuot_rmk,tbl_MProQuot.CustomerID, " +
                //    " CustomerName, tbl_DProQuot.ProductID, ProductName, MProQuot_app ,MProQuot_tamt," +
                //    " MProQuot_Rej, tbl_MProQuot.CreatedBy, tbl_MProQuot.CreatedAt, tbl_MProQuot.ISActive " +
                //    " from tbl_MProQuot " +
                //    " inner join tbl_DProQuot on tbl_MProQuot.MProQuot_id = tbl_DProQuot.MProQuot_id and tbl_MProQuot.ISActive = tbl_DProQuot.ISActive " +
                //    " inner join Products on tbl_DProQuot.ProductID = Products.ProductID " +
                //    " inner join Customers_ on tbl_MProQuot.CustomerID = Customers_.CustomerID " +
                //    " where MProQuot_app = 0 and MProQuot_Rej = 0 order by tbl_MProQuot.MProQuot_id desc  ";
                string queryString = " select MProQuot_sono,MProQuot_dat,CustomerName,MProQuot_rmk,  tbl_MProQuot.CreatedBy, tbl_MProQuot.CreatedAt,tbl_MProQuot.MProQuot_id,tbl_MProQuot.CustomerID, " +
                    "  MProQuot_app, MProQuot_Rej, tbl_MProQuot.ISActive " +
                    " from tbl_MProQuot " +
                    " inner join Customers_ on tbl_MProQuot.CustomerID = Customers_.CustomerID " +
                    " where MProQuot_app = 0 and MProQuot_Rej = 0  and tbl_MProQuot.ISActive = 1  order by tbl_MProQuot.MProQuot_id desc";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("MProQuot_sono");
                    dT_.Columns.Add("MProQuot_dat");
                    dT_.Columns.Add("CustomerName");
                    dT_.Columns.Add("MProQuot_rmk");
                    dT_.Columns.Add("CreatedBy");
                    dT_.Columns.Add("CreatedAt");
                    dT_.Columns.Add("MProQuot_Rej");
                    dT_.Columns.Add("CustomerID");
                    dT_.Columns.Add("MProQuot_app");
                    dT_.Columns.Add("ISActive");
                    dT_.Columns.Add("MProQuot_id");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["MProQuot_sono"] = row_[0];
                    dR_["MProQuot_dat"] = row_[1];
                    dR_["CustomerName"] = row_[2];
                    dR_["MProQuot_rmk"] = row_[3];
                    dR_["CreatedBy"] = row_[4];
                    dR_["CreatedAt"] = row_[5];
                    dR_["MProQuot_Rej"] = row_[6];
                    dR_["CustomerID"] = row_[7];
                    dR_["MProQuot_app"] = row_[8];
                    dR_["ISActive"] = row_[9];
                    dR_["MProQuot_id"] = row_[10];

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