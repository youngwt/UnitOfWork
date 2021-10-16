using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UnitOfWorkDemo.Configuration;
using UnitOfWorkDemo.IRepositories;

namespace UnitOfWorkDemo.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger _logger;
        public IUserRepository UserRepository {get; private set;}

        public UnitOfWork(ILoggerFactory logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger.CreateLogger("logs");
            _applicationDbContext = applicationDbContext;
            // this is from the example, no idea why it isn't injected in
            UserRepository = new UserRepository(_applicationDbContext, _logger);
        }

        public IUserRepository Users => throw new NotImplementedException();

        public async Task CompleteAsync()
        {
        await _applicationDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }
    }
}