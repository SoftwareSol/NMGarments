using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foods
{
    [Serializable]
    public class Products
    {
        private string productid;
        private string ProductTypeid;
        private string productname;
        private string pcksize;
        private string cost;
        private string productdiscriptions;
        private string supplier_customer;
        private string unit;
        private string producttype;
        private string createdby;
        private string createdat;
        private string pro_code;


        public virtual string ProductID { get; set; }
        public virtual string ProductTypeID { get; set; }
        public virtual string ProductName { get; set; }
        public virtual string PckSize { get; set; }
        public virtual string Cost { get; set; }
        public virtual string ProductDiscriptions { get; set; }
        public virtual string Supplier_CUstomer { get; set; }
        public virtual string Unit { get; set; }
        public virtual string ProductType { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? CreatedAt { get; set; }
        public virtual string Pro_Code { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + ProductID.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Products products = obj as Products;

            if (products == null)
            {
                return false;
            }

            if (this.ProductID == products.ProductID)
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