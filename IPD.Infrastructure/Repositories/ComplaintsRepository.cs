using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class ComplaintsRepository : Repository<Complaint>, IComplaintsRepository
    {
        private readonly DataContext context;

        public ComplaintsRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ComplaintDto>> GetAllLoadCompient(Guid aid)
        {
            try
            {
               var minDate = DateTime.Now.AddMonths(-3);
                var q = await (from c in _context.Complaints
                               .Where(e => e.DateCreated > minDate)
                               .Where(e => e.AdmissionID == aid)
                               .Include(e => e.Admissions)
                               .Include(c=>c.PatientsNcds)                             
                               join f in _context.Facilities
                               on c.FacilityCode equals f.FacilityCode into fc
                               from f in fc.DefaultIfEmpty()
                               select new ComplaintDto
                               {
                                   AdmissionID = aid,
                                   ComplaintID = c.ComplaintID,
                                   ComplaintName = c.ComplaintName,
                                   DateCreated = c.DateCreated,
                                   FacilityName = f.FacilityName,
                                   Epilepsy = c.Epilepsy,
                                   PatientsNcds = c.PatientsNcds
                                   .Select(e => new PatientsNcdDto()
                                   {
                                       NcdsID = e.NcdsID,
                                       NcdsName = e.Ncds.NcdName,
                                       PatientNcdsID=e.PatientNcdsID,
                                   }).ToList(),
                                   PatientAllergy = c.PatientAllergy
                                   .Select(x=> new PatientAllergyDto()
                                   {
                                       AllergiesID = x.AllergiesID,
                                       AllergiesName = x.Allergies.AllergiesName,
                                       PatientAllergiesID=x.PatientAllergiesID,
                                   }).ToList(),
                                   ComplaintHistory = c.ComplaintHistory,
                                   Diabetes = c.Diabetes,
                                   Hypertention = c.Hypertention,   
                                   SpecialNote = c.SpecialNote,
                                   SystemsReview = c.SystemsReview,
                               }).OrderByDescending(e => e.DateCreated)

                         .ToListAsync();
                return q;
            }
            catch
            {
                throw;
            }
        }
    }
}