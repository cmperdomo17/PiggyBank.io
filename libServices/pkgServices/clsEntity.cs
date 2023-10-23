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
        #region Constructors
        public clsEntity(string prmOID) => attOID = prmOID;
        #endregion  
        #region Getters
        public string getOID() => attOID;
        #endregion    
        #region Utilities
        public virtual string toString()=> throw new NotImplementedException();

        public virtual bool copyTo<T>(T prmOtherObject) => throw new NotImplementedException();
        #endregion
        #region Destroyer
        public virtual bool die()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
