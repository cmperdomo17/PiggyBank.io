using System;
using System.Security.Cryptography.X509Certificates;
using pkgServices;

namespace pkgPiggyBank.pkgDomain
{
    /// <summary>
    /// Representa una clase base para definir una moneda en la alcancia.
    /// </summary>
    public class clsCoin : clsEntity, IComparable<clsCoin>
    {
        #region Attributes

        /// <summary>
        /// Almacena el valor de la moneda.
        /// </summary>
        protected double attValue;

        /// <summary>
        /// Almacena el año en que se creo la moneda.
        /// </summary>
        protected int attYear;

        /// <summary>
        /// Almacena la moneda asociada a la instancia de la clase clsCoin.
        /// </summary>
        protected clsCurrency attCurrency;

        /// <summary>
        /// Almacena la alcancia asociada a la instancia de la clase clsCoin.
        /// </summary>
        protected clsPiggyBank attPiggy;

        #endregion
        #region Operations
        #region Constructors

        /// <summary>
        /// Constructor predeterminado de la clase clsCoin.
        /// </summary>
        public clsCoin()
        {
        }

        /// <summary>
        /// Constructor para crear una instancia de la clase clsCoin con valores iniciales.
        /// </summary>
        /// <param name="prmOID">Identificador unico de la moneda.</param>
        /// <param name="prmValue">Valor de la moneda.</param>
        /// <param name="prmCurrency">Moneda de la instancia clsCoin.</param>
        /// <param name="prmYear">Año en que se creo la moneda.</param>
        public clsCoin(string prmOID, double prmValue, clsCurrency prmCurrency, int prmYear) : base(prmOID)
        {
            attValue = prmValue;
            attCurrency = prmCurrency;
            attYear = prmYear;
        }

        #endregion
        #region Getters

        /// <summary>
        /// Obtiene el valor de la moneda.
        /// </summary>
        /// <returns>El valor de la moneda.</returns>
        public double getValue() => attValue;

        /// <summary>
        /// Obtiene el año en que se creo la moneda.
        /// </summary>
        /// <returns>El año en que se creo la moneda.</returns>
        public int getYear() => attYear;

        /// <summary>
        /// Obtiene la moneda asociada a la instancia de la clase clsCoin.
        /// </summary>
        /// <returns>La moneda asociada a la instancia de la clase clsCoin.</returns>
        public clsCurrency GetCurrency() => attCurrency;

        /// <summary>
        /// Obtiene la alcancia asociada a la instancia de la clase clsCoin.
        /// </summary>
        /// <returns>La alcancia asociada a la instancia de la clase clsCoin.</returns>
        public clsPiggyBank getPiggy() => attPiggy;

        #endregion
        #region Setters

        /// <summary>
        /// Modifica los atributos de la instancia de la clase clsCoin.
        /// </summary>
        /// <param name="prmArgs">Lista de argumentos para la modificacion.</param>
        /// <returns>Devuelve true si la modificacion fue exitosa; de lo contrario, false.</returns>
        public bool modify(List<object> prmArgs)
        {
            if (attPiggy != null) return false;
            if (attOID.CompareTo((string)prmArgs[0]) != 0) return false;
            attValue = (double)prmArgs[1];
            attCurrency = (clsCurrency)prmArgs[2];
            attYear = (int)prmArgs[3];
            return true;
        }

        /// <summary>
        /// Modifica el valor y el año de la moneda.
        /// </summary>
        /// <param name="prmValue">Nuevo valor de la moneda.</param>
        /// <param name="prmYear">Nuevo año de la moneda.</param>
        /// <returns>Devuelve true si la modificacion fue exitosa; de lo contrario, false.</returns>
        public bool modify(double prmValue, int prmYear)
        {
            if (attPiggy != null) return false;
            attValue = prmValue;
            attYear = prmYear;
            return true;
        }

        /// <summary>
        /// Asigna una alcancia a la instancia de la clase clsCoin.
        /// </summary>
        /// <param name="prmObject">Objeto de tipo clsPiggyBank a asignar.</param>
        /// <returns>Devuelve true si la asignacion fue exitosa; de lo contrario, false.</returns>
        public bool setPiggy(clsPiggyBank prmObject)
        {
            if (attPiggy != null) return false;
            if (attPiggy.CompareTo(prmObject) != 0) return false;
            attPiggy = prmObject;
            return true;
        }

        #endregion
        #region Utilities

        /// <summary>
        /// Devuelve una representacion en cadena de la instancia de clsCoin.
        /// </summary>
        /// <returns>Una cadena que representa la informacion de la moneda.</returns>
        public override string ToString()
        {
            return "{Coin Info}\n" + "{OID}:\t" + attOID + "\n{Value}:\t" + attValue + "\n{Currency}:\t" + attCurrency + "\n{Year}:\t" + attYear + "\n" + attCurrency.ToString();
        }

        /// <summary>
        /// Compara la instancia actual de clsCoin con otra instancia de clsCoin.
        /// </summary>
        /// <param name="prmOther">Otra instancia de clsCoin a comparar.</param>
        /// <returns>
        /// Devuelve 0 si las instancias son iguales, 1 si la instancia actual es mayor,
        /// y -1 si la instancia actual es menor.
        /// </returns>
        public int CompareTo(clsCoin prmOther)
        {
            if (attOID == prmOther.attOID
                && attValue == prmOther.attValue
                && attYear == prmOther.attYear
                && attCurrency == prmOther.attCurrency
                && attPiggy == prmOther.attPiggy) return 0;
            if (attValue == prmOther.attValue)
                return 1;
            return -1;
        }

        /// <summary>
        /// Copia los valores de la instancia actual de clsCoin a otra instancia de clsCoin.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto a copiar.</typeparam>
        /// <param name="prmOtherObject">Otra instancia de clsCoin.</param>
        /// <returns>Devuelve true si la copia fue exitosa; de lo contrario, false.</returns>
        public override bool copyTo<T>(T prmOtherObject)
        {
            clsCoin? varObjOther = prmOtherObject as clsCoin;
            if (varObjOther == null) return false;
            varObjOther.attOID = attOID;
            varObjOther.attValue = attValue;
            varObjOther.attCurrency = attCurrency;
            varObjOther.attYear = attYear;
            varObjOther.attPiggy = attPiggy;
            return true;
        }

        #endregion
        #region Destroyer

        /// <summary>
        /// Realiza la operacion de eliminacion de la instancia de la clase clsCoin.
        /// </summary>
        /// <returns>Devuelve true si la eliminacion fue exitosa; de lo contrario, false.</returns>
        public override bool die()
        {
            throw new NotImplementedException();
        }

        #endregion
        #endregion
    }
}
