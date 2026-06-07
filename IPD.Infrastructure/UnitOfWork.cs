using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Repositories;
using IPD.Infrastructure.Sql;
using Microsoft.Extensions.Configuration;

namespace IPD.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly DataContext dbcontext;
        private readonly IConfiguration configuration;

        public UnitOfWork(DataContext context, IConfiguration configuration)
        {
            this.dbcontext = context;
            this.configuration = configuration;
        }

        public void SaveChanges()
        {
            dbcontext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await dbcontext.SaveChangesAsync();
        }

        #region TinkhundlaRepository

        private ITinkhundlaRepository? tinkhundlaRepository;

        public ITinkhundlaRepository TinkhundlaRepository
        {
            get
            {
                if (tinkhundlaRepository == null)
                    tinkhundlaRepository = new TinkhundlaRepository(dbcontext);

                return tinkhundlaRepository;
            }
        }

        #endregion TinkhundlaRepository

        #region ChiefdomsRepository

        private IChiefdomsRepository? chiefdomsRepository;

        public IChiefdomsRepository ChiefdomsRepository
        {
            get
            {
                if (chiefdomsRepository == null)
                    chiefdomsRepository = new ChiefdomsRepository(dbcontext);

                return chiefdomsRepository;
            }
        }

        #endregion ChiefdomsRepository

        #region CountriesRepository

        private ICountriesRepository? countriesRepository;

        public ICountriesRepository CountriesRepository
        {
            get
            {
                if (countriesRepository == null)
                    countriesRepository = new CountriesRepository(dbcontext);

                return countriesRepository;
            }
        }

        #endregion CountriesRepository

        #region PatientsRepository

        private IPatientsRepository? patientsRepository;

        public IPatientsRepository PatientsRepository
        {
            get
            {
                if (patientsRepository == null)
                    patientsRepository = new PatientsRepository(dbcontext);

                return patientsRepository;
            }
        }

        #endregion PatientsRepository

        #region FacilityRepository

        private IFacilityRepository? facilityRepository;

        public IFacilityRepository FacilityRepository
        {
            get
            {
                if (facilityRepository == null)
                    facilityRepository = new FacilityRepository(dbcontext);

                return facilityRepository;
            }
        }

        #endregion FacilityRepository

        #region UserAccountRepository

        private IUserAccountRepository? userAccountRepository;

        public IUserAccountRepository UserAccountRepository
        {
            get
            {
                if (userAccountRepository == null)
                    userAccountRepository = new UserAccountRepository(dbcontext);

                return userAccountRepository;
            }
        }

        #endregion UserAccountRepository

        #region RegionRepository

        private IRegionRepository? regionRepository;

        public IRegionRepository RegionRepository
        {
            get
            {
                if (regionRepository == null)
                    regionRepository = new RegionRepository(dbcontext);

                return regionRepository;
            }
        }

        #endregion RegionRepository

        #region UserRightRepository

        private IUserRightRepository? userRightRepository;

        public IUserRightRepository UserRightRepository
        {
            get
            {
                if (userRightRepository == null)
                    userRightRepository = new UserRightRepository(dbcontext);

                return userRightRepository;
            }
        }

        #endregion UserRightRepository

        #region RecoveryRequestRepository

        private IRecoveryRequestRepository? recoveryRequestRepository;

        public IRecoveryRequestRepository RecoveryRequestRepository
        {
            get
            {
                if (recoveryRequestRepository == null)
                    recoveryRequestRepository = new RecoveryRequestRepository(dbcontext);

                return recoveryRequestRepository;
            }
        }

        #endregion RecoveryRequestRepository

        #region AdmissionRepository

        private IAdmissionRepository? admissionRepository;

        IAdmissionRepository IUnitOfWork.AdmissionRepository
        {
            get
            {
                if (admissionRepository == null)
                    admissionRepository = new AdmissionRepository(dbcontext);

                return admissionRepository;
            }
        }

        #endregion AdmissionRepository

        #region VitalRepository

        private IVitalRepository? vitalRepository;

        public IVitalRepository VitalRepository
        {
            get
            {
                if (vitalRepository == null)
                    vitalRepository = new VitalRepository(dbcontext);

                return vitalRepository;
            }
        }

        #endregion VitalRepository

        #region DiabeticProfileRepository

        private IDiabeticProfileRepository? diabeticProfileRepository;

        public IDiabeticProfileRepository DiabeticProfileRepository
        {
            get
            {
                if (diabeticProfileRepository == null)
                    diabeticProfileRepository = new DiabeticProfileRepository(dbcontext);

                return diabeticProfileRepository;
            }
        }

        #endregion DiabeticProfileRepository

        #region NursingCareRepository

        private INursingCareRepository? nursingCareRepository;

        public INursingCareRepository NursingCareRepository
        {
            get
            {
                if (nursingCareRepository == null)
                    nursingCareRepository = new NursingCareRepository(dbcontext);

                return nursingCareRepository;
            }
        }

        #endregion NursingCareRepository

        #region DoctorsNoteRepository

        private IDoctorsNoteRepository? doctorsNoteRepository;

        public IDoctorsNoteRepository DoctorsNoteRepository
        {
            get
            {
                if (doctorsNoteRepository == null)
                    doctorsNoteRepository = new DoctorsNoteRepository(dbcontext);

                return doctorsNoteRepository;
            }
        }

        #endregion DoctorsNoteRepository

        #region PrescriptionsRepository

        private IPrescriptionsRepository? prescriptionsRepository;

        public IPrescriptionsRepository PrescriptionsRepository
        {
            get
            {
                if (prescriptionsRepository == null)
                    prescriptionsRepository = new PrescriptionsRepository(dbcontext);

                return prescriptionsRepository;
            }
        }

        #endregion PrescriptionsRepository

        #region Login

        private ILogin? login;

        public ILogin Login
        {
            get
            {
                if (login == null)
                    login = new Login(dbcontext);

                return login;
            }
        }

        #endregion Login

        #region PinSearchRepository

        private IPinSearchRepository? pinSearchRepository;

        public IPinSearchRepository PinSearchRepository
        {
            get
            {
                if (pinSearchRepository == null)
                    pinSearchRepository = new PinSearchRepository(configuration);

                return pinSearchRepository;
            }
        }

        #endregion PinSearchRepository

        #region DischargeRepository

        private DischargeRepository? dischargeRepository;

        public IDischargeRepository DischargeRepository
        {
            get
            {
                if (dischargeRepository == null)
                {
                    dischargeRepository = new DischargeRepository(dbcontext);
                }

                return dischargeRepository;
            }
        }

        #endregion DischargeRepository

        #region DischargeStatusRepository

        private IDischargeStatusRepository? dischargeStatusesRepository;

        public IDischargeStatusRepository DischargeStatusRepository
        {
            get
            {
                if (dischargeStatusesRepository == null)
                {
                    dischargeStatusesRepository = new DischargeStatusRepository(dbcontext);
                }

                return dischargeStatusesRepository;
            }
        }

        #endregion DischargeStatusRepository

        #region InterReferralsRepository

        private IInterReferralsRepository? interReferralsRepository;

        public IInterReferralsRepository InterReferralsRepository
        {
            get
            {
                if (interReferralsRepository == null)
                    interReferralsRepository = new InterReferralsRepository(dbcontext);

                return interReferralsRepository;
            }
        }

        #endregion InterReferralsRepository

        #region DeathCertificateRepository

        private IDeathCertificateRepository? deathCertificateRepository;

        public IDeathCertificateRepository DeathCertificateRepository
        {
            get
            {
                if (deathCertificateRepository == null)
                    deathCertificateRepository = new DeathCertificateRepository(dbcontext);

                return deathCertificateRepository;
            }
        }

        #endregion DeathCertificateRepository

        #region SurgeriesRepository

        private ISurgeriesRepository? surgeriesRepository;

        public ISurgeriesRepository SurgeriesRepository
        {
            get
            {
                if (surgeriesRepository == null)
                    surgeriesRepository = new SurgeriesRepository(dbcontext);

                return surgeriesRepository;
            }
        }

        #endregion SurgeriesRepository

        #region PostSurgeriesReposity

        private IPostSurgeriesReposity? postSurgeriesReposity;

        public IPostSurgeriesReposity PostSurgeriesReposity
        {
            get
            {
                if (postSurgeriesReposity == null)
                    postSurgeriesReposity = new PostSurgeriesReposity(dbcontext);

                return postSurgeriesReposity;
            }
        }

        #endregion PostSurgeriesReposity

        #region SurgeryTypesRepository

        private ISurgeryTypesRepository? surgeryTypesRepository;

        public ISurgeryTypesRepository SurgeryTypesRepository
        {
            get
            {
                if (surgeryTypesRepository == null)
                    surgeryTypesRepository = new SurgeryTypesRepository(dbcontext);

                return surgeryTypesRepository;
            }
        }

        #endregion SurgeryTypesRepository

        #region SurgicalProceduresRepository

        private ISurgicalProceduresRepository? surgicalProceduresRepository;

        public ISurgicalProceduresRepository SurgicalProceduresRepository
        {
            get
            {
                if (surgicalProceduresRepository == null)
                    surgicalProceduresRepository = new SurgicalProceduresRepository(dbcontext);

                return surgicalProceduresRepository;
            }
        }

        #endregion SurgicalProceduresRepository

        #region ProceduresRepository

        private IProceduresRepository? proceduresRepository;

        public IProceduresRepository ProceduresRepository
        {
            get
            {
                if (proceduresRepository == null)
                    proceduresRepository = new ProceduresRepository(dbcontext);

                return proceduresRepository;
            }
        }

        #endregion ProceduresRepository

        #region DepartmentRepository

        private IDepartmentRepository? departmentRepository;

        public IDepartmentRepository DepartmentRepository
        {
            get
            {
                if (departmentRepository == null)
                    departmentRepository = new DepartmentRepository(dbcontext);

                return departmentRepository;
            }
        }

        #endregion DepartmentRepository

        #region ComplaintsRepository

        private IComplaintsRepository? complaintsRepository;

        public IComplaintsRepository ComplaintsRepository
        {
            get
            {
                if (complaintsRepository == null)
                    complaintsRepository = new ComplaintsRepository(dbcontext);

                return complaintsRepository;
            }
        }

        #endregion ComplaintsRepository

        #region LanguageRepository

        private ILanguageRepository? languageRepository;

        public ILanguageRepository LanguageRepository
        {
            get
            {
                if (languageRepository == null)
                    languageRepository = new LanguageRepository(dbcontext);

                return languageRepository;
            }
        }

        #endregion LanguageRepository

        #region AllergiesRepository

        private IAllergiesRepository? allergiesRepository;

        public IAllergiesRepository AllergiesRepository
        {
            get
            {
                if (allergiesRepository == null)
                    allergiesRepository = new AllergiesRepository(dbcontext);

                return allergiesRepository;
            }
        }

        #endregion AllergiesRepository

        #region NcdsRepository

        private INcdsRepository? ncdsRepository;

        public INcdsRepository NcdsRepository
        {
            get
            {
                if (ncdsRepository == null)
                    ncdsRepository = new NcdsRepository(dbcontext);

                return ncdsRepository;
            }
        }

        #endregion NcdsRepository

        #region DiagonosisExamimationsRepository

        private IDiagonosisExamimationsRepository? diagonosisExamimationsRepository;

        public IDiagonosisExamimationsRepository DiagonosisExamimationsRepository
        {
            get
            {
                if (diagonosisExamimationsRepository == null)
                    diagonosisExamimationsRepository = new DiagonosisExamimationsRepository(dbcontext);

                return diagonosisExamimationsRepository;
            }
        }

        #endregion DiagonosisExamimationsRepository

        #region MedicationRepository

        private IMedicationRepository? medicationRepository;

        public IMedicationRepository MedicationRepository
        {
            get
            {
                if (medicationRepository == null)
                    medicationRepository = new MedicationRepository(dbcontext);

                return medicationRepository;
            }
        }

        #endregion MedicationRepository

        #region IntervalRepository

        private IIntervalRepository? intervalRepository;

        public IIntervalRepository IntervalRepository
        {
            get
            {
                if (intervalRepository == null)
                    intervalRepository = new IntervalRepository(dbcontext);

                return intervalRepository;
            }
        }

        #endregion IntervalRepository

        #region DirectionRepository

        private IDirectionRepository? directionRepository;

        public IDirectionRepository DirectionRepository
        {
            get
            {
                if (directionRepository == null)
                    directionRepository = new DirectionRepository(dbcontext);

                return directionRepository;
            }
        }

        #endregion DirectionRepository

        #region MedicationPlanRepository

        private IMedicationPlanRepository? medicationPlanRepository;

        public IMedicationPlanRepository MedicationPlanRepository
        {
            get
            {
                if (medicationPlanRepository == null)
                    medicationPlanRepository = new MedicationPlanRepository(dbcontext);

                return medicationPlanRepository;
            }
        }

        #endregion MedicationPlanRepository

        #region PatientExaminationsRepository

        private IPatientExaminationsRepository? patientExaminationsRepository;

        public IPatientExaminationsRepository PatientExaminationsRepository
        {
            get
            {
                if (patientExaminationsRepository == null)
                    patientExaminationsRepository = new PatientExaminationRepository(dbcontext);

                return patientExaminationsRepository;
            }
        }

        private IPatientsNcdRepository? patientsNcdRepository;

        public IPatientsNcdRepository PatientsNcdRepository
        {
            get
            {
                if (patientsNcdRepository == null)
                    patientsNcdRepository = new PatientsNcdRepository(dbcontext);

                return patientsNcdRepository;
            }
        }

        private IPatientAllergyRepository? patientAllergyRepository;

        public IPatientAllergyRepository PatientAllergyRepository
        {
            get
            {
                if (patientAllergyRepository == null)
                    patientAllergyRepository = new PatientAllergyRepository(dbcontext);

                return patientAllergyRepository;
            }
        }

        #endregion PatientExaminationsRepository

        #region ICDDiagonosisCodeRepository

        private ICDDiagonosisCodeRepository? iCDDiagonosisCodeRepository;

        public IICDDiagonosisCodeRepository ICDDiagonosisCodeRepository
        {
            get
            {
                if (iCDDiagonosisCodeRepository == null)
                    iCDDiagonosisCodeRepository = new ICDDiagonosisCodeRepository(dbcontext);

                return iCDDiagonosisCodeRepository;
            }
        }

        #endregion ICDDiagonosisCodeRepository

        #region PatientDiagnosisRepository

        private IPatientDiagnosisRepository? patientDiagnosisRepository;

        public IPatientDiagnosisRepository PatientDiagnosisRepository
        {
            get
            {
                if (patientDiagnosisRepository == null)
                    patientDiagnosisRepository = new PatientDiagnosisRepository(dbcontext);

                return patientDiagnosisRepository;
            }
        }

        #endregion PatientDiagnosisRepository

        #region PatientDetailsRepository

        //private IPatientDetailsRepository? patientDetailsRepository;
        //public IPatientDetailsRepository PatientDetailsRepository
        //{
        //    get
        //    {
        //        if (patientDetailsRepository == null)
        //            patientDetailsRepository = new PatientDetailsRepository(dbcontext);

        //        return patientDetailsRepository;
        //    }
        //}

        #endregion PatientDetailsRepository

        #region TreatmentPlansRepository

        private ITreatmentPlansRepository? treatmentPlansRepository;

        public ITreatmentPlansRepository TreatmentPlansRepository
        {
            get
            {
                if (treatmentPlansRepository == null)
                    treatmentPlansRepository = new TreatmentPlansRepository(dbcontext);

                return treatmentPlansRepository;
            }
        }

        #endregion TreatmentPlansRepository

        #region PartographRepository

        private IPartographRepository partographRepository;

        public IPartographRepository PartographRepository
        {
            get
            {
                if (partographRepository == null)
                    partographRepository = new PartographRepository(dbcontext);

                return partographRepository;
            }
        }

        #endregion PartographRepository

        #region DiagonosisDetailRepository

        private IDiagonosisDetailRepository diagonosisDetailRepository;

        public IDiagonosisDetailRepository DiagonosisDetailRepository
        {
            get
            {
                if (diagonosisDetailRepository == null)
                    diagonosisDetailRepository = new DiagonosisDetailRepository(dbcontext);

                return diagonosisDetailRepository;
            }
        }

        #endregion DiagonosisDetailRepository

        #region InterationalReferralRepository

        private IInternationalReferralRepository internationalReferralRepository;

        public IInternationalReferralRepository InternationalReferralRepository
        {
            get
            {
                if (internationalReferralRepository == null)
                    internationalReferralRepository = new InternationalReferralRepository(dbcontext);

                return internationalReferralRepository;
            }
        }

        #endregion InterationalReferralRepository

        #region FetalHeartRatesRepository

        private IFetalHeartRatesRepository fetalHeartRatesRepository;

        public IFetalHeartRatesRepository FetalHeartRatesRepository
        {
            get
            {
                if (fetalHeartRatesRepository == null)
                    fetalHeartRatesRepository = new FetalHeartRatesRepository(dbcontext);

                return fetalHeartRatesRepository;
            }
        }

        #endregion FetalHeartRatesRepository

        #region LiquorsRepository

        private ILiquorsRepository liquorsRepository;

        public ILiquorsRepository LiquorsRepository
        {
            get
            {
                if (liquorsRepository == null)
                    liquorsRepository = new LiquorsRepository(dbcontext);

                return liquorsRepository;
            }
        }

        #endregion LiquorsRepository

        #region MouldingsRepository

        private IMouldingsRepository mouldingsRepository;

        public IMouldingsRepository MouldingsRepository
        {
            get
            {
                if (mouldingsRepository == null)
                    mouldingsRepository = new MouldingsRepository(dbcontext);

                return mouldingsRepository;
            }
        }

        #endregion MouldingsRepository

        #region CervixRepository

        private ICervixRepository crvixRepository;

        public ICervixRepository CervixRepository
        {
            get
            {
                if (crvixRepository == null)
                    crvixRepository = new CervixRepository(dbcontext);

                return crvixRepository;
            }
        }

        #endregion CervixRepository

        #region DescentOfHeadsRepository

        private IDescentOfHeadsRepository descentOfHeadsRepository;

        public IDescentOfHeadsRepository DescentOfHeadsRepository
        {
            get
            {
                if (descentOfHeadsRepository == null)
                    descentOfHeadsRepository = new DescentOfHeadsRepository(dbcontext);

                return descentOfHeadsRepository;
            }
        }

        #endregion DescentOfHeadsRepository

        #region ContractionsRepository

        private IContractionsRepository contractionsRepository;

        public IContractionsRepository ContractionsRepository
        {
            get
            {
                if (contractionsRepository == null)
                    contractionsRepository = new ContractionsRepository(dbcontext);

                return contractionsRepository;
            }
        }

        #endregion ContractionsRepository

        #region OxytocinRepository

        private IOxytocinRepository oxytocinRepository;

        public IOxytocinRepository OxytocinRepository
        {
            get
            {
                if (oxytocinRepository == null)
                    oxytocinRepository = new OxytocinRepository(dbcontext);

                return oxytocinRepository;
            }
        }

        #endregion OxytocinRepository

        #region DropsRepository

        private IDropsRepository dropsRepository;

        public IDropsRepository DropsRepository
        {
            get
            {
                if (dropsRepository == null)
                    dropsRepository = new DropsRepository(dbcontext);

                return dropsRepository;
            }
        }

        #endregion DropsRepository

        #region MedicinesRepository

        private IMedicinesRepository medicinesRepository;

        public IMedicinesRepository MedicinesRepository
        {
            get
            {
                if (medicinesRepository == null)
                    medicinesRepository = new MedicinesRepository(dbcontext);

                return medicinesRepository;
            }
        }

        #endregion MedicinesRepository

        #region PulseRepository

        private IPulseRepository pulseRepository;

        public IPulseRepository PulseRepository
        {
            get
            {
                if (pulseRepository == null)
                    pulseRepository = new PulseRepository(dbcontext);

                return pulseRepository;
            }
        }

        #endregion PulseRepository

        #region BloodPressureRepository

        private IBloodPressureRepository bloodPressureRepository;

        public IBloodPressureRepository BloodPressureRepository
        {
            get
            {
                if (bloodPressureRepository == null)
                    bloodPressureRepository = new BloodPressureRepository(dbcontext);

                return bloodPressureRepository;
            }
        }

        #endregion BloodPressureRepository

        #region TemperaturesRepository

        private ITemperaturesRepository temperaturesRepository;

        public ITemperaturesRepository TemperaturesRepository
        {
            get
            {
                if (temperaturesRepository == null)
                    temperaturesRepository = new TemperaturesRepository(dbcontext);

                return temperaturesRepository;
            }
        }

        #endregion TemperaturesRepository

        #region ProteinsRepository

        private IProteinsRepository proteinsRepository;

        public IProteinsRepository ProteinsRepository
        {
            get
            {
                if (proteinsRepository == null)
                    proteinsRepository = new ProteinsRepository(dbcontext);

                return proteinsRepository;
            }
        }

        #endregion ProteinsRepository

        #region AcetonesRepository

        private IAcetonesRepository acetonesRepository;

        public IAcetonesRepository AcetonesRepository
        {
            get
            {
                if (acetonesRepository == null)
                    acetonesRepository = new AcetonesRepository(dbcontext);

                return acetonesRepository;
            }
        }

        #endregion AcetonesRepository

        #region VolumesRepository

        private IVolumesRepository volumesRepository;

        public IVolumesRepository VolumesRepository
        {
            get
            {
                if (volumesRepository == null)
                    volumesRepository = new VolumesRepository(dbcontext);

                return volumesRepository;
            }
        }

        #endregion VolumesRepository

        #region ExaminationDetailsRepository

        private IExaminationDetailsRepository examinationDetailsRepository;

        public IExaminationDetailsRepository ExaminationDetailsRepository
        {
            get
            {
                if (examinationDetailsRepository == null)
                    examinationDetailsRepository = new ExaminationDetailsRepository(dbcontext);
                return examinationDetailsRepository;
            }
        }

        #endregion ExaminationDetailsRepository

        #region BirthDetailsRepository

        private IBirthDetailsRepository birthDetailsRepository;

        public IBirthDetailsRepository BirthDetailsRepository
        {
            get
            {
                if (birthDetailsRepository == null)
                    birthDetailsRepository = new BirthDetailsRepository(dbcontext);
                return birthDetailsRepository;
            }
        }

        #endregion BirthDetailsRepository

        #region PartographDetailsRepository
        private IPartographDetailsRepository? partographDetailsRepository;
        public IPartographDetailsRepository PartographDetailsRepository
        {
            get
            {
                if (partographDetailsRepository == null)
                    partographDetailsRepository = new PartographDetailsRepository(dbcontext);

                return partographDetailsRepository;
            }
        }
        #endregion PartographDetailsRepository
        #region ReportRepository
        private IReportRepository reportRepository;
        public IReportRepository ReportRepository
        {
            get
            {
                if (reportRepository == null)
                    reportRepository = new ReportRepository(dbcontext);

                return reportRepository;
            }
        }
        #endregion
    }
}
