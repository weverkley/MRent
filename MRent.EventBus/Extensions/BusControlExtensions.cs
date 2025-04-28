using MassTransit;

namespace MRent.EventBus.Extensions
{
    public static class BusControlExtensions
    {
        public static Uri DetermineQueueEndpoint(this IBus bus, Type messageType)
        {
            return DetermineSendEndpointAddressByConvention(bus.Address, messageType, "--queue");
        }

        public static Uri DetermineSendEndpointAddressByConvention(Uri address, Type messageType, string aditionalPath = null, string pathSeparator = "/")
        {
            return new Uri(address, DetermineSendEndpointNameByConvention(messageType, aditionalPath, pathSeparator));
        }

        public static string DetermineSendEndpointNameByConvention(Type messageType, string aditionalPath = null, string pathSeparator = "/")
        {
            var messageTypeName = messageType.Name.ToLower();
            var messageTypeNamespace = messageType.Namespace.ToLower();
            var messageTypeEndpointName = string.Concat(messageTypeNamespace, pathSeparator, messageTypeName);

            if (!string.IsNullOrEmpty(aditionalPath))
                messageTypeEndpointName = messageTypeEndpointName += aditionalPath;

            return messageTypeEndpointName;
        }
    }
}
