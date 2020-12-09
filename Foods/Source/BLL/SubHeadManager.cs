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
    public class SubHeadManager
    {
        private SubHead subhead;

        public SubHeadManager(SubHead _subhead)
        {
            subhead = _subhead;
        }

        private string GetKey(ISession _iSession)
        {
            string uniqueKey = null;
            ISession session = _iSession;
            try
            {
                session.BeginTransaction();
                string queryString = "select max(cast(SubHeadID as int)) from SubHead";

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
            if (subhead == null)
            {
                return;
            }
            ISession session = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                ITransaction transaction = session.BeginTransaction();

                if (string.IsNullOrEmpty(subhead.SubHeadID))
                { subhead.SubHeadID = GetKey(session); }

                //if (string.IsNullOrEmpty(city.CityID))
                //{ city.CityID = GetKey(session); }

                session.SaveOrUpdate(subhead);
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
                session.Delete(subhead);
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

        public static List<SubHead> GetList(string Name)
        {
            ISession session = null;
            List<SubHead> objectsList = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                objectsList = (List<SubHead>)session.CreateCriteria(typeof(SubHead)).List<SubHead>();
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
          
        public static DataTable GetSubHeadList()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = "select * from SubHead where SubHeadName != 'Del' and SubHeadName !='NULL'";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("SubHeadID");
                    dT_.Columns.Add("SubHeadName");
                    dT_.Columns.Add("SubHeadGeneratedID");
                    dT_.Columns.Add("HeadGeneratedID");
                    dT_.Columns.Add("CreatedBy");
                    dT_.Columns.Add("CraetedAt");
                    dT_.Columns.Add("SubHeadKey");
                    
                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["SubHeadID"] = row_[0];
                    dR_["SubHeadName"] = row_[1];
                    dR_["SubHeadGeneratedID"] = row_[2];
                    dR_["HeadGeneratedID"] = row_[3];
                    dR_["CreatedBy"] = row_[4];
                    dR_["CraetedAt"] = row_[5];
                    dR_["SubHeadKey"] = row_[6];
                  
                  
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

        public static DataTable GetSubHeadForGrid(string SubAccName)
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                //string queryString = "SELECT HeadGeneratedID, SubHeadID, SubHeadGeneratedID, SubHeadName FROM SubHead where SubHeadGeneratedID ='" + SubAccName + "' or SubHeadName ='" + SubAccName + "'";
                string searcSubHead = "Select  HeadGeneratedID, SubHeadID, SubHeadGeneratedID, SubHeadName from SubHead where HeadGeneratedID = '" + SubAccName + "' or SubHeadGeneratedID = '" + SubAccName +
                                      "' or SubHeadName = '" + SubAccName + "'";
                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(searcSubHead);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("HeadGeneratedID");
                    dT_.Columns.Add("SubHeadID");
                    dT_.Columns.Add("SubHeadGeneratedID");
                    dT_.Columns.Add("SubHeadName");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();

                    dR_["HeadGeneratedID"] = row_[0];
                    dR_["SubHeadID"] = row_[1];
                    dR_["SubHeadGeneratedID"] = row_[2];
                    dR_["SubHeadName"] = row_[3];


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

        public static DataTable GetSubHead(string AccountName)
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = "SELECT  SubHeadGeneratedID, SubHeadName FROM SubHead where HeadGeneratedID ='" + AccountName + "' and SubHeadName not like '%DEL%'";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("SubHeadGeneratedID");
                    dT_.Columns.Add("SubHeadName");
                    
                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["SubHeadGeneratedID"] = row_[0];
                    dR_["SubHeadName"] = row_[1];
                  
                  
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