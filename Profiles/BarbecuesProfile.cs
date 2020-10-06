using AutoMapper;
using BarbecueSpace.Dtos;
using BarbecueSpace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarbecueSpace.Profiles
{
    public class BarbecuesProfile : Profile
    {
        /// <summary>
        /// Mapeamento dos Dtos para as models
        /// </summary>
        public BarbecuesProfile()
        {
            // Model para view barbecue
            CreateMap<Barbecue, BarbecueReadDto>();
            // View para model barbecue
            CreateMap<BarbecueCreateDto, Barbecue>();

            // Model para view person
            CreateMap<Person, PersonReadDto>();
            // View para model person
            CreateMap<PersonDto, Person>();

            // view para model barbecuePerson
            CreateMap<BarbecuePersonDto, BarbecuePerson>();
        }
    }
}
