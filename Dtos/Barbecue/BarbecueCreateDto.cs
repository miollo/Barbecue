using System;
using System.ComponentModel.DataAnnotations;

namespace BarbecueSpace.Dtos
{
    /// <summary>
    /// Dto para criar/alterar churrasco
    /// </summary>
    public class BarbecueCreateDto
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; }

        public string Observations { get; set; }

        public decimal Value { get; set; }

        public decimal? DrinkValue { get; set; }
    }
}
