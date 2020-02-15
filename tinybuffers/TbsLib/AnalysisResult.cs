using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TbsLib.TbsTypes;

namespace TbsLib
{
    public class AnalysisResult : ParseResult
    {
        public readonly IList<ITbsType> AnalysedTypes;
        public readonly CompileLog AnalysisLog;

        public AnalysisResult(ParseResult parseResult) : base(parseResult)
        {
            AnalysisLog = new CompileLog();
            AnalysedTypes = AnalyseParseResult(parseResult,AnalysisLog);
        }


        public IList<ITbsType> AnalyseParseResult(ParseResult parseResult, CompileLog log)
        {
            List<ITbsType> promotedTypes = new List<ITbsType>(ParsedTypes.Count() + TbsBuildIn.Types.Count());
            promotedTypes.AddRange(TbsBuildIn.Types);

            IList<ITbsParseType> typesToProcess = parseResult.ParsedTypes.ToList();


            while (true)
            {
                int noOfProcessedInItteration = 0;

                for (int i = 0; i < typesToProcess.Count(); i++)
                {
                    var parsedType = typesToProcess[i];
                    var tmpLog = new CompileLog();
                    var promotedType = parsedType.Promote(promotedTypes, tmpLog);//May not log to Output because a fail doesnt mean it wont be possible later
                    if (promotedType != null)
                    {
                        typesToProcess.RemoveAt(i);
                        promotedTypes.Add(promotedType);
                        log.Append(tmpLog);
                        noOfProcessedInItteration++;
                    }
                }

                if (noOfProcessedInItteration == 0)
                {
                    //Nothing can be done anymore (either because every thing is done or because there is a circle dependency in the tbs)
                    break;
                }
            }

            if (typesToProcess.Count() > 0)
            {
                log?.Append(new CompileMessage(CompileMessage.Serverity.Error, "There is a Circular Dependency in the given tbs-File"));
            }

            promotedTypes.RemoveRange(0, TbsBuildIn.Types.Count()); // Remove BuildIns (they are not parsed)

            return promotedTypes;
        }
    }
}
