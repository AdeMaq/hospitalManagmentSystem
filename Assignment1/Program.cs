using System;
using System.Collections.Generic;
using System.IO;
using HospitalDAL;

namespace Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            HospitalRepository repository = new HospitalRepository();
            bool running = true;

            while (running)
            {
                Console.WriteLine("=== Hospital Management System ===");
                Console.WriteLine("1. Add a new patient");
                Console.WriteLine("2. Update a patient");
                Console.WriteLine("3. Delete a patient");
                Console.WriteLine("4. Search for patients by name");
                Console.WriteLine("5. View all patients");
                Console.WriteLine("6. Add a new doctor");
                Console.WriteLine("7. Update a doctor");
                Console.WriteLine("8. Delete a doctor");
                Console.WriteLine("9. Search for doctors by specialization");
                Console.WriteLine("10. View all doctors");
                Console.WriteLine("11. Book an appointment");
                Console.WriteLine("12. View all appointments");
                Console.WriteLine("13. Search appointments by doctor or patient");
                Console.WriteLine("14. Cancel an appointment");
                Console.WriteLine("15. View history of deleted records");
                Console.WriteLine("16. Exit the application");
                Console.Write("Enter option:");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddNewPatient(repository);
                        break;
                    case "2":
                        UpdatePatient(repository);
                        break;
                    case "3":
                        DeletePatient(repository);
                        break;
                    case "4":
                        SearchPatients(repository);
                        break;
                    case "5":
                        ViewAllPatients(repository);
                        break;
                    case "6":
                        AddNewDoctor(repository);
                        break;
                    case "7":
                        UpdateDoctor(repository);
                        break;
                    case "8":
                        DeleteDoctor(repository);
                        break;
                    case "9":
                        SearchDoctors(repository);
                        break;
                    case "10":
                        ViewAllDoctors(repository);
                        break;
                    case "11":
                        BookAppointment(repository);
                        break;
                    case "12":
                        ViewAllAppointments(repository);
                        break;
                    case "13":
                        SearchAppointments(repository);
                        break;
                    case "14":
                        CancelAppointment(repository);
                        break;
                    case "15":
                        ViewDeletedRecords();
                        break;
                    case "16":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        private static void AddNewPatient(HospitalRepository repository)
        {
            Patient patient = new Patient();
            Console.Write("Enter Name: ");
            patient.Name = Console.ReadLine();
            Console.Write("Enter Email: ");
            patient.Email = Console.ReadLine();
            Console.Write("Enter Disease: ");
            patient.Disease = Console.ReadLine();
            repository.InsertPatient(patient);
            Console.WriteLine("Patient added successfully.");
        }

        private static void UpdatePatient(HospitalRepository repository)
        {
            Patient patient = new Patient();
            Console.Write("Enter Patient ID to update: ");
            patient.PatientId = int.Parse(Console.ReadLine());
            Console.Write("Enter New Name: ");
            patient.Name = Console.ReadLine();
            Console.Write("Enter New Email: ");
            patient.Email = Console.ReadLine();
            Console.Write("Enter New Disease: ");
            patient.Disease = Console.ReadLine();
            repository.UpdatePatientInDatabase(patient);
            Console.WriteLine("Patient updated successfully.");
        }

        private static void DeletePatient(HospitalRepository repository)
        {
            Console.Write("Enter Patient ID to delete: ");
            int patientId = int.Parse(Console.ReadLine());
            repository.DeletePatientFromDatabase(patientId);
        }

        private static void SearchPatients(HospitalRepository repository)
        {
            Console.Write("Enter name to search for patients: ");
            string name = Console.ReadLine();
            List<Patient> patients = repository.SearchPatientsInDatabase(name);
            foreach (var patient in patients)
            {
                Console.WriteLine($"ID: {patient.PatientId}, Name: {patient.Name}, Email: {patient.Email}, Disease: {patient.Disease}");
            }
        }

        private static void ViewAllPatients(HospitalRepository repository)
        {
            List<Patient> patients = repository.GetAllPatientsFromDatabase();
            foreach (var patient in patients)
            {
                Console.WriteLine($"ID: {patient.PatientId}, Name: {patient.Name}, Email: {patient.Email}, Disease: {patient.Disease}");
            }
        }

        private static void AddNewDoctor(HospitalRepository repository)
        {
            Doctor doctor = new Doctor();
            Console.Write("Enter Name: ");
            doctor.Name = Console.ReadLine();
            Console.Write("Enter Specialization: ");
            doctor.Specialization = Console.ReadLine();
            repository.InsertDoctor(doctor);
            Console.WriteLine("Doctor added successfully.");
        }

        private static void UpdateDoctor(HospitalRepository repository)
        {
            Doctor doctor = new Doctor();
            Console.Write("Enter Doctor ID to update: ");
            doctor.DoctorId = int.Parse(Console.ReadLine());
            Console.Write("Enter New Name: ");
            doctor.Name = Console.ReadLine();
            Console.Write("Enter New Specialization: ");
            doctor.Specialization = Console.ReadLine();
            repository.UpdateDoctorInDatabase(doctor);
            Console.WriteLine("Doctor updated successfully.");
        }

        private static void DeleteDoctor(HospitalRepository repository)
        {
            Console.Write("Enter Doctor ID to delete: ");
            int doctorId = int.Parse(Console.ReadLine());
            repository.DeleteDoctorFromDatabase(doctorId);
        }

        private static void SearchDoctors(HospitalRepository repository)
        {
            Console.Write("Enter specialization to search for doctors: ");
            string specialization = Console.ReadLine();
            List<Doctor> doctors = repository.SearchDoctorsInDatabase(specialization);
            foreach (var doctor in doctors)
            {
                Console.WriteLine($"ID: {doctor.DoctorId}, Name: {doctor.Name}, Specialization: {doctor.Specialization}");
            }
        }

        private static void ViewAllDoctors(HospitalRepository repository)
        {
            List<Doctor> doctors = repository.GetAllDoctorsFromDatabase();
            foreach (var doctor in doctors)
            {
                Console.WriteLine($"ID: {doctor.DoctorId}, Name: {doctor.Name}, Specialization: {doctor.Specialization}");
            }
        }

        private static void BookAppointment(HospitalRepository repository)
        {
            Appointment appointment = new Appointment();
            Console.Write("Enter Patient ID: ");
            appointment.PatientId = int.Parse(Console.ReadLine());
            Console.Write("Enter Doctor ID: ");
            appointment.DoctorId = int.Parse(Console.ReadLine());
            do
            {
                Console.Write("Enter Appointment Date (yyyy-mm-dd): ");
                appointment.AppointmentDate = DateTime.Parse(Console.ReadLine());
                if (appointment.AppointmentDate < DateTime.Now)
                {
                    Console.WriteLine("Appointment date must be in the future.");
                }
            } while (appointment.AppointmentDate < DateTime.Now);
            repository.InsertAppointment(appointment);
            Console.WriteLine("Appointment booked successfully.");
        }

        private static void ViewAllAppointments(HospitalRepository repository)
        {
            List<Appointment> appointments = repository.GetAllAppointmentsFromDatabase();
            foreach (var appointment in appointments)
            {
                Console.WriteLine($"ID: {appointment.AppointmentId}, Patient ID: {appointment.PatientId}, Doctor ID: {appointment.DoctorId}, Date: {appointment.AppointmentDate}");
            }
        }

        private static void SearchAppointments(HospitalRepository repository)
        {
            Console.Write("Enter doctor ID(or 0 to skip): ");
            int doctorId = int.Parse(Console.ReadLine());
            Console.Write("Enter patient ID(or 0 to skip): ");
            int patientId = int.Parse(Console.ReadLine());
            List<Appointment> appointments = repository.SearchAppointmentsInDatabase(doctorId, patientId);
            foreach (var appointment in appointments)
            {
                Console.WriteLine($"ID: {appointment.AppointmentId}, Patient ID: {appointment.PatientId}, Doctor ID: {appointment.DoctorId}, Date: {appointment.AppointmentDate}");
            }

        }

        private static void CancelAppointment(HospitalRepository repository)
        {
            Console.Write("Enter appointment ID to cancel: ");
            int id = int.Parse(Console.ReadLine());
            repository.DeleteAppointmentFromDatabase(id);

        }

        private static void ViewDeletedRecords()
        {
            Console.WriteLine("=== View Deleted Records ===");
            Console.WriteLine("1. View Deleted Patients");
            Console.WriteLine("2. View Deleted Doctors");
            Console.WriteLine("3. View Deleted Appointments");
            var choice = Console.ReadLine();
            string fileName = "";
            switch (choice)
            {
                case "1":
                    fileName = "DeletedPatients.txt";
                    break;
                case "2":
                    fileName = "DeletedDoctors.txt";
                    break;
                case "3":
                    fileName = "DeletedAppointments.txt";
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }
            if (File.Exists(fileName))
            {
                string[] deletedRecords = File.ReadAllLines(fileName);

                foreach (var record in deletedRecords)
                {
                    Console.WriteLine(record);
                }
            }
            else
            {
                Console.WriteLine("No deleted records found.");
            }
        }
    }

}