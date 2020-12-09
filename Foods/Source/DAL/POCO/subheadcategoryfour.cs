using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foods
{
    [Serializable]
    public class subheadcategoryfour
    {
        private string subheadid;
        private string subheadname;
        private string subheadgeneratedid;
        private string headgeneratedid;
        private string createdby;
        private string createdat;
        private string subheadkey;

        public virtual string subheadcategoryfourID { get; set; }
        public virtual string subheadcategoryfourName { get; set; }
        public virtual string subheadcategoryfourGeneratedID { get; set; }
        public virtual string HeadGeneratedID { get; set; }
        public virtual string SubHeadGeneratedID { get; set; }
        public virtual string SubHeadCategoriesGeneratedID { get; set; }
        public virtual DateTime? CreatedAt { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual string SubFourKey { get; set; }


        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + subheadcategoryfourID.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            subheadcategoryfour subheadcategoriesfour = new subheadcategoryfour();

            if (subheadcategoriesfour == null)
            {
                return false;
            }

            if (this.subheadcategoryfourID == subheadcategoriesfour.subheadcategoryfourID)
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