using System;
using System.Collections.Generic;
using pkgServices;
using pkgServices.pkgInterfaces;
//using pkgServices.pkgCollections;

namespace pkgPiggyBank.pkgDomain
{
    public class clsCurrency : clsEntity, IComparable<clsCurrency>
    {
        #region Attributes
        private string attName;
        private double attTRM;
        private List<clsCoin> attCoins;
        private List<clsBill> attBills;
        private clsPiggyBank attPiggy;
        private List<double> attCoinsValues;
        private List<double> attBillsValues;
        #endregion
        #region Operations
        #region Constructors
        public clsCurrency(string prmOID, string prmName, double prmTRM, List<double> prmCoinsValues, List<double> prmBillsValues) : base(prmOID)
        {
            attName = prmName;
            attTRM = prmTRM;
            attCoinsValues = prmCoinsValues;
            attBillsValues = prmBillsValues;
        }
        #endregion
        #region Getters
        public string getName() => attName;
        public double getTRM() => attTRM;
        public List<double> getCoinsValues() => attCoinsValues;
        public List<double> getBillsValues() => attBillsValues;
        #endregion
        #region Setters
        public bool modifyThis(string prmName, double prmTRM)
        {
            attName = prmName;
            attTRM = prmTRM;
            return true;
        }

        #endregion
        #region Utilities
        public override string toString()
        {
            return "{Currency Info}\n" + "{OID}:\t" + attOID + "\n" + "{Name}:\t" + attName + "\n" + "{TRM}:\t" + attTRM + "\n";
        }
        public bool areEqualsCollections<T>(List<T> prmACollection, List<T> prmOtherCollection)
        where T : IComparable<T>
        {
            if(prmACollection.Count != prmOtherCollection.Count) return false;
            for (int varIdx=0; varIdx< prmACollection.Count;varIdx++)
            {
                if (prmACollection[varIdx].CompareTo(prmOtherCollection[varIdx]) != 0){
                    return false;
                }
            }
            return true;
        }
        public int CompareTo(clsCurrency prmOther)
        {
            if (attOID == prmOther.attOID && attName == prmOther.attName && attTRM == prmOther.attTRM && clsCollections<clsCoin>.areEqualsCollections(attCoins, prmOther.attCoins) && clsCollections<clsBill>.areEqualsCollections(attBills, prmOther.attBills) && attPiggy.CompareTo(prmOther.attPiggy) == 0 && clsCollections <double>.areEqualsCollections(attCoinsValues, prmOther.attCoinsValues) && clsCollections<double>.areEqualsCollections(attBillsValues, prmOther.attBillsValues))
                return 0;
            return 1;
        }
        #endregion
        #endregion
    }
}