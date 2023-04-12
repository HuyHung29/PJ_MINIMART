namespace MINIMART.Common.Functions
{
    public class CommonFunction
    {
        public string FormatString(dynamic value)
        {
            return value.ToString().Trim();
        }

        public DateTime FormatDateTime(DateTime dateTime)
        {
            return dateTime;
        }

        public decimal FormatNumber(decimal number)
        {
            return number;
        }
    }
}
