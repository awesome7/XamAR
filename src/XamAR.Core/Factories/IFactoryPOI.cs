using System.Collections.Generic;
using XamAR.Core.Models;

namespace XamAR.Core.Factories
{
    public interface IFactoryPOI
    {
        IEnumerable<Drawable> Create(string text = null);
    }
}
