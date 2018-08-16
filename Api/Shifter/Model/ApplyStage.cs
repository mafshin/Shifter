using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shifter.Model
{
    [Flags]
    public enum ApplyStage
    {
        Preprocess = 0 ,
        WithinProcess = 1,
        Postprocess = 2
    }
}
