using System;
using System.Collections.Generic;

namespace pkgPiggyBank.pkgDomain{
    public class clsController{

        #region Attributes
        private static clsController attInstance;
        private List<clsCurrency> attCurrencies;
        private clsPiggyBank attPiggy;
        #endregion

        #region Constructors
        public static clsController getInstance(){
            throw new NotImplementedException();
        }
        #endregion

        #region Cruds
        public bool toRegisterCurrency(string prmOID, string prmName, double prmTRM){
            throw new NotImplementedException();
        }

        public bool toRegisterPiggyBank(string prmOID, string prmName){
            throw new NotImplementedException();
        }

        public bool toRegisterCoin(string prmOID, double prmValue, string prmCurrencyOID, int prmYear){
            throw new NotImplementedException();
        }

        public bool toRegisterBill(string prmOID, double prmValue, string prmCurrencyOID, int prmYear, int prmMonth, int prmDay){
            throw new NotImplementedException();
        }

        public bool toUpdateCurrency(string prmOID, string prmName, double prmTRM){
            throw new NotImplementedException();
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