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
        /// <param name="prmArgs">Lista de argumentos para la modificación.</param>
        /// <returns>Devuelve true si la modificación fue exitosa; de lo contrario, false.</returns>
        bool modify(List<object> prmArgs);
        #endregion
        #region Getters
        /// <summary>
        /// Obtiene el identificador único de la entidad.
        /// </summary>
        /// <returns>El identificador único de la entidad.</returns>
        string getOID();
        #endregion
        #region Destroyer
        /// <summary>
        /// Realiza la operación de eliminación de la entidad.
        /// </summary>
        /// <returns>Devuelve true si la eliminación fue exitosa; de lo contrario, false.</returns>
        bool die();
        #endregion
        #region Utilities
        /// <summary>
        /// Devuelve una representación en cadena de la entidad.
        /// </summary>
        /// <returns>Una cadena que representa la información de la entidad.</returns>
        string toString();

        /// <summary>
        /// Copia los valores de la entidad actual a otro objeto.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto a copiar.</typeparam>
        /// <param name="prmObject">Objeto al que se copiarán los valores.</param>
        /// <returns>Devuelve true si la copia fue exitosa; de lo contrario, false.</returns>
        bool copyTo<T>(T prmObject);
        #endregion
    }
}
