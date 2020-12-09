using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foods
{
    [Serializable]
    public class tbl_MSal
    {
        private string msal_id;
        private string msal_sono;
        private string msaldat;
        private string msalOrdcustadd;
        private string msal_rmk;
        private string msalordid;
        private string msalOrdclsd;
        private string customerid;
        private string createdby;
        private string createdat;
        private string isactive;
        private string isCre;
        private string isCash;
        private string Gatpassno;
        private string Schm;        
        private string Bons;
        private string outstanding;


        public virtual string MSal_id { get; set; }
        public virtual string MSal_sono { get; set; }
        public virtual DateTime? MSal_dat { get; set; }
        public virtual string MSal_Rmk { get; set; }
        public virtual string MSalOrdid { get; set; }
        public virtual string CustomerID { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? CreatedAt { get; set; }
        public virtual string ISActive { get; set; }
        public virtual string iscre { get; set; }
        public virtual string iscash { get; set; }
        public virtual string gatpassno { get; set; }
        public virtual string schm { get; set; }
        public virtual string bons { get; set; }
        public virtual string Outstanding { get; set; }


        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + MSal_id.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            tbl_MSal msal = obj as tbl_MSal;

            if (msal == null)
            {
                return false;
            }

            if (this.MSal_id == msal.MSal_id)
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