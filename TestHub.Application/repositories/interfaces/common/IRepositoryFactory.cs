using Domain.common;

namespace Application.repositories.interfaces.common;

public interface IRepositoryFactory
{
    IBaseRepository<T> CreateRepository<T>() where T : BaseAuditableEntity;
}