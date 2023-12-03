using System;
using System.Collections.Generic;
using pkgServices;
using pkgServices.pkgInterfaces;
using pkgServices.pkgCollections;

namespace pkgPiggyBank.pkgDomain{
    /// <summary>
    /// Representa una clase que define una moneda utilizada en la alcancia.
    /// </summary>
    public class clsCurrency : clsEntity, IComparable<clsCurrency>
    {
        #region Attributes
        /// <summary>
        /// Identificador unico de la moneda.
        /// </summary>
        private string OID;

        /// <summary>
        /// Nombre de la moneda.
        /// </summary>
        private string attName;

        /// <summary>
        /// Tasa de cambio de la moneda con respecto a otra moneda.
        /// </summary>
        private double attTRM;

        /// <summary>
        /// Lista de billetes asociados a la moneda.
        /// </summary>
        private List<clsBill> attBills;

        /// <summary>
        /// Lista de monedas asociadas a la moneda.
        /// </summary>
        private List<clsCoin> attCoins;

        /// <summary>
        /// Alcancia asociada a la moneda.
        /// </summary>
        private clsPiggyBank attPiggy;

        /// <summary>
        /// Lista de valores de monedas permitidos.
        /// </summary>
        private List<double> attCoinsValues;

        /// <summary>
        /// Lista de valores de billetes permitidos.
        /// </summary> 
        private List<double> attBillsValues;
        #endregion
        #region Operations
        #region Constructors
        /// <summary>
        /// Constructor privado para prevenir la creacion de instancias sin parametros.
        /// </summary>
        private clsCurrency() { }
        /// <summary>
        /// Constructor para crear una instancia de la clase clsCurrency con valores iniciales.
        /// </summary>
        /// <param name="prmOID">Identificador unico de la moneda.</param>
        /// <param name="prmName">Nombre de la moneda.</param>
        /// <param name="prmTRM">Tasa de cambio de la moneda.</param>
        /// <param name="prmCoinsValues">Lista de valores de monedas permitidos.</param>
        /// <param name="prmBillsValues">Lista de valores de billetes permitidos.</param>
        public clsCurrency(string prmOID, string prmName, double prmTRM, List<double> prmCoinsValues, List<double> prmBillsValues) : base(prmOID)
        {
            attName = prmName;
            attTRM = prmTRM;
            attCoinsValues = prmCoinsValues;
            attBillsValues = prmBillsValues;
        }
        #endregion
        #region Getters
        /// <summary>
        /// Obtiene el nombre de la moneda.
        /// </summary>
        /// <returns>El nombre de la moneda.</returns>
        public string getName() => attName;

        /// <summary>
        /// Obtiene la tasa de cambio de la moneda.
        /// </summary>
        /// <returns>La tasa de cambio de la moneda.</returns>
        public double getTRM() => attTRM;

        /// <summary>
        /// Obtiene la lista de valores de monedas permitidos.
        /// </summary>
        /// <returns>La lista de valores de monedas permitidos.</returns>
        public List<double> getCoinsValues() => attCoinsValues;

        /// <summary>
        /// Obtiene la lista de valores de billetes permitidos.
        /// </summary>
        /// <returns>La lista de valores de billetes permitidos.</returns>
        public List<double> getBillsValues() => attBillsValues;

        /// <summary>
        /// Recupera las monedas no asignadas que coinciden con los valores dados.
        /// </summary>
        /// <param name="prmValues">Lista de valores de monedas a buscar.</param>
        /// <returns>Una lista de monedas no asignadas que coinciden con los valores dados.</returns>
        public List<clsCoin> retrieveAsOutsideCoins(List<double> prmValues)
        {
            List<clsCoin> varResult = new List<clsCoin>();
            int varToFoundItems = prmValues.Count; 
            foreach (clsCoin varObj in attCoins)
            {
                if(varObj.getPiggy()==null && clsCollections.getIndexOf(varObj.getValue(),prmValues)!=-1)
                {
                    prmValues.Remove(varObj.getValue());
                    varResult.Add(varObj);
                    varToFoundItems--;
                }
            }
            if (varToFoundItems != 0) return null;
            return varResult;
        }

        /// <summary>
        /// Recupera los billetes no asignados que coinciden con los valores dados.
        /// </summary>
        /// <param name="prmValues">Lista de valores de billetes a buscar.</param>
        /// <returns>Una lista de billetes no asignados que coinciden con los valores dados.</returns>
        public List<clsBill> retrieveAsOutsideBills(List<double> prmValues)
        {
            List<clsBill> varResult = new List<clsBill>();
            int varToFoundItems = prmValues.Count;
            foreach (clsBill varObj in attBills)
            {
                if (varObj.getPiggy() == null && clsCollections.getIndexOf(varObj.getValue(), prmValues) != -1)
                {
                    prmValues.Remove(varObj.getValue());
                    varResult.Add(varObj);
                    varToFoundItems--;
                }
            }
            if (varToFoundItems != 0) return null;
            return varResult;
        }

        /// <summary>
        /// Recupera las monedas asignadas que coinciden con los valores dados.
        /// </summary>
        /// <param name="prmValues">Lista de valores de monedas a buscar.</param>
        /// <returns>Una lista de monedas asignadas que coinciden con los valores dados.</returns>
        public List<clsCoin> retrieveAsInsideCoins(List<double> prmValues)
        {
            List<clsCoin> varResult = new List<clsCoin>();
            int varToFoundItems = prmValues.Count;
            foreach (clsCoin varObj in attCoins)
            {
                if (varObj.getPiggy() != null && clsCollections.getIndexOf(varObj.getValue(), prmValues) != -1)
                {
                    prmValues.Remove(varObj.getValue());
                    varResult.Add(varObj);
                    varToFoundItems--;
                }
            }
            if (varToFoundItems != 0) return null;
            return varResult;
        }

        /// <summary>
        /// Recupera los billetes asignados que coinciden con los valores dados.
        /// </summary>
        /// <param name="prmValues">Lista de valores de billetes a buscar.</param>
        /// <returns>Una lista de billetes asignados que coinciden con los valores dados.</returns>
        public List<clsBill> retrieveAsInsideBills(List<double> prmValues)
        {
            List<clsBill> varResult = new List<clsBill>();
            int varToFoundItems = prmValues.Count;
            foreach (clsBill varObj in attBills)
            {
                if (varObj.getPiggy() != null && clsCollections.getIndexOf(varObj.getValue(), prmValues) != -1)
                {
                    prmValues.Remove(varObj.getValue());
                    varResult.Add(varObj);
                    varToFoundItems--;
                }
            }
            if (varToFoundItems != 0) return null;
            return varResult;
        }

        #endregion
        #region Setters
        /// <summary>
        /// Modifica los atributos de la instancia de la clase clsCurrency.
        /// </summary>
        /// <param name="prmName">Nuevo nombre de la moneda.</param>
        /// <param name="prmTRM">Nueva tasa de cambio de la moneda.</param>
        /// <param name="prmCoinsValues">Nueva lista de valores de monedas permitidos.</param>
        /// <param name="prmBillsValues">Nueva lista de valores de billetes permitidos.</param>
        /// <returns>Devuelve true si la modificacion fue exitosa; de lo contrario, false.</returns>
        public bool modify(string prmName, double prmTRM, List<double> prmCoinsValues, List<double> prmBillsValues) 
        {
            if (attCoins.Count != 0) return false;
            if (attBills.Count == 0) return false;
            attName = prmName;
            attTRM = prmTRM;        
            attCoinsValues= prmCoinsValues;
            attBillsValues= prmBillsValues;
            return true;
        }

        /// <summary>
        /// Asigna una alcancia a la instancia de la clase clsCurrency.
        /// </summary>
        /// <param name="prmObj">Objeto de tipo clsPiggyBank a asignar.</param>
        /// <returns>Devuelve true si la asignacion fue exitosa; de lo contrario, false.</returns>
        public bool setPiggyBank(clsPiggyBank prmObj)
        {
            if (prmObj == null) return false;
            if (attPiggy != null && attPiggy.getMoneyItemsCount()!=0) return false;
            if (prmObj.getCurrency().CompareTo(this) != 0) return false;
            attPiggy=prmObj;
            return true;
        }

        #endregion
        #region CRUD
        /// <summary>
        /// Asigna una alcancia a la instancia de la clase clsCurrency.
        /// </summary>
        /// <param name="prmObj">Objeto de tipo clsPiggyBank a asignar.</param>
        /// <returns>Devuelve true si la asignacion fue exitosa; de lo contrario, false.</returns>
        public bool toRegisterCoin(string prmOID, double prmValue, int prmYear)
        {
            return clsBrokerCrud.toRegisterEntity(new clsCoin(prmOID, prmValue, this, prmYear), attCoins);
        }

        /// <summary>
        /// Registra un nuevo billete en la lista de billetes asociados a la moneda.
        /// </summary>
        /// <param name="prmOID">Identificador unico del billete.</param>
        /// <param name="prmValue">Valor del billete.</param>
        /// <param name="prmDay">Dia en que se creo el billete.</param>
        /// <param name="prmMonth">Mes en que se creo el billete.</param>
        /// <param name="prmYear">Año en que se creo el billete.</param>
        /// <returns>Devuelve true si el registro fue exitoso; de lo contrario, false.</returns>
        public bool toRegisterBill(string prmOID, double prmValue, int prmDay, int prmMonth, int prmYear)
        {
            return clsBrokerCrud.toRegisterEntity(new clsBill(prmOID, prmValue, this, prmDay, prmMonth, prmYear), attBills);
        }

        /// <summary>
        /// Actualiza el valor y el año de una moneda en la lista de monedas asociadas a la moneda.
        /// </summary>
        /// <param name="prmOID">Identificador unico de la moneda a actualizar.</param>
        /// <param name="prmValue">Nuevo valor de la moneda.</param>
        /// <param name="prmYear">Nuevo año de la moneda.</param>
        /// <returns>Devuelve true si la actualizacion fue exitosa; de lo contrario, false.</returns>
        public bool toUpdateCoin(string prmOID, double prmValue, int prmYear)
        {
            clsCoin varObj = clsCollections.getItemWith(prmOID, attCoins);
            if (varObj == null) return false;
            return varObj.modify(prmValue, prmYear);
        }

        /// <summary>
        /// Actualiza un billete existente en la lista de billetes de la moneda.
        /// </summary>
        /// <param name="prmOID">Identificador unico del billete a actualizar.</param>
        /// <param name="prmValue">Nuevo valor del billete.</param>
        /// <param name="prmYear">Nuevo año del billete.</param>
        /// <returns>Devuelve true si la actualizacion fue exitosa; de lo contrario, false.</returns>
        public bool toUpdateBill(string prmOID, double prmValue, int prmYear)
        {
            clsBill varObj = clsCollections.getItemWith(prmOID, attBills);
            if (varObj == null) return false;
            return varObj.modify(prmValue, prmYear);
        }

        /// <summary>
        /// Elimina una moneda de la lista de monedas asociadas a la moneda actual.
        /// </summary>
        /// <param name="prmOID">Identificador unico de la moneda a eliminar.</param>
        /// <returns>Devuelve true si la eliminacion fue exitosa; de lo contrario, false.</returns>
        public bool toDeleteCoin(string prmOID)
        {
            clsCoin varObj = clsCollections.getItemWith(prmOID, attCoins);
            if (varObj == null) return false;
            return varObj.die();
        }

        /// <summary>
        /// Elimina un billete de la lista de billetes asociados a la moneda actual.
        /// </summary>
        /// <param name="prmOID">Identificador unico del billete a eliminar.</param>
        /// <returns>Devuelve true si la eliminacion fue exitosa; de lo contrario, false.</returns>
        public bool toDeleteBill(string prmOID)
        {
            clsBill varObj = clsCollections.getItemWith(prmOID, attBills);
            if (varObj == null) return false;
            return varObj.die();
        }

        #endregion
        #region Utilities
        /// <summary>
        /// Devuelve una representacion en cadena de la informacion de la moneda.
        /// </summary>
        /// <returns>Una cadena que representa la informacion de la moneda.</returns>
        public override string toString()
        {
            return "{Currency Info}\n" + 
                "{OID}:\t" + attOID + "\n" + 
                "{Name}:\t" + attName + "\n" + 
                "{TRM}:\t" + attTRM + "\n";
        }

        /// <summary>
        /// Compara la instancia actual de clsCurrency con otra instancia de clsCurrency.
        /// </summary>
        /// <param name="prmOther">Otra instancia de clsCurrency a comparar.</param>
        /// <returns>
        /// Devuelve 0 si las instancias son iguales, 1 si la instancia actual es mayor,
        /// y -1 si la instancia actual es menor.
        /// </returns>
        public int CompareTo(clsCurrency prmOther)
        {
            if (attOID.CompareTo(prmOther.attOID) == 0 &&
                attName == prmOther.attName &&
                attTRM == prmOther.attTRM &&
                clsCollections.areEqualsCollections(attCoins, prmOther.attCoins) &&
                clsCollections.areEqualsCollections(attBills, prmOther.attBills) &&
                attPiggy.CompareTo(prmOther.attPiggy) == 0 &&
                clsCollections.areEqualsCollections(attCoinsValues, prmOther.attCoinsValues) &&
                clsCollections.areEqualsCollections(attBillsValues, prmOther.attBillsValues)) {
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// Copia los valores de la instancia actual de clsCurrency a otra instancia de clsCurrency.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto a copiar.</typeparam>
        /// <param name="prmOtherObject">Otra instancia de clsCurrency.</param>
        /// <returns>Devuelve true si la copia fue exitosa; de lo contrario, false.</returns>
        public override bool copyTo<T>(T prmOtherObject)
        {
            clsCurrency varObjOther =prmOtherObject as clsCurrency;
            if (varObjOther == null) return false;
            varObjOther.attName = attName;
            varObjOther.attTRM = attTRM;
            varObjOther.attCoins = attCoins;
            varObjOther.attBills = attBills;
            varObjOther.attPiggy = attPiggy;
            varObjOther.attCoinsValues=attCoinsValues;
            varObjOther.attBillsValues = attBillsValues;
            return true;
        }

        #endregion
        #region Query
        /// <summary>
        /// Verifica si los valores dados son un subconjunto de los valores de monedas asociados a la moneda actual.
        /// </summary>
        /// <param name="prmValues">Lista de valores a verificar.</param>
        /// <returns>Devuelve true si los valores son un subconjunto; de lo contrario, false.</returns>
        public bool areValidValuesAsCoins(List<double> prmValues) => clsCollections.isSubSet(prmValues, attCoinsValues);

        /// <summary>
        /// Verifica si los valores dados son un subconjunto de los valores de billetes asociados a la moneda actual.
        /// </summary>
        /// <param name="prmValues">Lista de valores a verificar.</param>
        /// <returns>Devuelve true si los valores son un subconjunto; de lo contrario, false.</returns>
        public bool areValidValuesAsBills(List<double> prmValues) => clsCollections.isSubSet(prmValues, attBillsValues);

        #endregion
        #region Destroyer
        /// <summary>
        /// Realiza la operacion de eliminacion de la instancia de la clase clsCurrency.
        /// </summary>
        /// <returns>Devuelve true si la eliminacion fue exitosa; de lo contrario, false.</returns>
        public override bool die()
        {
            if (attCoins.Count!=0) return false;
            if (attBills.Count != 0) return false;
            if (attPiggy != null) return false;
            return true;
        }
        #endregion
        #endregion
    }
}
