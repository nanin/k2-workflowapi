using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DianPing.WorkFlow.Infrastructure.K2Exception
{
    [Serializable]
    public class ProcessNotExistException : Exception, ISerializable
    {
        public ProcessNotExistException()
            : base()
        {
        }
        public ProcessNotExistException(string message)
            : base(message)
        {
        }
        public ProcessNotExistException(string message, Exception innerException)
            : base(message, innerException)
        { }

    }
}
