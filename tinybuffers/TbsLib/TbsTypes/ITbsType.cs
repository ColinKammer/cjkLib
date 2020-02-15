using System;
using System.Collections.Generic;
using System.Text;
using TbsLib.outputGenerators;

namespace TbsLib
{
    public interface ITbsType : ITbsParseType
    {
        int PhysicalSize { get; }


        CompileLog GenerateDeclaration(StringBuilder codeBuilder, IOutputGenerator outputGenerator);
        CompileLog GenerateDefinition(StringBuilder codeBuilder, IOutputGenerator outputGenerator);

    }
}
