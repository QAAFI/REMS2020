using System;

namespace Rems.Application.Common
{
    public class Args<T> : EventArgs
    {
        public T Item { get; set; }
    }

    public class Args<T1, T2> : EventArgs
    {
        public T1 Item1 { get; set; }

        public T2 Item2 { get; set; }
    }

    public class RequestArgs<T, TResult> : EventArgs
    {
        public T Item { get; set; }

        public TResult Result { get; set; }
    }

    public class RequestArgs<T1, T2, TResult> : EventArgs
    {
        public T1 Item1 { get; set; }

        public T2 Item2 { get; set; }

        public TResult Result { get; set; }
    }
}
