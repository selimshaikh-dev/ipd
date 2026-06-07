using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class Login : ILogin
    {
        private readonly DataContext _dataContext;

        public Login(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }    
    }
}