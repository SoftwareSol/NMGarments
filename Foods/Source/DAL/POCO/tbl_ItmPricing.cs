using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foods
{
     [Serializable]
    public class tbl_ItmPricing
    {
    
        private string ItmPriId;
        private string Effdat;
        private string ProductId;
        private string CustomerId;
        private string itmpri_qty;
        private string unt_Cost;
        private string Cost;
        private string Crtd_by;
        private string Crtd_at;

        public virtual string ItmPriID { get; set; }
        public virtual string EffDat { get; set; }
        public virtual string ProductID { get; set; }
        public virtual string CustomerID { get; set; }
        public virtual string itmpri_Qty { get; set; }
        public virtual string unt_cost { get; set; }
        public virtual string cost { get; set; }
        public virtual string crtd_by { get; set; }
        public virtual DateTime? crtd_at { get; set; }
        

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + ItmPriID.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            tbl_ItmPricing ItmPricing = obj as tbl_ItmPricing;

            if (ItmPricing == null)
            {
                return false;
            }

            if (this.ItmPriID == ItmPricing.ItmPriID)
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