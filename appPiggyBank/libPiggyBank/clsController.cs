using System;
using System.Collections.Generic;

namespace pkgPiggyBank.pkgDomain{
    public class clsController{

        #region Attributes
        private static clsController attInstance; // Singleton
        private static List<clsCurrency> attCurrencies;
        private static clsPiggyBank attPiggy;
        #endregion

        #region Constructors
        private clsController(){
        }
        #endregion

        #region Cruds
        public static bool toRegisterCurrency(string prmOID, string prmName, double prmTRM, List<double> prmCoinsValues, List<double> prmBillsValues){
            
            if (getCurrencyWith(prmOID) != null) return false; // Ya existe una moneda con ese OID
        
            attCurrencies.Add(new clsCurrency(prmOID, prmName, prmTRM, prmCoinsValues, prmBillsValues););
            
            return true;

        }

        public static bool toRegisterPiggyBank(string prmOID, string prmName){
            throw new NotImplementedException();
        }

        public static bool toRegisterCoin(string prmOID, double prmValue, string prmCurrencyOID, int prmYear){
            throw new NotImplementedException();
        }

        public static bool toRegisterBill(string prmOID, double prmValue, string prmCurrencyOID, int prmYear, int prmMonth, int prmDay){
            throw new NotImplementedException();
        }

        public static bool toUpdateCurrency(string prmOID, string prmName, double prmTRM){
            throw new NotImplementedException();
        }

        public static bool toUpdatePiggyBank(string prmOID, string prmName){
            throw new NotImplementedException();
        }

        public static bool toUpdateCoin(string prmOID, double prmValue, string prmCurrencyOID, int prmYear){
            throw new NotImplementedException();
        }

        public static bool toUpdateBill(string prmOID, double prmValue, string prmCurrencyOID, int prmYear, int prmMonth, int prmDay){
            throw new NotImplementedException();
        }

        public static bool toDeleteCurrency(string prmOID){
            throw new NotImplementedException();
        }

        public static bool toDeletePiggyBank(string prmOID){
            throw new NotImplementedException();
        }

        public static bool toDeleteCoin(string prmOID){
            throw new NotImplementedException();
        }

        public static bool toDeleteBill(string prmOID){
            throw new NotImplementedException();
        }
        #endregion

        #region Getters
        public static clsController getInstance(){ // Singleton: Unicamente se puede instanciar una vez
            if (attInstance == null)
                attInstance = new clsController();
            return attInstance;
        }

        public static clsCurrency getCurrencyWith(string prmOID){
   
            return clsCollections.getItemWith<clsCurrency, string>(prmOID, attCurrencies);
        }
        

        #region Transaction

        public bool coinsIncome(List<clsCoin> prmItems){
            throw new NotImplementedException();
        }

        public List<clsCoin> coinsWhithdrawal(List<clsCoin> prmItems){
            throw new NotImplementedException();
        }

        public bool billsIncome(List<clsBill> prmItems){
            throw new NotImplementedException();
        }

        public List<clsBill> billsWhithdrawal(List<double> prmValues){
            throw new NotImplementedException();
        }   
        #endregion

    }
}