using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class DoctorsNoteRepository : Repository<DoctorsNote>, IDoctorsNoteRepository
    {
        private readonly DataContext _context;

        public DoctorsNoteRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DoctorNotesDto>> GetAllDoctorNote(Guid aid)
        {
            try
            {

                //var partographid= await char in context.partograph(char=>char.admissionid== aid)

                var minDate = DateTime.Now.AddMonths(-3);
                var q = await (from c in _context.DoctorsNotes
                               .Where(e => e.DateCreated > minDate)
                               .Where(e => e.AdmissionID == aid)
                               .Include(e => e.Admission)
                               .ThenInclude(e => e.BirthDetails)
                               join f in _context.Facilities
                               on c.FacilityCode equals f.FacilityCode into fc
                               from f in fc.DefaultIfEmpty()
                               select new DoctorNotesDto
                               {
                                   AdmissionID = aid,
                                   DoctorsNoteID = c.DoctorsNoteID,
                                   DateCreated = c.DateCreated,
                                   FacilityName = f.FacilityName,
                                   DateOfNote = c.DateOfNote,
                                   TimeOfNote = c.TimeOfNote,
                                   Observation = c.Observation,
                                   TestRequest = c.TestRequest
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