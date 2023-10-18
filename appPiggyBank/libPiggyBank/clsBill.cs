using System;
using pkgServices;
using pkgServices.pkgInterfaces;

namespace pkgPiggyBank.pkgDomain
{
   public class clsBill : clsCoin, IComparable<clsBill>
   {
        #region Attributes
        private int attMonth;
        private int attDay;

        #endregion
        #region Operations
        #region Constructors
        public clsBill(string prmOID, double prmValue, clsCurrency prmCurrency, int prmYear, int prmMonth, int prmDay) :
            base(prmOID, prmValue, prmCurrency, prmYear)
        {
            attDay = prmDay;
            attMonth = prmMonth;
        }
        #endregion
        #region Getters
        public int getMonth() => attMonth;
        public int getDay() => attDay;
        #endregion
        #region Setters
        public bool modifyThis(double prmValue, clsCurrency prmCurrency, int prmYear, int prmMonth, int prmDay)
        {
            if (!base.modifyThis(prmValue, prmCurrency, prmYear)) return false;
            attMonth = prmMonth;
            attDay = prmDay;
            return true;
        }

        #endregion
        #region Destroyer
        public override bool die()
        {
            return base.die();
        }
        #endregion
        #region Utilities
        public int CompareTo(clsBill prmOther)
        {
            if (base.CompareTo(prmOther) == 0 && (attMonth == prmOther.attMonth && attDay == prmOther.attDay))
                return 0;
            if(attValue>prmOther.attValue)
                return 1;
            return -1;
        }
        public override string toString()
        {
            return "{Bill Info}\n" + "{OID}:\t" + attOID + "\n" + "{Value}:\t" + attValue + "\n" + "{Year}:\t" + attYear + "\n" + "{Month}:\t" + attMonth + "\n" + "{Day}:\t" + attDay + "\n" + attCurrency.ToString();
        }

        
        #endregion
        #endregion
    }
}