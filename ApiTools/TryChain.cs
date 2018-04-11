using System;
using System.Collections.Generic;

namespace ApiTools
{
    public class TryChain<T>
    {
        public T Result { get; set; }

        public bool IsSucessFull { get; set; }

        public TryChain(Func<T> f)
        {
            try
            {
                Result = f();
                IsSucessFull = true;
            }
            catch (Exception)
            {
            }
        }

        public TryChain<T> ThenTry(Func<T> f)
        {
            if (!IsSucessFull)
            {
                try
                {
                    Result = f();
                    IsSucessFull = true;
                }
                catch (Exception)
                {
                }
            }
            return this;
        }

        public TryChain<T> ThenTry(IEnumerable<Func<T>> fs)
        {
            if (!IsSucessFull)
            {
                foreach (var f in fs)
                {
                    try
                    {
                        Result = f();
                        IsSucessFull = true;
                        break;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return this;
        }
    }
}
