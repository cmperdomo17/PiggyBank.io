using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pkgServices.pkgInterfaces
{
    /// <summary>
    /// Interfaz que define operaciones comunes para entidades.
    /// </summary>
    public interface iEntity
    {
        #region Setters
        /// <summary>
        /// Modifica los atributos de la entidad.
        /// </summary>
        /// <param name="prmArgs">Lista de argumentos para la modificacion.</param>
        /// <returns>Devuelve true si la modificacion fue exitosa; de lo contrario, false.</returns>
        bool modify(List<object> prmArgs);
        #endregion
        #region Getters
        /// <summary>
        /// Obtiene el identificador unico de la entidad.
        /// </summary>
        /// <returns>El identificador unico de la entidad.</returns>
        string getOID();
        #endregion
        #region Destroyer
        /// <summary>
        /// Realiza la operacion de eliminacion de la entidad.
        /// </summary>
        /// <returns>Devuelve true si la eliminacion fue exitosa; de lo contrario, false.</returns>
        bool die();
        #endregion
        #region Utilities
        /// <summary>
        /// Devuelve una representacion en cadena de la entidad.
        /// </summary>
        /// <returns>Una cadena que representa la informacion de la entidad.</returns>
        string toString();

        /// <summary>
        /// Copia los valores de la entidad actual a otro objeto.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto a copiar.</typeparam>
        /// <param name="prmObject">Objeto al que se copiaran los valores.</param>
        /// <returns>Devuelve true si la copia fue exitosa; de lo contrario, false.</returns>
        bool copyTo<T>(T prmObject);
        #endregion
    }
}
