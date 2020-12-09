using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foods
{
    [Serializable]
    public class tbl_MProQuot
    {
        private string mproquot_id;
        private string mproquot_dat;               
        private string mproquot_rmk;
        private string customerid;
        private string createdby;
        private string createdat;
        private string isactive;
        private string mproquot_app;
        private string mproquot_rej;
        private string mproquot_sono;


        public virtual string MProQuot_id { get; set; }
        public virtual DateTime? MProQuot_dat { get; set; }
        public virtual string MProQuot_rmk { get; set; }
        public virtual string CustomerID { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? CreatedAt { get; set; }
        public virtual string ISActive { get; set; }
        public virtual string MProQuot_app { get; set; }
        public virtual string MProQuot_Rej { get; set; }
        public virtual string MProQuot_sono { get; set; }


        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + MProQuot_id.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            tbl_MProQuot MProQuot = obj as tbl_MProQuot;

            if (MProQuot == null)
            {
                return false;
            }

            if (this.MProQuot_id == MProQuot.MProQuot_id)
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