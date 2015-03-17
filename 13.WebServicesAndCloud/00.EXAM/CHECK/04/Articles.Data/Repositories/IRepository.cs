namespace Articles.Data.Repositories
{
    using System.Linq;

    public  interface IRepository<T> where T : class
    {
        //All, Find, Add, Update, Delete(T), Delete(id), int SaveChanges

        IQueryable<T> All();

        void Add(T entity);

        T Find(object id);

        void Update(T entity);

        T Delete(T entity);

        T Delete(object id);

        int SaveChanges();

        //ChangeState(T entity, EntityState state)

    }
}
