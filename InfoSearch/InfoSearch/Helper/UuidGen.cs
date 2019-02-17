using Chilkat;

namespace InfoSearch.Helper
{
    public class UuidGen
    {
        public static string Gen() => new Crypt2().GenerateUuid();
    }
}
