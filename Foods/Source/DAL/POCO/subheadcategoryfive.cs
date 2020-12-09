using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foods
{
    [Serializable]
    public class subheadcategoryfive
    {
        private string subheadcategoryfiveid;
        private string subheadcategoryfivename;
        private string subheadcategoryfiveGeneratedid;
        private string HeadGeneratedid;
        private string subheadcategoriesgeneratedid;
        private string subheadcategoryfourgeneratedid;
        private string createdat;
        private string createdby;
        private string subfivekey;
        private string Isflag;

        public virtual string subheadcategoryfiveID { get; set; }
        public virtual string subheadcategoryfiveName { get; set; }
        public virtual string subheadcategoryfiveGeneratedID { get; set; }
        public virtual string HeadGeneratedID { get; set; }
        public virtual string SubHeadGeneratedID { get; set; }
        public virtual string SubHeadCategoriesGeneratedID { get; set; }
        public virtual string subheadcategoryfourGeneratedID { get; set; }
        public virtual DateTime? CreatedAt { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual string SubFiveKey { get; set; }
        public virtual string isflag { get; set; }


        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + subheadcategoryfiveID.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            subheadcategoryfive subheadcategoryfive = new subheadcategoryfive();

            if (subheadcategoryfive == null)
            {
                return false;
            }

            if (this.subheadcategoryfiveID == subheadcategoryfive.subheadcategoryfiveID)
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