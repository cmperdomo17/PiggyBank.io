using System;
using System.Collections.Generic;
using pkgServices.pkgInterfaces;
using pkgServices;

namespace pkgServices.pkgCruds
{
    public static class clsBrokerCrud{
        public static bool toRegisterEntity <entityType> (entityType prmEntity, List<entityType> prmCollection) 
        where entityType : iEntity
        {
            if (getItemWith(prmEntity.getOID(), prmCollection) != null) return false;
            prmCollection.Add(prmEntity);
            return true;
        }
    }
}