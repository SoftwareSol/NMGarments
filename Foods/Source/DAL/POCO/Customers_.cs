using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foods
{
    [Serializable]
    public class Customers_
    {
        private string customerid;
        private string customername;
        private string gst;
        private string Category;
        private string ntn;
        private string Customertype_;
        private string area;
        private string refnum;
        private string district;
        private string phoneno;
        private string email;
        private string cellno1;
        private string postalcode;
        private string cellno2;
        private string postalofficecontact;
        private string cellno3;
        private string nic;
        private string cellno4;
        private string City_;
        private string address;
        private string createdby;
        private string createddate;
        private string isactive;
        private string cus_code;
        
        public virtual string CustomerID { get; set; }
        public virtual string CustomerName { get; set; }
        public virtual string GST { get; set; }
        public virtual string category { get; set; }
        public virtual string NTN { get; set; }
        public virtual string customertype_ { get; set; }
        public virtual string Area { get; set; }
        public virtual string RefNum { get; set; }
        public virtual string District { get; set; }
        public virtual string PhoneNo { get; set; }
        public virtual string Email { get; set; }
        public virtual string CellNo1 { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string CellNo2 { get; set; }
        public virtual string PostalOfficeContact { get; set; }
        public virtual string CellNo3 { get; set; }
        public virtual string NIC { get; set; }
        public virtual string CellNo4 { get; set; }
        public virtual string city_ { get; set; }
        public virtual string Address { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual string IsActive { get; set; }
        public virtual string Cus_Code { get; set;}


        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + CustomerID.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Customers_ Customers = obj as Customers_;

            if (Customers == null)
            {
                return false;
            }

            if (this.CustomerID == Customers.CustomerID)
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