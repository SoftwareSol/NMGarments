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
    public class CustomerManager
    {
        private Customers_ Customers;

        public CustomerManager(Customers_ _Customers)
        {
            Customers = _Customers;
        }

        private string GetKey(ISession _iSession)
        {
            string uniqueKey = null;
            ISession session = _iSession;
            try
            {
                session.BeginTransaction();
                string queryString = "select max(cast(CustomerID as int)) from Customers_";

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
            if (Customers == null)
            {
                return;
            }
            ISession session = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                ITransaction transaction = session.BeginTransaction();

                if (string.IsNullOrEmpty(Customers.CustomerID))
                { Customers.CustomerID = GetKey(session); }


                if (string.IsNullOrEmpty(Customers.CustomerID))
                {
                    Customers.CustomerID = GetKey(session);
                }

                session.SaveOrUpdate(Customers);
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
                session.Delete(Customers);
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

        public static List<Customers_> GetList(string Name)
        {
            ISession session = null;
            List<Customers_> objectsList = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                objectsList = (List<Customers_>)session.CreateCriteria(typeof(Customers_)).List<Customers_>();
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

        public static DataTable GetCustomerList()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
               // string queryString = " select a.customerid, a.CustomerName, a.GST,c.CategoryID, c.category, a.ntn, b.CustomerTypeID, b.customertype_, a.area,  " +
                 //   " a.refnum, a.district, a.phoneno, a.email, a.cellno1, a.postalcode, a.CellNo2, a.PostalOfficeContact, a.CellNo3,  " +
                   // " a.NIC, a.CellNo4, d.CityID, d.city_, a.Address  from Customers_ a inner join customertype b on  " +
                    //" a.customertypeid = b.customertypeid inner join CustomerCategory c on a.categoryid = c.categoryid inner join City d on a.cityid = d.cityid ";

                string queryString = " select a.customerid, a.CustomerName, a.GST,c.CategoryID, c.category, a.ntn, b.CustomerTypeID, b.customertype_, a.area,  " +
                    " a.refnum, a.district, a.phoneno, a.email, a.cellno1, a.postalcode, a.CellNo2, a.PostalOfficeContact, a.CellNo3, " +
                    " a.NIC, a.CellNo4, d.CityID, d.city_, a.Address  from Customers_ a inner join customertype b on  " +
                    " a.customertype_ = b.customertype_ inner join CustomerCategory c on a.category = c.category inner join City d on a.city_ = d.city_ ";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("CustomerID");
                    dT_.Columns.Add("CustomerName");
                    dT_.Columns.Add("GST");
                    dT_.Columns.Add("CategoryID");   
                    dT_.Columns.Add("category");                                   
                    dT_.Columns.Add("NTN");
                    dT_.Columns.Add("CustomerTypeID");
                    dT_.Columns.Add("customertype_");                 
                    dT_.Columns.Add("Area");
                    dT_.Columns.Add("RefNum");
                    dT_.Columns.Add("District");
                    dT_.Columns.Add("PhoneNo");
                    dT_.Columns.Add("Email");
                    dT_.Columns.Add("CellNo1");
                    dT_.Columns.Add("PostalCode");
                    dT_.Columns.Add("CellNo2");
                    dT_.Columns.Add("PostalOfficeContact");
                    dT_.Columns.Add("CellNo3");
                    dT_.Columns.Add("NIC");
                    dT_.Columns.Add("CellNo4");
                    dT_.Columns.Add("CityID");
                    dT_.Columns.Add("city_");
                    dT_.Columns.Add("Address");
                    //dT_.Columns.Add("CreatedBy");
                    //dT_.Columns.Add("CreatedDate");
                    //dT_.Columns.Add("IsActive");
                    
                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["CustomerID"] = row_[0];
                    dR_["CustomerName"] = row_[1];
                    dR_["GST"] = row_[2];
                    dR_["CategoryID"] = row_[3];
                    dR_["category"] = row_[4];
                    
                    dR_["NTN"] = row_[5];
                    dR_["CustomerTypeID"] = row_[6];                    
                    dR_["customertype_"] = row_[7];
                    dR_["Area"] = row_[8];
                    dR_["RefNum"] = row_[9];
                    dR_["District"] = row_[10];
                    dR_["PhoneNo"] = row_[11];
                    dR_["Email"] = row_[12];
                    dR_["CellNo1"] = row_[13];
                    dR_["PostalCode"] = row_[14];
                    dR_["CellNo2"] = row_[15];
                    dR_["PostalOfficeContact"] = row_[16];
                    dR_["CellNo3"] = row_[17];
                    dR_["NIC"] = row_[18];
                    dR_["CellNo4"] = row_[19];
                    dR_["CityID"] = row_[20];
                    dR_["city_"] = row_[21];                    
                    dR_["Address"] = row_[22];
                    //dR_["CreatedBy"] = row_[19];
                    //dR_["CreatedDate"] = row_[20];
                    //dR_["IsActive"] = row_[21];

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


        public static DataTable GetCust()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = " select CustomerID,rtrim('[' + CAST(CustomerID AS VARCHAR(200)) + ']-' + CustomerName ) as [CustomerName] " +
                    " from Customers_ where ISActive = 'True' ";

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
    }
}