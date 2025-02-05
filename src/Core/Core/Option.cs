using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core;
public sealed record Option<T>(bool IsSome, T Value)
{
    public bool IsNone => !IsSome;

    public static Option<T> Some(T value) => new Option<T>(true, value);

    public static Option<T> None() => new Option<T>(false, default!);

    public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none) =>
        IsSome ? some(Value) : none();

    public void Match(Action<T> some, Action none)
    {
        if (IsSome)
        {
            some(Value);
        }
        else
        {
            none();
        }
    }
}
