using System;
using System.Collections;
using System.Collections.Generic;

namespace BarbecueSpace.Dtos
{
    /// <summary>
    /// Dto para visualização de um churrasco especifico
    /// </summary>
    public class BarbecueReadDto
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public string Description { get; set; }

        public string Observations { get; set; }

        public decimal? Value { get; set; }

        public decimal? DrinkValue { get; set; }

        public decimal TotalValue { get; set; }

        public int TotalPeople { get; set; }

        public List<BarbecuePersonReadDto> People { get; set; }
    }
}
