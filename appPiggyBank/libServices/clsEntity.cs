using pkgServices.pkgInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pkgServices
{
    /// <summary>
    /// Clase base que implementa la interfaz iEntity y proporciona funcionalidades comunes para entidades.
    /// </summary>
    public class clsEntity : iEntity 
    {
        #region Attributes
        /// <summary>
        /// Almacena el identificador unico de la entidad.
        /// </summary>
        protected string attOID;
        #endregion
        #region Constructors
        /// <summary>
        /// Constructor predeterminado de la clase clsEntity.
        /// </summary>
        public clsEntity() => attOID = "";

        /// <summary>
        /// Constructor de la clase clsEntity con un identificador unico inicial.
        /// </summary>
        /// <param name="prmOID">Identificador unico de la entidad.</param>
        public clsEntity(string prmOID) => attOID = prmOID;
        #endregion
        #region Setters
        /// <summary>
        /// Modifica los atributos de la instancia de la clase clsEntity.
        /// </summary>
        /// <param name="prmArgs">Lista de argumentos para la modificacion.</param>
        /// <returns>Devuelve true si la modificacion fue exitosa; de lo contrario, false.</returns>
        public virtual bool modify(List<object> prmArgs) => throw new NotImplementedException();
        #endregion
        #region Getters
        /// <summary>
        /// Obtiene el identificador unico de la entidad.
        /// </summary>
        /// <returns>El identificador unico de la entidad.</returns>
        public string getOID() => attOID;
        #endregion
        #region Destroyer
        /// <summary>
        /// Realiza la operacion de eliminacion de la instancia de la clase clsEntity.
        /// </summary>
        /// <returns>Devuelve true si la eliminacion fue exitosa; de lo contrario, false.</returns>
        public virtual bool die()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Utilities
        /// <summary>
        /// Devuelve una representacion en cadena de la instancia de clsEntity.
        /// </summary>
        /// <returns>Una cadena que representa la informacion de la entidad.</returns>
        public virtual string toString() => throw new NotImplementedException();

        /// <summary>
        /// Copia los valores de la instancia actual de clsEntity a otra instancia de clsEntity.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto a copiar.</typeparam>
        /// <param name="prmObject">Otra instancia de clsEntity.</param>
        /// <returns>Devuelve true si la copia fue exitosa; de lo contrario, false.</returns>
        public virtual bool copyTo<T>(T prmObject) => throw new NotImplementedException();
        #endregion
    }
}
