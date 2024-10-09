using System;

namespace HospitalDAL
{
    public class Patient
    {
        int patientID;
        public int PatientId { get; set; }

        string name;
        public string Name { get; set; }

        string email;
        public string Email { get; set; }

        string disease;
        public string Disease { get; set; }
    }
}
