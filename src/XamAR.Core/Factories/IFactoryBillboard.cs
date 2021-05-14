using System.Collections.Generic;
using XamAR.Core.Models;

namespace XamAR.Core.Factories
{
    public interface IFactoryBillboard
    {
        //NOTE Need to pass something to display on billboard.
        IEnumerable<Drawable> Create();
    }
}
