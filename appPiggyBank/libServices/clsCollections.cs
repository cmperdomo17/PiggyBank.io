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
            where itemType : IComparable<itemType>

            if (prmACollection.Count != prmOtherCollection.Count) return false;
            for (int varIdx = 0; varIdx < prmACollection.Count; varIdx++)
            {
                if (prmACollection[varIdx].CompareTo(prmOtherCollection[varIdx]) != 0){
                    return false;
                }
            }
            return true;
        }
        // Método de busqueda secuencial
        public static itemType getItemWith <itemType> (string prmOID, List<itemType> prmCollection) 
        where itemType : iEntity
        {
            foreach (itemType varObj in prmCollection)
                if (varObj.getOID().CompareTo(prmOID) == 0) return varObj; 
            return default;
        }

        public static bool toRegisterEntity <entityType> (entityType prmEntity, List<entityType> prmCollection) 
        where entityType : iEntity
        {
            if (getItemWith(prmEntity.getOID(), prmCollection) != null) return false;
            prmCollection.Add(prmEntity);
            return true;
        }
    }
}
