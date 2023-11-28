using System;
using System.Collections.Generic;
using pkgServices.pkgCollections;
using pkgServices;
using System.Data.Common;

namespace pkgPiggyBank.pkgDomain
{
   public class clsController
   {
        #region Attributes
        private static clsController attInstance;
        private  List<clsCurrency> attCurrencies;
        private  clsPiggyBank attPiggy;
        #endregion
        #region Constructors
        private clsController() { }
        #endregion
        #region Getters
        public static clsController getInstance()
        {
            if (attInstance == null) { 
                attInstance = new clsController();
            }
            return attInstance;
        }
        
        #endregion
        #region Cruds
        public bool toRegisterCurrency(string prmOID, string prmName, double prmTRM, List<double> prmCoinsValues, List<double> prmBillsValues)
        {
            return clsBrokerCrud.toRegisterEntity(new clsCurrency(prmOID, prmName, prmTRM, prmCoinsValues, prmBillsValues), attCurrencies);
        }

        public bool toRegisterPiggyBank(string prmOIDCurrency, int prmCoinsMaxCap, int prmBillsMaxCap, List<double> prmCoinsValues, List<double> prmBillsValues)
        {
            if(attPiggy!=null) return false;
            clsCurrency varObj = clsCollections.getItemWith(prmOIDCurrency, attCurrencies);
            if (varObj == null) return false;
            attPiggy=new clsPiggyBank(prmCoinsMaxCap, prmBillsMaxCap, prmCoinsValues, prmBillsValues, varObj);
            varObj.setPiggyBank(attPiggy);
            return true;
        }

        public bool toRegisterCoin(string prmOIDCurrency, string prmOID, double prmValue, int prmYear)
        {
            try
            {
                return clsCollections.getItemWith(prmOIDCurrency, attCurrencies).toRegisterCoin(prmOID, prmValue, prmYear); 
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool toRegisterBill(string prmOIDCurrency, string prmOID, double prmValue, int prmDay, int prmMonth, int prmYear)
        {
            try
            {
                return clsCollections.getItemWith(prmOIDCurrency, attCurrencies).toRegisterBill(prmOID, prmValue, prmDay, prmMonth, prmYear);
            }
            catch (Exception e) {
                return false;
            }
        }

        public bool toUptadeCurrency(string prmOID, string prmName, double prmTRM, List<double> prmCoinsValues, List<double> prmBillsValues)
        {
            clsCurrency varObj = clsCollections.getItemWith(prmOID, attCurrencies);
            if(varObj==null)return false;   
            return varObj.modify(prmName, prmTRM, prmCoinsValues, prmBillsValues);       
        }

        public bool toUpdatePiggyBank(string prmOIDCurrency, int prmCoinsMaxCap, int prmBillsMaxCap, List<double> prmCoinsValues, List<double> prmBillsValues)
        {
            if (attPiggy == null) return false;
            clsCurrency varObj = clsCollections.getItemWith(prmOIDCurrency, attCurrencies);
            if (varObj == null) return false;
            return attPiggy.modify(varObj, prmCoinsMaxCap, prmBillsMaxCap, prmCoinsValues, prmBillsValues);
        }

        public bool toUpdateCoin(string prmOIDCurrency, string prmOID, double prmValue, int prmYear)
        {
            clsCurrency varObj= clsCollections.getItemWith(prmOIDCurrency, attCurrencies);
            if (varObj == null) return false;
            return varObj.toUpdateCoin(prmOID, prmValue, prmYear);
        }

        public bool toUpdateBill(string prmOIDCurrency, string prmOID, double prmValue, int prmDay, int prmMonth, int prmYear)
        {
            clsCurrency varObj = clsCollections.getItemWith(prmOIDCurrency, attCurrencies);
            if (varObj == null) return false;
            return varObj.toUpdateBill(prmOID, prmValue, prmYear);
        }

        public bool toDeleteCurrency(string prmOID)
        {
            clsCurrency varObj = clsCollections.getItemWith(prmOID, attCurrencies);
            if (varObj == null) return false;
            if (!varObj.die()) return false;
            return attCurrencies.Remove(varObj);

        }

        public bool toDeletePiggyBank()
        {
            if(attPiggy==null) return false;
            return attPiggy.die();
        }

        public bool toDeleteCoin(string prmOIDCurrency, string prmOID)
        {
            clsCurrency varObj = clsCollections.getItemWith(prmOIDCurrency, attCurrencies);
            if (varObj == null) return false;
            return varObj.toDeleteCoin(prmOID);
        }

        public bool toDeleteBill(string prmOIDCurrency, string prmOID)
        {
            clsCurrency varObj = clsCollections.getItemWith(prmOIDCurrency, attCurrencies);
            if (varObj == null) return false;
            return varObj.toDeleteBill(prmOID);
        }


        #endregion
        #region Transactions
        public bool coinsIncome(List<double> prmValues)
        {
            if (attPiggy == null) return false;
            clsCurrency varObj = attPiggy.getCurrency();
            if(varObj== null) return false;
            if (!varObj.areValidValuesAsCoins(prmValues)) return false;
            List<clsCoin> varCoins = varObj.retrieveAsOutsideCoins(prmValues);
            if(varCoins == null) return false;
            if (varCoins.Count!=prmValues.Count) return false;  
            return attPiggy.coinsIncome(varCoins);
        }

        public bool coinsWithdrawal(List<double> prmValues)
        {
            if (attPiggy == null) return false;
            clsCurrency varObj = attPiggy.getCurrency();
            if (varObj == null) return false;
            List<clsCoin> varCoins = varObj.retrieveAsInsideCoins(prmValues);
            if(varCoins == null) return false;
            return attPiggy.coinsWithdrawal(varCoins);
        }

        public bool billsIncome(List<double> prmValues)
        {
            if(attPiggy == null) return false;
            clsCurrency varObj = attPiggy.getCurrency();
            if (varObj == null) return false;
            if (!varObj.areValidValuesAsBills(prmValues)) return false;
            List<clsBill> varBills = varObj.retrieveAsOutsideBills(prmValues);
            if (varBills == null) return false;
            return attPiggy.billsIncome(varBills);
        }

        public bool billsWithdrawal(List<double> prmValues)
        {
            if (attPiggy == null) return false;
            clsCurrency varObj = attPiggy.getCurrency();
            if (varObj == null) return false;
            List<clsBill> varBills = varObj.retrieveAsInsideBills(prmValues);
            if (varBills == null) return false;
            return attPiggy.billsWithdrawal(varBills);
        } 
        #endregion
    }
}