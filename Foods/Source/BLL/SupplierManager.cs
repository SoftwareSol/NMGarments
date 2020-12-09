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
    public class SupplierManager
    {
        private supplier supplier;

        public SupplierManager(supplier _supplier)
        {
            supplier = _supplier;
        }

        private string GetKey(ISession _iSession)
        {
            string uniqueKey = null;
            ISession session = _iSession;
            try
            {
                session.BeginTransaction();
                string queryString = "select max(cast(supplierId as int)) from supplier";

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
            if (supplier == null)
            {
                return;
            }
            ISession session = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                ITransaction transaction = session.BeginTransaction();

                if (string.IsNullOrEmpty(supplier.supplierId))
                { supplier.supplierId = GetKey(session); }


                if (string.IsNullOrEmpty(supplier.supplierId))
                {
                    supplier.supplierId = GetKey(session);
                }

                session.SaveOrUpdate(supplier);
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
                session.Delete(supplier);
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

        public static List<supplier> GetList(string Name)
        {
            ISession session = null;
            List<supplier> objectsList = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                objectsList = (List<supplier>)session.CreateCriteria(typeof(supplier)).List<supplier>();
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

        public static DataTable DDLCategory()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = " select CategoryID,Category from CustomerCategory";
                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("CategoryID");
                    dT_.Columns.Add("Category");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();

                    dR_["CategoryID"] = row_[0];
                    dR_["Category"] = row_[1];

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

        public static DataTable DDLCustomerType()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = " select CustomerTypeID,CustomerType_ from CustomerType";
                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("CustomerTypeID");
                    dT_.Columns.Add("CustomerType_");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();

                    dR_["CustomerTypeID"] = row_[0];
                    dR_["CustomerType_"] = row_[1];

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

        public static DataTable DDLCity()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = " select CityID,City_ from City";
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

        public static DataTable GetSupplierList()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = " select a.supplierId, a.suppliername, a.contactperson, a.BackUpContact, a.City_, a.phoneno, a.mobile, a.faxno, a.postalcode, a.designation, " +
                    " a.AddressOne, a.AddressTwo, a.CNIC, a.Url, a.BusinessNature, a.Email, a.NTNNTRNo, a.CreatedBy, convert(date, cast(a.CreatedDate as date) ,105) as [CreatedAt], a.IsActive, a.Sup_Code from supplier a order by a.supplierId desc";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("supplierId");
                    dT_.Columns.Add("suppliername");
                    dT_.Columns.Add("contactperson");
                    dT_.Columns.Add("BackUpContact");
                    dT_.Columns.Add("City_");
                    dT_.Columns.Add("phoneno");
                    dT_.Columns.Add("mobile");
                    dT_.Columns.Add("faxno");
                    dT_.Columns.Add("postalcode");
                    dT_.Columns.Add("designation");
                    dT_.Columns.Add("AddressOne");
                    dT_.Columns.Add("AddressTwo");
                    dT_.Columns.Add("CNIC");
                    dT_.Columns.Add("Url");
                    dT_.Columns.Add("BusinessNature");
                    dT_.Columns.Add("Email");
                    dT_.Columns.Add("NTNNTRNo");
                    dT_.Columns.Add("CreatedBy");
                    dT_.Columns.Add("CreatedDate");
                    dT_.Columns.Add("IsActive");
                    dT_.Columns.Add("Sup_Code");
                    
                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["supplierId"] = row_[0];
                    dR_["suppliername"] = row_[1];
                    dR_["contactperson"] = row_[2];
                    dR_["BackUpContact"] = row_[3];
                    dR_["City_"] = row_[4];
                    dR_["phoneno"] = row_[5];
                    dR_["mobile"] = row_[6];
                    dR_["faxno"] = row_[7];
                    dR_["postalcode"] = row_[8];
                    dR_["designation"] = row_[9];
                    dR_["AddressOne"] = row_[10];
                    dR_["AddressTwo"] = row_[11];
                    dR_["CNIC"] = row_[12];
                    dR_["Url"] = row_[13];
                    dR_["BusinessNature"] = row_[14];
                    dR_["Email"] = row_[15];
                    dR_["NTNNTRNo"] = row_[16];
                    dR_["CreatedBy"] = row_[17];
                    dR_["CreatedDate"] = row_[18];
                    dR_["IsActive"] = row_[19];
                    dR_["Sup_Code"] = row_[20];
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


        public static DataTable GetVen()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = " select ven_id,rtrim('[' + CAST(ven_nam AS VARCHAR(200)) + ']') as [VendorName] from t_ven  where ISActive = 'True' ";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("ven_id");
                    dT_.Columns.Add("VendorName");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["ven_id"] = row_[0];
                    dR_["VendorName"] = row_[1];

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