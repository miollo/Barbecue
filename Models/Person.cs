using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BarbecueSpace.Models
{
    /// <summary>
    /// Pessoas
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Id da pessoa
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome da pessoa
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Idade da pessoa
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Lista de churrasco que essa pessoa irá participar
        /// </summary>
        public ICollection<BarbecuePerson> BarbecuePeople { get; set; }
    }
}
