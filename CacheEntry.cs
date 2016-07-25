using System;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

namespace ReDES.Services.Common.Cache
{
    /// <summary>
    /// Clase que se encarga del manejo del Cache
    /// </summary>
    /// <remarks>Hennry García B. 26/01/2012</remarks>
    [Serializable]
    public class CacheEntry : ICacheManager
    {
        #region Attributes

        private Expiration _expiration = null;
        private const string DefaultCache = "Cache Manager";

        #endregion Attributes

        #region Properties

        /// <summary>
        /// Objeto para manipular el cache dado en el constructor.
        /// </summary>
        /// <remarks>Hennry García B. 26/01/2012</remarks>
        private ICacheManager _manager;

        /// <summary>
        /// Obtiene la cantidad de elementos que se encuentran en el cache.
        /// </summary>
        /// <remarks>Hennry García B. 26/01/2012</remarks>
        public int Count
        {
            get
            {
                return _manager.Count;
            }
        }

        /// <summary>
        /// Propiedad que obtienen un elemento del cache por el identificador
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks>Hennry García B. 26/01/2012</remarks>
        public object this[string key]
        {
            get { return _manager.GetData(key); }
        }


        #endregion properties

        /// <summary>
        /// Constructor de la clase CahceExpirationManager.
        /// Crea la clase y asigna el agente al que pertenece el cache.
        /// </summary>
        /// <param name="cacheName">Nombre del cache que se utiliza.</param>
        /// <remarks>Hennry García B. 26/01/2012</remarks>
        public CacheEntry(string cacheName)
        {
            try
            {
                _manager = CacheFactory.GetCacheManager(cacheName);
                _expiration = new Expiration();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Constructor de la clase Cache
        /// Crea la clase y asigna el agente al que pertenece el cache.
        /// </summary>
        /// <remarks>Hennry García B. 26/01/2012</remarks>
        public CacheEntry()
        {
            try
            {
                _manager = CacheFactory.GetCacheManager(DefaultCache);
                _expiration = new Expiration();
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        #region ICacheManager Members

        /// <summary>
        /// Método que se encarga de adicionar un elemento al cache
        /// </summary>
        /// <param name="key">Identificador del elemento</param>
        /// <param name="value">Valor a adicionar</param>
        /// <param name="scavengingPriority">Prioridad</param>
        /// <param name="refreshAction">Acción</param>
        /// <param name="expirations">Expiración del elemento en el cache</param>
        /// <remarks>Hennry García B. 26/01/2012</remarks>
        public void Add(string key, object value, CacheItemPriority scavengingPriority, ICacheItemRefreshAction refreshAction, params ICacheItemExpiration[] expirations)
        {
            _manager.Add(key, value, scavengingPriority, refreshAction, expirations);
        }

        /// <summary>
        /// Almacena una unidad de trabajo en el cache
        /// </summary>
        /// <param name="key">Identificador de trabajo que se almacenara</param>
        /// <param name="context">unidad de trabajo que se almacenara</param>
        /// <param name="scavengingPriority">Itempriority</param>
        /// <returns>Verdadero en caso que se haya realizado la acción, Falso en caso contrario</returns>
        /// <remarks>Hennry García B. 26/01/2012</remarks>
        public bool Add(string key, object context, CacheItemPriority scavengingPriority)
        {
            try
            {
                _manager.Add(key, context, scavengingPriority, _expiration);
                return true;
            }
            catch (System.Exception exp)
            {
                Console.WriteLine(exp.Message);
                return false;
            }
        }

        /// <summary>
        /// Método que se encarga de adicionar un valor en el cache
        /// </summary>
        /// <param name="key">Identificador del cache</param>
        /// <param name="value">Valor a adicionar en el cache</param>
        /// <remarks>Hennry García B. 26/01/2012</remarks>
        public void Add(string key, object value)
        {
            _manager.Add(key, value, CacheItemPriority.High, null, new NeverExpired());
        }

        /// <summary>
        /// Método que se encarga de verificar si un elemento se encuentra en el cache
        /// </summary>
        /// <param name="key">Identificador del elemento</param>
        /// <returns>Verdadero en caso que se encuentre el elemento, falso en caso contrario</returns>
        /// <remarks>Hennry García B. 26/01/2012</remarks>
        public bool Contains(string key)
        {
            return _manager.Contains(key);
        }

        /// <summary>
        /// Método que se encarga de limpiar el cache
        /// </summary>
        /// <remarks>Hennry García B. 26/01/2012</remarks>
        public void Flush()
        {
            _manager.Flush();
        }

        /// <summary>
        /// Método que se encarga de obtener información del cache
        /// </summary>
        /// <param name="key">Llave del elemento</param>
        /// <returns>Elemento encontrado</returns>
        /// <remarks>Hennry García B. 26/01/2012</remarks>
        public object GetData(string key)
        {
            return _manager.GetData(key);
        }

        /// <summary>
        /// Método que se encarga de eliminar un elemento del cache
        /// </summary>
        /// <param name="key">Identificador del elemento</param>
        /// <remarks>Hennry García B. 26/01/2012</remarks>
        public void Remove(string key)
        {
            _manager.Remove(key);
        }

        #endregion
    }
}
