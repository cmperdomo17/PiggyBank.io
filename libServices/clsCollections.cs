using pkgServices.pkgInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pkgServices;

namespace pkgServices
{
    public static class clsCollections<T> where T : IComparable<T> 
    {
        public static bool areEqualsCollections(List<T> prmACollection, List<T> prmOtherCollection)
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
    }
}
