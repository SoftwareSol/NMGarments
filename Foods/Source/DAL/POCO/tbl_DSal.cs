using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foods
{
    [Serializable]
    public class tbl_DSal
    {
        private string dsal_id;
        private string dsal_itmdes;
        private string dsal_itmqty;
        private string dsal_Itmunt;
        private string dsal_salcst;
        private string dsal_netttl;
        private string dsal_ttl;
        private string msal_id;
        private string productid;
        private string producttypeid;
        private string dis;
        private string Rat;
        private string amt;
        private string gst;
        private string gttl;



        public virtual string DSal_id { get; set; }
        public virtual string DSal_ItmDes { get; set; }
        public virtual string DSal_ItmQty { get; set; }
        public virtual string DSal_ItmUnt { get; set; }
        public virtual string DSal_salcst { get; set; }
        public virtual string DSal_netttl { get; set; }
        public virtual string DSal_ttl { get; set; }
        public virtual string MSal_id { get; set; }
        public virtual string ProductID { get; set; }
        public virtual string ProductTypeID { get; set; }
        public virtual string Dis { get; set; }
        public virtual string rat { get; set; }
        public virtual string Amt { get; set; }
        public virtual string GST { get; set; }
        public virtual string GTtl { get; set; }


        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + DSal_id.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            tbl_DSal dSal = obj as tbl_DSal;

            if (dSal == null)
            {
                return false;
            }

            if (this.DSal_id == dSal.DSal_id)
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