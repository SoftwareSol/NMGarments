using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foods
{
    [Serializable]
    public class tbl_DProQuot
    {
        private string dproquot_id;
        private string dproquot_sno;
        private string productid;
        private string dproquot_rat;
        private string mproquot_amt;
        private string mproquot_tamt;
        private string mproquot_narr;
        private string createdby;
        private string createdat;
        private string isactive;
        private string mproquot_id;



        public virtual string DProQuot_id { get; set; }
        public virtual string DProQuot_SNO { get; set; }
        public virtual string ProductID { get; set; }
        public virtual string DProQuot_rat { get; set; }
        public virtual string MProQuot_amt { get; set; }
        public virtual string MProQuot_tamt { get; set; }
        public virtual string MProQuot_narr { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? CreatedAt { get; set; }
        public virtual string ISActive { get; set; }
        public virtual string MProQuot_id { get; set; }


        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + DProQuot_id.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            tbl_DProQuot DProQuot = obj as tbl_DProQuot;

            if (DProQuot == null)
            {
                return false;
            }

            if (this.DProQuot_id == DProQuot.DProQuot_id)
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