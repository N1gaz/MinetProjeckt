using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    interface IRateAndCopy
    {
        double rating { get; }
        object DeepCopy();
    }
}
