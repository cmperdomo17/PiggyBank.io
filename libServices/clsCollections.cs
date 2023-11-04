using pkgServices.pkgInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pkgServices;

namespace pkgServices
{
    public static class clsCollections<itemType> where itemType : class, iEntity, IComparable<itemType>
    {
        public static bool areEqualsCollections<itemType>(List<itemType> prmACollection, List<itemType> prmOtherCollection) where itemType : IComparable<itemType>
        {
            if (prmACollection.Count != prmOtherCollection.Count) return false;
            for (int varIdx = 0; varIdx < prmACollection.Count; varIdx++)
            {
                if (prmACollection[varIdx].CompareTo(prmOtherCollection[varIdx]) != 0){
                    return false;
                }
            }
            return true;
        }
        /* 
            Para cada objeto cuyo item es itemType, busque en la lista prmList el objeto cuyo OID sea igual a prmOID
            Si lo encuentra, lo retorna, de lo contrario retorna null
        */
        public static itemType getItemWith <oidType, itemType> (oidType prmOID, List<itemType> prmList) where itemType : class, iEntity, IComparable<itemType> where oidType : IComparable<oidType>
            foreach (itemType varObj in prmList)
                if (varObj.getOID() == prmOID) return varObj; 
            return default;
        }
    }
}
