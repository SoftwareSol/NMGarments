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
using Foods;

namespace Foods
{
    public partial class frm_jv : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_ = null;
        int i = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Initials

            if (!IsPostBack)
            {
                SetInitRow();
                //FillGrid();
                BindDDL();
                TBjvdat.Text = DateTime.Now.ToShortDateString();
                //ptnSno();
            }
            #endregion
            
        }

        public void BindDDL()
        {
            try
            {
                //Items Name

                using (SqlCommand cmdpar = new SqlCommand())
                {
                    con.Close();
                    cmdpar.CommandText = " select rtrim('[' + CAST(SubHeadCategoriesGeneratedID AS VARCHAR(200)) + ']-' + SubHeadCategoriesName ) as [SubHeadCategoriesName], SubHeadCategoriesID from SubHeadCategories ";

                    cmdpar.Connection = con;
                    con.Open();

                    DataTable dtpar = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmdpar);
                    adp.Fill(dtpar);

                    for (int i = 0; i < GVJV.Rows.Count; i++)
                    {
                        DropDownList DDLPar = (DropDownList)GVJV.Rows[i].Cells[0].FindControl("DDL_Par");
                        DDLPar.DataSource = dtpar;
                        DDLPar.DataTextField = "SubHeadCategoriesName";
                        DDLPar.DataValueField = "SubHeadCategoriesID";
                        DDLPar.DataBind();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }



        private int save()
        {
            try
            {
                int j = 1;

                tbl_mjv mjv = new tbl_mjv();

                foreach (GridViewRow g1 in GVJV.Rows)
                {
                    string DDL_Par = (g1.FindControl("DDL_Par") as DropDownList).SelectedValue;
                    string TBDbt = (g1.FindControl("TBDbt") as TextBox).Text;
                    string TBCrd = (g1.FindControl("TBCrd") as TextBox).Text;
                    string TBnarr = (g1.FindControl("TBnarr") as TextBox).Text;
                    string JVSNO = (g1.FindControl("HF_JVSNo") as HiddenField).Value;

                    if (TBDbt == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                        lblalert.Text = "Debit Note Can not be Null";
                    }
                    else if (TBCrd == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                        lblalert.Text = "Credit Note Can not be Null";
                    }
                    else
                    {
                        int v;
                        int b;
                        string str = "select mjv_id, mjv_sono from tbl_mjv order by mjv_id desc";
                        SqlCommand cmd = new SqlCommand(str, con);
                        con.Open();

                        DataTable dt = new DataTable();
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);

                        adp.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {

                            SqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                if (string.IsNullOrEmpty(JVSNO))
                                {
                                    v = Convert.ToInt32(reader["mjv_id"].ToString());
                                    b = v + 1;
                                    JVSNO = "JV00 " + b.ToString();

                                }
                                //else if (JVSNO != "")
                                //{
                                //    v = Convert.ToInt32(reader["mjv_id"].ToString());
                                //    b = v + 1;
                                //    JVSNO = "JV00 " + b.ToString();

                                //}
                            }
                        }
                        else
                        {
                            JVSNO = "JV00 1";

                        }
                        con.Close();

                        mjv.mjv_id = HFjv.Value;

                        mjv.mjv_sono = string.IsNullOrEmpty(JVSNO) ? null : JVSNO;
                        mjv.mjv_dat = Convert.ToDateTime(string.IsNullOrEmpty(TBjvdat.Text) ? null : TBjvdat.Text);
                        mjv.ProductTypeID = "0";
                        mjv.SubHeadCategoriesID = string.IsNullOrEmpty(DDL_Par) ? null : DDL_Par;
                        mjv.mjv_Narr = string.IsNullOrEmpty(TBnarr) ? null : TBnarr;
                        mjv.mjv_debtamt = string.IsNullOrEmpty(TBDbt) ? null : TBDbt;
                        mjv.mjv_crdtamt = string.IsNullOrEmpty(TBCrd) ? null : TBCrd;
                        mjv.mjv_ttl = "0.0";
                        mjv.Bank_ID = "";
                        mjv.mjv_chqno = "";
                        mjv.mjv_chqdat = "01/01/1900";
                        mjv.mjv_taxper = "0.0";
                        mjv.mjv_taxamt = "0.0";
                        mjv.employeeID = "1";
                        mjv.CreatedBy = Session["user"].ToString();
                        mjv.CreatedAt = DateTime.Today;
                        mjv.ISActive = "True";
                        mjv.mjv_grdttl = "0.0";
                        mjv.mjv_Vchtyp = "JV";
                        mjv.ven_id = "0";


                        tbl_mjvManager mjvmanag = new tbl_mjvManager(mjv);
                        mjvmanag.Save();
                    }
                }
                    return j;                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void FillGrid()
        {
            try
            {
                DataTable dt_ = new DataTable();
                dt_ = tbl_mjvManager.GetJVList();

                //GVScrhJV.DataSource = dt_;
                //GVScrhJV.DataBind();

                ViewState["MJV"] = dt_;
            }
            catch (Exception ex)
            {
                //throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }

        }

        //private void ptnSno()
        //{
        //    try
        //    {
        //        int v;
        //        int b;
        //        string str = "select mjv_id, mjv_sono from tbl_mjv order by mjv_id desc";
        //        SqlCommand cmd = new SqlCommand(str, con);
        //        con.Open();

        //        DataTable dt = new DataTable();
        //        SqlDataAdapter adp = new SqlDataAdapter(cmd);

        //        adp.Fill(dt);

        //        if (dt.Rows.Count > 0)
        //        {

        //            SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                if (string.IsNullOrEmpty(HF_JVSNo.Value))
        //                {
        //                    v = Convert.ToInt32(reader["mjv_id"].ToString());
        //                    b = v + 1;
        //                    HF_JVSNo.Value = "JV00 " + b.ToString();

        //                }
        //                else if (HF_JVSNo.Value != "")
        //                {
        //                    v = Convert.ToInt32(reader["mjv_id"].ToString());
        //                    b = v + 1;
        //                    HF_JVSNo.Value = "JV00 " + b.ToString();
                        
        //                }
        //            }
        //        }
        //        else
        //        {
        //            HF_JVSNo.Value = "JV00 1";

        //        }
        //        con.Close();

        //    }
        //    catch (Exception ex)
        //    {
        //        // throw ex;
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
        //        //lbl_Heading.Text = "Error!";
        //        lblalert.Text = ex.Message;
        //    }

        //}


        protected void linkbtnadd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }


        private void SetInitRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("PARTICULARS", typeof(string)));
            dt.Columns.Add(new DataColumn("DEBIT", typeof(string)));
            dt.Columns.Add(new DataColumn("CREDIT", typeof(string)));
            dt.Columns.Add(new DataColumn("NARR", typeof(string)));
            dt.Columns.Add(new DataColumn("JVSNO", typeof(string)));

            dr = dt.NewRow();
            dr["PARTICULARS"] = string.Empty;
            dr["DEBIT"] = string.Empty;
            dr["CREDIT"] = string.Empty;
            dr["NARR"] = string.Empty;
            dr["JVSNO"] = string.Empty;

            dt.Rows.Add(dr);

            //Store the DataTable in ViewState
            ViewState["dt_adItm"] = dt;

            GVJV.DataSource = dt;
            GVJV.DataBind();
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
                        //extract the Controls values
                        DropDownList DDLPar = (DropDownList)GVJV.Rows[rowIndex].Cells[0].FindControl("DDL_Par");
                        TextBox TBDbt = (TextBox)GVJV.Rows[rowIndex].Cells[1].FindControl("TBDbt");
                        TextBox TBCrd = (TextBox)GVJV.Rows[rowIndex].Cells[2].FindControl("TBCrd");
                        TextBox TBnarr = (TextBox)GVJV.Rows[rowIndex].Cells[3].FindControl("TBnarr");
                        HiddenField HFJVSNO = (HiddenField)GVJV.Rows[rowIndex].Cells[4].FindControl("HF_JVSNo");

                        drRow = dt.NewRow();

                        dt.Rows[i - 1]["PARTICULARS"] = DDLPar.Text;
                        dt.Rows[i - 1]["DEBIT"] = TBDbt.Text;
                        dt.Rows[i - 1]["CREDIT"] = TBCrd.Text;
                        dt.Rows[i - 1]["NARR"] = TBnarr.Text;
                        dt.Rows[i - 1]["JVSNo"] = HFJVSNO.Value;

                        rowIndex++;

                        DDLPar.Focus();
                    }
                    dt.Rows.Add(drRow);
                    ViewState["dt_adItm"] = dt;

                    GVJV.DataSource = dt;
                    GVJV.DataBind();

                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreRow();
        }

        private void SetPreRow()
        {
            try
            {
                BindDDL();
                int rowIndex = 0;
                if (ViewState["dt_adItm"] != null)
                {
                    DataTable dt = (DataTable)ViewState["dt_adItm"];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DropDownList DDLPar = (DropDownList)GVJV.Rows[rowIndex].Cells[0].FindControl("DDL_Par");
                            TextBox TBDbt = (TextBox)GVJV.Rows[rowIndex].Cells[1].FindControl("TBDbt");
                            TextBox TBCrd = (TextBox)GVJV.Rows[rowIndex].Cells[2].FindControl("TBCrd");
                            TextBox TBnarr = (TextBox)GVJV.Rows[rowIndex].Cells[3].FindControl("TBnarr");
                            HiddenField HFJVSNO = (HiddenField)GVJV.Rows[rowIndex].Cells[4].FindControl("HF_JVSNo");

                            DDLPar.SelectedValue = dt.Rows[i]["PARTICULARS"].ToString();
                            TBDbt.Text = dt.Rows[i]["DEBIT"].ToString();
                            TBCrd.Text = dt.Rows[i]["CREDIT"].ToString();
                            TBnarr.Text = dt.Rows[i]["NARR"].ToString();
                            HFJVSNO.Value = dt.Rows[i]["JVSNO"].ToString();
                            //ChkCls.Checked = Convert.ToBoolean(dt.Rows[i]["CLOSED"].ToString());

                            rowIndex++;

                            DDLPar.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //throw;
                lbl_err.Text = ex.Message.ToString();
            }
        }

        protected void GVJV_RowDeleting(object sender, GridViewDeleteEventArgs e)
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

                    GVJV.DataSource = dt;
                    GVJV.DataBind();

                    SetPreRow();
                    //ptnSno();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;

                i = save();

                if (i > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "Data Saved";
                    SetInitRow();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "Opps Some thing is Wrong Please Call the Administrator!";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                //lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
            }

        }

    }
}