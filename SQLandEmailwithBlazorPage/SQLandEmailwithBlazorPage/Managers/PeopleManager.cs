using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SQLandEmailwithBlazorPage.Data;
using SQLandEmailwithBlazorPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLandEmailwithBlazorPage.Managers
{
    public class PeopleManager
    {
        TutorialDBContext _dbContext;
        private readonly EmailManager _emailManager;
        private readonly IConfiguration _configuration;
        public PeopleManager(TutorialDBContext tutorialDBContext, EmailManager emailman, IConfiguration configuration)
        {
            _dbContext = tutorialDBContext;
            _emailManager = emailman;
            _configuration = configuration;
        }

        public void PopulateDB()
        {
            PersonModel patrick = new PersonModel
            {
                FirstName = "Patrick",
                LastName = "Star",
                Email = "patrick.star@gmail.com",
                Age = 12,
                Ocupation = "Actor",
                Hobbies = "Eating"
            };
            PersonModel bob = new PersonModel
            {
                FirstName = "Sponge",
                LastName = "Bob",
                Email = "Sponge.Bob@gmail.com",
                Age = 15,
                Ocupation = "Cook",
                Hobbies = "Cooking"
            };
            PersonModel coding = new PersonModel
            {
                FirstName = "coding",
                LastName = "flamingo",
                Email = "codingflamingo@gmail.com",
                Age = 22,
                Ocupation = "Programmer",
                Hobbies = "Youtube"
            };
            _dbContext.People.Add(patrick);
            _dbContext.People.Add(coding);
            _dbContext.People.Add(bob);
            _dbContext.SaveChanges();
        }

        public PersonModel[] GetPeople()
        {
            PersonModel[] allPeople = _dbContext.People.ToArray();
            return allPeople;
        }

        public async Task DeleteUserAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("email cannot be empty");
            }
            var user = _dbContext.People.FirstOrDefault(i => i.Email.Equals(email));
            if (user == null)
            {
                throw new ArgumentException("User " + email + " was not found");
            }
            _dbContext.People.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddUserAsync(PersonModel user)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null");
            }
            PersonModel existingPerson = await _dbContext.People.FirstOrDefaultAsync(x 
                => x.Email == user.Email);
            if(existingPerson == null)
            {
                _dbContext.People.Add(user);
                await _dbContext.SaveChangesAsync();
                await _emailManager.SendWelcomeEmailAsync(user);
            }
        }
    }
}
