using pkgServices.pkgInterfaces;
using pkgServices.pkgCollections;

namespace pkgServices
{
    /// <summary>
    /// Clase estática que proporciona métodos para realizar operaciones CRUD (Create, Read, Update, Delete) en colecciones de entidades.
    /// </summary>
    public static class clsBrokerCrud
    {
        /// <summary>
        /// Registra una entidad en una colección si no existe.
        /// </summary>
        /// <typeparam name="entityType">Tipo de entidad a registrar.</typeparam>
        /// <param name="prmEntity">Entidad a registrar.</param>
        /// <param name="prmCollection">Colección en la que se va a registrar la entidad.</param>
        /// <returns>Devuelve true si la entidad se registró correctamente; de lo contrario, false.</returns>
        public static bool toRegisterEntity<entityType>(entityType prmEntity, List<entityType> prmCollection)
        where entityType : iEntity
        {
            
            if (clsCollections.getItemWith(prmEntity.getOID(), prmCollection) != null) return false;
            prmCollection.Add(prmEntity);
            return true;
        }
    }
}