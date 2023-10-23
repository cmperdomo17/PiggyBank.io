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

        public override bool modify(List<object> prmArgs){
            clsBill varObjMemento = new clsBill();
            this.copyTo(varObjMemento);
            if(!base.modify(prmArgs)) return false;
            try {
                attMonth = (int)prmArgs[4];
                attDay = (int)prmArgs[5];
                return true;
            }
            catch (Exception e){
                varObjMemento.copyTo(this);
                return false;
            }
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

        public override bool copyTo<T>(T prmOtherObject)
        {
            clsBill varObjOther = prmOtherObject as clsBill;
            if (varObjOther == null) return false;
            varObjOther.attOID = attOID;
            varObjOther.attValue = attValue;
            varObjOther.attCurrency = attCurrency;
            varObjOther.attYear = attYear;
            varObjOther.attMonth = attMonth;
            varObjOther.attDay = attDay;
            return true;
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