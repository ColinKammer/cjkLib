using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TbsLib.outputGenerators;

namespace TbsLib
{
    public class TbsParseStructMember
    {
        public TbsParseStructMember(string type, string identifier, int arraySize = 1)
        {
            Typename = type;
            Identifier = identifier;
            ArraySize = arraySize;
        }

        public readonly string Typename;
        public readonly string Identifier;
        public readonly int ArraySize;

        public bool IsScalar
        {
            get
            {
                return ArraySize == 1;
            }
        }

        public TbsStructMember Promote(in IList<ITbsType> knownTypes, int localOffset, CompileLog log = null)
        {
            string requiredTypename = Typename;
            var fittingTypename = knownTypes.Where(x => x.Typename == requiredTypename);

            if (fittingTypename.Count() == 0)
            {
                log?.Append(new CompileMessage(CompileMessage.Serverity.Error, $"Unkown Type {requiredTypename}"));
                return null;
            }
            if (fittingTypename.Count() > 1)
            {
                log?.Append(new CompileMessage(CompileMessage.Serverity.Error, $"Multiple Definitions for Type {requiredTypename}"));
                return null;
            }

            return new TbsStructMember(fittingTypename.First(), Identifier, localOffset, ArraySize);
        }
    }

    public class TbsStructMember : TbsParseStructMember
    {
        public TbsStructMember(ITbsType type, string identifier, int localOffset, int arraySize = 1) : base(type.Typename, identifier, arraySize)
        {
            Type = type;
            LocalOffset = localOffset;
        }

        public readonly ITbsType Type;
        public readonly int LocalOffset;

        public int PhysicalSize
        {
            get
            {
                return ArraySize * Type.PhysicalSize;
            }
        }
    }


    public class TbsParseStruct : ITbsParseType
    {
        public readonly IList<TbsParseStructMember> ParseMembers = new List<TbsParseStructMember>();
        public readonly string Typename;

        public TbsParseStruct(in string typename, in IList<TbsParseStructMember> parseMembers)
        {
            Typename = typename;
            ParseMembers = parseMembers;
        }

        public TbsParseStruct(in string typename, in IList<TbsStructMember> members)
        {
            Typename = typename;
            ParseMembers = new List<TbsParseStructMember>(members.Count());

            foreach (var member in members)
            {
                ParseMembers.Add(member);
            }
        }

        public TbsParseStruct(AntlrTbsParser.TbsDefinitionParser.StructdefContext definitionContext)
        {
            Typename = definitionContext.TYPENAME().GetText();

            foreach (var member in definitionContext.structmember())
            {
                var scalarMember = member as AntlrTbsParser.TbsDefinitionParser.StructMemberScalarContext;
                var arrayMember = member as AntlrTbsParser.TbsDefinitionParser.StructMemberArrayContext;

                if (scalarMember != null)
                {
                    var type = scalarMember.TYPENAME().GetText();
                    var id = scalarMember.IDENTIFIER().GetText();
                    ParseMembers.Add(new TbsParseStructMember(type, id));
                }
                else if (arrayMember != null)
                {
                    var type = arrayMember.TYPENAME().GetText();
                    var id = arrayMember.IDENTIFIER().GetText();
                    var sizeStr = arrayMember.POSNUMBER().GetText();
                    int size = int.Parse(sizeStr);
                    ParseMembers.Add(new TbsParseStructMember(type, id, size));
                }
                else
                {
                    throw new NotImplementedException("Grammar got extended withut adding Code");
                }
            }
        }

        string ITbsParseType.Typename => Typename;

        public ITbsType Promote(IList<ITbsType> knownTypes, CompileLog log)
        {
            int currentOffset = 0;

            IList<TbsStructMember> members = new List<TbsStructMember>(ParseMembers.Count());
            bool errorOcurred = false;

            foreach (var parseMember in ParseMembers)
            {
                var member = parseMember.Promote(knownTypes, currentOffset, log);
                if (member == null)
                {
                    log?.Append(new CompileMessage(CompileMessage.Serverity.Error, $"Error Analysing Struct {Typename}"));
                    errorOcurred = true;
                    continue;
                }

                currentOffset += member.PhysicalSize;
                members.Add(member);
            }

            if (errorOcurred)
            {
                return null;
            }

            return new TbsStruct(Typename, members);
        }
    }

    public class TbsStruct : TbsParseStruct, ITbsType
    {
        public readonly IList<TbsStructMember> Members = new List<TbsStructMember>();

        public TbsStruct(in string typename, in IList<TbsStructMember> members) : base(typename, members)
        {
            Members = members;
        }

        public int PhysicalSize => Members.Last().LocalOffset + Members.Last().PhysicalSize;


        public CompileLog GenerateDeclaration(StringBuilder codeBuilder, IOutputGenerator outputGenerator)
        {
            return outputGenerator.generateStructDeclaration(codeBuilder, this);
        }

        public CompileLog GenerateDefinition(StringBuilder codeBuilder, IOutputGenerator outputGenerator)
        {
            return outputGenerator.generateStructDefinition(codeBuilder, this);
        }
    }
}
