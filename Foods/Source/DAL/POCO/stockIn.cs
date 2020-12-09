using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foods
{
    [Serializable]
    public class stockIn
    {
        private string stockinid;
        private string Itm_Nam;
        private string stockqty;
        private string units;
        private string Weight;
        private string date;
        private string UnitPrice;
        private string Amount;
        private string Created_By;
        private string Create_At;


        public virtual string StockInID { get; set; }
        public virtual string Itm_nam { get; set; }
        public virtual string StockQty { get; set; }
        public virtual string Units { get; set; }
        public virtual string weight { get; set; }
        public virtual string Date { get; set; }
        public virtual string unitprice { get; set; }
        public virtual string amount { get; set; }
        public virtual string created_by { get; set; }
        public virtual string create_at { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + StockInID.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            stockIn stkIn = obj as stockIn;

            if (stkIn == null)
            {
                return false;
            }

            if (this.StockInID == stkIn.StockInID)
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