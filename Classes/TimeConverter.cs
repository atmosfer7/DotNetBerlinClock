using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        public static class Light
        {
            public static readonly string Red = "R";
            public static readonly string Yellow = "Y";
            public static readonly string Off = "O"; 
        }
        public string ConvertTime(string aTime)
        {
            try
            {
                var timeUnit = aTime.Split(':');
                int hoursOfDay = Convert.ToInt16(timeUnit[0]);
                int minutesOfDay = Convert.ToInt16(timeUnit[1]);
                int secondsOfDay = Convert.ToInt16(timeUnit[2]);

                int firstRow = hoursOfDay / 5;
                int secondRow = hoursOfDay % 5;

                string output = string.Concat(
                      GetLightOnOrOff(secondsOfDay),
                      GetHoursOfDay(firstRow, 4),
                      GetHoursOfDay(secondRow, 4),
                      GetFiveMinutesOfDay(minutesOfDay, 11),
                      GetOneMinutesOfDay(minutesOfDay, 4));
                return output;
            }
            catch
            {
                throw new FormatException("The Time Format Is Not Correct, Please Check Your Time Unit");
            }
        }
        private string GetLightOnOrOff(int secondsOfDay)
        {
            return secondsOfDay % 2 == 0 ? Light.Yellow : Light.Off;
        }
        private string GetHoursOfDay(int hourOfDay, int columns)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < columns; i++)
            {
                result.Append(hourOfDay > 0 ? Light.Red : Light.Off);
                hourOfDay -= 1;
            }
            return result.ToString();
        }
        private string GetFiveMinutesOfDay(int minutesOfDay, int columns)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= columns; i++)
            {
                if (minutesOfDay > 5 && i % 3 == 0)
                {
                    result.Append(Light.Red);
                }
                else if (minutesOfDay >= 5)
                {
                    result.Append(Light.Yellow);
                }
                else
                {
                    result.Append(Light.Off);
                }
                minutesOfDay -= 5;
            }
            return result.ToString();
        }
        private string GetOneMinutesOfDay(int minutesOfDay, int columns)
        {
            var oneMinutes = minutesOfDay % 5;
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < columns; i++)
            {
                result.Append(oneMinutes > 0 ? Light.Yellow : Light.Off);
                oneMinutes -= 1;
            }
            return result.ToString();
        }

    }
}
