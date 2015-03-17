using ServicesExam.Data.Repositories;
using ServicesExam.Model;
using System;

namespace ServicesExam.Data
{
    public interface IServicesExamData
    {
        //TODO: Add all the IRepositories here in this interface
     IRepository<Game> Games { get; }

     IRepository<Guess> Guesses { get; }

     IRepository<Message> Messages { get; }
        void SaveChanges();
    }
}
