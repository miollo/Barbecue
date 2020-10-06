namespace BarbecueSpace.Models
{
    /// <summary>
    /// Classe intermediaria do vinculo entre pessoas e churrascos
    /// </summary>
    public class BarbecuePerson
    {
        /// <summary>
        /// Vinculo com churrascos
        /// </summary>
        public int BarbecueId { get; set; }
        public Barbecue Barbecue { get; set; }

        /// <summary>
        /// Vinculo com pessoas
        /// </summary>
        public int PersonId { get; set; }
        public Person Person { get; set; }

        /// <summary>
        /// Booleano que indica se a pessoa irá beber
        /// </summary>
        public bool Drink { get; set; }

        /// <summary>
        /// Booleano que indica se a pessoa já pagou
        /// </summary>
        public bool Payed { get; set; }
    }
}
