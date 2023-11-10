using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libServices.pkgServices.pkgCollections
{
    public static class clsCollections<T> where T : IComparable<T>
    {
        public static bool areEqualCollections<itemType>(List<itemType> prmACollection, List<itemType> prmOtherCollection) 
        where itemType : IComparable<itemType>
        {
            if (prmThisCollection.Count != prmOtherCollection.Count) { return false; }
            for (int varIdx = 0; varIdx < prmThisCollection.Count;  varIdx++)
            {
                if (prmThisCollection[varIdx].CompareTo(prmOtherCollection[varIdx]) < 0) { return false; }
            }
            return true;
        }
    }
}
