using DryIoc;

namespace XamAR.Core
{
    public static class DI
    {
        public static IContainer Container { get; } = new Container();

        public static IContainer AddCore(this IContainer container)
        {
            container.Register<ObjectManagerService>(Reuse.Singleton);

            return container;
        }
    }
}
