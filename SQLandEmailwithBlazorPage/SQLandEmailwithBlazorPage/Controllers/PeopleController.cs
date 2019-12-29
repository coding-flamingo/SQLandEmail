using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SQLandEmailwithBlazorPage.Data;
using SQLandEmailwithBlazorPage.Managers;
using SQLandEmailwithBlazorPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLandEmailwithBlazorPage.Controllers
{
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly TutorialDBContext _dbContext;
        private readonly PeopleManager _peopleManager;
        public PeopleController(TutorialDBContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _peopleManager = new PeopleManager(_dbContext, configuration);
        }

        [Route("api/PopulateDB")]
        [HttpGet]
        public void PopulateDB()
        {
            _peopleManager.PopulateDB();
        }

        [Route("api/AddUser")]
        [HttpPost]
        public async Task AddUserAsync(PersonModel user)
        {
            if(user == null)
            {
                throw new ArgumentNullException("user cannot be empty");
            }
            await _peopleManager.AddUserAsync(user);
        }

        [Route("api/GetPeople")]
        [HttpGet]
        public PersonModel[] GetPeople()
        {
            return _peopleManager.GetPeople();
        }

        [Route("api/DeleteUser")]
        [HttpGet]
        public async Task DeleteUserAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("email cannot be empty");
            }
            await _peopleManager.DeleteUserAsync(email);

        }
    }
}
