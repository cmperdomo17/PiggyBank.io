using System;
using System.Collections.Generic;
using libServices.pkgServices;
using libServices.pkgServices.pkgInterfaces;
using libServices.pkgServices.pkgCollections;

namespace pkgPiggyBank.pkgDomain{
    public class clsCurrency : clsEntity, IComparable<clsCurrency>
    {
        #region Attributes
        private string attName;
        private double attTRM;
        private List<clsCoin> attCoins;
        private List<clsBill> attBills;
        private clsPiggyBank attPiggy;   
        private List<double> attCoinsValues;
        private List<double> attBillsValues;
        #endregion

        #region Constructors
        public clsCurrency(string prmOID, string prmName, double prmTRM, List<double> prmCoinsValues, List<double> prmBillsValues) :base(prmOID)
        {
            attName = prmName;
            attTRM = prmTRM;
            attCoinsValues = prmCoinsValues;
            attBillsValues = prmBillsValues;
        }
        #endregion

        #region Getters
        public string getName()=>attName;

        public double getTRM()=>attTRM;

        public List<double> getCoinsValues() => attCoinsValues;

        public List<double> getBillsValues() => attBillsValues;
        #endregion

        #region Setters
        public bool modifyThis(string prmName, double prmTRM){
            attName = prmName;
            attTRM = prmTRM;
            return true;    
        }
        #endregion
       
        #region Utilities
        public override string ToString()
        {
            return "{OID]}:\t + attOID + \n{Name}:\t + attName + \n{TRM}:\t + attTRM + \n";
        }
        public int CompareTo(clsCurrency prmOther)
        {
            if (attOID == prmOther.attOID
                && attName == prmOther.attName
                && attTRM == prmOther.attTRM
                && clsCollections<clsCoin>.areEqualCollections(attCoins, prmOther.attCoins)
                && clsCollections<clsBill>.areEqualCollections(attBills, prmOther.attBills)
                && attPiggy.CompareTo(prmOther.attPiggy) == 0
                && clsCollections<double>.areEqualCollections(attCoinsValues, prmOther.attCoinsValues)
                && clsCollections<double>.areEqualCollections(attBillsValues, prmOther.attBillsValues))
                return 0;
            return 1;
        }
        #endregion
    }
}