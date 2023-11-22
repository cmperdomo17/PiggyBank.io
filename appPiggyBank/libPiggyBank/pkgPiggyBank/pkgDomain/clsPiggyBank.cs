using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using pkgServices;
using pkgServices.pkgCollections;

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

        public bool coinsWithdrawal(List<clsCoin> prmItems)
        {
            if (prmItems == null) return false;
            if (!isValidAmount(prmItems)) return false; // TODO: Implementar
            foreach (clsCoin varObj in prmItems){
                attCoins.Remove(varObj);
                varObj.setPiggy(null); // TODO: Implementar
                attCoinsCount--;
                attTotalBalance -= varObj.getValue();
                attCoinsBalance -= varObj.getValue();
                UpdateDownCoinsBalanceByValue(varObj); // TODO: Implementar
                UpdateDownCoinsCountByValue(varObj); // TODO: Implementar
            }
            return true;
        }

        public bool coinsIncome(List<clsCoin> prmItems)
        {
            if (prmItems==null || prmItems.Count == 0) return false;
            if (prmItems.Count + attCoinsCount > attCoinsMaxCapacity) return false;
            if (!AreAccepted(prmItems)) return false; // ? REVISAR
            
            foreach (clsCoin varObj in prmItems){
                attCoins.Add(varObj);
                varObj.setPiggy(this); // ? REVISAR
                attTotalBalance += varObj.getValue();
                attCoinsBalance += varObj.getValue();
                attCoinsCount++;
                UpdateUpCoinsBalanceByValue(varObj); // TODO: Implementar
                UpdateUpCoinsCountByValue(varObj); // TODO: Implementar
            }
            return true;
        }


// ! --------------------------------METODOS POR REVISAR ---------------------------------------------------

        // ? REVISAR
        private void UpdateDownCoinsBalanceByValue(clsCoin prmItem)
        {
            // validar que el valor de la moneda este en la lista de valores aceptados
            if (attCoinsAcceptedValues.Contains(prmItem.getValue()))
            {
                // Obtener el indice del valor de la moneda en la lista de valores aceptados
                int varIdx = attCoinsAcceptedValues.IndexOf(prmItem.getValue());
                // Disminuir el balance de la moneda en la lista de balances por valor
                attCoinsBalanceByValue[varIdx] -= prmItem.getValue();
            }
            else {
                // Si el valor de la moneda no esta en la lista de valores aceptados se agrega a la lista de valores aceptados
                attCoinsAcceptedValues.Add(prmItem.getValue());
                // Se agrega el valor de la moneda a la lista de balances por valor
                attCoinsBalanceByValue.Add(prmItem.getValue());
                // Añade 1 al conteo de monedas por valor
                attCoinsCountByValue.Add(1);
            }
        }
        
        // ? REVISAR
        private void UpdateDownCoinsCountByValue(clsCoin prmItem)
        {
            // validar que el valor de la moneda este en la lista de valores aceptados
            if (attCoinsAcceptedValues.Contains(prmItem.getValue()))
            {
                // Obtener el indice del valor de la moneda en la lista de valores aceptados
                int varIdx = attCoinsAcceptedValues.IndexOf(prmItem.getValue());
                // Disminuir el conteo de la moneda en la lista de conteos por valor
                attCoinsCountByValue[varIdx]--;
            }
            else {
                // Si el valor de la moneda no esta en la lista de valores aceptados se agrega a la lista de valores aceptados
                attCoinsAcceptedValues.Add(prmItem.getValue());
                // Se agrega el valor de la moneda a la lista de balances por valor
                attCoinsBalanceByValue.Add(prmItem.getValue());
                // Añade 1 al conteo de monedas por valor
                attCoinsCountByValue.Add(1);
            }
        }

        // ? REVISAR
        private bool isValidAmount(List<clsCoin> prmItems)
        {
            double varAmount = 0;
            // Recorre la lista de monedas
            foreach (clsCoin varObj in prmItems)
            {
                // Suma el valor de la moneda al monto
                varAmount += varObj.getValue();
            }
            // Retorna verdadero si el monto es menor o igual al balance de monedas
            if (varAmount <= attCoinsBalance) return true;
            // En caso contrario retorna falso
            return false;
        }

        // ? REVISAR
        private bool AreAccepted(List<clsCoin> prmItems)
        {
            // Recorre la lista de monedas 
            foreach (clsCoin varObj in prmItems)
            {
                // Si el valor de la moneda no esta en la lista de valores aceptados retorna falso
                if (!attCoinsAcceptedValues.Contains(varObj.getValue())) return false;
            }
            // En caso contrario retorna verdadero y la moneda es aceptada
            return true;
        }

        // ? REVISAR
        private void UpdateUpCoinsBalanceByValue(clsCoin prmItem)
        {
            // validar que el valor de la moneda este en la lista de valores aceptados
            if (attCoinsAcceptedValues.Contains(prmItem.getValue()))
            {
                // Obtener el indice del valor de la moneda en la lista de valores aceptados
                int varIdx = attCoinsAcceptedValues.IndexOf(prmItem.getValue());
                // Aumentar el balance de la moneda en la lista de balances por valor
                attCoinsBalanceByValue[varIdx] += prmItem.getValue();
            }
            else {
                // Si el valor de la moneda no esta en la lista de valores aceptados se agrega a la lista de valores aceptados
                attCoinsAcceptedValues.Add(prmItem.getValue());
                // Se agrega el valor de la moneda a la lista de balances por valor
                attCoinsBalanceByValue.Add(prmItem.getValue());
                // Añade 1 al conteo de monedas por valor
                attCoinsCountByValue.Add(1);
            }
        }

        // ? REVISAR
        private void UpdateUpCoinsCountByValue(clsCoin prmItem)
        {
            // validar que el valor de la moneda este en la lista de valores aceptados
            if (attCoinsAcceptedValues.Contains(prmItem.getValue()))
            {
                // Obtener el indice del valor de la moneda en la lista de valores aceptados
                int varIdx = attCoinsAcceptedValues.IndexOf(prmItem.getValue());
                // Aumentar el conteo de la moneda en la lista de conteos por valor
                attCoinsCountByValue[varIdx]++;
            }
            else {
                // Si el valor de la moneda no esta en la lista de valores aceptados se agrega a la lista de valores aceptados
                attCoinsAcceptedValues.Add(prmItem.getValue());
                // Se agrega el valor de la moneda a la lista de balances por valor
                attCoinsBalanceByValue.Add(prmItem.getValue());
                // Añade 1 al conteo de monedas por valor
                attCoinsCountByValue.Add(1);
            }
        }

// ! --------------------------------METODOS POR REVISAR ---------------------------------------------------
        public List<clsCoin> getCoinsByValue(double prmValue)
        {
            List<clsCoin> varCoins = new List<clsCoin>();
            foreach (clsCoin varCoin in attCoins)
            {
                if (varCoin.getValue() == prmValue)
                {
                    varCoins.Add(varCoin);
                }
            }
            return varCoins;
        }

        public List<clsBill> billsWithdrawal(List<double> prmValues)
        {
            throw new NotImplementedException();
        }
        #endregion

        public void removeCoin(string prmOID)
        {
            clsCoin? varObj = clsCollections.getItemWith(prmOID, attCoins);
            if (varObj == null) return;
            attCoins.Remove(varObj);
            attCoinsCount--;
            attCoinsBalance -= varObj.getValue();
            int varIdx = attCoinsAcceptedValues.IndexOf(varObj.getValue());
            attCoinsCountByValue[varIdx]--;
            attCoinsBalanceByValue[varIdx] -= varObj.getValue();
            attTotalBalance -= varObj.getValue();
        }

        #region Utilities

        public int CompareTo(clsPiggyBank prmOther)
        {
            throw new NotImplementedException();
        }

        public override bool copyTo<T>(T prmOtherObject)
        {
            clsPiggyBank? varObjOther = prmOtherObject as clsPiggyBank;
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