using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    using System;

    public static class ChainMethoads
    {
        public static IChain<T> Wrap<T>(this T a)
        {
            return new Chain<T>(a);
        }

        public interface IChain<T>
        {
            IChain<U> Calc<U>(Func<T, U> f);
            IChain<T> Action(Action<T> f);
            IChain<T> Action<U>(Func<T, U> f);

            IChain<T> Repeat(int n, Func<T, T> f);
            // T Tear();
        }

        private class Chain<T> : IChain<T>
        {
            private T a;

            public Chain(T a)
            {
                this.a = a;
            }

            public IChain<U> Calc<U>(Func<T, U> f)
            {
                return f(this.a).Wrap();
            }

            public IChain<T> Action(Action<T> f)
            {
                f(this.a);
                return this;
            }

            public IChain<T> Action<U>(Func<T, U> f)
            {
                f(this.a);
                return this;
            }

            public IChain<T> Repeat(int n, Func<T, T> f)
            {
                return n > 0 ? this.Calc(f).Repeat(n - 1, f) : this;
            }

            public T Tear()
            {
                return this.a;
            }
        }
    }
}