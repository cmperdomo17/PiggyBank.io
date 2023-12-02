using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pkgServices;
using pkgServices.pkgInterfaces;

namespace pkgServices.pkgCollections
{
    /// <summary>
    /// Clase estática que proporciona métodos para operaciones comunes con colecciones.
    /// </summary>
    public static class clsCollections
    {
        /// <summary>
        /// Compara dos colecciones para determinar si son iguales.
        /// </summary>
        /// <typeparam name="itemType">Tipo de elementos en las colecciones.</typeparam>
        /// <param name="prmACollection">Primera colección a comparar.</param>
        /// <param name="prmOtherCollection">Segunda colección a comparar.</param>
        /// <returns>Devuelve true si las colecciones son iguales; de lo contrario, false.</returns>
        public static bool areEqualsCollections<itemType>(List<itemType> prmACollection, List<itemType> prmOtherCollection)
        where itemType : IComparable<itemType>
        { 

            if (prmACollection.Count != prmOtherCollection.Count) return false;
            for (int varIdx = 0; varIdx<prmACollection.Count; varIdx++)
            {
                if (prmACollection[varIdx].CompareTo(prmOtherCollection[varIdx]) != 0){
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Obtiene un elemento de una colección basándose en su identificador único.
        /// </summary>
        /// <typeparam name="itemType">Tipo de elementos en la colección.</typeparam>
        /// <param name="prmOID">Identificador único del elemento a buscar.</param>
        /// <param name="prmCollection">Colección en la que buscar el elemento.</param>
        /// <returns>Devuelve el elemento encontrado o null si no se encuentra.</returns>
        public static itemType? getItemWith<itemType>(string prmOID, List<itemType> prmCollection)
        where itemType : iEntity
        {
            foreach (itemType varObj in prmCollection)
                if (varObj.getOID().CompareTo(prmOID) == 0) return varObj;
            return default;
        }

        /// <summary>
        /// Obtiene el índice de un elemento en una colección.
        /// </summary>
        /// <typeparam name="itemType">Tipo de elementos en la colección.</typeparam>
        /// <param name="prmItem">Elemento cuyo índice se va a buscar.</param>
        /// <param name="prmCollection">Colección en la que buscar el índice.</param>
        /// <returns>Devuelve el índice del elemento encontrado o -1 si no se encuentra.</returns>
        public static int getIndexOf<itemType>(itemType prmItem, List<itemType> prmCollection)
        where itemType : IComparable<itemType>
        {
            int varIdx;
            for (varIdx = 0; varIdx < prmCollection.Count; varIdx++)
                if (prmCollection[varIdx].CompareTo(prmItem) == 0)
                    return varIdx;
            return -1;
        }

        /// <summary>
        /// Comprueba si una colección es un subconjunto de otra colección.
        /// </summary>
        /// <typeparam name="itemType">Tipo de elementos en las colecciones.</typeparam>
        /// <param name="prmCollectionGuest">Colección que se verifica como subconjunto.</param>
        /// <param name="prmCollectionHost">Colección en la que se verifica la existencia del subconjunto.</param>
        /// <returns>Devuelve true si prmCollectionGuest es un subconjunto de prmCollectionHost; de lo contrario, false.</returns>
        public static bool isSubSet<itemType>(List<itemType> prmCollectionGuest, List<itemType> prmCollectionHost)
        where itemType : IComparable<itemType>
        {
            foreach (itemType varItem in prmCollectionGuest)

                if (getIndexOf(varItem, prmCollectionHost) == -1) return false;
            return true;
        }
    }
}