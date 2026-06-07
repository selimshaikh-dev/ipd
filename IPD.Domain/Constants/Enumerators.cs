using System.ComponentModel.DataAnnotations;

namespace IPD.Domain.Constants
{
    public class Enumerators
    {
        public enum RowStatus : byte
        {
            Deleted = 0,
            Active = 1,
            Inactive = 2,
            Archived = 3
        }
        public enum UserType : byte
        {
            [Display(Name = "General User")]
            GeneralUser = 1,

            [Display(Name = "Administrator")]
            Administrator = 2,

            [Display(Name = "HMIS Analyst")]
            HMISAnalyst = 3,

            [Display(Name = "Facility Champion")]
            FacilityChampion = 4
        }
        public enum RowSyncStatus : byte
        {
            Synced = 0,
            NotSynced = 1
        }
        public enum MaritalStatuses : byte
        {
            Single = 1, Married, Widowed, Divorced, Separated
        }

        public enum Sex : byte
        {
            Male = 1, Female = 2, Unknown = 3
        }

        public enum Apparatus : byte
        {
            Stretcher = 1, Sitting
        }

        public enum PatientsConditions : byte
        {
            Stable = 1,
            Unstable = 2,
            Dead = 3
        }

        public enum PatientsTransferApparatus : byte
        {
            Stretcher = 1,
            Sitting = 2
        }

        public enum ReferralTypeEnum : byte
        {
            [Display(Name = "International Referral")]
            InternationalReferral = 1,

            [Display(Name = "Local Referral")]
            LocalReferral = 2
        }

        public enum Diabetes : byte
        {
            Yes = 1,
            No = 2
        }
        public enum Hypertention : byte
        {
            Yes = 1,
            No = 2
        }
        public enum Epilepsy : byte
        {
            Yes = 1,
            No = 2
        }

        public enum Gravida : byte
        {
            
        }

        public enum IsSuccessfulDelivery : byte
        {
            Yes = 1,
            No = 0
        }

        public enum TypeOfDelivery : byte
        {
            [Display(Name = "Vaginal delivery")]
            VaginalDelivery = 1,

            [Display(Name = "Cesarian section")]
            CesarianSection = 2,

            [Display(Name = "Vacuum extraction")]
            VacuumExtraction = 3
        }
        public enum UserAccessModule
        {
            Admissions = 1,
            ChiefComplaints = 2,
            Clients = 3,
            DeathCertificates = 4,
            DiabeticProfile = 5,
            Diagnosis = 6,
            Discharge = 7,
            DoctorsNote = 8,
            InternationalReferral = 9,
            InterReferrals = 10,
            MedicationPlan = 11,
            NursingCare = 12,
            Partograph = 13,
            PatientExaminations = 14,
            PostSurgeries = 15,
            Referral = 16,
            Surgeries = 17,
            TreatmentPlan = 18,
            Users = 19,
            Vitals = 20,
        }
    }
}