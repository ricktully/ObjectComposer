using System;

namespace Rtully.LinkedIn.Articles.ObjectComposer
{
    public static class TimeUtils
    {
        /// <summary>
        /// If Kind is unspecified the value is assumed to be UTC
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime ConvertToUTC(DateTime? dateTime)
        {
            // assign UtcNow if the dateTime is not provided
            DateTime dt = dateTime ?? DateTime.UtcNow;

            if (dt.Kind == DateTimeKind.Unspecified)
            {
                //Correct time value but not specified time zone
                dt = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
            }
            else
            {
                //local or utc
                dt = dt.ToUniversalTime();
            }
            return dt;
        }
    }
}
