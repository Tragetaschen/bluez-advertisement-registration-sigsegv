using Dbus;

namespace Reproduction
{
    public class ObjectManager : OrgFreedesktopDbusObjectManager
    {
        public ObjectManager(Connection connection, ObjectPath root)
            : base(connection, root)
        { }
    }
}
