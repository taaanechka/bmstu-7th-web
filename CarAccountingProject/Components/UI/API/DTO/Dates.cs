using Microsoft.AspNetCore.Mvc;

namespace API.DTO
{
    public class Dates
    {
        [FromQuery(Name = "date")]
        public string Date { get; set; } = "";

        [FromQuery(Name = "dateFrom")]
        public string DateFrom { get; set; } = "";

        [FromQuery(Name = "dateTo")]
        public string DateTo { get; set; } = "";
    }
}