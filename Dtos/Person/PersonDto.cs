using System.ComponentModel.DataAnnotations;

namespace BarbecueSpace.Dtos
{
    /// <summary>
    /// Dto para criar/alterar pessoas
    /// </summary>
    public class PersonDto
    {
        [Required]
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
