using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace ReDES.Services.Common.Cache
{
    /// <summary>
    /// Realiza acciones sobre los items expirados del cache
    /// </summary>
    [Serializable]
    class Expiration : ICacheItemRefreshAction
    {
        /// <summary>
        /// Constructor Expiración
        /// </summary>
        /// <param name="CacheName">Nombre del cache</param>
        public Expiration()
        {

        }

        /// <summary>
        /// Evento de expiración del objeto en el cache
        /// </summary>
        /// <param name="removeKey"></param>
        /// <param name="expiredValue"></param>
        /// <param name="removalReason"></param>
        public void Refresh(string removeKey, object expiredValue, CacheItemRemovedReason removalReason)
        {

        }
    }
}
