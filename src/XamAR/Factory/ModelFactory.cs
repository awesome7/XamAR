using XamAR.Core.Models;

namespace XamAR.Factory
{
    /// <summary>
    /// Defines interface used for registering user-specific model creators with parameters.
    /// </summary>
    public abstract class ModelFactory<TParam> : ModelFactory
    {
        /// <summary>
        /// Create platform-specific model with parameters, and wrap it into ModelWrapper.
        /// </summary>
        /// <remarks>Use AsModelWrapper extension method</remarks>
        public abstract ARModel CreateModel(TParam param);

        public override ARModel CreateModel()
        {
            return CreateModel(default);
        }
    }

    /// <summary>
    /// Defines interface used for registering user-specific model creators.
    /// </summary>
    public abstract class ModelFactory
    {
        /// <summary>
        /// Create platform-specific model, and wrap it into ModelWrapper.
        /// </summary>
        /// <remarks>Use AsModelWrapper extension method</remarks>
        public abstract ARModel CreateModel();
    }
}
