using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foods
{
    [Serializable]
    public class supplier
    {
        private string SupplierID;
        private string SupplierName;
        private string ContactPerson;
        private string backupcontact;
        private string city_;
        private string PhoneNo;
        private string Mobile;
        private string FaxNo;
        private string Postalcode;
        private string addressone;
        private string addresstwo;
        private string cnic;
        private string url;
        private string businessnature;
        private string email;
        private string NTNNTRno;
        private string sup_code;


        public virtual string supplierId { get; set; }
        public virtual string suppliername { get; set; }
        public virtual string contactperson { get; set; }
        public virtual string BackUpContact { get; set; }
        public virtual string City_ { get; set; }
        public virtual string phoneno { get; set; }
        public virtual string mobile { get; set; }
        public virtual string faxno { get; set; }
        public virtual string postalcode { get; set; }
        public virtual string designation { get; set; }
        public virtual string AddressOne { get; set; }
        public virtual string AddressTwo { get; set; }
        public virtual string CNIC { get; set; }
        public virtual string Url { get; set; }
        public virtual string BusinessNature { get; set; }
        public virtual string Email { get; set; }
        public virtual string NTNNTRNo { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual string IsActive { get; set; }
        public virtual string Sup_Code { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + supplierId.GetHashCode();
               // hash = hash * 23 + CmCode.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            supplier supplier = obj as supplier;

            if (supplier == null)
            {
                return false;
            }

            if (
            this.supplierId == supplier.supplierId 
            //&&
            //this.CmCode == genPlaceType.CmCode
            )
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