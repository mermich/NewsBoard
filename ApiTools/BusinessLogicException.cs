using System;
using System.Collections.Generic;

namespace ApiTools
{
    public class BusinessLogicException : Exception
    {
        public List<string> Errors { get; set; } = new List<string>();

        public BusinessLogicException() { }

        public BusinessLogicException(string message)
        {
            Errors.Add(message);
        }

        public void AddError(string message)
        {
            if (!Errors.Contains(message))
                Errors.Add(message);
        }


        public override string Message
        {
            get
            {
                return string.Join("", Errors);
            }
        }
    }
}
