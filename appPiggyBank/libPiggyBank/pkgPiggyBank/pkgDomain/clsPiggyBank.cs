using pkgServices;
using pkgServices.pkgCollections;

namespace pkgPiggyBank.pkgDomain
{
   public class clsPiggyBank : clsEntity, IComparable<clsPiggyBank> 
   {
        #region Attributes
        #region Owns
        private int attCoinsMaxCapacity;
        private List<double> attCoinsAcceptedValues;
        private List<double> attBillsAcceptedValues;
        private int attBillsMaxCapacity;
        private clsCurrency attCurrency;
        #endregion
        #region Derivables
        private double attTotalBalance;
        private double attCoinsBalance;
        private double attBillsBalance;
        private int attCoinsCount;
        private int attBillsCount;
        private List<double> attCoinsBalanceByValue;
        private List<double> attBillsBalanceByValue;
        private List<int> attCoinsCountByValue;
        private List<int> attBillsCountByValue;
        #endregion
        #region Associatives
        private List<clsCoin> attCoins;
        private List<clsBill> attBills;
        #endregion
        #endregion
        #region Operations
        #region Constructors
        public clsPiggyBank(int prmCoinsMaxCap, int prmBillsMaxCap, List<double> prmCoinsValues, List<double> prmBillsValues, clsCurrency prmCurrency)
            :base(default)
        {
            attCoinsMaxCapacity = prmCoinsMaxCap;
            attBillsMaxCapacity = prmBillsMaxCap;   
            attCoinsAcceptedValues= prmCoinsValues;
            attBillsAcceptedValues= prmBillsValues;
            attCurrency = prmCurrency;
        }

        
        public clsPiggyBank() { }
        #endregion
        #region Getters
        public int getCoinsMaxCapacity() => attCoinsMaxCapacity;
        public List<double> getCoinsAcceptedValues() => attCoinsAcceptedValues;
        public double getCoinsBalance() => attCoinsBalance;
        public int getCoinsCount()=> attCoinsCount;
        public List<double> getCoinsBalanceByValue() => attCoinsBalanceByValue;
        public List<int> getCoinsCountByValue() => attCoinsCountByValue;
        public int getBillsMaxCapacity() => attBillsMaxCapacity;
        public List<double> getBillsAcceptedValues() => attBillsAcceptedValues;
        public double getBillsBalance() => attBillsBalance;
        public List<double> getBillsBalanceByValue()=> attBillsBalanceByValue;
        public int getBillsCount()=> attBillsCount;
        public List<int> getBillsCountByValue() => attBillsCountByValue;    
        public double getTotalBalance()=> attTotalBalance;
        public int getMoneyItemsCount() => attCoinsCount + attBillsCount;
        public clsCurrency getCurrency() => attCurrency;
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
            if(prmValues==null) return false; 
                for (int varIdx=0;  varIdx<attCoinsAcceptedValues.Count ; varIdx++)
                {
                    int varFoundedIndex= prmValues.IndexOf(attCoinsAcceptedValues[varIdx]);
                    if(varFoundedIndex == -1 && attCoinsCountByValue[varIdx]>0) return false;  //Domain Rule
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
                if (varFoundedIndex == -1 && attBillsCountByValue[varIdx] > 0) return false;  //Domain Rule
            }
            attBillsAcceptedValues = prmValues;
            attBillsBalanceByValue = new List<double>(prmValues.Count);
            attBillsCountByValue = new List<int>(prmValues.Count);
            return true;
        }

        private bool setCurrency(clsCurrency prmObject)
        {
            if (prmObject==null) return false;
            if(attCoins.Count>0 && attBills.Count>0) return false; 
            if (attCurrency.CompareTo(prmObject) == 0) return false; 
            attCurrency =prmObject;
            setCoinsAcceptedValues(attCurrency.getCoinsValues());
            setBillsAcceptedValues(attCurrency.getBillsValues());

            return true;
        }
        public bool modify(clsCurrency prmCurrency,int prmCoinsMaxCap, int prmBillsMaxCap, List<double> prmCoinsValues, List<double> prmBillsValues)
        {
            if (setCoinsMaxCapacity(prmCoinsMaxCap))
                if (setBillsMaxCapacity(prmBillsMaxCap))
                    if (setCoinsAcceptedValues(prmCoinsValues))
                        if (setBillsAcceptedValues(prmBillsValues))
                            if (setCurrency(prmCurrency))
                                return true;
            return false;
        }
        #endregion
        #region Transactions
        private void updateBalance(clsCoin prmItem,bool prmUp) 
        {
            int varFactor=(prmUp) ? 1 : -1;
            attTotalBalance += varFactor*prmItem.getValue();
            attCoinsCount+=varFactor;
            attCoinsBalance += varFactor * prmItem.getValue();
            int varIdxValue=clsCollections.getIndexOf(prmItem.getValue(),attCoinsAcceptedValues);
            attCoinsBalanceByValue[varIdxValue] += varFactor * prmItem.getValue();
            attCoinsCountByValue[varIdxValue] += varFactor; 
        }
        private void updateBalance(clsBill prmItem,bool prmUp)
        {
            int varFactor = (prmUp) ? 1 : -1;
            attTotalBalance += varFactor * prmItem.getValue();
            attBillsCount += varFactor;
            attBillsBalance += varFactor * prmItem.getValue();
            int varIdxValue = clsCollections.getIndexOf(prmItem.getValue(), attBillsAcceptedValues);
            attBillsBalanceByValue[varIdxValue] += varFactor * prmItem.getValue();
            attBillsCountByValue[varIdxValue] += varFactor;

        }
        private bool areAccepted(List<clsCoin> prmItems)
        {
            foreach (clsCoin varObj in prmItems)
            {
                if (clsCollections.getIndexOf(varObj.getValue(),attCoinsAcceptedValues)==-1) return false;    
            }
            return true;
        }
        private bool areAccepted(List<clsBill> prmItems)
        {
            foreach (clsBill varObj in prmItems)
            {
                if (clsCollections.getIndexOf(varObj.getValue(), attBillsAcceptedValues) == -1) return false;
            }
            return true;
        }

        public bool coinsIncome(List<clsCoin> prmItems)
        {
            if (prmItems == null || prmItems.Count == 0) return false;
            if (prmItems.Count + attCoins.Count > attCoinsMaxCapacity) return false;                                
            if (!areAccepted(prmItems)) return false;
            foreach (clsCoin varObj in prmItems)
            { 
                attCoins.Add(varObj);
                varObj.setPiggy(this);
                updateBalance(varObj, true);
            }
            return true;
        }
        public bool coinsWithdrawal(List<clsCoin> prmItems)
        {
            if (prmItems == null) return false; 
            foreach (clsCoin varObj in prmItems)
            { 
                attCoins.Remove(varObj);
                varObj.setPiggy(null);
                updateBalance(varObj, false);
            }
            return true;
        }
        public bool billsIncome(List<clsBill> prmItems)
        {
            if (prmItems == null || prmItems.Count == 0) return false;
            if (prmItems.Count + attBills.Count > attBillsMaxCapacity) return false;
            if (!areAccepted(prmItems)) return false;
            foreach (clsBill varObj in prmItems)
            {
                attBills.Add(varObj);
                varObj.setPiggy(this);
                updateBalance(varObj, true);
            }
            return true;
        }
        public bool billsWithdrawal(List<clsBill> prmItems)
        {
            if (prmItems == null) return false;
            foreach (clsBill varObj in prmItems)
            {
                attBills.Remove(varObj);
                varObj.setPiggy(null);
                updateBalance(varObj, false);
            }
            return true;
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
            if (varObjOther != null) return false;
            attOID = varObjOther.attOID;
            attCoinsMaxCapacity = varObjOther.attCoinsMaxCapacity;
            attBillsMaxCapacity = varObjOther.attBillsMaxCapacity;
            attCoinsAcceptedValues = varObjOther.attCoinsAcceptedValues;
            attBillsAcceptedValues = varObjOther.attBillsAcceptedValues;
			attCurrency = varObjOther.attCurrency;
            return true;
        }
        public override string toString()
        {
            return "{Piggy Info}\n"
            +"{Coins Max Capacity}:\t" + attCoinsMaxCapacity + "\n"
            +"{Coins Accepted Values}:\t" + attCoinsAcceptedValues + "\n"
            +"{Coins Balance By Value}:\t" + attCoinsBalanceByValue + "\n"
            + "{Coins Count By Value}:\t" + attCoinsCountByValue + "\n"
            + "{Coins Balance}:\t" + attCoinsBalance + "\n"
            + "{Coins Count}:\t" + attCoinsCount + "\n"

            +"{Bills Max Capacity}:\t" + attBillsMaxCapacity + "\n"
            +"{Bills Accepted Values}:\t" + attBillsAcceptedValues + "\n"
            +"{Bills Balance By Value}:\t" + attBillsBalanceByValue + "\n"
            +"{Bills Count By Value}:\t" + attBillsCountByValue + "\n"
            +"{Bills Balance}:\t" + attBillsBalance + "\n"
            + "{Bills Count}:\t" + attBillsCount + "\n"

            + "{Total Balance}:\t" + attTotalBalance + "\n"
            +"{OID-Currency}" + attCurrency.getOID();
        }

        #endregion
        #region Destroyer
        public override bool die()
        {
            if (attCoins == null) return false;
            if (attBills == null) return false;
            foreach (clsCoin varObj in attCoins)
            {
                varObj.setPiggy(null);
                this.attCoins.Remove(varObj);
            }
            foreach (clsBill varObj in attBills)
            {
                varObj.setPiggy(null);
                this.attBills.Remove(varObj);
            }
            attCurrency.setPiggyBank(null);
            this.setCurrency(null);
            return true;
        }
        #endregion
        #endregion
    }
}