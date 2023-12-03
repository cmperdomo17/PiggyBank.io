using System;
using System.Collections.Generic;
using pkgServices.pkgCollections;
using pkgServices;
using System.Data.Common;

namespace pkgPiggyBank.pkgDomain
{
    /// <summary>
    /// Clase que actua como controlador central para la gestion de monedas, alcancias y transacciones.
    /// </summary>
    public class clsController
   {
        #region Attributes
        /// <summary>
        /// Instancia unica del controlador (patron Singleton).
        /// </summary>
        private static clsController attInstance;

        /// <summary>
        /// Lista de monedas gestionadas por el controlador.
        /// </summary>
        private List<clsCurrency> attCurrencies;

        /// <summary>
        /// Instancia de la alcancia gestionada por el controlador.
        /// </summary>
        private clsPiggyBank attPiggy;
        #endregion
        #region Constructors
        /// <summary>
        /// Constructor privado para asegurar que solo hay una instancia del controlador (patron Singleton).
        /// </summary>
        private clsController() { }
        #endregion
        #region Getters
        /// <summary>
        /// Obtiene la instancia unica del controlador (patron Singleton).
        /// </summary>
        /// <returns>La instancia unica del controlador.</returns>
        public static clsController getInstance()
        {
            if (attInstance == null) { 
                attInstance = new clsController();
            }
            return attInstance;
        }

        #endregion
        #region Cruds
        /// <summary>
        /// Registra una nueva moneda con sus valores de monedas y billetes.
        /// </summary>
        /// <param name="prmOID">Identificador unico de la moneda.</param>
        /// <param name="prmName">Nombre de la moneda.</param>
        /// <param name="prmTRM">Tasa de cambio de la moneda.</param>
        /// <param name="prmCoinsValues">Lista de valores de monedas disponibles.</param>
        /// <param name="prmBillsValues">Lista de valores de billetes disponibles.</param>
        /// <returns>Devuelve true si el registro fue exitoso; de lo contrario, false.</returns>
        public bool toRegisterCurrency(string prmOID, string prmName, double prmTRM, List<double> prmCoinsValues, List<double> prmBillsValues)
        {
            return clsBrokerCrud.toRegisterEntity(new clsCurrency(prmOID, prmName, prmTRM, prmCoinsValues, prmBillsValues), attCurrencies);
        }

        /// <summary>
        /// Registra una nueva alcancia con sus limites y valores disponibles.
        /// </summary>
        /// <param name="prmOIDCurrency">Identificador unico de la moneda asociada a la alcancia.</param>
        /// <param name="prmCoinsMaxCap">Limite maximo de monedas en la alcancia.</param>
        /// <param name="prmBillsMaxCap">Limite maximo de billetes en la alcancia.</param>
        /// <param name="prmCoinsValues">Lista de valores de monedas disponibles en la alcancia.</param>
        /// <param name="prmBillsValues">Lista de valores de billetes disponibles en la alcancia.</param>
        /// <returns>Devuelve true si el registro fue exitoso; de lo contrario, false.</returns>
        public bool toRegisterPiggyBank(string prmOIDCurrency, int prmCoinsMaxCap, int prmBillsMaxCap, List<double> prmCoinsValues, List<double> prmBillsValues)
        {
            if(attPiggy!=null) return false;
            clsCurrency varObj = clsCollections.getItemWith(prmOIDCurrency, attCurrencies);
            if (varObj == null) return false;
            attPiggy=new clsPiggyBank(prmCoinsMaxCap, prmBillsMaxCap, prmCoinsValues, prmBillsValues, varObj);
            varObj.setPiggyBank(attPiggy);
            return true;
        }

        /// <summary>
        /// Registra una nueva moneda en la alcancia.
        /// </summary>
        /// <param name="prmOIDCurrency">Identificador unico de la moneda en la alcancia.</param>
        /// <param name="prmOID">Identificador unico de la moneda a registrar.</param>
        /// <param name="prmValue">Valor de la moneda.</param>
        /// <param name="prmYear">Año en que se creo la moneda.</param>
        /// <returns>Devuelve true si el registro fue exitoso; de lo contrario, false.</returns>
        public bool toRegisterCoin(string prmOIDCurrency, string prmOID, double prmValue, int prmYear)
        {
            try
            {
                return clsCollections.getItemWith(prmOIDCurrency, attCurrencies).toRegisterCoin(prmOID, prmValue, prmYear); 
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Registra un nuevo billete en la alcancia.
        /// </summary>
        /// <param name="prmOIDCurrency">Identificador unico de la moneda en la alcancia.</param>
        /// <param name="prmOID">Identificador unico del billete a registrar.</param>
        /// <param name="prmValue">Valor del billete.</param>
        /// <param name="prmDay">Dia en que se creo el billete.</param>
        /// <param name="prmMonth">Mes en que se creo el billete.</param>
        /// <param name="prmYear">Año en que se creo el billete.</param>
        /// <returns>Devuelve true si el registro fue exitoso; de lo contrario, false.</returns>
        public bool toRegisterBill(string prmOIDCurrency, string prmOID, double prmValue, int prmDay, int prmMonth, int prmYear)
        {
            try
            {
                return clsCollections.getItemWith(prmOIDCurrency, attCurrencies).toRegisterBill(prmOID, prmValue, prmDay, prmMonth, prmYear);
            }
            catch (Exception e) {
                return false;
            }
        }

        /// <summary>
        /// Actualiza los valores de una moneda en la alcancia.
        /// </summary>
        /// <param name="prmOID">Identificador unico de la moneda a actualizar.</param>
        /// <param name="prmName">Nuevo nombre de la moneda.</param>
        /// <param name="prmTRM">Nueva tasa de cambio de la moneda.</param>
        /// <param name="prmCoinsValues">Nuevos valores de monedas disponibles.</param>
        /// <param name="prmBillsValues">Nuevos valores de billetes disponibles.</param>
        /// <returns>Devuelve true si la actualizacion fue exitosa; de lo contrario, false.</returns>
        public bool toUptadeCurrency(string prmOID, string prmName, double prmTRM, List<double> prmCoinsValues, List<double> prmBillsValues)
        {
            clsCurrency varObj = clsCollections.getItemWith(prmOID, attCurrencies);
            if(varObj==null)return false;   
            return varObj.modify(prmName, prmTRM, prmCoinsValues, prmBillsValues);       
        }

        /// <summary>
        /// Actualiza los valores y limites de la alcancia asociada a una moneda.
        /// </summary>
        /// <param name="prmOIDCurrency">Identificador unico de la moneda asociada a la alcancia.</param>
        /// <param name="prmCoinsMaxCap">Nuevo limite maximo de monedas en la alcancia.</param>
        /// <param name="prmBillsMaxCap">Nuevo limite maximo de billetes en la alcancia.</param>
        /// <param name="prmCoinsValues">Nuevos valores de monedas disponibles en la alcancia.</param>
        /// <param name="prmBillsValues">Nuevos valores de billetes disponibles en la alcancia.</param>
        /// <returns>Devuelve true si la actualizacion fue exitosa; de lo contrario, false.</returns>
        public bool toUpdatePiggyBank(string prmOIDCurrency, int prmCoinsMaxCap, int prmBillsMaxCap, List<double> prmCoinsValues, List<double> prmBillsValues)
        {
            if (attPiggy == null) return false;
            clsCurrency varObj = clsCollections.getItemWith(prmOIDCurrency, attCurrencies);
            if (varObj == null) return false;
            return attPiggy.modify(varObj, prmCoinsMaxCap, prmBillsMaxCap, prmCoinsValues, prmBillsValues);
        }

        /// <summary>
        /// Actualiza los valores de una moneda en la alcancia.
        /// </summary>
        /// <param name="prmOIDCurrency">Identificador unico de la moneda en la alcancia.</param>
        /// <param name="prmOID">Identificador unico de la moneda a actualizar.</param>
        /// <param name="prmValue">Nuevo valor de la moneda.</param>
        /// <param name="prmYear">Nuevo año de la moneda.</param>
        /// <returns>Devuelve true si la actualizacion fue exitosa; de lo contrario, false.</returns>
        public bool toUpdateCoin(string prmOIDCurrency, string prmOID, double prmValue, int prmYear)
        {
            clsCurrency varObj= clsCollections.getItemWith(prmOIDCurrency, attCurrencies);
            if (varObj == null) return false;
            return varObj.toUpdateCoin(prmOID, prmValue, prmYear);
        }

        /// <summary>
        /// Actualiza los valores de un billete en la alcancia.
        /// </summary>
        /// <param name="prmOIDCurrency">Identificador unico de la moneda en la alcancia.</param>
        /// <param name="prmOID">Identificador unico del billete a actualizar.</param>
        /// <param name="prmValue">Nuevo valor del billete.</param>
        /// <param name="prmDay">Nuevo dia de creacion del billete.</param>
        /// <param name="prmMonth">Nuevo mes de creacion del billete.</param>
        /// <param name="prmYear">Nuevo año de creacion del billete.</param>
        /// <returns>Devuelve true si la actualizacion fue exitosa; de lo contrario, false.</returns>
        public bool toUpdateBill(string prmOIDCurrency, string prmOID, double prmValue, int prmDay, int prmMonth, int prmYear)
        {
            clsCurrency varObj = clsCollections.getItemWith(prmOIDCurrency, attCurrencies);
            if (varObj == null) return false;
            return varObj.toUpdateBill(prmOID, prmValue, prmYear);
        }

        /// <summary>
        /// Elimina una moneda de la lista de monedas gestionadas por el controlador.
        /// </summary>
        /// <param name="prmOID">Identificador unico de la moneda a eliminar.</param>
        /// <returns>Devuelve true si la eliminacion fue exitosa; de lo contrario, false.</returns>
        public bool toDeleteCurrency(string prmOID)
        {
            clsCurrency varObj = clsCollections.getItemWith(prmOID, attCurrencies);
            if (varObj == null) return false;
            if (!varObj.die()) return false;
            return attCurrencies.Remove(varObj);

        }

        /// <summary>
        /// Elimina la alcancia actual si existe.
        /// </summary>
        /// <returns>Devuelve true si la eliminacion de la alcancia fue exitosa; de lo contrario, false.</returns>
        public bool toDeletePiggyBank()
        {
            if(attPiggy==null) return false;
            return attPiggy.die();
        }

        /// <summary>
        /// Elimina una moneda especifica de una moneda dada.
        /// </summary>
        /// <param name="prmOIDCurrency">Identificador unico de la moneda asociada a la moneda a eliminar.</param>
        /// <param name="prmOID">Identificador unico de la moneda a eliminar.</param>
        /// <returns>Devuelve true si la eliminacion fue exitosa; de lo contrario, false.</returns>
        public bool toDeleteCoin(string prmOIDCurrency, string prmOID)
        {
            clsCurrency varObj = clsCollections.getItemWith(prmOIDCurrency, attCurrencies);
            if (varObj == null) return false;
            return varObj.toDeleteCoin(prmOID);
        }

        /// <summary>
        /// Elimina un billete especifico de una moneda dada.
        /// </summary>
        /// <param name="prmOIDCurrency">Identificador unico de la moneda asociada al billete a eliminar.</param>
        /// <param name="prmOID">Identificador unico del billete a eliminar.</param>
        /// <returns>Devuelve true si la eliminacion fue exitosa; de lo contrario, false.</returns>
        public bool toDeleteBill(string prmOIDCurrency, string prmOID)
        {
            clsCurrency varObj = clsCollections.getItemWith(prmOIDCurrency, attCurrencies);
            if (varObj == null) return false;
            return varObj.toDeleteBill(prmOID);
        }


        #endregion
        #region Transactions
        /// <summary>
        /// Realiza un ingreso de monedas a la alcancia.
        /// </summary>
        /// <param name="prmValues">Lista de valores de las monedas a ingresar.</param>
        /// <returns>Devuelve true si el ingreso fue exitoso; de lo contrario, false.</returns>
        public bool coinsIncome(List<double> prmValues)
        {
            if (attPiggy == null) return false;
            clsCurrency varObj = attPiggy.getCurrency();
            if(varObj== null) return false;
            if (!varObj.areValidValuesAsCoins(prmValues)) return false;
            List<clsCoin> varCoins = varObj.retrieveAsOutsideCoins(prmValues);
            if(varCoins == null) return false;
            if (varCoins.Count!=prmValues.Count) return false;  
            return attPiggy.coinsIncome(varCoins);
        }

        /// <summary>
        /// Realiza una retirada de monedas de la alcancia.
        /// </summary>
        /// <param name="prmValues">Lista de valores de las monedas a retirar.</param>
        /// <returns>Devuelve true si la retirada fue exitosa; de lo contrario, false.</returns>
        public bool coinsWithdrawal(List<double> prmValues)
        {
            if (attPiggy == null) return false;
            clsCurrency varObj = attPiggy.getCurrency();
            if (varObj == null) return false;
            List<clsCoin> varCoins = varObj.retrieveAsInsideCoins(prmValues);
            if(varCoins == null) return false;
            return attPiggy.coinsWithdrawal(varCoins);
        }

        /// <summary>
        /// Realiza un ingreso de billetes a la alcancia.
        /// </summary>
        /// <param name="prmValues">Lista de valores de los billetes a ingresar.</param>
        /// <returns>Devuelve true si el ingreso fue exitoso; de lo contrario, false.</returns>
        public bool billsIncome(List<double> prmValues)
        {
            if(attPiggy == null) return false;
            clsCurrency varObj = attPiggy.getCurrency();
            if (varObj == null) return false;
            if (!varObj.areValidValuesAsBills(prmValues)) return false;
            List<clsBill> varBills = varObj.retrieveAsOutsideBills(prmValues);
            if (varBills == null) return false;
            return attPiggy.billsIncome(varBills);
        }

        /// <summary>
        /// Realiza una retirada de billetes de la alcancia.
        /// </summary>
        /// <param name="prmValues">Lista de valores de los billetes a retirar.</param>
        /// <returns>Devuelve true si la retirada fue exitosa; de lo contrario, false.</returns>
        public bool billsWithdrawal(List<double> prmValues)
        {
            if (attPiggy == null) return false;
            clsCurrency varObj = attPiggy.getCurrency();
            if (varObj == null) return false;
            List<clsBill> varBills = varObj.retrieveAsInsideBills(prmValues);
            if (varBills == null) return false;
            return attPiggy.billsWithdrawal(varBills);
        } 
        #endregion
    }
}