using DryIoc;

namespace XamAR.Diagnostics
{
    public static class Diagnostics
    {
        public static IContainer AddDiagnostics(this IContainer container)
        {
            container.Register<EntityUpdateServiceSkipFrames>(Reuse.Singleton);

            return container;
        }
    }
}
