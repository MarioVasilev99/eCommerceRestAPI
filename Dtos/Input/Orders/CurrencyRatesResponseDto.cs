namespace eCommerceRestAPI.Dtos.Input.Orders
{
    using System;
    using System.Collections.Generic;

    public class CurrencyRatesResponseDto
    {
        public Dictionary<string, decimal> Rates { get; set; }

        public string Base { get; set; }

        public DateTime Date { get; set; }
    }
}
