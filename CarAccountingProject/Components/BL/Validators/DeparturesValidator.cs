namespace BL
{
    public class DeparturesValidator
    {
        public static void ValidateDeparture(Departure departure)
        {
            if (departure == null ||
                departure.UserId < 1 ||
                departure.DepartureDate == default(DateTime))
            {
                throw new DeparturesValidatorFailException();
            }
        }
    }
}