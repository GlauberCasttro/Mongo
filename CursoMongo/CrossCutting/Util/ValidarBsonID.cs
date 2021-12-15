using System.Text.RegularExpressions;

namespace CrossCutting.Util
{
    public static class ValidarBsonID
    {
        public static bool ValidarBSON(this string bson)
        {
            var pattern = new Regex("^[0-9a-fA-F]{24}$", RegexOptions.IgnoreCase);

            return pattern.IsMatch(bson);
        }
    }
}