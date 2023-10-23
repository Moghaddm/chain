using Chain.Domain.Entities;

namespace Chain.Application.Contract.Ports.Repositories;

public interface IRateRepository
{
    Task GiveRate(Rate rate);
    Task RollbackRate(Rate rate);
}