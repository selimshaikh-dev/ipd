namespace IPD.Infrastructure.Contracts
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Declare SaveChanges method.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Declare SaveChangesAsync method.
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();

        IPrescriptionsRepository PrescriptionsRepository { get; }
        ITinkhundlaRepository TinkhundlaRepository { get; }
        IChiefdomsRepository ChiefdomsRepository { get; }
        ICountriesRepository CountriesRepository { get; }
        IPatientsRepository PatientsRepository { get; }
        IFacilityRepository FacilityRepository { get; }
        IUserAccountRepository UserAccountRepository { get; }
        IRegionRepository RegionRepository { get; }
        IUserRightRepository UserRightRepository { get; }
        IRecoveryRequestRepository RecoveryRequestRepository { get; }
        IAdmissionRepository AdmissionRepository { get; }
        IVitalRepository VitalRepository { get; }
        IDiabeticProfileRepository DiabeticProfileRepository { get; }
        INursingCareRepository NursingCareRepository { get; }
        IDoctorsNoteRepository DoctorsNoteRepository { get; }
        IPinSearchRepository PinSearchRepository { get; }
        IDischargeRepository DischargeRepository { get; }
        IDischargeStatusRepository DischargeStatusRepository { get; }
        IInterReferralsRepository InterReferralsRepository { get; }
        IDeathCertificateRepository DeathCertificateRepository { get; }
        ISurgeriesRepository SurgeriesRepository { get; }
        IPostSurgeriesReposity PostSurgeriesReposity { get; }
        ISurgicalProceduresRepository SurgicalProceduresRepository { get; }
        ISurgeryTypesRepository SurgeryTypesRepository { get; }
        IProceduresRepository ProceduresRepository { get; }
        ILanguageRepository LanguageRepository { get; }
        IComplaintsRepository ComplaintsRepository { get; }
        IAllergiesRepository AllergiesRepository { get; }
        INcdsRepository NcdsRepository { get; }
        IDiagonosisExamimationsRepository DiagonosisExamimationsRepository { get; }
        IPatientExaminationsRepository PatientExaminationsRepository { get; }
        ILogin Login { get; }
        IDepartmentRepository DepartmentRepository { get; }
        IIntervalRepository IntervalRepository { get; }
        IMedicationRepository MedicationRepository { get; }
        IDirectionRepository DirectionRepository { get; }
        IMedicationPlanRepository MedicationPlanRepository { get; }
        IPatientsNcdRepository PatientsNcdRepository { get; }
        IPatientAllergyRepository PatientAllergyRepository { get; }
        IICDDiagonosisCodeRepository ICDDiagonosisCodeRepository { get; }
        IPatientDiagnosisRepository PatientDiagnosisRepository { get; }
        ITreatmentPlansRepository TreatmentPlansRepository { get; }
        IPartographRepository PartographRepository { get; }
        IInternationalReferralRepository InternationalReferralRepository { get; }
        IDiagonosisDetailRepository DiagonosisDetailRepository { get; }
        IFetalHeartRatesRepository FetalHeartRatesRepository { get; }
        ILiquorsRepository LiquorsRepository { get; }
        IMouldingsRepository MouldingsRepository { get; }
        ICervixRepository CervixRepository { get; }
        IDescentOfHeadsRepository DescentOfHeadsRepository { get; }
        IContractionsRepository ContractionsRepository { get; }
        IOxytocinRepository OxytocinRepository { get; }
        IDropsRepository DropsRepository { get; }
        IMedicinesRepository MedicinesRepository { get; }
        IPulseRepository PulseRepository { get; }
        IBloodPressureRepository BloodPressureRepository { get; }
        ITemperaturesRepository TemperaturesRepository { get; }
        IProteinsRepository ProteinsRepository { get; }
        IAcetonesRepository AcetonesRepository { get; }
        IVolumesRepository VolumesRepository { get; }
        IExaminationDetailsRepository ExaminationDetailsRepository { get; }
        IBirthDetailsRepository BirthDetailsRepository { get; }
        IPartographDetailsRepository PartographDetailsRepository { get; }
        IReportRepository ReportRepository { get; }
        
    }
}