using System;
using System.Collections.Generic;
using System.ComponentModel;
using libServices.pkgServices;

namespace pkgPiggyBank.pkgDomain
{
    public class clsPiggyBank : clsEntity, IComparable<clsPiggyBank>
    {
        #region Owns
        private int attCoinsMaxCapacity;
        private List<double> attCoinsAcceptedValues;
        private int attBillsMaxCapacity;
        private List<double> attBillsAcceptedValues;
        public clsCurrency attCurrency;
        #endregion
        #region Derivables
        private double attTotalBalance;
        private double attCoinsBalance;
        private int attCoinsCount;
        private List<double> attCoinsBalanceByValue;
        private List<int> attCoinsCountByValue;
        private double attBillsBalance;
        private int attBillsCount;
        private List<double> attBillsBalanceByValue;
        private List<int> attBillsCountByValue;
        #endregion
        #region Asociativos
        private List<clsCoin> attCoins;
        private List<clsBill> attBills;
        #endregion
        #region Constructor 

        private clsPiggyBank() { }       
        public clsPiggyBank(int prmCoinsMaxCap, int prmBilsMaxCap, List<double> prmCoinsValues, List<double> prmBillValues, clsCurrency prmCurrency) : base("0")
        {
            attCoinsMaxCapacity = prmCoinsMaxCap;
            attBillsMaxCapacity = prmBilsMaxCap;
            attCoinsAcceptedValues = prmCoinsValues;
            attCoinsBalanceByValue = prmBillValues;
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

        public double getBillBalance() => attBillsBalance;

        public int getBillsCount() => attBillsCount;

        public List<double> getBillBalanceByValue() => attBillsBalanceByValue;

        public List<int> getBillsCountByValue() => attCoinsCountByValue;

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
            if (prmValues == null) return false;
            for (int i = 0; i < prmValues.Count; i++)
            {
                int varFoundIndex = prmValues.IndexOf(attCoinsAcceptedValues[i]);
                if (varFoundIndex == -1 && attCoinsCountByValue[i] > 0) return false;
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
            for (int varIdx = 0; varIdx < prmValues.Count; varIdx++)
            {
                int varFoundIndex = prmValues.IndexOf(attBillsAcceptedValues[varIdx]);
                if (varFoundIndex == -1 && attBillsCountByValue[varIdx] > 0) return false;
            }

            attBillsAcceptedValues = prmValues;
            attBillsBalanceByValue = new List<double>(prmValues.Count);
            attBillsCountByValue = new List<int>(prmValues.Count);
            return true;
        }

        private bool setCurrency(clsCurrency prmObject)
        {
            if (prmObject == null) return false;
            if (attCoins.Count > 0 && attBills.Count > 0) return false;
            if (attCurrency.CompareTo(prmObject) == 0) return false;
            attCurrency = prmObject;
            setCoinsAcceptedValues(attCurrency.getCoinsValues());
            setBillsAcceptedValues(attCurrency.getBillsValues());
            return true;
        }
        
        public override bool modify(List<object> prmArgs)
        {
            if ((string)prmArgs[0] != attOID) return false;
            clsPiggyBank varObjMemento = new clsPiggyBank();
            this.copyTo(varObjMemento);

            try {
                if (setCoinsMaxCapacity((int)prmArgs[1]))
                    if (setBillsMaxCapacity((int)prmArgs[2]))
                        if (setCoinsAcceptedValues((List<double>)prmArgs[3]))
                            if (setBillsAcceptedValues((List<double>)prmArgs[4]))
                                if (setCurrency((clsCurrency)prmArgs[5]))
                                    return true;
                
                varObjMemento.copyTo(this);
                return false;

            } catch (Exception e) {
                varObjMemento.copyTo(this);
                return false;
            }
        }

        #endregion
        #region Transacction
        public List<clsCoin> coinsIncome(List<clsCoin> prmItems)
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

        public List<clsBill> billsWithdeawal(List<double> prmValues)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Utilities

        public int CompareTo(clsPiggyBank prmOther)
        {
            throw new NotImplementedException();
        }

        public override bool copyTo<T>(T prmOtherObject)
        {
            clsPiggyBank varObjOther = prmOtherObject as clsPiggyBank;
            if (varObjOther == null) return false;
            attOID = varObjOther.attOID;
            attCoinsMaxCapacity = varObjOther.attCoinsMaxCapacity;
            attBillsMaxCapacity = varObjOther.attBillsMaxCapacity;
            attCoinsAcceptedValues = varObjOther.attCoinsAcceptedValues;
            attCoinsBalanceByValue = varObjOther.attCoinsBalanceByValue;
            attCurrency = varObjOther.attCurrency;

            return true;
        }
        public override string toString()
        {
            string varResult = "{Piggy Infor}\n";
            varResult += "{Coins Max Capacity}\t" + attCoinsMaxCapacity + "\n";
            varResult += "{Coins Accepeted Values}\t" + attCoinsAcceptedValues + "\n";
            varResult += "{Coins Balanace By Value}\t" + attCoinsBalanceByValue + "\n";
            varResult += "{Coins Count By Value}\t" + attCoinsCountByValue + "\n";
            varResult += "{Coins Balanace}\t" + attCoinsBalance + "\n";
            varResult += "{Coins Count}\t" + attCoinsCount + "\n";

            varResult += "{Bills Max Capacity}\t" + attCoinsMaxCapacity + "\n";
            varResult += "{Bills Accepeted Values}\t" + attBillsAcceptedValues + "\n";
            varResult += "{Bills Balanace By Value}\t" + attBillsBalanceByValue + "\n";
            varResult += "{Bills Count By Value}\t" + attBillsCountByValue + "\n";
            varResult += "{Bills Balanace}\t" + attBillsBalance + "\n";
            varResult += "{Bill Count}\t" + attBillsCount + "\n";

            varResult += "{Total Balance}\n" + attTotalBalance + "\n";
            varResult += attCurrency.ToString();

            return varResult;
        }

        
        #endregion
    }
}