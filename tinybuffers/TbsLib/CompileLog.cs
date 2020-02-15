using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TbsLib
{
    public class CompileLog : IEnumerable<CompileMessage>
    {
        IList<CompileMessage> Entries = new List<CompileMessage>();

        public bool ContainsErrors
        {
            get
            {
                return Entries.Any(x => x.MServerity == CompileMessage.Serverity.Error);
            }
        }
        public bool ContainsWarnings
        {
            get
            {
                return Entries.Any(x => x.MServerity == CompileMessage.Serverity.Warning);
            }
        }

        public void Append(CompileMessage message)
        {
            Entries.Add(message);
        }

        public void Append(CompileLog log)
        {
            foreach (var message in log)
            {
                Append(message);
            }
        }

        public override string ToString()
        {
            string retVal = "";
            foreach (var message in this)
            {
                retVal += message.ToString() + '\n';
            }

            return retVal;
        }

        public IEnumerator<CompileMessage> GetEnumerator()
        {
            return Entries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Entries.GetEnumerator();
        }
    }
}
