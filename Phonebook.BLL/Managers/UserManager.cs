using AutoMapper;
using Phonebook.BLL.Managers.Interfaces;
using Phonebook.BLL.Models;
using Phonebook.DAL.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Phonebook.Shared.Models;
using Phonebook.DAL.Database.Entities;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Phonebook.BLL.Managers.Settings;
using Microsoft.AspNetCore.Identity;

namespace Phonebook.BLL.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ApiSettings apiSettings;
        private readonly IPasswordHasher<string> passwordHasher;
        public UserManager(IMapper mapper, IUnitOfWork unitOfWork, ApiSettings apiSettings, IPasswordHasher<string> passwordHasher)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.apiSettings = apiSettings;
            this.passwordHasher = passwordHasher;
        }
        public async Task<OperationResult> AddUserAsync(UserModel model)
        {
            var userDTO = mapper.Map<UserModel, User>(model);

            if (await unitOfWork.UserRepository.Exists(model.UserName))
            {
                return new OperationResult(false, "UserName exists!");
            }
            userDTO.Password = passwordHasher.HashPassword(model.UserName, model.Password);
            await unitOfWork.UserRepository.AddAsync(userDTO);
            return await unitOfWork.CompleteAsync();
        }
        public async Task<string> Authenticate(LoginModel model)
        {
            var user = await unitOfWork.UserRepository.GetUserInfo(model.UserName);

            if (user == null)
                return null;

            var checkpassword = passwordHasher.VerifyHashedPassword(model.UserName, user.Password, model.Password);
            if (checkpassword != PasswordVerificationResult.Success)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(apiSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
