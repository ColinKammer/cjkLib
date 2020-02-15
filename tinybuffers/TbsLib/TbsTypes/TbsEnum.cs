using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AntlrTbsParser;
using TbsLib.outputGenerators;

namespace TbsLib
{
    public class TbsParseEnum : ITbsParseType
    {
        public readonly IList<string> Options = new List<string>();
        public readonly string Typename;

        public TbsParseEnum(in string typename, in IList<string> options)
        {
            Typename = typename;
            Options = options;
        }

        public TbsParseEnum(TbsDefinitionParser.EnumdefContext definitionContext)
        {
            Typename = definitionContext.TYPENAME().GetText();

            foreach (var option in definitionContext.enumoption())
            {
                var optName = option.IDENTIFIER().GetText();
                Options.Add(optName);
            }
        }

        string ITbsParseType.Typename => Typename;

        public ITbsType Promote(IList<ITbsType> knownTypes, CompileLog log)
        {
            if (Options.Count() > Byte.MaxValue)
            {
                log?.Append(new CompileMessage(CompileMessage.Serverity.Error, $"Enum {Typename} uses more options than can represented by the underlaying Datatype"));
            }

            return new TbsEnum(Typename, Options);
        }
    }


    public class TbsEnum : TbsParseEnum, ITbsType
    {
        public TbsEnum(in string typename, in IList<string> options) : base(typename, options)
        {
        }

        public int PhysicalSize => 1; //Enums allways use 1 byte (aka 256 possible options)

        public CompileLog GenerateDeclaration(StringBuilder codeBuilder, IOutputGenerator outputGenerator)
        {
            return outputGenerator.generateEnumDeclration(codeBuilder, this);
        }

        public CompileLog GenerateDefinition(StringBuilder codeBuilder, IOutputGenerator outputGenerator)
        {
            return outputGenerator.generateEnumDefinition(codeBuilder, this);
        }

    }
}
