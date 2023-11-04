using pkgServices.pkgInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pkgServices
{
    public class clsEntity <oidType> :iEntity <oidType> 
    {
        #region Attributes
        protected oidType attOID;
        #endregion
        #region Constructors
        public clsEntity(oidType prmOID) => attOID = prmOID; 
        #endregion
        #region Getters
        public oidType getOID() => attOID;
        #endregion
        #region Destroyer
        public virtual bool die()
        {
            throw new NotImplementedException();
        } 
        #endregion
        #region Utilities
        public virtual string toString() => throw new NotImplementedException();
        #endregion
    }
}
