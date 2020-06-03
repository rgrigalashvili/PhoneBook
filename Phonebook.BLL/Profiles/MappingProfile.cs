using AutoMapper;
using Phonebook.BLL.Models;
using Phonebook.DAL.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonebook.BLL.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EntityBase, BaseModel>();
            CreateMap<Person, PersonModel>();
            CreateMap<User, UserModel>();
            CreateMap<PhoneNumber, PhoneNumberModel>();

            CreateMap<BaseModel, EntityBase>();
            CreateMap<PersonModel, Person>();
            CreateMap<UserModel, User>();
            CreateMap<PhoneNumberModel, PhoneNumber>();
        }
    }
}