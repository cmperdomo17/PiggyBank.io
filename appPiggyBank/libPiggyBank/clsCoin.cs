using System;
using pkgServices;
using pkgServices.pkgInterfaces;

namespace pkgPiggyBank.pkgDomain
{
   public class clsCoin:clsEntity,IComparable<clsCoin>
   {
        #region Attributes
        protected double attValue;
        protected int attYear;
        protected clsCurrency attCurrency;
        protected clsPiggyBank attPiggy;
        #endregion
        #region Operations
        #region Constructors
        public clsCoin(string prmOID, double prmValue, clsCurrency prmCurrency, int prmYear) : base(prmOID)
        {
            attValue = prmValue;
            attCurrency = prmCurrency;
            attYear = prmYear;
        }
        #endregion
        #region Getters
        public double getValue() => attValue;
        public int getYear() => attYear;
        public clsCurrency getCurrency() => attCurrency;
        public clsPiggyBank getPiggy() => attPiggy;
        #endregion
        #region Setters
        public bool modifyThis(double prmValue, clsCurrency prmCurrency, int prmYear)
        {
            if (attPiggy != null) return false;
            attValue = prmValue;
            attCurrency = prmCurrency;
            attYear = prmYear;
            return true;
        }

        #endregion
        #region Utilities
        public override string toString()
        {
            return "{Coin Info}\n" + "{OID}:\t" + attOID + "\n" + "{Value}:\t" + attValue + "\n" + "{Year}:\t" + attYear + "\n" + attCurrency.ToString();
        }
        public int CompareTo(clsCoin prmOther)
        {
            if(attOID == prmOther.attOID && attValue == prmOther.attValue && attYear == prmOther.attYear && attCurrency == prmOther.attCurrency && attPiggy == prmOther.attPiggy) return 0;
            if (attValue > prmOther.attValue) 
                return 1;
            return -1;
        }

        #endregion
        #region Destroyer
        public override bool die()
        {
            throw new NotImplementedException();
        } 
        #endregion
        #endregion

    }
}