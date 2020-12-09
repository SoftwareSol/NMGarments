using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foods
{
    [Serializable]
    public class SubHead
    {
        private string subheadid;
        private string subheadname;
        private string subheadgeneratedid;
        private string headgeneratedid;
        private string createdby;
        private string craetedat;
        private string subheadkey;

        public virtual string SubHeadID { get; set; }
        public virtual string SubHeadName { get; set; }
        public virtual string SubHeadGeneratedID { get; set; }
        public virtual string HeadGeneratedID { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? CraetedAt { get; set; }
        public virtual string SubHeadKey { get; set; }


        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + SubHeadID.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            SubHead subhead = new SubHead();

            if (subhead == null)
            {
                return false;
            }

            if (this.SubHeadID == subhead.SubHeadID)
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