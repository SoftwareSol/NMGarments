using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foods
{
    public class CustomerType
    {
        private string customertypeid;
        private string customertype;

        public virtual string CustomerTypeID { get; set; }
        public virtual string CustomerType_ { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + CustomerTypeID.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            CustomerType CustomerType = obj as CustomerType;

            if (CustomerType == null)
            {
                return false;
            }

            if (this.CustomerTypeID == CustomerType.CustomerTypeID)
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