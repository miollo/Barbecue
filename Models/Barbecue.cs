using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BarbecueSpace.Models
{
    /// <summary>
    /// Churrascos
    /// </summary>
    public class Barbecue
    {
        /// <summary>
        /// Id do churrasco
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Dia em que será realizado
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Descrição
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Observações adicionais
        /// </summary>
        public string Observations { get; set; }

        /// <summary>
        /// Valor sugerido para o churrasco
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Valor sugerido para as bebidas
        /// </summary>
        public decimal? DrinkValue { get; set; }

        /// <summary>
        /// Lista de pessoas que irão participar do churrasco
        /// </summary>
        public ICollection<BarbecuePerson> BarbecuePeople { get; set; }
    }
}
