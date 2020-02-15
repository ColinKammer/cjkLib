using csCjkLib;
using System;
using System.IO;
using TbsLib;
using TbsLib.outputGenerators;

namespace TbsCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            CmdLineArgs cmdLineArgs = new CmdLineArgs(args);

            string srcPath = cmdLineArgs.GetFirstSwitchArgument(""); //direct after call
            if (srcPath == null)
            {
                Console.WriteLine("ERROR: No Input File (Tbs-Definition was given");
                DumpHelp();
                return;
            }

            Console.WriteLine($"Reading Tbs-Definition in File {srcPath}...");
            var srcCode = File.ReadAllText(srcPath);

            Console.WriteLine("Parsing Tbs-Definition...");
            var parseResult = new ParseResult(srcCode);

            Console.WriteLine("Analsing Tbs-Definition...");
            var analysisResult = new AnalysisResult(parseResult);

            Console.WriteLine(analysisResult.AnalysisLog.ToString());

            if(analysisResult.AnalysisLog.ContainsErrors)
            {
                Console.WriteLine("ERROR: Errors Occured during Tbs Analysis - aborting");
                return;
            }

            Console.WriteLine("Generating Output...");

            if (cmdLineArgs.HasSwitch("cpp"))
            {
                string nmespce = cmdLineArgs.GetFirstSwitchArgument("namespace", "TinybuffersGenerated");

                IOutputGenerator generator = new CppGenerator(nmespce);

                string outputPath = GetDefaultPath(srcPath, generator);
                outputPath = cmdLineArgs.GetFirstSwitchArgument("cpp", outputPath);

                GenerateAndSave(generator, outputPath, analysisResult);
            }

            if (cmdLineArgs.HasSwitch("cs"))
            {
                string nmespce = cmdLineArgs.GetFirstSwitchArgument("namespace", "TinybuffersGenerated");
                bool isPublic = cmdLineArgs.HasSwitch("public");

                IOutputGenerator generator = new CsGenerator(nmespce, isPublic);

                string outputPath = GetDefaultPath(srcPath, generator);
                outputPath = cmdLineArgs.GetFirstSwitchArgument("cs", outputPath);

                GenerateAndSave(generator, outputPath, analysisResult);
            }

            Console.WriteLine("DONE");
        }

        static string GetDefaultPath(string inputPath, IOutputGenerator generator)
        {
            var dotPosition = inputPath.LastIndexOf('.');
            if(dotPosition < 0)
            {
                return inputPath + generator.DefaultExtension;
            }
            var basePath = inputPath.Substring(0, dotPosition);
            return basePath + generator.DefaultExtension;
        }

        static void GenerateAndSave(IOutputGenerator generator, string outputPath, AnalysisResult analysisResult)
        {
            var outputCode = generator.generateCode(analysisResult);
            File.WriteAllText(outputPath, outputCode);
        }

        static void DumpHelp()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("TbsCompiler.exe <input>.tbs [-cpp [<out.cpp>]] [-cs [<out.cs>]");
        }
    }
}
