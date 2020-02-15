using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TbsLib.TbsTypes;

namespace TbsLib
{
    public class ParseResult
    {
        public readonly IList<ITbsParseType> ParsedTypes;

        public ParseResult(IList<ITbsParseType> parsedTypes)
        {
            ParsedTypes = parsedTypes;
        }

        public ParseResult(ParseResult parseResult)
        {
            ParsedTypes = parseResult.ParsedTypes;
        }

        public ParseResult(string srcCode)
        {
            ParsedTypes = ParseString(srcCode).ParsedTypes;
        }

        public static ParseResult ParseString(string code)
        {
            AntlrInputStream inputStream = new AntlrInputStream(code);
            var lexer = new AntlrTbsParser.TbsDefinitionLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
            var parser = new AntlrTbsParser.TbsDefinitionParser(commonTokenStream);
            var root = parser.file();

            IList<ITbsParseType> definedTypes = new List<ITbsParseType>();

            foreach (var definition in root.definition())
            {
                var enumDefinition = definition as AntlrTbsParser.TbsDefinitionParser.EnumDefinitionContext;
                var structDefinition = definition as AntlrTbsParser.TbsDefinitionParser.StructDefinitionContext;

                if (enumDefinition != null) definedTypes.Add(new TbsParseEnum(enumDefinition.enumdef()));
                else if (structDefinition != null) definedTypes.Add(new TbsParseStruct(structDefinition.structdef()));
                else throw new NotImplementedException("Grammar got extended withut adding Code");
            }

            return new ParseResult(definedTypes);
        }
    }
}
