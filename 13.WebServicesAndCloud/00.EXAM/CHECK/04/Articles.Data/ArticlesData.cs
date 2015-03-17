namespace Articles.Data
{
    using Articles.Data.Repositories;
    using Articles.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class ArticlesData : IArticlesData
    {
        //Single context
        //Dictionary to hold repositories and search by type

        private DbContext context;
        private Dictionary<Type, object> repositories;

        public ArticlesData(ArticlesDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public Repositories.IRepository<Game> Games
        {
            get { return this.GetRepository<Game>(); }
        }

        public Repositories.IRepository<Guess> Guesses
        {
            get { return this.GetRepository<Guess>(); }
        }

        public Repositories.IRepository<Notification> Notifications
        {
            get { return this.GetRepository<Notification>(); }
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            //gets the relevant repository from the dictionary, or creates it if necessary
            var typeOfRepo = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepo))
            {
                var newRepo = Activator.CreateInstance(typeof(EFRepository<T>), context);
                this.repositories.Add(typeOfRepo, newRepo);
            }
            return (IRepository<T>)this.repositories[typeOfRepo];
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }
    }
}
