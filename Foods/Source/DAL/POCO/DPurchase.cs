using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foods
{
     [Serializable]
    public class DPurchase
    {  
        private string dpurid;
        private string Pronam;
        private string Productid;
        private string qty;
        private string total;
        private string subtotl;
        private string saletax;
        private string nettotal;
        private string unit;
        private string cost;
        private string protyp;
        private string createdby;
        private string createdat;
        private string grossttal;
 
        public virtual string DPurID { get; set; }
        public virtual string MPurID { get; set; }
        public virtual string ProductID { get; set; }
        public virtual string ProDes { get; set; }
        public virtual string Qty { get; set; }
        public virtual string Total { get; set; }
        public virtual string SubTotl { get; set; }
        public virtual string SaleTax { get; set; }
        public virtual string NetTotal { get; set; }
        public virtual string Unit { get; set; }
        public virtual string ProTyp { get; set; }
        public virtual string Cost { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual string CreatedAt { get; set; }
        public virtual string GrossTtal { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + DPurID.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            DPurchase dpurchase = obj as DPurchase;

            if (dpurchase == null)
            {
                return false;
            }

            if (this.DPurID == dpurchase.DPurID)
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
