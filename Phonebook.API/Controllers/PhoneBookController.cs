using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phonebook.BLL.Managers.Interfaces;
using Phonebook.BLL.Models;

namespace Phonebook.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class PhoneBookController : ControllerBase
    {
        private readonly IPersonManager personManager;
        public PhoneBookController(IPersonManager personManager)
        {
            this.personManager = personManager;
        }
        
        [Route("addPerson")]
        [HttpPost]
        public async Task<IActionResult> AddPerson(PersonModel model)
        {
            return Ok(await personManager.AddPersonAsync(model));
        }

        [Route("deletePerson")]
        [HttpPost]
        public async Task<IActionResult> DeletePerson(int personID)
        {
            return Ok(await personManager.DeletePersonAsync(personID));
        }

        [Route("addPhone")]
        [HttpPost]
        public async Task<IActionResult> AddPhone(PhoneNumberModel model)
        {
            return Ok(await personManager.AddPhoneAsync(model));
        }

        [Route("getAllPersonsPhoneNumbers")]
        [HttpGet]
        public async Task<IActionResult> GetAllPersonsPhoneNumbers(int personID, int userID)
        {
            return Ok(await personManager.GetAllPhoneNumbersForPerson(personID, userID));
        }

        [Route("deletePhone")]
        [HttpPost]
        public async Task<IActionResult> DeletePhone(int phoneID)
        {
            return Ok(await personManager.DeletePhoneAsync(phoneID));
        }

        [Route("identifyPhoneOwner")]
        [HttpGet]
        public async Task<IActionResult> IdentifyPhoneOwner(string phoneNumber, int userID)
        {
            return Ok(await personManager.IdentifyPersonByPhone(phoneNumber, userID));
        }

    }
}