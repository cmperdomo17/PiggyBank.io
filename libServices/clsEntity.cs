using pkgServices.pkgInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pkgServices
{
    public class clsEntity :iEntity
    {
        #region Attributes
        protected string attOID;
        #endregion
        #region Constructors
        public clsEntity(string prmOID) => attOID = prmOID; 
        #endregion
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
        public virtual string toString() => throw new NotImplementedException();
        #endregion
    }
}
