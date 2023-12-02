using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace pkgPiggyBank.pkgDomain{
    /// <summary>
    /// Representa una clase que define una moneda con valor en billetes.
    /// </summary>
    public class clsBill : clsCoin, IComparable<clsBill> 
   {
        #region Attributes
        /// <summary>
        /// Representa una clase que define una moneda con valor en billetes.
        /// </summary>
        private int attMonth;

        /// <summary>
        /// Almacena el día en que se creó el billete.
        /// </summary>
        private int attDay;
        #endregion
        #region Operations
        #region Constructors
        /// <summary>
        /// Constructor privado para prevenir la creación de instancias sin parámetros.
        /// </summary>
        private clsBill() { }

        /// <summary>
        /// Constructor para crear una instancia de la clase clsBill con valores iniciales.
        /// </summary>
        /// <param name="prmOID">Identificador único del billete.</param>
        /// <param name="prmValue">Valor del billete.</param>
        /// <param name="prmCurrency">Moneda del billete.</param>
        /// <param name="prmDay">Día en que se creó el billete.</param>
        /// <param name="prmMonth">Mes en que se creó el billete.</param>
        /// <param name="prmYear">Año en que se creó el billete.</param>
        public clsBill(string prmOID, double prmValue, clsCurrency prmCurrency, int prmDay, int prmMonth, int prmYear) :
        base(prmOID, prmValue, prmCurrency, prmYear)
        {
            attDay = prmDay;
            attMonth = prmMonth;   
        }
        #endregion
        #region Getters
        /// <summary>
        /// Obtiene el mes en que se creó el billete.
        /// </summary>
        /// <returns>El mes en que se creó el billete.</returns>
        public int getMonth() => attMonth;

        /// <summary>
        /// Obtiene el día en que se creó el billete.
        /// </summary>
        /// <returns>El día en que se creó el billete.</returns>
        public int getDay() => attDay;
        #endregion
        #region Setters
        /// <summary>
        /// Modifica los atributos de la instancia de la clase clsBill.
        /// </summary>
        /// <param name="prmArgs">Lista de argumentos para la modificación.</param>
        /// <returns>Devuelve true si la modificación fue exitosa; de lo contrario, false.</returns>
        public bool modify(List<object> prmArgs)
        {
            if (!base.modify(prmArgs)) return false;
                attMonth = (int)prmArgs[4];
                attDay = (int)prmArgs[5];
                return true;
        }
        #endregion
        #region Destroyer
        /// <summary>
        /// Realiza la operación de eliminación de la instancia de la clase clsBill.
        /// </summary>
        /// <returns>Devuelve true si la eliminación fue exitosa; de lo contrario, false.</returns>
        public override bool die()
        {
            return base.die();
        }
        #endregion
        #region Utilities
        /// <summary>
        /// Compara la instancia actual de clsBill con otra instancia de clsBill.
        /// </summary>
        /// <param name="prmOther">Otra instancia de clsBill a comparar.</param>
        /// <returns>
        /// Devuelve 0 si las instancias son iguales, 1 si la instancia actual es mayor,
        /// y -1 si la instancia actual es menor.
        /// </returns>
        public int CompareTo(clsBill prmOther)
        {
            if (base.CompareTo(prmOther)==0 && (attMonth == prmOther.attMonth && attDay == prmOther.attDay))
            {
                return 0;
            }
            if(attValue>prmOther.attValue) { 
                return 1;
            }
            return -1;
        }

        /// <summary>
        /// Devuelve una representación en cadena de la instancia de clsBill.
        /// </summary>
        /// <returns>Una cadena que representa la información del billete.</returns>
        public override string toString() { 
            return "{Bill Info}\n"+
                   "{OID}\t" + attOID + "\n" + 
                   "{Value}:\t" + attValue + "\n" +
                   "{Day}:\t" + attDay + "\n" +
                   "{Month}:\t" + attMonth + "\n" +
                   "{Year}:\t" + attYear + "\n" +
                   "{OID-Currency}" + attCurrency.getOID();
        }

        /// <summary>
        /// Copia los valores de la instancia actual de clsBill a otra instancia de clsBill.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto a copiar.</typeparam>
        /// <param name="prmOtherObject">Otra instancia de clsBill.</param>
        /// <returns>Devuelve true si la copia fue exitosa; de lo contrario, false.</returns>
        public override bool copyTo<T>(T prmOtherObject)
        {
            clsBill varObjOther =prmOtherObject as clsBill;
            if (varObjOther==null) return false;
            if (!base.copyTo(varObjOther)) return false;
            varObjOther.attMonth=attMonth;
            varObjOther.attDay=attDay;
            return true;
        }
        #endregion 
        #endregion
    }
}