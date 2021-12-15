using System;

namespace Domain.Util
{
    public static class EnumValidation
    {
        public static bool IsEnum<T>(T @enum) => (Enum.IsDefined(typeof(T), @enum));
    }
}