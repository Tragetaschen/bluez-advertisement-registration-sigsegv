using Dbus;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reproduction
{
    [DbusConsume("org.bluez.LEAdvertisingManager1",
        Destination = "org.bluez")]
    public interface IOrgBluezLEAdvertisingManager1 : IDisposable
    {
        Task RegisterAdvertisementAsync(ObjectPath advertisement, IDictionary<string, object> options);
        Task UnregisterAdvertisementAsync(ObjectPath advertisement);
    }
}
