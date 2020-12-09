using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foods
{
    [Serializable]
    public class CustomerCategory
    {
        private string customerid;
        private string customername;

        public virtual string CategoryID { get; set; }
        public virtual string Category { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + CategoryID.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            CustomerCategory CustomersCategory = obj as CustomerCategory;

            if (CustomersCategory == null)
            {
                return false;
            }

            if (this.CategoryID == CustomersCategory.CategoryID)
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