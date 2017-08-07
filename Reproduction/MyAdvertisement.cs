using Dbus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Reproduction
{
    public class MyAdvertisement : IOrgBluezLEAdvertisement1
    {
        public MyAdvertisement(OrgFreedesktopDbusObjectManager objectManager)
            => Path = objectManager.AddObject<IOrgBluezLEAdvertisement1, MyAdvertisement>(this, "./");

        public string Type => "peripheral";

        public IEnumerable<string> ServiceUUIDs => new[] { "e81aef6d-b509-4d4c-aa07-d88c339889af" };
        public IEnumerable<string> SolicitUUIDs => new string[] { };
        public ObjectPath Path { get; }
        public bool IncludeTxPower => true;

        public event PropertyChangedEventHandler PropertyChanged { add { } remove { } }
        public Task ReleaseAsync() => throw new NotImplementedException();

        public void Dispose() { }
    }
}
