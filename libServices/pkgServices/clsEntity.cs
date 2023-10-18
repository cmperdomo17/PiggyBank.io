using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libServices.pkgServices.pkgInterfaces;

namespace libServices.pkgServices
{
    public class clsEntity : iEntity
    {
        #region Atributtes
        protected string attOID;
        #endregion
        public clsEntity(string prmOID) => attOID = prmOID;
        #region Getters
        public string getOID() => attOID;
        #endregion
        #region Destroyer
        public virtual bool die()
        {
            throw new NotImplementedException();
        }
        #endregion
        
        #region Utilities
        public virtual string toString()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
