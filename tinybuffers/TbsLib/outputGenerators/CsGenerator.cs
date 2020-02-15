using System;
using System.Collections.Generic;
using System.Text;
using Tinybuffers;

namespace TbsLib.outputGenerators
{
    public class CsGenerator : IOutputGenerator
    {
        readonly string CsNamespace;
        readonly bool CsPublicDatastructure;

        public CsGenerator(string csNamespace, bool csPublicDatastructure)
        {
            CsNamespace = csNamespace;
            CsPublicDatastructure = csPublicDatastructure;
        }

        public string DefaultExtension => ".cs";

        public string generateCode(AnalysisResult from)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("using System;");
            builder.AppendLine("using Tinybuffers;");
            builder.AppendLine();

            if (CsNamespace != null) builder.AppendLine($"namespace {CsNamespace} {{");

            TypedefBuildInDatatypes(builder);

            foreach (var type in from.AnalysedTypes)
            {
                type.GenerateDeclaration(builder, this);
            }

            builder.AppendLine();

            foreach (var type in from.AnalysedTypes)
            {
                type.GenerateDefinition(builder, this);
            }

            if (CsNamespace != null) builder.AppendLine($"}}");
            return builder.ToString();
        }

        public CompileLog generateEnumDeclration(StringBuilder codeBuilder, TbsEnum tbs)
        {
            return new CompileLog(); //C# doesnt require forward Declarations
        }

        public CompileLog generateEnumDefinition(StringBuilder codeBuilder, TbsEnum tbs)
        {
            var log = new CompileLog();
            if (CsPublicDatastructure) codeBuilder.Append("public ");
            codeBuilder.AppendLine($"enum {tbs.Typename} : UINT8 {{");
            foreach (var option in tbs.Options)
            {
                codeBuilder.AppendLine($"  {option},"); //ToDo add manual NumericAssociation (=3)
            }
            codeBuilder.AppendLine($"}};\n");
            return log;
        }

        public CompileLog generateStructDeclaration(StringBuilder codeBuilder, TbsStruct tbs)
        {
            return new CompileLog(); //C# doesnt require forward Declarations
        }

        public CompileLog generateStructDefinition(StringBuilder codeBuilder, TbsStruct tbs)
        {
            var log = new CompileLog();
            if (CsPublicDatastructure) codeBuilder.Append("public ");
            codeBuilder.AppendLine($"class {tbs.Typename} : Tinybuffers.IBufferable {{");
            codeBuilder.AppendLine($"  public int Size => {tbs.PhysicalSize};");
            codeBuilder.AppendLine("");

            //Members
            foreach (var member in tbs.Members)
            {

                if (member.IsScalar)
                {
                    codeBuilder.AppendLine($"  public {member.Typename} {member.Identifier}; //@Offset {member.LocalOffset}");
                }
                else
                {
                    codeBuilder.AppendLine($"  public {member.Typename}[] {member.Identifier} = new {member.Typename}[{member.ArraySize}]; //@Offset {member.LocalOffset}");
                }
            }
            codeBuilder.AppendLine("");

            //BuildBuffer Method
            codeBuilder.AppendLine($"public void BuildBuffer(byte[] buffer, int atOffset = 0) {{");
            foreach (var member in tbs.Members)
            {
                string additionalCast = (member.Type is TbsEnum) ? "(Byte)" : "";
                codeBuilder.AppendLine($"  buffer.SetPartialBuffer({additionalCast}this.{member.Identifier}, atOffset + {member.LocalOffset});");
            }
            codeBuilder.AppendLine($"}}");

            //UnpackBuffer Method
            codeBuilder.AppendLine($"public void Unbuffer(in byte[] buffer, int atOffset = 0) {{");
            foreach (var member in tbs.Members)
            {
                if (member.Type is TbsEnum)
                {
                    codeBuilder.AppendLine($"  Byte _{member.Identifier} = (Byte)this.{member.Identifier};");
                    codeBuilder.AppendLine($"  buffer.ReadInto(ref _{member.Identifier}, atOffset + {member.LocalOffset});");
                    codeBuilder.AppendLine($"  this.{member.Identifier} = ({member.Typename})_{member.Identifier};");
                }
                else
                {
                    codeBuilder.AppendLine($"  buffer.ReadInto(ref this.{member.Identifier}, atOffset + {member.LocalOffset});");
                }
            }
            codeBuilder.AppendLine($"}}");

            //Conviniece Methods
            codeBuilder.AppendLine($"public {tbs.Typename}(in byte[] buffer) {{");
            codeBuilder.AppendLine($"  Unbuffer(buffer);");
            codeBuilder.AppendLine($"}}");

            codeBuilder.AppendLine($"public byte[] BuildBuffer() {{");
            codeBuilder.AppendLine($"  byte[] outputBuffer = new byte[Size];");
            codeBuilder.AppendLine($"  BuildBuffer(outputBuffer);");
            codeBuilder.AppendLine($"  return outputBuffer;");
            codeBuilder.AppendLine($"}}");


            codeBuilder.AppendLine($"}};\n");
            return log;
        }

        public void TypedefBuildInDatatypes(StringBuilder codeBuilder)
        {
            codeBuilder.AppendLine("using UINT8 = Byte;");
            codeBuilder.AppendLine("using INT8 = SByte;");
            codeBuilder.AppendLine("using UINT16 = UInt16;");
            codeBuilder.AppendLine("using INT16 = Int16;");
            codeBuilder.AppendLine("using UINT32 = UInt32;");
            codeBuilder.AppendLine("using INT32 = Int32;");
            codeBuilder.AppendLine("using UINT64 = UInt32;");
            codeBuilder.AppendLine("using INT64 = Int32;");
        }
    }
}
