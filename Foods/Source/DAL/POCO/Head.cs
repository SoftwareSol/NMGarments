using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foods
{
    [Serializable]
    public class Head
    {

        private string headid;
        private string headname;
        private string headgeneratedid;
        private string createdat;
        private string createdby;
        private string headkey;

        public virtual string HeadID { get; set; }
        public virtual string HeadName { get; set; }
        public virtual string HeadGeneratedID { get; set; }
        public virtual DateTime? CreatedAt { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual string HeadKey { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + HeadID.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Head head = new Head();

            if (head == null)
            {
                return false;
            }

            if (this.HeadID == head.HeadID)
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