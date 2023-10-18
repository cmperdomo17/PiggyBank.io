using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace pkgPiggyBank.pkgDomain{
    public class clsBill : clsCoin, IComparable<clsBill>
    {
        
        #region Attributes
        private int attMonth;
        private int attDay;
        #endregion
        #region Operations
        
        #region Constructors
        public clsBill(string prmOID, double prmValue, int prmYear, clsCurrency prmCurrency, int prmMonth, int prmDay) : base(prmOID, prmValue, prmCurrency, prmYear){ 
            
            attDay = prmDay; 
            attMonth = prmMonth;
   
        }
        #endregion

        #region Getters
        public int getMonth() => attMonth; // Return attMonth

        public int getDay() => attDay; // Return attDay

        #endregion

        #region Setters
        public bool modifyThis(double prmValue, clsCurrency prmCurrency, int prmYear, int prmMonth, int prmDay){
            
            if(!base.modifyThis(prmValue, prmCurrency, prmYear)) return false;
            attDay = prmDay;
            attMonth = prmMonth;
            attYear = prmYear;

            return true;

        }
        #endregion

        #region Utilities
        public override string ToString(){
            return "{Bill Info}\n" + "{OID}:\t" + attOID + "\n{Value}:\t" + attValue + "\n{Currency}:\t" + attCurrency + "\n{Year}:\t" + attYear + "\n{Month}:\t" + attMonth + "\n{Day}:\t" + attDay + "\n" + attCurrency.ToString();
        }
        public int CompareTo(clsBill prmOther)
        {
            if (base.CompareTo(prmOther) == 0
                && attMonth == prmOther.attMonth
                && attDay == prmOther.attDay)
                return 0;
            return 1;
        }
        #endregion

        #region Destroyer
        public override bool die()
        {
            return base.die();
        }
        #endregion
        #endregion
    }
}