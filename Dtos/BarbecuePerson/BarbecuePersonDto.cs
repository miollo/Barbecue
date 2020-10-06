namespace BarbecueSpace.Dtos
{
    /// <summary>
    /// Dto para criar/editar pessoas vinculadas ao churrasco
    /// </summary>
    public class BarbecuePersonDto
    {
        public int BarbecueId { get; set; }

        public int PersonId { get; set; }

        public bool Drink { get; set; }

        public bool Payed { get; set; }
    }
}
