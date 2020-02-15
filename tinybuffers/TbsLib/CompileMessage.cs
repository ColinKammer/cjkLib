using System;
using System.Collections.Generic;
using System.Text;

namespace TbsLib
{
    public readonly struct CompileMessage
    {
        public enum Serverity
        {
            Log,
            Warning,
            Error,
        }

        public readonly string RawMessage;
        public readonly Serverity MServerity;
        public readonly string Detail;

        public CompileMessage(Serverity serverity, string message)
        {
            MServerity = serverity;
            RawMessage = message;
            Detail = "";
        }
        public CompileMessage(Serverity serverity, string message, string datails)
        {
            MServerity = serverity;
            RawMessage = message;
            Detail = datails;
        }

        public override string ToString()
        {
            return MServerity.ToString() + ": " + RawMessage;
        }

        public string ToStringDetailed()
        {
            return MServerity.ToString() + ": " + RawMessage + " - " + Detail;
        }
    }
}
