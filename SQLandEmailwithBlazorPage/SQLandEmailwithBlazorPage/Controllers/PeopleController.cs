using Microsoft.AspNetCore.Mvc;
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
        public PeopleController(TutorialDBContext dbContext)
        {
            _dbContext = dbContext;
            _peopleManager = new PeopleManager(_dbContext);
        }

        [Route("api/PopulateDB")]
        [HttpGet]
        public void PopulateDB()
        {
            _peopleManager.PopulateDB();
        }

        [Route("api/AddUser")]
        [HttpPost]
        public void AddUser(PersonModel user)
        {
            if(user == null)
            {
                throw new ArgumentNullException("user cannot be empty");
            }
            _peopleManager.AddUser(user);
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
