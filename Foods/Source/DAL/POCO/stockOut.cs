using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foods
{
    [Serializable]
    public class stockOut
    {
        private string stockoutid;
        private string itmnme;
        private string stockqty;
        private string units;
        private string Weight;
        private string date;
        private string Unit_Price;
        private string Amount;
        private string Created_By;
        private string Create_At;


        public virtual string StockOutID { get; set; }
        public virtual string ItmNme { get; set; }
        public virtual string StockQty { get; set; }
        public virtual string Units { get; set; }
        public virtual string weight { get; set; }
        public virtual string Date { get; set; }
        public virtual string unit_price { get; set; }
        public virtual string amount { get; set; }
        public virtual string created_by { get; set; }
        public virtual string create_at { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + StockOutID.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            stockOut stkOt = obj as stockOut;

            if (stkOt == null)
            {
                return false;
            }

            if (this.StockOutID == stkOt.StockOutID)
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