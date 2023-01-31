using LanguageExt.Common;

namespace DomainDrivenDesign
{
    public static class Extensions
    {
        public static Exception GetException<T>(this Result<T> value)
        {
            return value.Match(x => null, x => x);
        }

        public static T GetResult<T>(this Result<T> value)
        {
            return value.Match(x => x, x => default(T));
        }
    }
}
