using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foods
{
    [Serializable]
    public class MPurchase
    {
        private string mpurid;
        private string ven_ID;
        private string vndradd;
        private string vndrcntct;
        private string purno;
        private string mpurdate;
        private string createdby;
        private string createdat;
        private string cnic;
        private string ntnno;
        private string tobeprntd;
        private string ck_act;
        private string mpurrmk;
        private string paytyp_id;
        private string Vchtyp_id;
        private string csh_Amt;
        private string cashbnk_id;
        private string chque_no;
        private string subheadcategoryfourid;
        private string out_standing;



        public virtual string MPurID { get; set; }
        public virtual string ven_id { get; set; }
        public virtual string VndrAdd { get; set; }
        public virtual string VndrCntct { get; set; }
        public virtual string PurNo { get; set; }
        public virtual string MPurDate { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual string CreatedAt { get; set; }
        public virtual string CNIC { get; set; }
        public virtual string NTNNo { get; set; }
        public virtual string ToBePrntd { get; set; }
        public virtual string ck_Act { get; set; }
        public virtual string MPurRmk { get; set; }        
        public virtual string PayTyp_id { get; set; }
        public virtual string vchtyp_id { get; set; }
        public virtual string csh_amt { get; set; }
        public virtual string CashBnk_id { get; set; }
        public virtual string chque_No { get; set; }
        public virtual string subheadcategoryfourID { get; set; }
        public virtual string Out_Standing { get; set; }

        


        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + MPurID.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            MPurchase mpurchase = obj as MPurchase;

            if (mpurchase == null)
            {
                return false;
            }

            if (this.MPurID == mpurchase.MPurID)
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