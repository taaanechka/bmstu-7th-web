namespace BL
{
    public class LinksOwnerCarDepartureValidator
    {
        public static void ValidateLinkOwnerCarDeparture(LinkOwnerCarDeparture linkOwnerCarDeparture)
        {
            if (linkOwnerCarDeparture == null ||
                linkOwnerCarDeparture.OwnerId < 1 || 
                linkOwnerCarDeparture.CarId.Length == 0 ||
                linkOwnerCarDeparture.DepartureId < 1)
            {
                throw new LinksOwnerCarDepartureValidatorFailException();
            }
        }
    }
}