using AutoMapper;
using BarbecueSpace.Data.Repositories.Interfaces;
using BarbecueSpace.Dtos;
using BarbecueSpace.Dtos.BarbecuePerson;
using BarbecueSpace.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BarbecueSpace.Controllers
{
    [Route("api/barbecues")]
    [ApiController]
    public class BarbecuesController : ControllerBase
    {
        private readonly IBarbecuePersonRepository _barbecuePersonRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IBarbecueRepository _repository;
        private readonly IMapper _mapper;
        

        public BarbecuesController(IBarbecuePersonRepository barbecuePersonRepository, IBarbecueRepository repository, IPersonRepository personRepository,IMapper mapper)
        {
            _barbecuePersonRepository = barbecuePersonRepository;
            _personRepository = personRepository;
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Busca todos churrascos, não exibe pessoas presentes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<BarbecueReadDto>> GetAllBarbecues()
        {
            var barbecueItems = _repository.GetAll();
            var barbecuePeopleFromRepo = _barbecuePersonRepository.GetAll();
            var peopleListFromRepo = _personRepository.GetAll();

            var barbsReadDto = barbecueItems
                .Select(x => new
                {
                    x.Id,
                    Date = x.Date.ToString("dd/MM/yyyy"),
                    x.Description,
                    x.Observations,
                    x.Value,
                    x.DrinkValue,
                    TotalPeople = x.BarbecuePeople != null ? x.BarbecuePeople.Count : 0,
                    TotalValue = x.BarbecuePeople != null ? (x.BarbecuePeople.Count * x.Value) + (x.BarbecuePeople.Where(x => x.Drink).ToList().Count * x.DrinkValue.Value) : 0
                 })
                .ToList();

            return Ok(barbsReadDto);

        }

        /// <summary>
        /// Busca um churrasco especifico por Id e suas informações
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult <BarbecueReadDto> GetBarbecueById(int id)
        {
            var barbecueItem = _repository.Get(id);
            var barbecuePeopleFromRepo = _barbecuePersonRepository.GetAll();
            var peopleListFromRepo = _personRepository.GetAll();

            if(barbecueItem != null)
            {
                var barbReadDto = _mapper.Map<BarbecueReadDto>(barbecueItem);
                barbReadDto.Date = barbecueItem.Date.ToString("dd/MM/yyyy");
                
                if (barbecueItem.BarbecuePeople != null)
                {
                    barbReadDto.People = barbecueItem.BarbecuePeople
                        .Select(x => new BarbecuePersonReadDto
                        {
                            Id = x.PersonId,
                            Name = x.Person.Name,
                            Drink = x.Drink,
                            Payed = x.Payed
                        })
                        .ToList();

                    barbReadDto.TotalPeople = barbecueItem.BarbecuePeople.Count;
                    barbReadDto.TotalValue = (barbecueItem.BarbecuePeople.Count * barbecueItem.Value) + (barbecueItem.BarbecuePeople.Where(x => x.Drink).ToList().Count * barbecueItem.DrinkValue.Value);
                }
                return Ok(barbReadDto);

            }

            return NotFound("Não encontramos informações sobre esse churrasco.");
        }

        /// <summary>
        /// Cria um novo churrasco
        /// </summary>
        /// <param name="barb"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<BarbecueReadDto> CreateBarbecue(BarbecueCreateDto barb)
        {
            if(DateTime.Compare(barb.Date, DateTime.Today) < 0)
            {
                return BadRequest("Não é possível marcar um churrasco em um dia que já passou.");
            }

            var barbecueModel = _mapper.Map<Barbecue>(barb);
            _repository.Add(barbecueModel);
            _repository.SaveChanges();

            var barbecueReadDto = _mapper.Map<BarbecueReadDto>(barbecueModel);

            return Ok(barbecueReadDto);
        }

        /// <summary>
        /// Atualiza um churrasco existente
        /// </summary>
        /// <param name="id"></param>
        /// <param name="barbUpdate"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult UpdateBarbecue(int id, BarbecueUpdateDto barbUpdate)
        {
            var barbFromRepo = _repository.Get(id);
            var barbecuePeopleFromRepo = _barbecuePersonRepository.GetAll();
            var peopleListFromRepo = _personRepository.GetAll();

            if (barbFromRepo == null)
            {
                return NotFound("Churrasco não encontrado.");

            } else if (DateTime.Compare(barbFromRepo.Date, DateTime.Today) < 0)
            {
                return BadRequest("Não é possível alterar a data de um churrasco que já aconteceu.");
            } 
            
            // Verifico quais itens foram alterados
            if(barbUpdate.Date.HasValue)
            {
                if (DateTime.Compare(barbUpdate.Date.Value, DateTime.Today) < 0)
                {
                    return BadRequest("Não é possível alterar a data do churrasco para um dia no passado.");
                }

                barbFromRepo.Date = barbUpdate.Date.Value;
            }
            if (barbUpdate.Description != null)
            {
                barbFromRepo.Description = barbUpdate.Description;
            }
            if (barbUpdate.Observations != null)
            {
                barbFromRepo.Observations = barbUpdate.Observations;
            }
            if (barbUpdate.Value.HasValue)
            {
                if(barbFromRepo.BarbecuePeople != null)
                {
                    return BadRequest("Não é possível alterar o valor cobrado de um churrasco que já tem participantes.");
                }
                barbFromRepo.Value = barbUpdate.Value.Value;
            }
            if (barbUpdate.DrinkValue.HasValue)
            {
                if (barbFromRepo.BarbecuePeople != null)
                {
                    return BadRequest("Não é possível alterar o valor da bebida cobrado de um churrasco que já tem participantes.");
                }
                barbFromRepo.DrinkValue = barbUpdate.DrinkValue;
            }

            _repository.SaveChanges();

            return Ok("Informações atualizadas com sucesso.");
        }

        /// <summary>
        /// Deleta um churrasco existente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteBarbecue(int id)
        {
            var barbFromRepo = _repository.Get(id);
            var barbecuePeopleFromRepo = _barbecuePersonRepository.GetAll();


            if (barbFromRepo == null)
            {
                return NotFound("Churrasco não encontrado.");

            }
 
            if(barbFromRepo.BarbecuePeople != null)
            {
                var listBarbecuePerson = barbecuePeopleFromRepo
                    .Where(x=> x.BarbecueId == id)
                    .ToList();
                _barbecuePersonRepository.RemoveMultiple(listBarbecuePerson);
                _barbecuePersonRepository.SaveChanges();
            }

            _repository.Remove(barbFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Adiciona uma pessoa a um churrasco especifico
        /// </summary>
        /// <param name="barbPersonDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addPersonOnBarbecue")]
        public ActionResult AddPersonOnBarbecue(BarbecuePersonDto barbPersonDto)
        {
            var barbFromRepo = _repository.Get(barbPersonDto.BarbecueId);
            var personFromRepo = _personRepository.Get(barbPersonDto.PersonId);
            var barbecuePersonFromRepo = _barbecuePersonRepository.GetBarbecuePerson(barbPersonDto.BarbecueId, barbPersonDto.PersonId);

            if (barbFromRepo == null)
            {
                return NotFound("Churrasco não encontrado.");

            }
            else if (personFromRepo == null)
            {
                return NotFound("Pessoa não encontrada.");

            }
            else if (DateTime.Compare(barbFromRepo.Date, DateTime.Today) < 0)
            {
                return BadRequest("Não é possível adicionar uma pessoa em um churrasco que já aconteceu.");
            }
            else if (barbecuePersonFromRepo != null)
            {
                return BadRequest("Essa pessoa já está confirmada nesse churrasco.");
            }
 
            var barbecuePersonModel = _mapper.Map<BarbecuePerson>(barbPersonDto);
            _barbecuePersonRepository.Add(barbecuePersonModel);
            _barbecuePersonRepository.SaveChanges();
            _repository.SaveChanges();

            return Ok("Adicionado ao churrasco");
        }

        /// <summary>
        /// Atualiza se pessoa vai ou nao beber e se já pagou
        /// </summary>
        /// <param name="id"></param>
        /// <param name="barbUpdateDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdatePersonOnBarbecue/{id}")]
        public ActionResult UpdatePersonOnBarbecue(int id, BarbecuePersonUpdateDto barbUpdateDto)
        {
            var barbFromRepo = _repository.Get(id);
            var personFromRepo = _personRepository.Get(barbUpdateDto.PersonId);
            var barbecuePersonFromRepo = _barbecuePersonRepository.GetBarbecuePerson(barbecueId: id, personId: barbUpdateDto.PersonId);

            if (barbFromRepo == null)
            {
                return NotFound("Churrasco não encontrado.");

            }
            else if (personFromRepo == null)
            {
                return NotFound("Pessoa não encontrada.");

            } else if (barbecuePersonFromRepo == null)
            {
                return NotFound("Pessoa não encontrada na lista de participantes do churrasco.");
            }

            barbecuePersonFromRepo.Drink = barbUpdateDto.Drink.HasValue ? barbUpdateDto.Drink.Value : barbecuePersonFromRepo.Drink;
            barbecuePersonFromRepo.Payed = barbUpdateDto.Payed.HasValue ? barbUpdateDto.Payed.Value : barbecuePersonFromRepo.Payed; ;
            _barbecuePersonRepository.SaveChanges();

            return Ok("Informações atualizadas com sucesso");
        }

        /// <summary>
        /// Remove uma pessoa de um churrasco
        /// </summary>
        /// <param name="id"></param>
        /// <param name="barbDeleteDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("DeletePersonFromBarbecue/{id}")]
        public ActionResult DeletePersonFromBarbecue(int id,  BarbecuePersonDeleteDto barbDeleteDto)
        {
            var barbFromRepo = _repository.Get(id);
            var personFromRepo = _personRepository.Get(barbDeleteDto.PersonId);
            var barbecuePersonFromRepo = _barbecuePersonRepository.GetBarbecuePerson(barbecueId: id, personId: barbDeleteDto.PersonId);

            if (barbFromRepo == null)
            {
                return NotFound("Churrasco não encontrado.");

            } else if (personFromRepo == null)
            {
                return NotFound("Pessoa não encontrada.");
            } else if(barbecuePersonFromRepo == null)
            {
                return NotFound("Pessoa não encontrada na lista de participantes para ser removida.");
            }

            _barbecuePersonRepository.Remove(barbecuePersonFromRepo);
            _barbecuePersonRepository.SaveChanges();

            return Ok("Removido do churrasco com sucesso");
        }

        /// <summary>
        /// Adiciona todas pessoas cadastradas a um churrasco que não possua participantes ainda
        /// </summary>
        /// <param name="id"></param>
        /// <param name="addAllPeopleDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("addAllPeopleOnBarbecue/{id}")]
        public ActionResult AddAllPersonOnBarbecue(int id, BarbecueAllPeopleDto addAllPeopleDto)
        {

            var barbFromRepo = _repository.Get(id);
            var barbecuePeopleFromRepo = _barbecuePersonRepository.GetAll();
            var peopleListFromRepo = _personRepository.GetAll();

            if (barbFromRepo == null)
            {
                return NotFound("Churrasco não encontrado.");

            } else if (DateTime.Compare(barbFromRepo.Date, DateTime.Today) < 0)
            {
                return BadRequest("Não é possível adicionar pessoas em um churrasco que já aconteceu.");
            } else if (barbFromRepo.BarbecuePeople != null)
            {
                return BadRequest("Não é possível adicionar todas pessoas no churrasco " + barbFromRepo.Description + " pois o mesmo já possui pessoas confirmadas.");
            }

            if (barbFromRepo != null)
            {
                var barbecuePeople = new List<BarbecuePerson>();
                foreach (var item in peopleListFromRepo)
                {
                    barbecuePeople.Add(new BarbecuePerson
                    {
                        BarbecueId = id,
                        PersonId = item.Id,
                        Drink = addAllPeopleDto.Drink,
                        Payed = addAllPeopleDto.Payed
                    });
                }

                if (barbecuePeople.Count > 0)
                {
                    _barbecuePersonRepository.AddMultiple(barbecuePeople);
                    _barbecuePersonRepository.SaveChanges();
                }
            }

            return Ok("Todas pessoas foram adicionadas no churrasco "+ barbFromRepo.Description);
        }

    }
}
