using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using ReDES.Services.Common.Logging;
using System;

namespace ReDES.Services.Common.Exceptions
{
    public static class ExceptionEntry
    {

        /// <summary>
        /// Método que se encarga de adicionar una entrada a las excepciones.
        /// En caso que no exista la política de excepciones, se manejará la exepción en la política general del servicio
        /// </summary>
        /// <param name="ex">Descripción de la excepción</param>
        /// <param name="policy">Política de excepciones</param>
        public static void HandleException(string ex, string policy)
        {
            HandleException(new System.Exception(ex), policy);
        }

        /// <summary>
        /// Método que se encarga de adicionar una entrada a las excepciones.
        /// En caso que no exista la política de excepciones, se manejará la exepción en la política general del servicio
        /// </summary>
        /// <param name="ex">Excepción a manejar</param>
        /// <param name="policy">Política de excepciones</param>
        public static void HandleException(System.Exception ex, string policy)
        {
            Exception exceptionToThrow = null;
            Boolean rethrow = false;

            try
            {
                var exManager = EnterpriseLibraryContainer.Current.GetInstance<ExceptionManager>();
                rethrow = exManager.HandleException(ex, policy, out exceptionToThrow);
            }
            catch
            {
                try
                {
                    var exManager = EnterpriseLibraryContainer.Current.GetInstance<ExceptionManager>();
                    rethrow = exManager.HandleException(ex, Common.Constants.ExceptionNames.ReDESServicesPolicy, out exceptionToThrow);
                }
                catch (System.Exception innerEx)
                {
                    string errorMsg = "An unexpected exception occured while " +
                        "calling HandleException with policy '" + policy + "'. ";
                    errorMsg += Environment.NewLine + innerEx.ToString();
                    errorMsg += Environment.NewLine + ex.ToString();
                    LoggingEntry.Instance.LoggingWrite(LoggingEntry.Category.Error, LoggingEntry.Priority.Highest, errorMsg);
                }
            }
            if (rethrow)
            {
                throw  exceptionToThrow;
            }
        }
    }
}
