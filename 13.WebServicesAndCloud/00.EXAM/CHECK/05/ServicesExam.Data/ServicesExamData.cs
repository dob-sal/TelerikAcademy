using ServicesExam.Data.Repositories;
using ServicesExam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesExam.Data
{
    public class ServicesExamData : IServicesExamData 
    {
        private ApplicationDbContext context;
        private IDictionary<Type, object> repositories;


       public IRepository<Game> Games
       {
           get
           {
               return this.GetRepository<Game>();
           }
       }

      public IRepository<Guess> Guesses
      {
          get
          {
              return this.GetRepository<Guess>();
          }
      }

      public IRepository<Message> Messages
      {
          get
          {
              return this.GetRepository<Message>();
          }
      }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        public ServicesExamData(ApplicationDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public ServicesExamData()
            : this(new ApplicationDbContext())
        {
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(Repository<T>);

                //if (typeOfModel.IsAssignableFrom(typeof(Student)))
                //{
                //    type = typeof(StudentsRepository);
                //}

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeOfModel];
        }
    }
}
