namespace LinkConverterApplication.Repositories
{
    public interface IUnitOfWork
    { 
        IWebUrlRepository WebUrl { get; }
        IErrorRepository Error { get; }
    }
}