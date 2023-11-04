using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pkgServices.pkgInterfaces
{
    public interface iEntity
    {
        #region Getters
        oidType getOID();
        
        #endregion
        #region Destroyer
        bool die();
        #endregion
        #region Utilities
        string toString(); 
        #endregion
    }
}
