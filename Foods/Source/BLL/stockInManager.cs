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
    public class stockInManager
    {
        private stockIn stockin;

        public stockInManager(stockIn _stockin)
        {
            stockin = _stockin;
        }

        private string GetKey(ISession _iSession)
        {
            string uniqueKey = null;
            ISession session = _iSession;
            try
            {
                session.BeginTransaction();
                string queryString = "select max(cast(StockInID as int)) from stockIn";

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
            if (stockin == null)
            {
                return;
            }
            ISession session = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                ITransaction transaction = session.BeginTransaction();

                if (string.IsNullOrEmpty(stockin.StockInID))
                { stockin.StockInID = GetKey(session); }

                if (string.IsNullOrEmpty(stockin.StockInID))
                { stockin.StockInID = GetKey(session); }

                session.SaveOrUpdate(stockin);
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
                session.Delete(stockin);
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

        public static List<stockInManager> GetList(string Name)
        {
            ISession session = null;
            List<stockInManager> objectsList = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                objectsList = (List<stockInManager>)session.CreateCriteria(typeof(stockInManager)).List<stockInManager>();
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
        
        public static DataTable GetstckInList()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = " select tbl_Mstk.Mstk_id as [ID], convert(varchar(200), Mstk_dat, 101) as [Date],suppliername as [VendorName],PurNo as [PNO], " +
                    " Mstk_Rmk as [Rmk] from tbl_Mstk " +
                    " inner join MPurchase on tbl_Mstk.MPurID = MPurchase.MPurID " +
                    " inner join supplier on tbl_Mstk.ven_id = supplier.supplierId";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("ID");
                    dT_.Columns.Add("Date");
                    dT_.Columns.Add("VendorName");
                    dT_.Columns.Add("PNO");
                    dT_.Columns.Add("Rmk");                    
                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["ID"] = row_[0];
                    dR_["Date"] = row_[1];
                    dR_["VendorName"] = row_[2];
                    dR_["PNO"] = row_[3];
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

        public static DataTable BindDDL()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = "select ProductID,ProductName from Products";

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

        public static DataTable Getwh()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = " select wh_id, rtrim('[' + CAST(wh_id AS VARCHAR(200)) + ']-' + wh_nam ) as [wh_nam] from tbl_wh";
                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("wh_id");
                    dT_.Columns.Add("wh_nam");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();

                    dR_["wh_id"] = row_[0];
                    dR_["wh_nam"] = row_[1];

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