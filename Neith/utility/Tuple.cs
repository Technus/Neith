using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable IDE1006 // Naming Styles
namespace Neith.utility
{
    public abstract class Tuple1<X>
    {
        public X x { get; set; }
    }

    public abstract class Tuple2<X,Y> : Tuple1<X>
    {
        public Y y { get; set; }
    }

    public abstract class Tuple3<X,Y,Z> : Tuple2<X,Y>
    {
        public Z z { get; set; }
    }
}
#pragma warning restore IDE1006 // Naming Styles
