namespace LinkConverterApplication.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IWebUrlRepository webUrlRepository, IErrorRepository errorRepository)
        {
            WebUrl = webUrlRepository;
            Error = errorRepository;
        }

        public IWebUrlRepository WebUrl { get; }
        public IErrorRepository Error { get; }
    }
}