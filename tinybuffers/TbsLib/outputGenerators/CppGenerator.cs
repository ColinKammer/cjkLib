using System;
using System.Collections.Generic;
using System.Text;

namespace TbsLib.outputGenerators
{
    public readonly struct CppGenerator : IOutputGenerator
    {
        readonly string CppNamespace;

        public CppGenerator(string cppNamespace)
        {
            CppNamespace = cppNamespace;
        }

        public string DefaultExtension => ".h";

        public string generateCode(AnalysisResult from)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("#include <stdint.h>\n");

            if (CppNamespace != null) builder.AppendLine($"namespace {CppNamespace} {{");

            TypedefBuildInDatatypes(builder);

            foreach(var type in from.AnalysedTypes)
            {
                type.GenerateDeclaration(builder, this);
            }

            builder.AppendLine();

            foreach (var type in from.AnalysedTypes)
            {
                type.GenerateDefinition(builder, this);
            }

            if (CppNamespace != null) builder.AppendLine($"}}");
            return builder.ToString();
        }

        public CompileLog generateEnumDeclration(StringBuilder codeBuilder, TbsEnum tbs)
        {
            var log = new CompileLog();
            codeBuilder.AppendLine($"enum {tbs.Typename};");
            return log;
        }

        public CompileLog generateEnumDefinition(StringBuilder codeBuilder, TbsEnum tbs)
        {
            var log = new CompileLog();
            codeBuilder.AppendLine($"enum {tbs.Typename} {{");
            foreach(var option in tbs.Options)
            {
                codeBuilder.AppendLine($"{option},");
            }
            codeBuilder.AppendLine($"}};\n");
            return log;
        }

        public CompileLog generateStructDeclaration(StringBuilder codeBuilder, TbsStruct tbs)
        {
            var log = new CompileLog();
            codeBuilder.AppendLine($"struct {tbs.Typename};");
            return log;
        }

        public CompileLog generateStructDefinition(StringBuilder codeBuilder, TbsStruct tbs)
        {
            var log = new CompileLog();
            codeBuilder.AppendLine($"struct {tbs.Typename} {{ //Size={tbs.PhysicalSize}");
            foreach (var member in tbs.Members)
            {
                if(member.IsScalar)
                {
                    codeBuilder.AppendLine($"{member.Typename} {member.Identifier};//@Offset {member.LocalOffset}");
                }
                else
                {
                    codeBuilder.AppendLine($"{member.Typename} {member.Identifier}[{member.ArraySize}];//ArrSize={member.ArraySize} @Offset {member.LocalOffset}");
                }

            }
            codeBuilder.AppendLine($"}};\n");
            return log;
        }

        public void TypedefBuildInDatatypes(StringBuilder codeBuilder)
        {
            codeBuilder.AppendLine("typedef uint8_t UINT8;");
            codeBuilder.AppendLine("typedef int8_t INT8;");
            codeBuilder.AppendLine("typedef uint16_t UINT16;");
            codeBuilder.AppendLine("typedef int16_t INT16;");
            codeBuilder.AppendLine("typedef uint32_t UINT32;");
            codeBuilder.AppendLine("typedef int32_t INT32;");
            codeBuilder.AppendLine("typedef uint64_t UINT64;");
            codeBuilder.AppendLine("typedef int64_t INT64;");
            codeBuilder.AppendLine();
        }
    }
}
