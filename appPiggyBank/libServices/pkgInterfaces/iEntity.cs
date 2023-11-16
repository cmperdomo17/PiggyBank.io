using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pkgServices.pkgInterfaces
{
    public interface iEntity
    {
        #region Setters
        bool modify(List<object> prmArgs);
        #endregion
        #region Getters
        string getOID();
        #endregion
        #region Destroyer
        bool die();
        #endregion
        #region Utilities
        string toString();
        bool copyTo<T>(T prmObject);
        #endregion
    }
}
