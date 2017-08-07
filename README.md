This demo program reproduces a SIGSEGV crash in Bluez 4.43. The Bluez code hasn't changed in that area since, so it should (not) work in more recent versions as well.

The problem is that while calling org.bluez.LEAdvertisingManager1.RegisterAdvertisement, Bluez sends two method calls to the given object path:
- org.freedesktop.DBus.Properties.GetAll
- org.freedesktop.DBus.ObjectManager.GetManagedObjects

When these two methods are replied to in order (GetAll then GetManagedObjects), everything works as expected.
When the order is reversed, Bluez sends an error for the RegisterAdvertisement with "Failed to parse advertisement" after the GetManagedObjects returns and then dies by a SIGSEGV when GetAll returns.

The DbusCore implementation uses a thread pool to dispatch incoming method calls and doesn't have the necessary ordering requirements.

### Prerequisites

You need the .NET Core 1.1 SDK to build the project: https://www.microsoft.com/net/core#linuxubuntu
You need Mono to run the project.

After cloning this repository, run
```
git submodule update --init
```
to initialize the DbusCore submodule.

### Building

Go to the `Reproduction` folder and run
```
dotnet restore
dotnet publish -o ../artifacts
```

### Running

Navigate to the `artifacts` folder and run
```
mono Reproduction.exe
```

### Configuration
The code expects the adapter to be `hci0`. If that's not the case, you can change the string constant in `Reproduction/Program.cs`

The code currently depends on the thread pool dispatching and only sometimes breakes Bluez (you might need to run it a couple of times - I haven't checked on a multicore platform)
To make it fail reliably, you can change the GetAll handling: In `DbusCore/src/Dbus/Connection.ReceiveMethodCall.cs`. In line 62/63, change
```cs
Task.Run(() =>
    handlePropertyRequestAsync(replySerial, header, body));
```
to
```cs

Task.Run(async () =>
{
    await Task.Delay(1000);
    await handlePropertyRequestAsync(replySerial, header, body);
});
```
