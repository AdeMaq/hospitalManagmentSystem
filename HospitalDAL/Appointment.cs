using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL
{
    public class Appointment
    {
        int appointmentId;
        public int AppointmentId { get; set; }

        int patientId;
        public int PatientId { get; set; }

        int doctorId;
        public int DoctorId { get; set; }

        DateTime appointmentDate;
        public DateTime AppointmentDate { get; set; }
    }
}
