namespace Application.Data
{
    using System;
    using System.Collections.Generic;

    using Application.Data.Contracts;
    using Application.Data.Repositories;
    using Application.Models;

    public class ApplicationData : IApplicationData
    {
        private readonly IDbContext context;

        private readonly IDictionary<Type, object> repositories;

        public ApplicationData()
            : this(new DbContext())
        {
        }

        public ApplicationData(IDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<Game> Games
        {
            get
            {
                return this.GetRepository<Game>();
            }
        }

        public IRepository<Score> Scores
        {
            get
            {
                return this.GetRepository<Score>();
            }
        }

        public IRepository<Color> Colors
        {
            get
            {
                return this.GetRepository<Color>();
            }
        }

        public IRepository<Guess> Guesses
        {
            get
            {
                return this.GetRepository<Guess>();
            }
        }

        public IRepository<Nodification> Notifications
        {
            get
            {
                return this.GetRepository<Nodification>();
            }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);

            if (this.repositories.ContainsKey(typeOfModel))
            {
                return (EfRepository<T>)this.repositories[typeOfModel];
            }

            var type = typeof(EfRepository<T>);

            this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));

            return (EfRepository<T>)this.repositories[typeOfModel];
        }
    }
}