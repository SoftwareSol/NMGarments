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
    public class HeadManager
    {
        private Head head;

        public HeadManager(Head _head)
        {
            head = _head;
        }

        private string GetKey(ISession _iSession)
        {
            string uniqueKey = null;
            ISession session = _iSession;
            try
            {
                session.BeginTransaction();
                string queryString = "select max(cast(HeadID as int)) from Head";

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
            if (head == null)
            {
                return;
            }
            ISession session = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                ITransaction transaction = session.BeginTransaction();

                if (string.IsNullOrEmpty(head.HeadID))
                { head.HeadID = GetKey(session); }

                //if (string.IsNullOrEmpty(city.CityID))
                //{ city.CityID = GetKey(session); }

                session.SaveOrUpdate(head);
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
                session.Delete(head);
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

        public static List<Head> GetList(string Name)
        {
            ISession session = null;
            List<Head> objectsList = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                objectsList = (List<Head>)session.CreateCriteria(typeof(Head)).List<Head>();
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

        public static DataTable GetHeadList()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = "select * from Head";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("HeadID");
                    dT_.Columns.Add("HeadName");
                    dT_.Columns.Add("HeadGeneratedID");
                    dT_.Columns.Add("CreatedAt");
                    dT_.Columns.Add("CreatedBy");
                    dT_.Columns.Add("HeadKey");
                    
                    
                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["HeadID"] = row_[0];
                    dR_["HeadName"] = row_[1];
                    dR_["HeadGeneratedID"] = row_[2];
                    dR_["CreatedAt"] = row_[3];
                    dR_["CreatedBy"] = row_[4];
                    dR_["HeadKey"] = row_[5];                    
                  
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


        public static DataTable GetHeadForDLL()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = "select HeadName, HeadGeneratedID from Head";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("HeadName");
                    dT_.Columns.Add("HeadGeneratedID");
                }

                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["HeadName"] = row_[0];
                    dR_["HeadGeneratedID"] = row_[1];

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

        public static DataTable GetHeadForGrid(string head)
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string searcHead = "Select HeadID, HeadGeneratedID, HeadName from Head where HeadGeneratedID = '" + head + "' or HeadName = '" + head + "'";


                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(searcHead);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("HeadID");
                    dT_.Columns.Add("HeadGeneratedID");
                    dT_.Columns.Add("HeadName");
                }

                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["HeadID"] = row_[0];
                    dR_["HeadGeneratedID"] = row_[1];
                    dR_["HeadName"] = row_[2];

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