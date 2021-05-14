using DryIoc;
using XamAR.Exceptions;

namespace XamAR.Factory
{
    public class FactoryService
    {
        private static readonly Container s_container = new Container();

        /// <summary>
        ///     Registers factory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// 
        public static void RegisterFactory<TFactory, TParam>(string name) where TFactory : ModelFactory<TParam>
        {
            s_container.Register<ModelFactory<TParam>, TFactory>(serviceKey: name);
        }

        /// <summary>
        ///     Registers factory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// 
        public static void RegisterFactory<TFactory>(string name) where TFactory : ModelFactory
        {
            s_container.Register<ModelFactory, TFactory>(serviceKey: name);
        }

        internal static ModelFactory GetInstance(string identifier)
        {
            ModelFactory factory = s_container.Resolve<ModelFactory>(identifier, IfUnresolved.ReturnDefaultIfNotRegistered);

            if (factory == null)
            {
                string msg = $"Factory with name \"{identifier}\" has not been registered yet!";
                throw new FactoryNotRegisteredException(msg);
            }

            return factory;
        }
    }
}
