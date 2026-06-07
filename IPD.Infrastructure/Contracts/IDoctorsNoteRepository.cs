using IPD.Domain.Dto;
using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IDoctorsNoteRepository : IRepository<DoctorsNote>
    {
        Task<IEnumerable<DoctorNotesDto>> GetAllDoctorNote(Guid aid);
    }
}