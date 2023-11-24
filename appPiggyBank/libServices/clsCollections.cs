using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pkgServices;
using pkgServices.pkgInterfaces;

namespace pkgServices.pkgCollections
{
    public static class clsCollections
    {
        public static bool areEqualsCollections<itemType>(List<itemType> prmACollection, List<itemType> prmOtherCollection)
        where itemType : IComparable<itemType>
        { 

            if (prmACollection.Count != prmOtherCollection.Count) return false;
            for (int varIdx = 0; varIdx<prmACollection.Count; varIdx++)
            {
                if (prmACollection[varIdx].CompareTo(prmOtherCollection[varIdx]) != 0){
                    return false;
                }
            }
            return true;
        }
        // Método de busqueda secuencial
        public static itemType? getItemWith<itemType>(string prmOID, List<itemType> prmCollection)
        where itemType : iEntity
        {
            foreach (itemType varObj in prmCollection)
                if (varObj.getOID().CompareTo(prmOID) == 0) return varObj;
            return default;
        }

        public static int getIndexOf<itemType>(itemType prmItem, List<itemType> prmCollection)
        where itemType : IComparable<itemType>
        {
            int varIdx;
            for (varIdx = 0; varIdx < prmCollection.Count; varIdx++)
                if (prmCollection[varIdx].CompareTo(prmItem) == 0)
                    return varIdx;
            return -1;
        } 

        public static bool isSubSet<itemType>(List<itemType> prmCollectionGuest, List<itemType> prmCollectionHost)
        where itemType : IComparable<itemType>
        {
            foreach (itemType varItem in prmCollectionGuest)

                if (getIndexOf(varItem, prmCollectionHost) == -1) return false;
            return true;
        }
    }
}