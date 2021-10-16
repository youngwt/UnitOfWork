using UnitOfWorkDemo.IRepositories;
using System.Threading.Tasks;

namespace UnitOfWorkDemo.Configuration
{
    public interface IUnitOfWork
    {
         IUserRepository Users {get; }
         Task CompleteAsync();
    }
}