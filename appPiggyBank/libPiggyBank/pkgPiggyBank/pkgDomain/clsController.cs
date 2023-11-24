using System;
using System.Collections.Generic;
using pkgServices.pkgCollections;
using pkgServices;
using System.Data.Common;

namespace pkgPiggyBank.pkgDomain{
    public class clsController{

        #region Attributes
        private static clsController? attInstance; // Singleton
        private List<clsCurrency> attCurrencies;
        private clsPiggyBank? attPiggy;
        #endregion

        #region Constructors
        private clsController(){
        }
        #endregion

        #region Cruds
        public bool toRegisterCurrency(string prmOID, string prmName, double prmTRM, List<double> prmCoinsValues, List<double> prmBillsValues){
            return clsBrokerCrud.toRegisterEntity(new clsCurrency(prmOID, prmName, prmTRM, prmCoinsValues, prmBillsValues), attCurrencies);
        }

        public bool toRegisterPiggyBank(string prmOIDCurrency, int prmCoinsMaxCap, int prmBillsMaxCap, List<double> prmCoinsCap, List<double> prmBillsCap, List<double> prmCoinsCount, List<double> prmBillsCount){
            if (attPiggy != null) return false;
            clsCurrency? varObj = clsCollections.getItemWith(prmOIDCurrency, attCurrencies);
            if (varObj == null) return false;
            attPiggy = new clsPiggyBank(prmCoinsMaxCap, prmBillsMaxCap, prmCoinsCap, prmBillsCap, varObj);
            varObj.setPiggyBank(attPiggy);
            return true;
        }

        public bool toRegisterCoin(string prmOIDCurrency, string prmOID, double prmValue, int prmYear){
            try{
                return clsCollections.getItemWith(prmOIDCurrency, attCurrencies).toRegisterCoin(prmOID, prmValue, prmYear);
            }
            catch (Exception e){
                return false;
            }
        }

        public bool toRegisterBill(string prmOIDCurrency, string prmOID, double prmValue, int prmDay, int prmMonth, int prmYear){
            try{
                return clsCollections.getItemWith(prmOID, attCurrencies).toRegisterBill(prmOID, prmValue, prmDay, prmMonth, prmYear);
            }
            catch (Exception e){
                return false;
            }
        }

        public bool toUpdateCurrency(string prmOID, string prmName, double prmTRM){
            clsCurrency? varObj = clsCollections.getItemWith(prmOID, attCurrencies);
            // 1. No se puede modificar la divisa si no existe
            if (varObj == null) return false;
            // 2. No se puede modificar la divisa si ya existe una alcancia
            if (attPiggy != null) return false;
            // 3. No se puede modificar la divisa si ya existen monedas o billetes
            if (varObj.getSizeCoins() != 0) return false;

            // Actualizar la divisa, mandando una lista de argumentos
            varObj.modify(new List<object> { prmOID, prmName, prmTRM });
            return true;
        }

        public bool toUpdatePiggyBank(string prmOID, string prmName){
            throw new NotImplementedException();
        }

        public bool toUpdateCoin(string prmOID, double prmValue, string prmCurrencyOID, int prmYear){
            throw new NotImplementedException();
        }

        public bool toUpdateBill(string prmOID, double prmValue, string prmCurrencyOID, int prmYear, int prmMonth, int prmDay){
            throw new NotImplementedException();
        }

        public bool toDeleteCurrency(string prmOID){
            throw new NotImplementedException();
        }

        public bool toDeletePiggyBank(string prmOID){
            throw new NotImplementedException();
        }

        public bool toDeleteCoin(string prmOID){
            throw new NotImplementedException();
        }

        public bool toDeleteBill(string prmOID){
            throw new NotImplementedException();
        }
        #endregion

        #region Getters
        public static clsController getInstance(){ // Singleton: Unicamente se puede instanciar una vez
            if (attInstance == null)
                attInstance = new clsController();
            return attInstance;
        }

        public clsCurrency getCurrencyWith(string prmOID)
        {
            return clsCollections.getItemWith(prmOID, attCurrencies);
        }


        #region Transaction

        public bool coinsIncome(List<double> prmValues){
            if (attPiggy == null) return false;
            clsCurrency varObj = attPiggy.getCurrency();
            if (varObj == null) return false;
            if (!varObj.AreValidCoins(prmValues)) return false; 
            List <clsCoin> varCoins = varObj.RetrieveAsOutsideCoins(prmValues); 
            if (varCoins == null) return false;
            return attPiggy.coinsIncome(varCoins);
        }

        public bool coinsWithdrawal(List<double> prmValues)
        {
            if (attPiggy == null) return false;
            clsCurrency varObj = attPiggy.getCurrency();
            if (varObj == null) return false;
            List <clsCoin> varCoins = varObj.RetrieveAsInsideCoins(prmValues); 
            if (varCoins == null) return false;
            return attPiggy.coinsWithdrawal(varCoins);
        }

        public bool billsIncome(List<double> prmValues){
            if (attPiggy == null) return false;
            clsCurrency varObj = attPiggy.getCurrency();
            if (varObj == null) return false;
            if (!varObj.AreValidBills(prmValues)) return false; 
            List <clsBill> varBills = varObj.RetrieveAsOutsideBills(prmValues); 
            if (varBills == null) return false;
            return attPiggy.billsIncome(varBills);
        } 

        public bool billsWithdrawal(List<double> prmValues)
        {
            if (attPiggy == null) return false;
            clsCurrency varObj = attPiggy.getCurrency();
            if (varObj == null) return false;
            List <clsBill> varBills = varObj.RetrieveAsInsideBills(prmValues);
            if (varBills == null) return false;
            return attPiggy.billsWithdrawal(varBills);
        }

        #endregion

    }
}
#endregion