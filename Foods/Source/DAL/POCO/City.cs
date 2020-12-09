using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foods
{
    [Serializable]
    public class City
    {
        private string cityid;
        private string city;

        public virtual string CityID { get; set; }
        public virtual string City_ { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + CityID.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            City city = obj as City;

            if (city == null)
            {
                return false;
            }

            if (this.CityID == city.CityID)
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