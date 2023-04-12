namespace Banchmark
{
    public class DataParser
    {
        public int GetYearFromDateTime(string dateTimeAsString)
        {
            var dateTime = DateTime.Parse(dateTimeAsString);
            return dateTime.Year;
        }

        public int GetYearFromDateTime2(string dateTimeAsString)
        {
            return int.Parse(dateTimeAsString.Split('-')[0]);
        }
    }
}
