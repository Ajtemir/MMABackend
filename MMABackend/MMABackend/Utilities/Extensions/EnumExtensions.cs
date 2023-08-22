using System;

namespace MMABackend.Utilities.Extensions
{
    public static partial class EnumExtensions
    {
        public static int ToInt(this Enum en) => Convert.ToInt32(en);
    }
}