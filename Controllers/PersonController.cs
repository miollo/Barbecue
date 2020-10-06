using AutoMapper;
using BarbecueSpace.Data.Repositories.Interfaces;
using BarbecueSpace.Dtos;
using BarbecueSpace.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BarbecueSpace.Controllers
{
    [Route("api/people")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _repository;
        private readonly IMapper _mapper;

        public PersonController(IPersonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Busca todas pessoas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<PersonReadDto>> GetAllPeople()
        {
            var people = _repository.GetAll();

            return Ok(_mapper.Map<IEnumerable<PersonReadDto>>(people));
        }

        /// <summary>
        /// Busca uma pessoa pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<PersonReadDto> GetPersonById(int id)
        {
            var person = _repository.Get(id);

            if (person != null)
            {
                return Ok(_mapper.Map<PersonReadDto>(person));

            }

            return NotFound();
        }

        /// <summary>
        /// Cria uma nova pessoa
        /// </summary>
        /// <param name="pp"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<PersonReadDto> CreatePerson(PersonDto pp)
        {
            var ppModel = _mapper.Map<Person>(pp);
            _repository.Add(ppModel);
            _repository.SaveChanges();

            var personReadDto = _mapper.Map<PersonReadDto>(ppModel);

            return Ok(personReadDto);
        }

        /// <summary>
        /// Edita uma pessoa pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ppUpdate"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult UpdatePerson(int id, PersonDto ppUpdate)
        {
            var personFromRepo = _repository.Get(id);

            if (personFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(ppUpdate, personFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Deleta uma pessoa pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeletePerson(int id)
        {
            var personFromRepo = _repository.Get(id);

            if (personFromRepo == null)
            {
                return NotFound();
            }

            _repository.Remove(personFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
