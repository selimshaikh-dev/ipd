using IPD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Sql
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> o) : base(o)
        {

        }
        public DbSet<Chiefdom> Chiefdoms { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<RecoveryRequest> RecoveryRequests { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Tinkhundla> Tinkhundla { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<UserRight> UserRights { get; set; }
        public DbSet<Admission> Admissions { get; set; }
        public DbSet<Vital> Vitals { get; set; }
        public DbSet<DischargeStatus> DischargeStatuses { get; set; }
        public DbSet<Discharge> Discharges { get; set; }
        public DbSet<DiabeticProfile> DiabeticProfiles { get; set; }
        public DbSet<NursingCare> NursingCares { get; set; }
        public DbSet<DoctorsNote> DoctorsNotes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<InterDepartmentReferral> InterDepartmentReferrals { get; set; }
        public DbSet<DeathCertificate> DeathCertificates { get; set; }
        public DbSet<Surgery> Surgeries { get; set; }
        public DbSet<PostSurgery> PostSurgeries { get; set; }
        public DbSet<SurgeryType> SurgeryTypes { get; set; }
        public DbSet<SurgicalProcedure> SurgicalProcedures { get; set; }
        public DbSet<Procedure> Procedure { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Ncd> Ncds { get; set; }
        public DbSet<Allergy> Allergies { get; set; }
        public DbSet<PatientAllergy> PatientAllergies { get; set; }
        public DbSet<PatientsNcd> PatientsNcds { get; set; }
        public DbSet<PatientExamination> PatientExaminations { get; set; }
        public DbSet<ExaminationDetail> ExaminationDetails { get; set; }
        public DbSet<DiagnosisExamination> DiagonosisExaminations { get; set; }
        public DbSet<ICDDigonosisCode> ICDDigonosisCodes { get; set; }
        public DbSet<PatientDiagnosis> PatientDiagnosis { get; set; }
        public DbSet<TreatmentPlan> TreatmentPlans { get; set; }
        public DbSet<MedicationPlan> MedicationPlans { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Interval> Intervals { get; set; }
        public DbSet<Direction> Directions { get; set; }
        public DbSet<Partograph> Partograph { get; set; }
        public DbSet<InternationalReferral> InternationalReferrals { get; set; }
        public DbSet<DiagonosisDetail> DiagonosisDetails { get; set; }
        public DbSet<FetalHeartRate> FetalHeartRates { get; set; }
        public DbSet<Liquor> Liquors { get; set; }
        public DbSet<Moulding> Mouldings { get; set; }
        public DbSet<Cervix> Cervixes { get; set; }
        public DbSet<DescentOfHead> DescentOfHeads { get; set; }
        public DbSet<Contraction> Contractions { get; set; }
        public DbSet<Oxytocin> Oxytocins { get; set; }
        public DbSet<Drop> Drops { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<BloodPressure> BloodPressures { get; set; }
        public DbSet<Pulse> Pulses { get; set; }
        public DbSet<Temperature> Temperatures { get; set; }
        public DbSet<Protein> Proteins { get; set; }
        public DbSet<Acetone> Acetones { get; set; }
        public DbSet<Volume> volumes { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<BirthDetail> BirthDetails { get; set; }
        public DbSet<UserAccess> userAccesses { get; set; }
        public DbSet<PartographDetail> PartographDetails { get; set; }
        #region Override SaveChange Method to Track Modified Date
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<BaseModel>())
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.DateCreated = DateTime.Now;
                        entry.Entity.IsRowDeleted = false;
                        break;
                    case EntityState.Modified:
                        entry.Property(p => p.CreatedBy).IsModified = false;
                        entry.Property(p => p.DateCreated).IsModified = false;
                        entry.Entity.DateModified = DateTime.Now;
                        break;
                }
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseModel>())
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.DateCreated = DateTime.Now;
                        entry.Entity.IsRowDeleted = false;
                        break;
                    case EntityState.Modified:
                        entry.Property(p => p.CreatedBy).IsModified = false;
                        entry.Property(p => p.DateCreated).IsModified = false;
                        entry.Entity.DateModified = DateTime.Now;
                        break;
                }
            return base.SaveChangesAsync(cancellationToken);
        }
        #endregion

    }
}
