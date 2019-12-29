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

        public PeopleManager(TutorialDBContext tutorialDBContext, IConfiguration configuration)
        {
            _dbContext = tutorialDBContext;
            _emailManager = new EmailManager(configuration);

        }

        public void PopulateDB()
        {
            PersonModel patrick = new PersonModel();
            patrick.FirstName = "Patrick";
            patrick.LastName = "Star";
            patrick.Email = "patrick.star@gmail.com";
            patrick.Age = 12;
            patrick.Ocupation = "Actor";
            patrick.Hobbies = "Eating";
            PersonModel bob = new PersonModel();
            bob.FirstName = "Sponge";
            bob.LastName = "Bob";
            bob.Email = "Sponge.Bob@gmail.com";
            bob.Age = 15;
            bob.Ocupation = "Cook";
            bob.Hobbies = "Cooking";
            PersonModel coding = new PersonModel();
            coding.FirstName = "coding";
            coding.LastName = "flamingo";
            coding.Email = "codingflamingo@gmail.com";
            coding.Age = 22;
            coding.Ocupation = "Programmer";
            coding.Hobbies = "Youtube";
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
            _dbContext.People.Add(user);
            await _dbContext.SaveChangesAsync();
            await _emailManager.SendWelcomeEmailAsync(user);

        }
    }
}
