using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace ReDES.Services.Common.Exceptions
{
    public class GlobalExceptionHandler : IErrorHandler
    {

        public bool HandleError(Exception error)
        {
            ExceptionEntry.HandleException(error, ReDES.Services.Common.Constants.ExceptionNames.ReDESServicesPolicy);
            //Representa una falla en el SOAP
            if (error is FaultException)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
            ExceptionEntry.HandleException(error, ReDES.Services.Common.Constants.ExceptionNames.ReDESServicesPolicy);
        }
    }

    public class GlobalExceptionHandlerBehaviourAttribute : Attribute, IServiceBehavior
    {
        private readonly Type _errorHandlerType;

        public GlobalExceptionHandlerBehaviourAttribute(Type errorHandlerType)
        {
            _errorHandlerType = errorHandlerType;
        }

        #region IServiceBehavior Members

        public void Validate(ServiceDescription description,
                             ServiceHostBase serviceHostBase)
        {
        }

        public void AddBindingParameters(ServiceDescription description,
                                         ServiceHostBase serviceHostBase,
                                         Collection<ServiceEndpoint> endpoints,
                                         BindingParameterCollection parameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription description,
                                          ServiceHostBase serviceHostBase)
        {
            IErrorHandler handler =
                (IErrorHandler)Activator.CreateInstance(_errorHandlerType);

            foreach (ChannelDispatcherBase dispatcherBase in
                serviceHostBase.ChannelDispatchers)
            {
                var channelDispatcher = dispatcherBase as ChannelDispatcher;
                if (channelDispatcher != null)
                    channelDispatcher.ErrorHandlers.Add(handler);
            }
        }

        #endregion
    }
}
