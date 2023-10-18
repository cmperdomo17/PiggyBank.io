using System;
using System.Collections.Generic;

namespace pkgPiggyBank.pkgDomain
{
   public class clsController
   {
      #region Attributes
      private static clsController attInstance;
      private List<clsCurrency> attCurrencies;
      private clsPiggyBank attPiggy;
        #endregion
      #region Operations
      #region Getters
        public static clsController getInstance()
        {
            throw new NotImplementedException();
        }
        #endregion
      #region Cruds
        public bool toRegisterCurrency(int prmOID, string prmName, double prmTRM)
        {
            throw new NotImplementedException();
        }

        public bool toRegisterPiggyBank(string prmOIDCurrency, int prmCoinsMaxCap, int prmBillMaxCap, List<double> prmCoinsValues, List<double> prmBillValues)
        {
            throw new NotImplementedException();
        }

        public bool toRegisterCoin(string prmOIDCurrency, string prmOID, double prmValue, int prmYear)
        {
            throw new NotImplementedException();
        }

        public bool toRegisterBill(string prmOIDCurrency, string prmOID, double prmValue, int prmDay, int prmMonth, int prmYear)
        {
            throw new NotImplementedException();
        }

        public bool toUpdateCurrency(int prmOID, string prmName, double prmTRM)
        {
            throw new NotImplementedException();
        }

        public bool toUpdatePiggyBank(string prmOIDCurrency, int prmCoinsMaxCap, int prmBillMaxCap, List<double> prmCoinsValues, List<double> prmBillValues)
        {
            throw new NotImplementedException();
        }

        public bool toUpdateCoin(string prmOIDCurrency, string prmOID, double prmValue, int prmYear)
        {
            throw new NotImplementedException();
        }

        public bool toUpdateBill(string prmOIDCurrency, string prmOID, double prmValue, int prmDay, int prmMonth, int prmYear)
        {
            throw new NotImplementedException();
        }

        public bool toDeleteCurrency(string prmOID)
        {
            throw new NotImplementedException();
        }

        public bool toDeletePiggyBank()
        {
            throw new NotImplementedException();
        }

        public bool toDeleteCoin(string prmOIDCurrency, string prmOID)
        {
            throw new NotImplementedException();
        }

        public bool toDeleteBill(string prmOIDCurrency, string prmOID)
        {
            throw new NotImplementedException();
        }
        #endregion
      #region Transaction
        public bool coinsIncome(List<string> prmCoinsOID)
        {
            throw new NotImplementedException();
        }
        public List<clsCoin> coinsWithdrawal(List<double> prmValues)
        {
            throw new NotImplementedException();
        }
        public bool billsIncome(List<string> prmBillsOID)
        {
            throw new NotImplementedException();
        }
        public List<clsBill> billsWithdrawal(List<double> prmValues)
        {
            throw new NotImplementedException();
        }
      #endregion 
      #endregion
    }
}