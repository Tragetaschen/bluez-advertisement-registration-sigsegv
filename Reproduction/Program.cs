using Dbus;
using Dbus.CodeGenerator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Reproduction
{
    public static class Program
    {
        private const string adapterPath = "/org/bluez/hci0";
        private const string publishPath = "/com/example/advertisement";

        public static void Main(string[] args)
        {
            if (args.Length == 1 && args[0] == "gen")
            {
                var contents = Generator.Run();
                File.WriteAllText("DbusImplementation.Generated.cs", @"namespace Reproduction
{
" + contents + @"
}");
                return;
            }

            DbusImplementations.Init();
            work().GetAwaiter().GetResult();
        }

        private static async Task work()
        {
            using (var connection = await Connection.CreateAsync(new DbusConnectionOptions
            {
                Address = Connection.SystemBusAddress
            }))
            using (var objectManager = new ObjectManager(connection, publishPath))
            using (var advertisement = new MyAdvertisement(objectManager))
            using (var advertisementManager = connection.Consume<IOrgBluezLEAdvertisingManager1>(adapterPath))
            {
                var runningAdvertisementTask = advertisementManager.RegisterAdvertisementAsync(
                    objectManager.Root,
                    new Dictionary<string, object>()
                );

                try
                {
                    await runningAdvertisementTask;
                    Console.WriteLine("Success");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed: " + e.Message);
                    Console.WriteLine("The Bluez daemon should have died");
                }

                Console.WriteLine("Done, press enter to end");
                Console.ReadLine();
            }
        }
    }
}
