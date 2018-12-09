using System;
using System.Runtime.Serialization;

namespace AngularDemo.Utility
{
    [Serializable]
    public class CustomException : ApplicationException
    {
        public CustomException() { }

        public CustomException(string message) : base(message) { }

        public CustomException(string message, Exception inner) : base(message, inner) { }

        protected CustomException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}