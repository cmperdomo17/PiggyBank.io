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
        private clsCurrency() { }
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

        public override bool modify(List<object> prmArgs){
            if ((string)prmArgs[0] != attOID) return false;
            clsCurrency varObjMemento = new clsCurrency();
            this.copyTo(varObjMemento);
            try {
                attName = (string)prmArgs[0];
                attTRM = (double)prmArgs[1];
                return true;
            }
            catch (Exception e){
                varObjMemento.copyTo(this);
                return false;
            }
        }
        public bool setPiggyBank(clsPiggyBank prmObj){
            if (prmObj == null) return false;
            if (attPiggy != null && attPiggy.getMoneyItemsCount() != 0) return false;
            if (prmObj.getCurrency().CompareTo(this) != 0) return false;
            attPiggy = prmObj;
            return true;
        }
        #endregion   
        #region Utilities
        public override string ToString()
        {
            return "{OID]}:\t + attOID + \n{Name}:\t + attName + \n{TRM}:\t + attTRM + \n";
        }
        public int CompareTo(clsCurrency <oidType> prmOther)
        {
            if (attOID == prmOther.attOID
                && attName == prmOther.attName
                && attTRM == prmOther.attTRM
                && ClsCollections<string, clsCoin>.areEqualsCollections(attCoins, prmOther.attCoins)
                && ClsCollections<string, clsBill>.areEqualsCollections(attBills, prmOther.attBills)
                && attPiggy.CompareTo(prmOther.attPiggy) == 0
                && ClsCollections<double>.areEqualsCollections(attCoinsValues, prmOther.attCoinsValues)
                && ClsCollections<double>.areEqualsCollections(attBillsValues, prmOther.attBillsValues)
                )
                return 0;
            return 1;
        }
        
        public override bool copyTo<T>(T prmObject)
        {
            clsCurrency varObjOther = prmObject as clsCurrency;
            if (prmObject.GetType() != typeof(clsCurrency)) return false;

            varObjCurrency.attOID = attOID;
            varObjCurrency.attName = attName;
            varObjCurrency.attTRM = attTRM;
            varObjCurrency.attCoins = attCoins;
            varObjCurrency.attBills = attBills;
            varObjCurrency.attPiggy = attPiggy;
            varObjCurrency.attCoinsValues = attCoinsValues;
            varObjCurrency.attBillsValues = attBillsValues;
            return true;
        }   
        #endregion
        #region CRUDS
        public bool toRegisterCoin(string prmOID, double prmValue, int prmYear){
            return clsCollections.toRegisterEntity(new clsCoin(prmOID, prmValue, this, prmYear), attCoins);
        }

        public bool toRegisterBill(string prmOID, double prmValue, int prmDay, int prmMonth, int prmYear){
            return clsCollections.toRegisterEntity(new clsBill(prmOID, prmValue, this, prmDay, prmMonth, prmYear), attBills);
        }
        #endregion
    }
}