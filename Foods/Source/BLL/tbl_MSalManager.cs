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
    public class tbl_MSalManager
    {
         private tbl_MSal MSal;

         public tbl_MSalManager(tbl_MSal _MSal)
        {
            MSal = _MSal;
        }

        private string GetKey(ISession _iSession)
        {
            string uniqueKey = null;
            ISession session = _iSession;
            try
            {
                session.BeginTransaction();
                string queryString = "select  max(cast(MSal_id as int)) from tbl_MSal";

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
            if (MSal == null)
            {
                return;
            }
            ISession session = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                ITransaction transaction = session.BeginTransaction();

                if (string.IsNullOrEmpty(MSal.MSal_id))
                { MSal.MSal_id = GetKey(session); }

                session.SaveOrUpdate(MSal);
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
                session.Delete(MSal);
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

        public static List<tbl_MSal> GetList(string Name)
        {
            ISession session = null;
            List<tbl_MSal> objectsList = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                objectsList = (List<tbl_MSal>)session.CreateCriteria(typeof(tbl_MSal)).List<tbl_MSal>();
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



        public static DataTable GetSAL()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = " select MSal_id,rtrim('[' + CAST(replace(convert(NVARCHAR, MSal_dat, 106), ' ', '-') AS VARCHAR(200)) + ']-' + MSal_sono ) as [MSal_sono] " +
                    " from tbl_MSal where ISActive = 'True' ";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("MSal_id");
                    dT_.Columns.Add("MSal_sono");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["MSal_id"] = row_[0];
                    dR_["MSal_sono"] = row_[1];

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
        
        public static DataTable GetSO()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = " select MSalOrdid,rtrim('[' + CAST(MSalOrdid AS VARCHAR(200)) + ']-' + MSalOrdsono ) as [MSalOrdsono] " +
                    " from tbl_MSalOrd ";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("MSalOrdid");
                    dT_.Columns.Add("MSalOrdsono");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["MSalOrdid"] = row_[0];
                    dR_["MSalOrdsono"] = row_[1];

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
        
        public static DataTable GetMSalList()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = " select MSal_id,MSal_sono,CustomerName,MSal_dat,tbl_MSal.CreatedBy,tbl_MSal.CreatedAt from tbl_MSal " +
                    " inner join Customers_ on tbl_MSal.CustomerID = Customers_.CustomerID";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("MSal_id");
                    dT_.Columns.Add("MSal_sono");
                    dT_.Columns.Add("CustomerName");
                    dT_.Columns.Add("MSal_dat");
                    dT_.Columns.Add("CreatedBy");
                    dT_.Columns.Add("CreatedAt");

                    
                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();

                    dR_["MSal_id"] = row_[0];
                    dR_["MSal_sono"] = row_[1];
                    dR_["CustomerName"] = row_[2];
                    dR_["MSal_dat"] = row_[3];
                    dR_["CreatedBy"] = row_[4];
                    dR_["CreatedAt"] = row_[5];

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


        public static DataTable GetMSalList(string sono)
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = " select MSal_id,MSal_sono,tbl_MSal.CustomerID,SubHeadCategoriesName as [CustomerName],MSal_dat,tbl_MSal.CreatedBy,tbl_MSal.CreatedAt from tbl_MSal " +
                    " inner join SubHeadCategories on tbl_MSal.custacc = SubHeadCategories.SubHeadCategoriesGeneratedID where MSal_sono = '" + sono + "'";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("MSal_id");
                    dT_.Columns.Add("MSal_sono");
                    dT_.Columns.Add("CustomerID");
                    dT_.Columns.Add("CustomerName");
                    dT_.Columns.Add("MSal_dat");
                    dT_.Columns.Add("CreatedBy");
                    dT_.Columns.Add("CreatedAt");


                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();

                    dR_["MSal_id"] = row_[0];
                    dR_["MSal_sono"] = row_[1];
                    dR_["CustomerID"] = row_[2];                    
                    dR_["CustomerName"] = row_[3];
                    dR_["MSal_dat"] = row_[4];
                    dR_["CreatedBy"] = row_[5];
                    dR_["CreatedAt"] = row_[6];

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