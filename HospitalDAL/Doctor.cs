using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL
{
    public class Doctor
    {
        int doctorId;
        public int DoctorId { get; set; }

        string name;
        public string Name { get; set; }

        string specialization;
        public string Specialization { get; set; }
    }
}
