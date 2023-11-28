using System;
using System.Collections.Generic;
using pkgServices;
using pkgServices.pkgInterfaces;
using pkgServices.pkgCollections;

namespace pkgPiggyBank.pkgDomain{
    public class clsCurrency : clsEntity, IComparable<clsCurrency>
    {
         #region Attributes
        private string OID;
        private string attName;
        private double attTRM;
        private List<clsBill> attBills;
        private List<clsCoin> attCoins;
        private clsPiggyBank attPiggy;
        private List<double> attCoinsValues;
        private List<double> attBillsValues;
        #endregion
        #region Operations
        #region Constructors
        private clsCurrency() { }
        public clsCurrency(string prmOID, string prmName, double prmTRM, List<double> prmCoinsValues, List<double> prmBillsValues) : base(prmOID)
        {
            attName = prmName;
            attTRM = prmTRM;
            attCoinsValues = prmCoinsValues;
            attBillsValues = prmBillsValues;
        }
        #endregion
        #region Getters
        public string getName() => attName;
        public double getTRM() => attTRM;
        public List<double> getCoinsValues() => attCoinsValues;
        public List<double> getBillsValues() => attBillsValues;
        public List<clsCoin> retrieveAsOutsideCoins(List<double> prmValues)
        {
            List<clsCoin> varResult = new List<clsCoin>();
            int varToFoundItems = prmValues.Count; 
            foreach (clsCoin varObj in attCoins)
            {
                if(varObj.getPiggy()==null && clsCollections.getIndexOf(varObj.getValue(),prmValues)!=-1)
                {
                    prmValues.Remove(varObj.getValue());
                    varResult.Add(varObj);
                    varToFoundItems--;
                }
            }
            if (varToFoundItems != 0) return null;
            return varResult;
        }
        public List<clsBill> retrieveAsOutsideBills(List<double> prmValues)
        {
            List<clsBill> varResult = new List<clsBill>();
            int varToFoundItems = prmValues.Count;
            foreach (clsBill varObj in attBills)
            {
                if (varObj.getPiggy() == null && clsCollections.getIndexOf(varObj.getValue(), prmValues) != -1)
                {
                    prmValues.Remove(varObj.getValue());
                    varResult.Add(varObj);
                    varToFoundItems--;
                }
            }
            if (varToFoundItems != 0) return null;
            return varResult;
        }
        public List<clsCoin> retrieveAsInsideCoins(List<double> prmValues)
        {
            List<clsCoin> varResult = new List<clsCoin>();
            int varToFoundItems = prmValues.Count;
            foreach (clsCoin varObj in attCoins)
            {
                if (varObj.getPiggy() != null && clsCollections.getIndexOf(varObj.getValue(), prmValues) != -1)
                {
                    prmValues.Remove(varObj.getValue());
                    varResult.Add(varObj);
                    varToFoundItems--;
                }
            }
            if (varToFoundItems != 0) return null;
            return varResult;
        }
        public List<clsBill> retrieveAsInsideBills(List<double> prmValues)
        {
            List<clsBill> varResult = new List<clsBill>();
            int varToFoundItems = prmValues.Count;
            foreach (clsBill varObj in attBills)
            {
                if (varObj.getPiggy() != null && clsCollections.getIndexOf(varObj.getValue(), prmValues) != -1)
                {
                    prmValues.Remove(varObj.getValue());
                    varResult.Add(varObj);
                    varToFoundItems--;
                }
            }
            if (varToFoundItems != 0) return null;
            return varResult;
        }

        #endregion
        #region Setters
        public bool modify(string prmName, double prmTRM, List<double> prmCoinsValues, List<double> prmBillsValues) 
        {
            if (attCoins.Count != 0) return false;
            if (attBills.Count == 0) return false;
            attName = prmName;
            attTRM = prmTRM;        
            attCoinsValues= prmCoinsValues;
            attBillsValues= prmBillsValues;
            return true;
        }
        
        public bool setPiggyBank(clsPiggyBank prmObj)
        {
            if (prmObj == null) return false;
            if (attPiggy != null && attPiggy.getMoneyItemsCount()!=0) return false;
            if (prmObj.getCurrency().CompareTo(this) != 0) return false;
            attPiggy=prmObj;
            return true;
        }
        
        #endregion
        #region CRUD
        public bool toRegisterCoin(string prmOID, double prmValue, int prmYear)
        {
            return clsBrokerCrud.toRegisterEntity(new clsCoin(prmOID, prmValue, this, prmYear), attCoins);
        }
        public bool toRegisterBill(string prmOID, double prmValue, int prmDay, int prmMonth, int prmYear)
        {
            return clsBrokerCrud.toRegisterEntity(new clsBill(prmOID, prmValue, this, prmDay, prmMonth, prmYear), attBills);
        }
        public bool toUpdateCoin(string prmOID, double prmValue, int prmYear)
        {
            clsCoin varObj = clsCollections.getItemWith(prmOID, attCoins);
            if (varObj == null) return false;
            return varObj.modify(prmValue, prmYear);
        }

        public bool toUpdateBill(string prmOID, double prmValue, int prmYear)
        {
            clsBill varObj = clsCollections.getItemWith(prmOID, attBills);
            if (varObj == null) return false;
            return varObj.modify(prmValue, prmYear);
        }

        public bool toDeleteCoin(string prmOID)
        {
            clsCoin varObj = clsCollections.getItemWith(prmOID, attCoins);
            if (varObj == null) return false;
            return varObj.die();
        }

        public bool toDeleteBill(string prmOID)
        {
            clsBill varObj = clsCollections.getItemWith(prmOID, attBills);
            if (varObj == null) return false;
            return varObj.die();
        }

        #endregion
        #region Utilities
        public override string toString()
        {
            return "{Currency Info}\n" + 
                "{OID}:\t" + attOID + "\n" + 
                "{Name}:\t" + attName + "\n" + 
                "{TRM}:\t" + attTRM + "\n";
        }


        public int CompareTo(clsCurrency prmOther)
        {
            if (attOID.CompareTo(prmOther.attOID) == 0 &&
                attName == prmOther.attName &&
                attTRM == prmOther.attTRM &&
                clsCollections.areEqualsCollections(attCoins, prmOther.attCoins) &&
                clsCollections.areEqualsCollections(attBills, prmOther.attBills) &&
                attPiggy.CompareTo(prmOther.attPiggy) == 0 &&
                clsCollections.areEqualsCollections(attCoinsValues, prmOther.attCoinsValues) &&
                clsCollections.areEqualsCollections(attBillsValues, prmOther.attBillsValues)) {
                return 0;
            }
            return 1;
        }
        public override bool copyTo<T>(T prmOtherObject)
        {
            clsCurrency varObjOther =prmOtherObject as clsCurrency;
            if (varObjOther == null) return false;
            varObjOther.attName = attName;
            varObjOther.attTRM = attTRM;
            varObjOther.attCoins = attCoins;
            varObjOther.attBills = attBills;
            varObjOther.attPiggy = attPiggy;
            varObjOther.attCoinsValues=attCoinsValues;
            varObjOther.attBillsValues = attBillsValues;
            return true;
        }

        #endregion
        #region Query
        public bool areValidValuesAsCoins(List<double> prmValues) => clsCollections.isSubSet(prmValues, attCoinsValues);
        public bool areValidValuesAsBills(List<double> prmValues) => clsCollections.isSubSet(prmValues, attBillsValues);

        #endregion
        #region Destroyer
        public override bool die()
        {
            if (attCoins.Count!=0) return false;
            if (attBills.Count != 0) return false;
            if (attPiggy != null) return false;
            return true;
        }
        #endregion

        #endregion

    }

}
