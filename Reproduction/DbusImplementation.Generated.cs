namespace Reproduction
{

    public static partial class DbusImplementations
    {
        static partial void DoInit()
        {
            global::Dbus.Connection.AddPublishProxy<global::Dbus.IOrgFreedesktopDbusObjectManagerProvide>(global::Dbus.OrgFreedesktopDbusObjectManager_Proxy.Factory);
            global::Dbus.Connection.AddConsumeImplementation<global::Dbus.IOrgFreedesktopDbus>(IOrgFreedesktopDbus_Implementation.Factory);
            global::Dbus.Connection.AddConsumeImplementation<global::Dbus.IOrgFreedesktopDbusObjectManager>(IOrgFreedesktopDbusObjectManager_Implementation.Factory);
            global::Dbus.Connection.AddPublishProxy<global::Reproduction.IOrgBluezLEAdvertisement1>(IOrgBluezLEAdvertisement1_Proxy.Factory);
            global::Dbus.Connection.AddConsumeImplementation<global::Reproduction.IOrgBluezLEAdvertisingManager1>(IOrgBluezLEAdvertisingManager1_Implementation.Factory);
        }
    }

    public sealed class IOrgFreedesktopDbus_Implementation : global::Dbus.IOrgFreedesktopDbus
    {
        private readonly global::Dbus.Connection connection;
        private readonly global::Dbus.ObjectPath path;
        private readonly string destination;
        private readonly global::System.Collections.Generic.List<System.IDisposable> eventSubscriptions = new global::System.Collections.Generic.List<System.IDisposable>();

        private IOrgFreedesktopDbus_Implementation(global::Dbus.Connection connection, global::Dbus.ObjectPath path, string destination)
        {
            this.connection = connection;
            this.path = path ?? "/org/freedesktop/DBus";
            this.destination = destination ?? "org.freedesktop.DBus";
            eventSubscriptions.Add(connection.RegisterSignalHandler(
                this.path,
                "org.freedesktop.DBus",
                "NameAcquired",
                this.handleNameAcquired
            ));

        }

        public static global::Dbus.IOrgFreedesktopDbus Factory(global::Dbus.Connection connection, global::Dbus.ObjectPath path, string destination)
        {
            return new IOrgFreedesktopDbus_Implementation(connection, path, destination);
        }


        public async global::System.Threading.Tasks.Task AddMatchAsync(global::System.String match)
        {
            var sendBody = global::Dbus.Encoder.StartNew();
            var sendIndex = 0;
            global::Dbus.Encoder.Add(sendBody, ref sendIndex, match);

            var receivedMessage = await connection.SendMethodCall(
                this.path,
                "org.freedesktop.DBus",
                "AddMatch",
                this.destination,
                sendBody,
                "s"
            ).ConfigureAwait(false);
            receivedMessage.Signature.AssertEqual("");
            return;
        }

        public async global::System.Threading.Tasks.Task<global::System.String> HelloAsync()
        {
            var sendBody = global::Dbus.Encoder.StartNew();

            var receivedMessage = await connection.SendMethodCall(
                this.path,
                "org.freedesktop.DBus",
                "Hello",
                this.destination,
                sendBody,
                ""
            ).ConfigureAwait(false);
            receivedMessage.Signature.AssertEqual("s");
            var decoderIndex = 0;
            var result = global::Dbus.Decoder.GetString(receivedMessage.Body, ref decoderIndex);
            return result;
        }

        public async global::System.Threading.Tasks.Task<global::System.Collections.Generic.IEnumerable<global::System.String>> ListNamesAsync()
        {
            var sendBody = global::Dbus.Encoder.StartNew();

            var receivedMessage = await connection.SendMethodCall(
                this.path,
                "org.freedesktop.DBus",
                "ListNames",
                this.destination,
                sendBody,
                ""
            ).ConfigureAwait(false);
            receivedMessage.Signature.AssertEqual("as");
            var decoderIndex = 0;
            var result = global::Dbus.Decoder.GetArray(receivedMessage.Body, ref decoderIndex, global::Dbus.Decoder.GetString);
            return result;
        }

        public async global::System.Threading.Tasks.Task RemoveMatchAsync(global::System.String match)
        {
            var sendBody = global::Dbus.Encoder.StartNew();
            var sendIndex = 0;
            global::Dbus.Encoder.Add(sendBody, ref sendIndex, match);

            var receivedMessage = await connection.SendMethodCall(
                this.path,
                "org.freedesktop.DBus",
                "RemoveMatch",
                this.destination,
                sendBody,
                "s"
            ).ConfigureAwait(false);
            receivedMessage.Signature.AssertEqual("");
            return;
        }

        public async global::System.Threading.Tasks.Task<global::System.UInt32> RequestNameAsync(global::System.String name, global::System.UInt32 flags)
        {
            var sendBody = global::Dbus.Encoder.StartNew();
            var sendIndex = 0;
            global::Dbus.Encoder.Add(sendBody, ref sendIndex, name);
            global::Dbus.Encoder.Add(sendBody, ref sendIndex, flags);

            var receivedMessage = await connection.SendMethodCall(
                this.path,
                "org.freedesktop.DBus",
                "RequestName",
                this.destination,
                sendBody,
                "su"
            ).ConfigureAwait(false);
            receivedMessage.Signature.AssertEqual("u");
            var decoderIndex = 0;
            var result = global::Dbus.Decoder.GetUInt32(receivedMessage.Body, ref decoderIndex);
            return result;
        }

        public event global::System.Action<global::System.String> NameAcquired;
        private void handleNameAcquired(global::Dbus.MessageHeader header, byte[] body)
        {
            header.BodySignature.AssertEqual("s");
            var decoderIndex = 0;
            var decoded0 = global::Dbus.Decoder.GetString(body, ref decoderIndex);
            NameAcquired?.Invoke(decoded0);
        }

        public void Dispose()
        {
            eventSubscriptions.ForEach(x => x.Dispose());
        }
    }

    public sealed class IOrgFreedesktopDbusObjectManager_Implementation : global::Dbus.IOrgFreedesktopDbusObjectManager
    {
        private readonly global::Dbus.Connection connection;
        private readonly global::Dbus.ObjectPath path;
        private readonly string destination;
        private readonly global::System.Collections.Generic.List<System.IDisposable> eventSubscriptions = new global::System.Collections.Generic.List<System.IDisposable>();

        private IOrgFreedesktopDbusObjectManager_Implementation(global::Dbus.Connection connection, global::Dbus.ObjectPath path, string destination)
        {
            this.connection = connection;
            this.path = path ?? "";
            this.destination = destination ?? "";
            eventSubscriptions.Add(connection.RegisterSignalHandler(
                this.path,
                "org.freedesktop.DBus.ObjectManager",
                "InterfacesAdded",
                this.handleInterfacesAdded
            ));
            eventSubscriptions.Add(connection.RegisterSignalHandler(
                this.path,
                "org.freedesktop.DBus.ObjectManager",
                "InterfacesRemoved",
                this.handleInterfacesRemoved
            ));

        }

        public static global::Dbus.IOrgFreedesktopDbusObjectManager Factory(global::Dbus.Connection connection, global::Dbus.ObjectPath path, string destination)
        {
            return new IOrgFreedesktopDbusObjectManager_Implementation(connection, path, destination);
        }


        public async global::System.Threading.Tasks.Task<global::System.Collections.Generic.IDictionary<global::Dbus.ObjectPath, global::System.Collections.Generic.IDictionary<global::System.String, global::System.Collections.Generic.IDictionary<global::System.String, global::System.Object>>>> GetManagedObjectsAsync()
        {
            var sendBody = global::Dbus.Encoder.StartNew();

            var receivedMessage = await connection.SendMethodCall(
                this.path,
                "org.freedesktop.DBus.ObjectManager",
                "GetManagedObjects",
                this.destination,
                sendBody,
                ""
            ).ConfigureAwait(false);
            receivedMessage.Signature.AssertEqual("a{oa{sa{sv}}}");
            var decoderIndex = 0;
            var result = global::Dbus.Decoder.GetDictionary(receivedMessage.Body, ref decoderIndex, global::Dbus.Decoder.GetObjectPath, (byte[] result_v_b, ref int result_v_i) =>
            {
                var result_v_inner = global::Dbus.Decoder.GetDictionary(result_v_b, ref result_v_i, global::Dbus.Decoder.GetString, (byte[] result_v_inner_v_b, ref int result_v_inner_v_i) =>
                {
                    var result_v_inner_v_inner = global::Dbus.Decoder.GetDictionary(result_v_inner_v_b, ref result_v_inner_v_i, global::Dbus.Decoder.GetString, global::Dbus.Decoder.GetObject);

                    return result_v_inner_v_inner;
                });

                return result_v_inner;
            });
            return result;
        }

        public event global::System.Action<global::Dbus.ObjectPath, global::System.Collections.Generic.IDictionary<global::System.String, global::System.Collections.Generic.IDictionary<global::System.String, global::System.Object>>> InterfacesAdded;
        private void handleInterfacesAdded(global::Dbus.MessageHeader header, byte[] body)
        {
            header.BodySignature.AssertEqual("oa{sa{sv}}");
            var decoderIndex = 0;
            var decoded0 = global::Dbus.Decoder.GetObjectPath(body, ref decoderIndex);
            var decoded1 = global::Dbus.Decoder.GetDictionary(body, ref decoderIndex, global::Dbus.Decoder.GetString, (byte[] decoded1_v_b, ref int decoded1_v_i) =>
            {
                var decoded1_v_inner = global::Dbus.Decoder.GetDictionary(decoded1_v_b, ref decoded1_v_i, global::Dbus.Decoder.GetString, global::Dbus.Decoder.GetObject);

                return decoded1_v_inner;
            });
            InterfacesAdded?.Invoke(decoded0, decoded1);
        }
        public event global::System.Action<global::Dbus.ObjectPath, global::System.Collections.Generic.IEnumerable<global::System.String>> InterfacesRemoved;
        private void handleInterfacesRemoved(global::Dbus.MessageHeader header, byte[] body)
        {
            header.BodySignature.AssertEqual("oas");
            var decoderIndex = 0;
            var decoded0 = global::Dbus.Decoder.GetObjectPath(body, ref decoderIndex);
            var decoded1 = global::Dbus.Decoder.GetArray(body, ref decoderIndex, global::Dbus.Decoder.GetString);
            InterfacesRemoved?.Invoke(decoded0, decoded1);
        }

        public void Dispose()
        {
            eventSubscriptions.ForEach(x => x.Dispose());
        }
    }

    public sealed class IOrgBluezLEAdvertisement1_Proxy : global::Dbus.IProxy
    {

        public string InterfaceName { get; }

        private readonly global::Dbus.Connection connection;
        private readonly global::Reproduction.IOrgBluezLEAdvertisement1 target;
        private readonly global::Dbus.ObjectPath path;

        private global::System.IDisposable registration;

        private IOrgBluezLEAdvertisement1_Proxy(global::Dbus.Connection connection, global::Reproduction.IOrgBluezLEAdvertisement1 target, global::Dbus.ObjectPath path)
        {
            this.connection = connection;
            this.target = target;
            this.path = path;
            InterfaceName = "org.bluez.LEAdvertisement1";
            registration = connection.RegisterObjectProxy(
                path ?? "",
                InterfaceName,
                this
            );

            target.PropertyChanged += HandlePropertyChangedEventAsync;
        }

        public static IOrgBluezLEAdvertisement1_Proxy Factory(global::Dbus.Connection connection, Reproduction.IOrgBluezLEAdvertisement1 target, global::Dbus.ObjectPath path)
        {
            return new IOrgBluezLEAdvertisement1_Proxy(connection, target, path);
        }
        private async void HandlePropertyChangedEventAsync(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var sendBody = global::Dbus.Encoder.StartNew();
            var sendIndex = 0;
            global::Dbus.Encoder.Add(sendBody, ref sendIndex, "org.bluez.LEAdvertisement1");
            global::Dbus.Encoder.AddArray(sendBody, ref sendIndex, (global::System.Collections.Generic.List<byte> sendBody_e, ref int sendIndex_e) =>
            {
                global::Dbus.Encoder.EnsureAlignment(sendBody_e, ref sendIndex_e, 8);
                global::Dbus.Encoder.Add(sendBody_e, ref sendIndex_e, e.PropertyName);
                switch (e.PropertyName)
                {
                    default:
                        throw new System.NotSupportedException("Property encoding not supported for the given property" + e.PropertyName);
                }
            }, true);
            global::Dbus.Encoder.Add(sendBody, ref sendIndex, 0);

            await connection.SendSignalAsync(
                path,
                "org.freedesktop.DBus.Properties",
                "PropertiesChanged",
                sendBody,
                "sa{sv}as"
            ).ConfigureAwait(false);
        }

        public void EncodeProperties(global::System.Collections.Generic.List<byte> sendBody, ref int sendIndex)
        {
            global::Dbus.Encoder.AddArray(sendBody, ref sendIndex, (global::System.Collections.Generic.List<byte> sendBody_e, ref int sendIndex_e) =>
            {
                global::Dbus.Encoder.EnsureAlignment(sendBody_e, ref sendIndex_e, 8);
                global::Dbus.Encoder.Add(sendBody_e, ref sendIndex_e, "Type");
                EncodeType(sendBody_e, ref sendIndex_e);
                global::Dbus.Encoder.EnsureAlignment(sendBody_e, ref sendIndex_e, 8);
                global::Dbus.Encoder.Add(sendBody_e, ref sendIndex_e, "ServiceUUIDs");
                EncodeServiceUUIDs(sendBody_e, ref sendIndex_e);
                global::Dbus.Encoder.EnsureAlignment(sendBody_e, ref sendIndex_e, 8);
                global::Dbus.Encoder.Add(sendBody_e, ref sendIndex_e, "SolicitUUIDs");
                EncodeSolicitUUIDs(sendBody_e, ref sendIndex_e);
                global::Dbus.Encoder.EnsureAlignment(sendBody_e, ref sendIndex_e, 8);
                global::Dbus.Encoder.Add(sendBody_e, ref sendIndex_e, "Path");
                EncodePath(sendBody_e, ref sendIndex_e);
                global::Dbus.Encoder.EnsureAlignment(sendBody_e, ref sendIndex_e, 8);
                global::Dbus.Encoder.Add(sendBody_e, ref sendIndex_e, "IncludeTxPower");
                EncodeIncludeTxPower(sendBody_e, ref sendIndex_e);
            }, true);
        }

        public void EncodeProperty(global::System.Collections.Generic.List<byte> sendBody, ref int sendIndex, string propertyName)
        {
            switch (propertyName)
            {
                case "Type":
                    EncodeType(sendBody, ref sendIndex);
                    break;
                case "ServiceUUIDs":
                    EncodeServiceUUIDs(sendBody, ref sendIndex);
                    break;
                case "SolicitUUIDs":
                    EncodeSolicitUUIDs(sendBody, ref sendIndex);
                    break;
                case "Path":
                    EncodePath(sendBody, ref sendIndex);
                    break;
                case "IncludeTxPower":
                    EncodeIncludeTxPower(sendBody, ref sendIndex);
                    break;
                default:
                    throw new global::Dbus.DbusException(
                        global::Dbus.DbusException.CreateErrorName("UnknownProperty"),
                        "No such Property: " + propertyName
                    );
            }
        }

        private void EncodeType(global::System.Collections.Generic.List<byte> sendBody, ref int sendIndex)
        {
            var value = target.Type;
            global::Dbus.Encoder.Add(sendBody, ref sendIndex, (global::Dbus.Signature)"s");
            global::Dbus.Encoder.Add(sendBody, ref sendIndex, value);

        }

        private void EncodeServiceUUIDs(global::System.Collections.Generic.List<byte> sendBody, ref int sendIndex)
        {
            var value = target.ServiceUUIDs;
            global::Dbus.Encoder.Add(sendBody, ref sendIndex, (global::Dbus.Signature)"as");
            global::Dbus.Encoder.Add(sendBody, ref sendIndex, value, global::Dbus.Encoder.Add);

        }

        private void EncodeSolicitUUIDs(global::System.Collections.Generic.List<byte> sendBody, ref int sendIndex)
        {
            var value = target.SolicitUUIDs;
            global::Dbus.Encoder.Add(sendBody, ref sendIndex, (global::Dbus.Signature)"as");
            global::Dbus.Encoder.Add(sendBody, ref sendIndex, value, global::Dbus.Encoder.Add);

        }

        private void EncodePath(global::System.Collections.Generic.List<byte> sendBody, ref int sendIndex)
        {
            var value = target.Path;
            global::Dbus.Encoder.Add(sendBody, ref sendIndex, (global::Dbus.Signature)"o");
            global::Dbus.Encoder.Add(sendBody, ref sendIndex, value);

        }

        private void EncodeIncludeTxPower(global::System.Collections.Generic.List<byte> sendBody, ref int sendIndex)
        {
            var value = target.IncludeTxPower;
            global::Dbus.Encoder.Add(sendBody, ref sendIndex, (global::Dbus.Signature)"b");
            global::Dbus.Encoder.Add(sendBody, ref sendIndex, value);

        }

        public System.Threading.Tasks.Task HandleMethodCallAsync(uint replySerial, global::Dbus.MessageHeader header, byte[] body, bool shouldSendReply)
        {
            switch (header.Member)
            {
                case "Release":
                    return handleReleaseAsync(replySerial, header, body, shouldSendReply);
                default:
                    throw new global::Dbus.DbusException(
                        global::Dbus.DbusException.CreateErrorName("UnknownMethod"),
                        "Method not supported"
                    );
            }
        }

        private async System.Threading.Tasks.Task handleReleaseAsync(uint replySerial, global::Dbus.MessageHeader header, byte[] receivedBody, bool shouldSendReply)
        {
            header.BodySignature.AssertEqual("");
            await target.ReleaseAsync().ConfigureAwait(false);

            if (!shouldSendReply)
                return;
            var sendBody = global::Dbus.Encoder.StartNew();
            await connection.SendMethodReturnAsync(replySerial, header.Sender, sendBody, "").ConfigureAwait(false);
        }


        public void Dispose()
        {
            registration.Dispose();
        }
    }

    public sealed class IOrgBluezLEAdvertisingManager1_Implementation : global::Reproduction.IOrgBluezLEAdvertisingManager1
    {
        private readonly global::Dbus.Connection connection;
        private readonly global::Dbus.ObjectPath path;
        private readonly string destination;
        private readonly global::System.Collections.Generic.List<System.IDisposable> eventSubscriptions = new global::System.Collections.Generic.List<System.IDisposable>();

        private IOrgBluezLEAdvertisingManager1_Implementation(global::Dbus.Connection connection, global::Dbus.ObjectPath path, string destination)
        {
            this.connection = connection;
            this.path = path ?? "";
            this.destination = destination ?? "org.bluez";

        }

        public static global::Reproduction.IOrgBluezLEAdvertisingManager1 Factory(global::Dbus.Connection connection, global::Dbus.ObjectPath path, string destination)
        {
            return new IOrgBluezLEAdvertisingManager1_Implementation(connection, path, destination);
        }


        public async global::System.Threading.Tasks.Task RegisterAdvertisementAsync(global::Dbus.ObjectPath advertisement, global::System.Collections.Generic.IDictionary<global::System.String, global::System.Object> options)
        {
            var sendBody = global::Dbus.Encoder.StartNew();
            var sendIndex = 0;
            global::Dbus.Encoder.Add(sendBody, ref sendIndex, advertisement);
            global::Dbus.Encoder.Add(sendBody, ref sendIndex, options, global::Dbus.Encoder.Add, global::Dbus.Encoder.AddVariant);

            var receivedMessage = await connection.SendMethodCall(
                this.path,
                "org.bluez.LEAdvertisingManager1",
                "RegisterAdvertisement",
                this.destination,
                sendBody,
                "oa{sv}"
            ).ConfigureAwait(false);
            receivedMessage.Signature.AssertEqual("");
            return;
        }

        public async global::System.Threading.Tasks.Task UnregisterAdvertisementAsync(global::Dbus.ObjectPath advertisement)
        {
            var sendBody = global::Dbus.Encoder.StartNew();
            var sendIndex = 0;
            global::Dbus.Encoder.Add(sendBody, ref sendIndex, advertisement);

            var receivedMessage = await connection.SendMethodCall(
                this.path,
                "org.bluez.LEAdvertisingManager1",
                "UnregisterAdvertisement",
                this.destination,
                sendBody,
                "o"
            ).ConfigureAwait(false);
            receivedMessage.Signature.AssertEqual("");
            return;
        }


        public void Dispose()
        {
            eventSubscriptions.ForEach(x => x.Dispose());
        }
    }

}