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
    public class MPurchaseManager
    {
        private MPurchase mPureMng;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);

        public MPurchaseManager(MPurchase _mPureMng)
        {
            mPureMng = _mPureMng;
        }

        private string GetKey(ISession _iSession)
        {
            string uniqueKey = null;
            ISession session = _iSession;
            try
            {
                session.BeginTransaction();
                string queryString = "select max(cast(MPurID as int)) from MPurchase";

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
            if (mPureMng == null)
            {
                return;
            }
            ISession session = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                ITransaction transaction = session.BeginTransaction();

                if (string.IsNullOrEmpty(mPureMng.MPurID))
                { mPureMng.MPurID = GetKey(session); }

                session.SaveOrUpdate(mPureMng);
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
                session.Delete(mPureMng);
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

        public static List<MPurchaseManager> GetList(string Name)
        {
            ISession session = null;
            List<MPurchaseManager> objectsList = null;
            try
            {
                session = NHibernateHelper.GetCurrentSession();
                objectsList = (List<MPurchaseManager>)session.CreateCriteria(typeof(MPurchaseManager)).List<MPurchaseManager>();
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

        public static DataTable BindVenDDL()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = " select rtrim('[' + CAST(ven_id AS VARCHAR(200)) + ']-' + ven_nam ) as [ven_nam], ven_id  from t_ven where IsActive = 1";
                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("ven_nam");
                    dT_.Columns.Add("ven_id");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();

                    dR_["ven_nam"] = row_[0];
                    dR_["ven_id"] = row_[1];

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

        public static DataTable BindCusDDL()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = " select rtrim('[' + CAST(CustomerID AS VARCHAR(200)) + ']-' + CustomerName ) as [CustomerName], CustomerID  from Customers_";
                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("CustomerName");
                    dT_.Columns.Add("CustomerID");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();

                    dR_["CustomerName"] = row_[0];
                    dR_["CustomerID"] = row_[1];

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

        public static DataTable GetMPurList()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                //string queryString = " select MPurID, subheadcategoryfivename,VndrAdd,VndrCntct,PurNo,MPurDate,CNIC,NTNNo,MPurchase.CreatedBy,MPurchase.CreatedAt,ToBePrntd from MPurchase " +
                  //  " inner join subheadcategoryfive on MPurchase.ven_id = subheadcategoryfive.subheadcategoryfiveID ";

                string queryString = " select MPurID, suppliername,VndrAdd,VndrCntct,PurNo,MPurDate,supplier.CNIC,NTNNo, " +
                    " MPurchase.CreatedBy,MPurchase.CreatedAt,ToBePrntd from MPurchase  inner join " +
                    " supplier on MPurchase.ven_id = supplier.supplierId ";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {  
                    dT_.Columns.Add("MPurID");
                    dT_.Columns.Add("suppliername");
                    dT_.Columns.Add("VndrAdd");
                    dT_.Columns.Add("VndrCntct");
                    dT_.Columns.Add("PurNo");
                    dT_.Columns.Add("MPurDate");
                    dT_.Columns.Add("CNIC");
                    dT_.Columns.Add("NTNNo");
                    dT_.Columns.Add("CreatedBy");
                    dT_.Columns.Add("CreatedAt");
                    dT_.Columns.Add("ToBePrntd");                    
                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["MPurID"] = row_[0];
                    dR_["suppliername"] = row_[1];
                    dR_["VndrAdd"] = row_[2];
                    dR_["VndrCntct"] = row_[3];
                    dR_["PurNo"] = row_[4];
                    dR_["MPurDate"] = row_[5];
                    dR_["CNIC"] = row_[6];
                    dR_["NTNNo"] = row_[7];
                    dR_["CreatedBy"] = row_[8];
                    dR_["CreatedAt"] = row_[9];
                    dR_["ToBePrntd"] = row_[9];
                  
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

        public static DataTable GetDPurList(string MPurID)
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = "select DPurID,MPurID,ProDes,Qty,Unit,Cost,NetTotal from DPurchase where MPurID =" + MPurID + "";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("MPurID");
                    dT_.Columns.Add("DPurID");
                    dT_.Columns.Add("ProDes");
                    dT_.Columns.Add("Qty");
                    dT_.Columns.Add("Unit");
                    dT_.Columns.Add("Cost");
                    dT_.Columns.Add("NetTotal");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["MPurID"] = row_[0];
                    dR_["DPurID"] = row_[1];
                    dR_["ProDes"] = row_[2];
                    dR_["Qty"] = row_[3];
                    dR_["Unit"] = row_[4];
                    dR_["Cost"] = row_[5];
                    dR_["NetTotal"] = row_[6];
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

        public static DataTable GetMPurList(string purno)
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                //string query = "select MPurID,ven_id,VndrAdd,VndrCntct,PurNo,MPurDate,CNIC,NTNNo,CreatedBy,CreatedAt,ToBePrntd from MPurchase where PurNo = '" + purno + "'";
                string query = "select MPurID, suppliername,VndrAdd,VndrCntct,PurNo,MPurDate,CNIC,NTNNo,MPurchase.CreatedBy,MPurchase.CreatedAt,ToBePrntd from MPurchase " +
                    " inner join supplier on MPurchase.ven_id = supplier.supplierId where PurNo = '" + purno + "'";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(query);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("MPurID");
                    dT_.Columns.Add("suppliername");
                    dT_.Columns.Add("VndrAdd");
                    dT_.Columns.Add("VndrCntct");
                    dT_.Columns.Add("PurNo");
                    dT_.Columns.Add("MPurDate");
                    dT_.Columns.Add("CNIC");
                    dT_.Columns.Add("NTNNo");
                    dT_.Columns.Add("CreatedBy");
                    dT_.Columns.Add("CreatedAt");
                    dT_.Columns.Add("ToBePrntd");
                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["MPurID"] = row_[0];
                    dR_["suppliername"] = row_[1];
                    dR_["VndrAdd"] = row_[2];
                    dR_["VndrCntct"] = row_[3];
                    dR_["PurNo"] = row_[4];
                    dR_["MPurDate"] = row_[5];
                    dR_["CNIC"] = row_[6];
                    dR_["NTNNo"] = row_[7];
                    dR_["CreatedBy"] = row_[8];
                    dR_["CreatedAt"] = row_[9];
                    dR_["ToBePrntd"] = row_[9];

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

        public static DataTable GetReqforPO(string venid)
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = " select MReq_sono,convert(date, cast(tbl_MReq.MReq_dat as date) ,105) as [MReq_dat],tbl_MReq.ven_id,ven_nam,ven_add from tbl_MReq " +
                    " inner join t_ven on tbl_MReq.ven_id = t_ven.ven_id " +
                    " where tbl_MReq.ven_id =" + venid + "";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("MReq_sono");
                    dT_.Columns.Add("MReq_dat");
                    dT_.Columns.Add("ven_id");
                    dT_.Columns.Add("ven_nam");
                    dT_.Columns.Add("ven_add");
                }

                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["MReq_sono"] = row_[0];
                    dR_["MReq_dat"] = row_[1];
                    dR_["ven_id"] = row_[2];
                    dR_["ven_nam"] = row_[3];
                    dR_["ven_add"] = row_[4];

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

        public static DataTable GetShpPO(string cusid)
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = "select CustomerID,CustomerName,[Address] from Customers_ where CustomerID =" + cusid + "";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("CustomerID");
                    dT_.Columns.Add("CustomerName");
                    dT_.Columns.Add("Address");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["CustomerID"] = row_[0];
                    dR_["CustomerName"] = row_[1];
                    dR_["Address"] = row_[2];


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


        public static DataTable GetPur()
        {
            ISession session = null;
            IList objectsList = null;
            DataTable dT_ = new DataTable();
            DataRow dR_ = null;
            try
            {
                string queryString = " select MPurID,rtrim('[' + CAST(ven_nam AS VARCHAR(200)) + ']') as [PUR] from MPurchase  inner join t_ven on MPurchase.ven_id = t_ven.ven_id ";

                session = NHibernateHelper.GetCurrentSession();
                IQuery iQuery = session.CreateSQLQuery(queryString);
                objectsList = iQuery.List();
                {
                    dT_.Columns.Add("MPurID");
                    dT_.Columns.Add("PUR");

                }
                foreach (object[] row_ in objectsList)
                {
                    dR_ = dT_.NewRow();
                    dR_["MPurID"] = row_[0];
                    dR_["PUR"] = row_[1];

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