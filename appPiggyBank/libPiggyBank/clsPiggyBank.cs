using System;
using System.Collections.Generic;
using System.Linq;
using pkgServices;
using pkgServices.pkgInterfaces;

namespace pkgPiggyBank.pkgDomain
{
   public class clsPiggyBank //:clsEntity
   {
        #region Attributes
        #region Owns
        private int attCoinsMaxCapacity;
        private List<double> attCoinsAcceptedValues;
        private int attBillsMaxCapacity;
        private List<double> attBillsAcceptedValues;
        private clsCurrency attCurrency;
        #endregion
        #region Derivables
        private double attCoinsBalance;
        private int attCoinsCount;
        private List<double> attCoinsBalanceByValue;
        private List<int> attCoinsCountByValue;
        private double attBillsBalance;
        private int attBillsCount;
        private List<double> attBillsBalanceByValue;
        private List<int> attBillsCountByValue;
        private double attTotalBalance;
        #endregion
        #region Associatives
        private List<clsCoin> attCoins;
        private List<clsBill> attBills;
        #endregion
        #endregion
        #region Operations
        #region Constructors
        public clsPiggyBank(int prmCoinsMaxCap, int prmBillMaxCap, List<double> prmCoinsValues, List<double> prmBillsValues, clsCurrency prmCurrency)
        {
            attCoinsMaxCapacity = prmCoinsMaxCap;
            attBillsMaxCapacity = prmBillMaxCap;
            attCoinsAcceptedValues = prmCoinsValues;
            attBillsAcceptedValues = prmBillsValues;
            attCurrency = prmCurrency;

        }
        #endregion
        #region Getters
        public int getCoinsMaxCapacity() => attCoinsMaxCapacity;
        public List<double> getCoinsAcceptedValues() => attCoinsAcceptedValues;
        public double getCoinsBalance() => attCoinsBalance;
        public int getCoinsCount() => attCoinsCount;
        public List<double> getCoinsBalanceByValue() => attCoinsBalanceByValue;
        public List<int> getCoinsCountByValue() => attCoinsCountByValue;
        public int getBillsMaxCapacity() => attBillsMaxCapacity;
        public List<double> getBillsAcceptedValues() => attBillsAcceptedValues;
        public double getBillsBalance() => attBillsBalance;
        public int getBillsCount() => attBillsCount;
        public List<double> getBillsBalanceByValue() => attBillsBalanceByValue;
        public List<int> getBillsCountByValue() => attBillsCountByValue;
        public double getTotalBalance() => attTotalBalance;
        #endregion
        #region Setters
        private bool setCoinsMaxCapacity(int prmValue)
        {
            if (prmValue < 0) return false; 
            if (attCoinsCount > prmValue) return false;
            attCoinsMaxCapacity = prmValue;
            return true;
        }

        private bool setCoinsAcceptedValues(List<double> prmValues)
        {
            if(prmValues == null ) return false;
            for (int varIdx = 0; varIdx < attCoinsAcceptedValues.Count; varIdx++)
            {
                int varFoundedIndex = prmValues.IndexOf(attCoinsAcceptedValues[varIdx]);
                if (varFoundedIndex == -1 && attCoinsCountByValue[varIdx] > 0) return false; //Domain rule
            }
            attCoinsAcceptedValues = prmValues;
            attCoinsBalanceByValue = new List<double>(prmValues.Count);
            attCoinsCountByValue = new List<int>(prmValues.Count);
            return true;
        }

        private bool setBillsMaxCapacity(int prmValue)
        {
            if (prmValue < 0) return false;
            if (attBillsCount > prmValue) return false;
            attBillsMaxCapacity = prmValue;
            return true;
        }

        private bool setBillsAcceptedValues(List<double> prmValues)
        {
            if (prmValues == null) return false;
            for (int varIdx = 0; varIdx < attBillsAcceptedValues.Count; varIdx++)
            {
                int varFoundedIndex = prmValues.IndexOf(attBillsAcceptedValues[varIdx]);
                if (varFoundedIndex == -1 && attBillsCountByValue[varIdx] > 0) return false; //Domain rule
            }
            attBillsAcceptedValues = prmValues;
            attBillsBalanceByValue = new List<double>(prmValues.Count);
            attBillsCountByValue = new List<int>(prmValues.Count);
            return true;
        }

        private bool setCurrency(clsCurrency prmObject)
        {
            if (prmObject == null) return false; //No null reference rule
            if (attCoins.Count() > 0 && attBills.Count() > 0 ) return false; //Domain rule
            if (attCurrency.CompareTo(prmObject)==0) return false; //Domain rule
            attCurrency = prmObject;
            setCoinsAcceptedValues(attCurrency.getCoinsValues());
            setBillsAcceptedValues(attCurrency.getBillsValues());
            return true;
        }

        public bool modifyThis(int prmCoinsMaxCap, int prmBillsMaxCap, List<double> prmCoinsValues, List<double> prmBillsValues, clsCurrency prmCurrency)
        {
            throw new NotImplementedException();
        }

        #endregion
        #region Transaction

        public bool coinsIncome(List<clsCoin> prmItems)
        {
            throw new NotImplementedException();
        }

        public List<clsCoin> coinsWithdrawal(List<double> prmValues)
        {
            throw new NotImplementedException();
        }

        public bool billsIncome(List<clsBill> prmItems)
        {
            throw new NotImplementedException();
        }

        public List<clsBill> billsWithdrawal(List<double> prmValues)
        {
            throw new NotImplementedException();
        } 

        #endregion
        #region Utilities
        public override string ToString()
        {
            string varResult = "{Piggy Info}:\n";
            varResult += "{Coins Max Capacity}:\t" + attCoinsMaxCapacity + "\n";
            varResult += "{Coins Accepted Values}:\t" + attCoinsAcceptedValues + "\n";
            varResult += "{Coins Balance By Value}:\t" + attCoinsBalanceByValue + "\n";
            varResult += "{Coins Count By Value}:\t" + attCoinsCountByValue + "\n";
            varResult += "{Coins Balance}:\t" + attCoinsBalance + "\n";
            varResult += "{Coins count}:\t" + attCoinsCount + "\n";

            varResult += "{Bills Max Capacity}:\t" + attBillsMaxCapacity + "\n";
            varResult += "{Bills Accepted Values}:\t" + attBillsAcceptedValues + "\n";
            varResult += "{Bills Balance By Value}:\t" + attBillsBalanceByValue + "\n";
            varResult += "{Bills Count By Value}:\t" + attBillsCountByValue + "\n";
            varResult += "{Bills Balance}:\t" + attBillsBalance + "\n";
            varResult += "{Bills count}:\t" + attBillsCount + "\n";

            varResult += "{Total Balance}:\t" + attTotalBalance + "\n";
            varResult += attCurrency.ToString();

            return varResult;
        }
        public int CompareTo(clsPiggyBank other)
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion

    }
}