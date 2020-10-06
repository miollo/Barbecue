using System;

namespace BarbecueSpace.Dtos
{
    public class BarbecueUpdateDto
    {
        public DateTime? Date { get; set; }

        public string Description { get; set; }

        public string Observations { get; set; }

        public decimal? Value { get; set; }

        public decimal? DrinkValue { get; set; }
    }
}
