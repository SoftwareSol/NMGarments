using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO;

using NHibernate;
using NHibernate.Criterion;
using Project;

namespace Foods
{
    public partial class MReq : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_ = null;
        int i = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillGrid();
                BindEmp();
                SetInitRowReq();
                ck_act.Checked = true;
                lbl_err.ForeColor = System.Drawing.Color.Red;
                TBReq_dat.Text = DateTime.Now.ToShortDateString();
                BindDpt();
                txtsono.Enabled = false;
                ptnSno();
            }
        }
        #region Events
        protected void SeacrhBtn_Click(object sender, EventArgs e)
        {

        }

        protected void linkbtnadd_Click(object sender, EventArgs e)
        {
 
        }

        protected void GVReq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GVReq_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void GVReq_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void GVDetReq_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Miscelleious

        private void ptnSno()
        {
            try
            {

                string str = "select isnull(max(cast(MReq_id as int)),0) as [MReq_id]  from tbl_MReq";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (string.IsNullOrEmpty(txtsono.Text))
                    {
                        int v = Convert.ToInt32(reader["MReq_id"].ToString());
                        int b = v + 1;
                        txtsono.Text = "Req00" + b.ToString();
                        //                        TBSubAccGeneratedID.Text = "SubAcc0" + b.ToString();
                        //                        TBAccountCategoryNameID.Text = "SubAccCat00" + b.ToString();
                        //TBaccountcategoriesfourGeneratedID.Text = "SubAccCat010" + b.ToString();
                        //TBsubaccountcategoriesfiveGeneratedID
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void FillGrid()
        {
            try
            {
                DataTable dtGetReq_ = new DataTable();

                //dtGetReq_ = tbl_MReqManager.GetRequisitionList();
                
                GVReq.DataSource = dtGetReq_;
                GVReq.DataBind();
                ViewState["GetReq"] = dtGetReq_;
            }
            catch (Exception ex)
            {
                throw;
                //lbl_err.Text = ex.Message;                                 
            }
        }
        private void SetPreRowReq()
        {
            try
            {
                //BindPro();
                int rowIndex = 0;
                if (ViewState["dt_adItm"] != null)
                {
                    DataTable dt = (DataTable)ViewState["dt_adItm"];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DropDownList ddlpro = (DropDownList)GVDetReq.Rows[rowIndex].Cells[0].FindControl("DDL_Pro");
                            TextBox Tbproqty = (TextBox)GVDetReq.Rows[rowIndex].Cells[1].FindControl("TBProQty");
                            TextBox Tbprorat = (TextBox)GVDetReq.Rows[rowIndex].Cells[2].FindControl("TBProrat");
                            TextBox Tbamt = (TextBox)GVDetReq.Rows[rowIndex].Cells[3].FindControl("TBAmt");
                            TextBox Tbnarr = (TextBox)GVDetReq.Rows[rowIndex].Cells[4].FindControl("TBNarr");

                            ddlpro.SelectedValue = dt.Rows[i]["PRODUCTS"].ToString();
                            Tbproqty.Text = dt.Rows[i]["QUANTITY"].ToString();
                            Tbprorat.Text = dt.Rows[i]["RATE"].ToString();
                            Tbamt.Text = dt.Rows[i]["AMOUNT"].ToString();
                            Tbnarr.Text = dt.Rows[i]["NARRATION"].ToString();
                            rowIndex++;

                            // ADDTOTAL();
                            Tbproqty.Focus();
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        private void SetInitRowReq()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("PRODUCTS", typeof(string)));
            dt.Columns.Add(new DataColumn("QUANTITY", typeof(string)));
            dt.Columns.Add(new DataColumn("RATE", typeof(string)));
            dt.Columns.Add(new DataColumn("AMOUNT", typeof(string)));
            dt.Columns.Add(new DataColumn("NARRATION", typeof(string)));

            dr = dt.NewRow();
            dr["PRODUCTS"] = string.Empty;
            dr["QUANTITY"] = string.Empty;
            dr["RATE"] = string.Empty;
            dr["AMOUNT"] = string.Empty;
            dr["NARRATION"] = string.Empty;

            dt.Rows.Add(dr);

            //Store the DataTable in ViewState
            ViewState["dt_adItm"] = dt;

            GVDetReq.DataSource = dt;
            GVDetReq.DataBind();
        }
        private void AddNewRow()
        {
            int rowIndex = 0;

            if (ViewState["dt_adItm"] != null)
            {
                DataTable dt = (DataTable)ViewState["dt_adItm"];
                DataRow drRow = null;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        DropDownList DDLPro = (DropDownList)GVDetReq.Rows[rowIndex].Cells[0].FindControl("DDL_Pro");
                        TextBox TBProQty = (TextBox)GVDetReq.Rows[rowIndex].Cells[1].FindControl("TBProQty");
                        TextBox TBProrat = (TextBox)GVDetReq.Rows[rowIndex].Cells[2].FindControl("TBProrat");
                        TextBox TBAmt = (TextBox)GVDetReq.Rows[rowIndex].Cells[3].FindControl("TBAmt");
                        TextBox TBNarr = (TextBox)GVDetReq.Rows[rowIndex].Cells[4].FindControl("TBNarr");

                        drRow = dt.NewRow();

                        dt.Rows[i - 1]["PRODUCTS"] = DDLPro.Text;
                        dt.Rows[i - 1]["QUANTITY"] = TBProQty.Text;
                        dt.Rows[i - 1]["RATE"] = TBProrat.Text;
                        dt.Rows[i - 1]["AMOUNT"] = TBAmt.Text;
                        dt.Rows[i - 1]["NARRATION"] = TBNarr.Text;

                        rowIndex++;

                        float GTotal = 0;
                        for (int j = 0; j < GVDetReq.Rows.Count; j++)
                        {
                            TextBox total = (TextBox)GVDetReq.Rows[j].FindControl("TBAmt");
                            //GTotal = Convert.ToSingle(TbAddPurNetTtl.Text);
                            GTotal += Convert.ToSingle(total.Text);
                        }
                        TBGrssTotal.Text = GTotal.ToString();
                    }
                    dt.Rows.Add(drRow);
                    ViewState["dt_adItm"] = dt;
                    
                    GVDetReq.DataSource = dt;
                    GVDetReq.DataBind();

                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreRowReq();
        }

        #endregion 

        #region Drop Down List

        public void BindEmp()
        {
            try
            {
                DataTable dt_Emp = new DataTable();
               
                dt_Emp = tbl_MProQuotManager.BindEmpDDL();

                DDL_emp.DataSource = dt_Emp;
                DDL_emp.DataTextField = "employeeName";
                DDL_emp.DataValueField = "employeeID";
                DDL_emp.DataBind();
                DDL_emp.Items.Add(new ListItem("--Select--", "0"));


            }
            catch (Exception ex)
            {
                lbl_err.Text = ex.Message.ToString();
            }
        }
        public void BindDpt()
        {

            using (SqlCommand cmdpro = new SqlCommand())
            {
                cmdpro.CommandText = " select rtrim('[' + CAST(Depart_id AS VARCHAR(200)) + ']-' + Depart_nam ) as [Depart_nam], Depart_id from tbl_Depart ";

                cmdpro.Connection = con;
                con.Open();

                DataTable dtdpt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmdpro);
                adp.Fill(dtdpt);

                DDL_Dpt.DataSource = dtdpt;
                DDL_Dpt.DataTextField = "Depart_nam";
                DDL_Dpt.DataValueField = "Depart_id";
                DDL_Dpt.DataBind();
                
                con.Close();
            }
        }
        #endregion

    }
}