using System;

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
    }
}
