using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foods
{
    [Serializable]
    public class SubHeadCategories
    {
        private string subheadcategoriesid;
        private string ven_ID;
        private string subheadcategoryname;
        private string subHeadcategoriesgeneratedid;
        private string headgeneratedid;
        private string subheadgeneratedid;        
        private string createdat;
        private string createdby;
        private string subcategorieskey;

        public virtual string SubHeadCategoriesID { get; set; }
        public virtual string ven_id { get; set; }
        public virtual string SubHeadCategoriesName { get; set; }
        public virtual string SubHeadCategoriesGeneratedID { get; set; }
        public virtual string HeadGeneratedID { get; set; }
        public virtual string SubHeadGeneratedID { get; set; }        
        public virtual DateTime? CreatedAt  { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual string SubCategoriesKey { get; set; }


        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + SubHeadCategoriesID.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            SubHeadCategories subheadcategories = new SubHeadCategories();

            if (subheadcategories == null)
            {
                return false;
            }

            if (this.SubHeadCategoriesID == subheadcategories.SubHeadCategoriesID)
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