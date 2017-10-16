namespace MusicR8r.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MsSqlDbContext context;

        public UnitOfWork(MsSqlDbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }
    }
}
