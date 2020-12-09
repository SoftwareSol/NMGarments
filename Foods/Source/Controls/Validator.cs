using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using Foods;

namespace Foods
{
    public class Validator
    {
        public bool Username(TextBox txt)
        {
            bool usr = false;
            Regex regexobj = new Regex(@"^[a-zA-Z]+$");
            if (!regexobj.IsMatch(txt.Text))
            {
                usr = true;
            }
            else
            {
                usr = false;
            }
            return usr;
        }

        public bool PhoneValidate(string txt)
        {
            bool phn = false;
            Regex regexobj = new Regex(@"^([0-9]*|\d*\.\d{1}?\d*)$");
            if (!regexobj.IsMatch(txt))
            {
                phn = true;
            }
            else
            {

                phn = false;
            }
            return phn;
        }
        public bool PhoneValidate1(TextBox txt)
        {
            bool phn = false;
            Regex regexobj = new Regex(@"^([0-9]*|\d*\.\d{1}?\d*)$");
            if (!regexobj.IsMatch(txt.Text))
            {
                phn = true;
            }
            else
            {
                phn = false;
            }
            return phn;
        }

        public bool EmailValidate(string txt)
        {
            bool email = false;
            Regex regexobj = new Regex(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$");
            if (!regexobj.IsMatch(txt))
            {
                email = true;
            }
            else
            {
                email = false;
               
            }
            return email;
        }
        public bool nullvalue(DropDownList txt)
        {
            bool nullval = false;
            Regex regexobj = new Regex(@"/^\s*\S.*$/");
            if (!regexobj.IsMatch(txt.Text))
            {
                nullval = false;
            }
            else
            {
                nullval = true;
            }
            return nullval;
        }

       
    }
}