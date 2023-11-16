using pkgServices.pkgInterfaces;
using pkgServices.pkgCollections;

namespace pkgServices
{
    public static class clsBrokerCrud
    {
        public static bool toRegisterEntity<entityType>(entityType prmEntity, List<entityType> prmCollection)
        where entityType : iEntity
        {
            
            if (clsCollections.getItemWith(prmEntity.getOID(), prmCollection) != null) return false;
            prmCollection.Add(prmEntity);
            return true;
        }
    }
}