using Dbus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Reproduction
{
    [DbusProvide("org.bluez.LEAdvertisement1")]
    public interface IOrgBluezLEAdvertisement1 : IDisposable, INotifyPropertyChanged
    {
        Task ReleaseAsync();

        string Type { get; }
        IEnumerable<string> ServiceUUIDs { get; }
        IEnumerable<string> SolicitUUIDs { get; }
        ObjectPath Path { get; }
        bool IncludeTxPower { get; }
    }
}
