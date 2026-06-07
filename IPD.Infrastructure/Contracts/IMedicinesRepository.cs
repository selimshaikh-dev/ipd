using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IMedicinesRepository : IRepository<Medicine>
    {
        Medicine UpdateMedicine(Medicine medicine);
    }
}