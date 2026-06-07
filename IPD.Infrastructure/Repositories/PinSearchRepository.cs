using Dapper;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace IPD.Infrastructure.Repositories
{
    public class PinSearchRepository : DapperRepository<Patient>, IPinSearchRepository
    {
        private readonly IConfiguration configuration;

        public PinSearchRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IEnumerable<Patient>> GetByPIN(long PIN)
        {
            try
            {
                var sql = "SELECT FirstName as FirstName,LastName as LastName,[Middle Name] as MiddleName,GenderID as Sex,BirthDate as DOB"
                + " FROM tblPopulationRegister"
                + " WHERE PIN = '" + PIN.ToString() + "'";

                //var sql = "SELECT FirstName as FirstName,LastName as LastName,[MiddleName] as MiddleName,Sex,DOB"
                //+ " FROM Patients"
                //+ " WHERE NationalID = '" + PIN.ToString() + "'";
                using (var connection = new SqlConnection(configuration.GetConnectionString("DapperConnection")))
                {
                    connection.Open();
                    // Map all products from database to a list of type Product defined in Models.
                    // this is done by using Async method which is also used on the GetByIdAsync
                    var result = await connection.QueryAsync<Patient>(sql);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}