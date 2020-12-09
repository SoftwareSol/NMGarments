using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foods
{
    [Serializable]
    public class tbl_mjv
    {
        private string Mjv_id;
        private string Mjv_sono;
        private string Mjv_dat;
        private string ProductTypeid;
        private string SubHeadCategoriesid;
        private string Mjv_narr;
        private string mjv_debtAmt;
        private string mjv_crdtAmt;
        private string bank_id;
        private string mjv_chqNO;
        private string mjv_chqDat;
        private string mjv_taxPer;
        private string mjv_taxAmt;
        private string employeeid;
        private string createdby;
        private string createdat;
        private string isactive;
        private string mjv_Grdttl;
        private string mjv_vchtyp;
        private string ven_ID;



        public virtual string mjv_id { get; set; }
        public virtual string mjv_sono { get; set; }
        public virtual DateTime? mjv_dat { get; set; }
        public virtual string ProductTypeID { get; set; }
        public virtual string SubHeadCategoriesID { get; set; }
        public virtual string mjv_Narr { get; set; }
        public virtual string mjv_debtamt { get; set; }
        public virtual string mjv_crdtamt { get; set; }
        public virtual string mjv_ttl { get; set; }
        public virtual string Bank_ID { get; set; }
        public virtual string mjv_chqno { get; set; }
        public virtual string mjv_chqdat { get; set; }
        public virtual string mjv_taxper { get; set; }
        public virtual string mjv_taxamt { get; set; }
        public virtual string employeeID { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? CreatedAt { get; set; }
        public virtual string ISActive { get; set; }
        public virtual string mjv_grdttl { get; set; }
        public virtual string mjv_Vchtyp { get; set; }
        public virtual string ven_id { get; set; }


        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + mjv_id.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            tbl_mjv Mjv = obj as tbl_mjv;

            if (Mjv == null)
            {
                return false;
            }

            if (this.mjv_id == Mjv.mjv_id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}