using System;
using System.Collections.Generic;
using System.Text;
using TbsLib.outputGenerators;

namespace TbsLib
{
    public interface ITbsParseType
    {
        string Typename { get; }

        ITbsType Promote(IList<ITbsType> knownTypes, CompileLog log);

    }
}
