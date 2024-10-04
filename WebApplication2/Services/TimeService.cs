namespace WebApplication2.Services
{
    public class TimeService
    {
        public string GetTimeOfDay()
        {
            var currentHour = DateTime.Now.Hour;

            if (currentHour >= 6 && currentHour < 12)
            {
                return "Зараз ранок";
            }
            else if (currentHour >= 12 && currentHour < 18)
            {
                return "Зараз день";
            }
            else if (currentHour >= 18 && currentHour < 24)
            {
                return "Зараз вечір";
            }
            else
            {
                return "Зараз ніч";
            }
        }
    }
}