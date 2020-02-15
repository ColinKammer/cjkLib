using System;
using System.Collections.Generic;
using System.Text;
using TbsLib.outputGenerators;

namespace TbsLib.TbsTypes
{
    public class TbsBuildInType : ITbsType
    {
        readonly string Typename;
        readonly int Size;

        public TbsBuildInType(string typename, int size)
        {
            Typename = typename;
            Size = size;
        }

        public int PhysicalSize => Size;
        string ITbsParseType.Typename => Typename;

        public CompileLog GenerateDeclaration(StringBuilder codeBuilder, IOutputGenerator outputGenerator)
        {
            return new CompileLog(); //Should be Build in in every Language
        }

        public CompileLog GenerateDefinition(StringBuilder codeBuilder, IOutputGenerator outputGenerator)
        {
            return new CompileLog(); //Nothing to Define for Build-Ins (should be simply typedefed in generator)
        }

        public ITbsType Promote(IList<ITbsType> knownTypes, CompileLog log)
        {
            return this;
        }
    }

    public static class TbsBuildIn
    {
        public static IList<TbsBuildInType> Types
        {
            get
            {
                return new List<TbsBuildInType>()
                {
                    new TbsBuildInType("UINT8", 1),
                    new TbsBuildInType("INT8", 1),
                    new TbsBuildInType("UINT16", 2),
                    new TbsBuildInType("INT16", 2),
                    new TbsBuildInType("UINT32", 4),
                    new TbsBuildInType("INT32", 4),
                    new TbsBuildInType("UINT64", 8),
                    new TbsBuildInType("INT64", 8),
                };
            }
        }



    }
}
