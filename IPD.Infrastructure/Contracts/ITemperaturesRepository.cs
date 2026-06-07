using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface ITemperaturesRepository : IRepository<Temperature>
    {
        Temperature UpdateTemperature(Temperature temperature);
    }
}