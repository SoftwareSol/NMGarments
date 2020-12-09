using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DataAccess;

namespace Foods.Source.IP.D.Global_Test
{
    public partial class copygrid : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        string query, PurItm;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetInitRow();
            }
        }

     
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetCat(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = "select ProductTypeName from tbl_producttype where ProductTypeName like '%" + prefixText + "%'";
            da = new SqlDataAdapter(str, con);
            dt = new DataTable();
            da.Fill(dt);
            List<string> Output = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
                Output.Add(dt.Rows[i][0].ToString());
            return Output;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetPro(string prefixText)
        {
            string protyp = HttpContext.Current.Session["cat"].ToString();

            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = "select ProductName from Products where ProductType ='" + protyp + "' and ProductName like '%" + prefixText + "%'";
            da = new SqlDataAdapter(str, con);
            dt = new DataTable();
            da.Fill(dt);
            List<string> Output = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
                Output.Add(dt.Rows[i][0].ToString());
            return Output;
        }

        protected void linkbtnadd_Click(object sender, EventArgs e)
        {

        }

        protected void GVStkItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void GVStkItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (ViewState["dt_adItm"] != null)
            {
                DataTable dt = (DataTable)ViewState["dt_adItm"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["dt_adItm"] = dt;

                    GVStkItems.DataSource = dt;
                    GVStkItems.DataBind();

                    //SetPreRowitm();
                }
            }
        }


        protected void ItmQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float GTotal = 0;
                for (int k = 0; k < GVStkItems.Rows.Count; k++)
                {
                    TextBox total = (TextBox)GVStkItems.Rows[k].FindControl("ItmQty");

                    if (total.Text != "")
                    {
                        GTotal += Convert.ToSingle(total.Text);
                    }
                }
                //TBttlqty.Text = GTotal.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void grid()
        {
            try
            {
                query = " select Dstk_ItmDes as [PRODUCT],Dstk_Qty as [QTY],Dstk_rat as [RATE], Dstk_unt as [SIZE], " +
                    " (Dstk_rat * Dstk_Qty) as [AMT], '' as [DSal_id] from tbl_Dstk";


                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    DataTable dtpro_ = new DataTable();

                    dtpro_ = DBConnection.GetQueryData(query);

                    if (dtpro_.Rows.Count > 0)
                    {
                        //GridView1.DataSource = dtpro_;
                        //GridView1.DataBind();

                        //ViewState["dt_salItms"] = dtpro_;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void TBCat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Session["cat"] = TBCat.Text.Trim();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void TBItms_TextChanged(object sender, EventArgs e)
        {
            try
            {
                query = " select  * from  Products where ProductName = '" + TBItms.Text.Trim() + "'";

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    PurItm = dt_.Rows[0]["ProductID"].ToString();

                    //query = " select '0.00' as [Dstk_ItmQty],Dstk_unt as [Dstk_sizes] from tbl_Dstk where ProductID = '" + PurItm + "'";
                    query = " select Dstk_ItmDes as [PRODUCT],Dstk_unt as [SIZE],  Dstk_Qty as [QTY], Dstk_rat as [RATE], " +
                            " (Dstk_rat * Dstk_Qty) as [AMT], '' as [DSal_id] from tbl_Dstk where ProductID = '" + PurItm + "'";

                    DataTable dtpro_ = new DataTable();

                    dtpro_ = DBConnection.GetQueryData(query);

                    if (dtpro_.Rows.Count > 0)
                    {
                        TB_Rat.Text = dtpro_.Rows[0]["RATE"].ToString();

                        //decimal ttl = 0;
                        //DataRow dr;
                        
                        //dr = dtpro_.NewRow();

                        //for (int i = 0; i <= dtpro_.Rows.Count; i++)
                        //{
                        //    dr[0] = TBItms.Text;
                        //    dr[1] = dtpro_.Rows[0]["SIZE"].ToString();
                        //    dr[2] = dtpro_.Rows[0]["QTY"].ToString();
                        //    dr[3] = TB_Rat.Text;
                        //    ttl = Convert.ToDecimal(dtpro_.Rows[0]["QTY"]) * Convert.ToDecimal(TB_Rat.Text.Trim());
                        //    dr[4] = ttl.ToString();

                        //}
                        //dtpro_.Rows.Add(dr);

                        GVStkItems.DataSource = dtpro_;
                        GVStkItems.DataBind();

                        DataRow dr;
                        decimal ttl  = 0;

                        foreach (GridViewRow row in GVStkItems.Rows)
                        {
                            TextBox itmsiz = (TextBox)row.FindControl("itmsiz");
                            TextBox ItQty = (TextBox)row.FindControl("ItQty");

                            dr = dtpro_.NewRow();
                            dr[0] = TBItms.Text;
                            dr[1] = itmsiz.Text;
                            dr[2] = ItQty.Text;
                            dr[3] = TB_Rat.Text;
                            ttl = Convert.ToDecimal(ItQty.Text.Trim()) * Convert.ToDecimal(TB_Rat.Text.Trim());
                            dr[4] = ttl.ToString();

                            dtpro_.Rows.Add(dr);
                        }


                        ViewState["dt_salItm"] = dtpro_;

                    }

                }
                else
                {
                    query = " INSERT INTO Products " +
                                    " ([ProductName],[ProductType],[CompanyId],[BranchId],[IsActive]) VALUES('"
                                    + TBItms.Text.Trim() + "','" + TBCat.Text.Trim()
                                    + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "','1')";
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btn_copy_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewItems();
                //dt_ = (DataTable)ViewState["dt_salItms"];

                //if (dt_.Rows.Count > 0)
                //{
                //    GVItms.DataSource = dt_;
                //    GVItms.DataBind();
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void SetInitRow()
        {
            DataTable dt = new DataTable();
            //DataRow dr = null;

            dt.Columns.Add(new DataColumn("PRODUCT", typeof(string)));
            dt.Columns.Add(new DataColumn("SIZE", typeof(string)));
            dt.Columns.Add(new DataColumn("QTY", typeof(string)));
            dt.Columns.Add(new DataColumn("RATE", typeof(string)));
            dt.Columns.Add(new DataColumn("AMT", typeof(string)));
            dt.Columns.Add(new DataColumn("DSal_id", typeof(string)));

            ViewState["dt_salItms"] = dt;


            GVItms.DataSource = dt;
            GVItms.DataBind();

            // For Stock
            DataTable dtstk = new DataTable();
            //DataRow drstk = null;

            dtstk.Columns.Add(new DataColumn("SIZE", typeof(string)));
            dtstk.Columns.Add(new DataColumn("QTY", typeof(string)));

            ViewState["dt_salItm"] = dtstk;

            GVStkItems.DataSource = dtstk;
            GVStkItems.DataBind();

            //dr = dt.NewRow();
            //dr["PRODUCT"] = string.Empty;
            //dr["SIZE"] = string.Empty;
            //dr["QTY"] = string.Empty;
            //dr["RATE"] = string.Empty;
            //dr["AMT"] = string.Empty;
            //dr["DSal_id"] = string.Empty;
                
            //dt.Rows.Add(dr);

            //Store the DataTable in ViewState

        }

        private void AddNewItems()
        {
            int rowIndex = 0;

            if (ViewState["dt_salItms"] != null)
            {
                DataTable dt = (DataTable)ViewState["dt_salItms"];
                
                DataRow drRow = null;
                //if (dt.Rows.Count > 0)
                {
                    //drRow = dt.NewRow();

                    //for (int i = 1; i <= dt.Rows.Count; i++)
                    //{  
                    //    //extract the TextBox values
                    //    for (int k = 0; k < GVItms.Rows.Count; k++)
                    //    {
                    //        Label lbl_Pro = (Label)GVItms.Rows[k].Cells[0].FindControl("lbl_Pro");
                    //        TextBox itmsiz = (TextBox)GVItms.Rows[k].Cells[1].FindControl("itmsiz");
                    //        TextBox ItmQty = (TextBox)GVItms.Rows[k].Cells[2].FindControl("ItmQty");
                    //        TextBox TBrat = (TextBox)GVItms.Rows[k].Cells[3].FindControl("TBrat");
                    //        TextBox TBamt = (TextBox)GVItms.Rows[k].Cells[4].FindControl("TBamt");
                    //        HiddenField HFDSal = (HiddenField)GVItms.Rows[k].Cells[5].FindControl("HFDSal");


                    //        dt.Rows[i - 1]["PRODUCT"] = lbl_Pro.Text;
                    //        dt.Rows[i - 1]["SIZE"] = itmsiz.Text;
                    //        dt.Rows[i - 1]["QTY"] = ItmQty.Text;
                    //        dt.Rows[i - 1]["RATE"] = TBrat.Text;
                    //        dt.Rows[i - 1]["AMT"] = TBamt.Text;
                    //        dt.Rows[i - 1]["DSal_id"] = HFDSal.Value;

                    //        rowIndex++;

                    //    }
                    //}
                   

                    DataTable dt3 = (DataTable)ViewState["dt_salItm"];

                    if (dt3.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt3.Rows.Count; i++)
                        {
                            dt3.Rows[i]["RATE"] = TB_Rat.Text;
                            //if (dt3.Rows[i]["QTY"] != "0")
                            //{
                               // dt3.Rows[i]["QTY"] = "0"; 
                            //}
                        }

                        dt.Merge(dt3, true, MissingSchemaAction.Ignore);
                    }

                    //dt.Rows.Add(drRow);
                    ViewState["dt_salItms"] = dt;

                    GVItms.DataSource = dt;
                    GVItms.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            SetPreRowitm();
        }

        private void SetPreRowitm()
        {
            try
            {
                int rowIndex = 0;
                if (ViewState["dt_salItms"] != null)
                {
                    DataTable dt = (DataTable)ViewState["dt_salItms"];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            Label lbl_Pro = (Label)GVItms.Rows[i].Cells[0].FindControl("lbl_Pro");
                            TextBox itmsiz = (TextBox)GVItms.Rows[i].Cells[1].FindControl("itmsiz");
                            TextBox ItmQty = (TextBox)GVItms.Rows[i].Cells[2].FindControl("ItmQty");
                            Label lblrat = (Label)GVItms.Rows[i].Cells[3].FindControl("lblrat");
                            TextBox TBamt = (TextBox)GVItms.Rows[i].Cells[4].FindControl("TBamt");
                            //HiddenField HFDSal = (HiddenField)GVItms.Rows[i].Cells[5].FindControl("HFDSal");


                            lbl_Pro.Text = dt.Rows[i]["PRODUCT"].ToString();
                            itmsiz.Text = dt.Rows[i]["SIZE"].ToString();
                            ItmQty.Text = dt.Rows[i]["QTY"].ToString();
                            lblrat.Text = dt.Rows[i]["RATE"].ToString();
                            TBamt.Text = dt.Rows[i]["AMT"].ToString();
                            //HFDSal.Value = dt.Rows[i]["DSal_id"].ToString();

                            if (itmsiz.Text == "")
                            {
                                itmsiz.Text = "0";
                            }

                            if (ItmQty.Text == "")
                            {
                                ItmQty.Text = "0";
                            }

                            if (lblrat.Text == "")
                            {
                                lblrat.Text = "0";
                            }

                            if (TBamt.Text == "")
                            {
                                TBamt.Text = "0";
                            }

                            rowIndex++;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ItmQty_TextChanged1(object sender, EventArgs e)
        {
        }

        protected void ItQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal ttl = 0;

                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add(new System.Data.DataColumn("PRODUCT", typeof(String)));
                dt.Columns.Add(new System.Data.DataColumn("SIZE", typeof(String)));
                dt.Columns.Add(new System.Data.DataColumn("QTY", typeof(String)));
                dt.Columns.Add(new System.Data.DataColumn("RATE", typeof(String)));
                dt.Columns.Add(new System.Data.DataColumn("AMT", typeof(String)));

                foreach (GridViewRow row in GVStkItems.Rows)
                {
                    TextBox itmsiz = (TextBox)row.FindControl("itmsiz");
                    TextBox ItQty = (TextBox)row.FindControl("ItQty");

                    dr = dt.NewRow();
                    dr[0] = TBItms.Text;
                    dr[1] = itmsiz.Text;
                    dr[2] = ItQty.Text;
                    dr[3] = TB_Rat.Text;
                    ttl = Convert.ToDecimal(ItQty.Text.Trim()) * Convert.ToDecimal(TB_Rat.Text.Trim());
                    dr[4] = ttl.ToString();

                    dt.Rows.Add(dr);
                }

                ViewState["dt_salItm"] = dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        DataTable GetDataTable(GridView dtg)
        {
            DataTable dt = new DataTable();

            // add the columns to the datatable            
            if (dtg.HeaderRow != null)
            {

                for (int i = 0; i < dtg.HeaderRow.Cells.Count; i++)
                {
                    dt.Columns.Add(dtg.HeaderRow.Cells[i].Text);
                }
            }

            //  add each of the data rows to the table
            foreach (GridViewRow row in dtg.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i] = row.Cells[i].Text.Replace(" ", "");
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}