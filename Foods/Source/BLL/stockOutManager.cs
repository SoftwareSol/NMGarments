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
    public class stockOutManager
    {
        private stockOut stockout;

        public stockOutManager(stockOut _stockout)
        {
            stockout = _stockout;
        }

        private string GetKey(ISession _iSession)
        {
            string uniqueKey = null;
            ISession session = _iSession;
            try
            {
                session.BeginTransaction();
                string queryString = "select max(cast(StockOutID as int)) from stockOut";

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
            if (stockout == null)
            {
                return;
            }
            ISession session = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                ITransaction transaction = session.BeginTransaction();

                if (string.IsNullOrEmpty(stockout.StockOutID))
                { stockout.StockOutID = GetKey(session); }

                if (string.IsNullOrEmpty(stockout.StockOutID))
                { stockout.StockOutID = GetKey(session); }

                session.SaveOrUpdate(stockout);
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
                session.Delete(stockout);
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

        public static List<stockOutManager> GetList(string Name)
        {
            ISession session = null;
            List<stockOutManager> objectsList = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                objectsList = (List<stockOutManager>)session.CreateCriteria(typeof(stockOutManager)).List<stockOutManager>();
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

        public static DataTable GetStckOtList()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = " select tbl_Mstk.Mstk_id as [ID], convert(varchar(200), Mstk_dat, 101) as [Date],MSal_sono as [SNO],CustomerName, " +
                    " Mstk_Rmk as [Rmk] from tbl_Mstk " +
                    " inner join tbl_MSal on tbl_Mstk.MSal_id = tbl_MSal.MSal_id " +
                    " inner join Customers_ on tbl_Mstk.CustomerID = Customers_.CustomerID";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("ID");
                    dT_.Columns.Add("Date");
                    dT_.Columns.Add("CustomerName");
                    dT_.Columns.Add("SNO");
                    dT_.Columns.Add("Rmk");
                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();

                    dR_["ID"] = row_[0];
                    dR_["Date"] = row_[1];
                    dR_["CustomerName"] = row_[2];
                    dR_["SNO"] = row_[3];
                    dR_["Rmk"] = row_[4];

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