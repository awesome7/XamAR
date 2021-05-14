using System;
using XamAR.Core.Models;

namespace XamAR.Core.Factories
{
    /// <summary>
    /// For raw objects that user passes to display.
    /// </summary>
    public interface IFactoryWrapper
    {
        /// <summary>
        /// Creates drawable for provided object, with
        /// default values extracted from model.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">
        /// When object is not allowed on platform.</exception>
        /// <exception cref="ArgumentNullException">
        /// Argument model is null.</exception>
        Drawable Create(object model);
    }
}
