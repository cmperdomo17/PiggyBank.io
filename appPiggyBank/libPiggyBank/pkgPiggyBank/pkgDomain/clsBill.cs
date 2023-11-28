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
        private clsBill() { }
        public clsBill(string prmOID, double prmValue, clsCurrency prmCurrency, int prmDay, int prmMonth, int prmYear) :
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
        public bool modify(List<object> prmArgs)
        {
            if (!base.modify(prmArgs)) return false;
                attMonth = (int)prmArgs[4];
                attDay = (int)prmArgs[5];
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
            if (base.CompareTo(prmOther)==0 && (attMonth == prmOther.attMonth && attDay == prmOther.attDay))
            {
                return 0;
            }
            if(attValue>prmOther.attValue) { 
                return 1;
            }
            return -1;
        }
        public override string toString() { 
            return "{Bill Info}\n"+
                   "{OID}\t" + attOID + "\n" + 
                   "{Value}:\t" + attValue + "\n" +
                   "{Day}:\t" + attDay + "\n" +
                   "{Month}:\t" + attMonth + "\n" +
                   "{Year}:\t" + attYear + "\n" +
                   "{OID-Currency}" + attCurrency.getOID();
        }
        public override bool copyTo<T>(T prmOtherObject)
        {
            clsBill varObjOther =prmOtherObject as clsBill;
            if (varObjOther==null) return false;
            if (!base.copyTo(varObjOther)) return false;
            varObjOther.attMonth=attMonth;
            varObjOther.attDay=attDay;
            return true;
        }
        #endregion 
        #endregion
    }
}