using System;
using System.Collections.Generic;
using pkgServices;
using pkgServices.pkgInterfaces;
using pkgServices.pkgCollections;

namespace pkgPiggyBank.pkgDomain{
    public class clsCurrency : clsEntity, IComparable<clsCurrency>
    {
        #region Attributes
        private string? attName;
        private double attTRM = 0;
        private List<clsCoin>? attCoins;
        private List<clsBill>? attBills;
        private clsPiggyBank? attPiggy;
        private List<double>? attCoinsValues;
        private List<double>? attBillsValues;
        #endregion
        #region Constructors
        public clsCurrency()
        {
        }
        public clsCurrency(string prmOID, string prmName, double prmTRM, List<double> prmCoinsValues, List<double> prmBillsValues) :base(prmOID)
        {
            attName = prmName;
            attTRM = prmTRM;
            attCoinsValues = prmCoinsValues;
            attBillsValues = prmBillsValues;
        }
        #endregion
        #region Getters
        public string getName() => attName;

        public double getTRM()=>attTRM;

        public List<double> getCoinsValues() => attCoinsValues;

        public List<double> getBillsValues() => attBillsValues;
        public int getSizeCoins() => attCoins.Count;
        public int getSizeBills() => attBills.Count;
        

        // ! ---------------------------------- METODOS POR REVISAR -------------------------------------//


        public List <clsCoin> RetrieveAsInsideCoins(List<double> prmValues)
        {
            List <clsCoin> varResult = new List<clsCoin>();
            int varToFoundItems = prmValues.Count;
            foreach (clsCoin varObj in attCoins){
                if (varObj.getPiggy() != null && clsCollections.getIndexOf(varObj.getValue(), prmValues) != -1){
                    varResult.Add(varObj);
                    prmValues.Remove(varObj.getValue());
                    varToFoundItems--;
                }
            }
            if (varToFoundItems != 0) return null;
            return varResult;
        }

        public List <clsBill> RetrieveAsInsideBills(List<double> prmValues)
        {
            List <clsBill> varResult = new List<clsBill>();
            int varToFoundItems = prmValues.Count;
            foreach (clsBill varObj in attBills){
                if (varObj.getPiggy() != null && clsCollections.getIndexOf(varObj.getValue(), prmValues) != -1){
                    varResult.Add(varObj);
                    prmValues.Remove(varObj.getValue());
                    varToFoundItems--;
                }
            }
            if (varToFoundItems != 0) return null;
            return varResult;
        }
        public List <clsCoin> RetrieveAsOutsideCoins(List<double> prmValues)
        {
            List <clsCoin> varResult = new List<clsCoin>();
            int varToFoundItems = prmValues.Count;
            foreach (clsCoin varObj in attCoins){
                if (varObj.getPiggy() == null && clsCollections.getIndexOf(varObj.getValue(), prmValues) != -1){
                    varResult.Add(varObj);
                    prmValues.Remove(varObj.getValue());
                    varToFoundItems--;
                }
            }
            if (varToFoundItems != 0) return null;
            return varResult;
        }

        public List <clsBill> RetrieveAsOutsideBills(List<double> prmValues)
        {
            List <clsBill> varResult = new List<clsBill>();
            int varToFoundItems = prmValues.Count;
            foreach (clsBill varObj in attBills){
                if (!prmValues.Contains(varObj.getValue())){
                    varResult.Add(varObj);
                    prmValues.Remove(varObj.getValue());
                    varToFoundItems--;
                }
            }
            if (varToFoundItems != 0) return null;
            return varResult;
        }

        //! NO USAR 
        public List <clsBill> RetriveAsInsideOutsideBills(List <double> prmValues, bool prmInside){
            List <clsBill> varResult = new List<clsBill>();
            int varToFoundItems = prmValues.Count;
            foreach (clsBill varObj in attBills){
                if ((prmInside? varObj.getPiggy() != null : varObj.getPiggy() == null) && clsCollections.getIndexOf(varObj.getValue(), prmValues) != -1){
                    varResult.Add(varObj);
                    prmValues.Remove(varObj.getValue());
                    varToFoundItems--;
                }
            }
            if (varToFoundItems != 0) return null;
            return varResult;
        }
        
        // ! ---------------------------------- METODOS POR REVISAR -------------------------------------//

        
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
        public int CompareTo(clsCurrency prmOther)
        {
            if (attOID == prmOther.attOID
                && attName == prmOther.attName
                && attTRM == prmOther.attTRM
                && clsCollections.areEqualsCollections(attCoins, prmOther.attCoins)
                && clsCollections.areEqualsCollections(attBills, prmOther.attBills)
                && attPiggy.CompareTo(prmOther.attPiggy) == 0
                && clsCollections.areEqualsCollections(attCoinsValues, prmOther.attCoinsValues)
                && clsCollections.areEqualsCollections(attBillsValues, prmOther.attBillsValues)
                )
                return 0;
            return 1;
        }
        
        public override bool copyTo<T>(T prmObject)
        {
            clsCurrency? varObjOther = prmObject as clsCurrency;
            if (prmObject.GetType() != typeof(clsCurrency)) return false;

            varObjOther.attOID = attOID;
            varObjOther.attName = attName;
            varObjOther.attTRM = attTRM;
            varObjOther.attCoins = attCoins;
            varObjOther.attBills = attBills;
            varObjOther.attPiggy = attPiggy;
            varObjOther.attCoinsValues = attCoinsValues;
            varObjOther.attBillsValues = attBillsValues;
            return true;
        }   

        // ! ---------------------------------- METODOS POR REVISAR -------------------------------------//


        #region Query

        public bool AreValidCoins(List<double> prmValues) => clsCollections.isSubSet(prmValues, attCoinsValues);
        public bool AreValidBills(List<double> prmValues) => clsCollections.isSubSet(prmValues, attBillsValues);

        #endregion

        // ! ---------------------------------- METODOS POR REVISAR -------------------------------------//

        #endregion
        #region CRUDS
        public bool toRegisterCoin(string prmOID, double prmValue, int prmYear){
            return clsBrokerCrud.toRegisterEntity(new clsCoin(prmOID, prmValue, this, prmYear), attCoins);
        }

        public bool toRegisterBill(string prmOID, double prmValue, int prmDay, int prmMonth, int prmYear){
            return clsBrokerCrud.toRegisterEntity(new clsBill(prmOID, prmValue, prmDay, this, prmMonth, prmYear), attBills);
        }
        #endregion
    }
}