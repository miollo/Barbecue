using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarbecueSpace.Dtos
{
    /// <summary>
    /// Dto para visualização das pessoas vinculadas no churrasco
    /// </summary>
    public class BarbecuePersonReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Drink { get; set; }

        public bool Payed { get; set; }
    }
}
