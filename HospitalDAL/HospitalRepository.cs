using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace HospitalDAL
{
    public class HospitalRepository
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hospital;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public void InsertPatient(Patient patient)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "INSERT INTO Patients (Name, Email, Disease) VALUES ( @Name, @Email, @Disease)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", patient.Name);
            command.Parameters.AddWithValue("@Email", patient.Email);
            command.Parameters.AddWithValue("@Disease", patient.Disease);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            connection.Close();
        }

        public void InsertDoctor(Doctor doctor)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "INSERT INTO Doctors ( Name, Specialization) VALUES ( @Name, @Specialization)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", doctor.Name);
            command.Parameters.AddWithValue("@Specialization", doctor.Specialization);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            connection.Close();
        }

        public void InsertAppointment(Appointment appointment)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "INSERT INTO Appointments (PatientId, DoctorId, AppointmentDate) VALUES (@PatientId, @DoctorId, @AppointmentDate)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PatientId", appointment.PatientId);
            command.Parameters.AddWithValue("@DoctorId", appointment.DoctorId);
            command.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            connection.Close();
        }


        public List<Patient> GetAllPatientsFromDatabase()
        {
            List<Patient> patients = new List<Patient>();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT PatientId, Name, Email, Disease FROM Patients";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Patient patient = new Patient
                    {
                        PatientId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Email = reader.GetString(2),
                        Disease = reader.GetString(3)
                    };
                    patients.Add(patient);
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            connection.Close();
            return patients;
        }

        public List<Doctor> GetAllDoctorsFromDatabase()
        {
            List<Doctor> doctors = new List<Doctor>();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT DoctorId, Name, Specialization FROM Doctors";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Doctor doctor = new Doctor
                    {
                        DoctorId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Specialization = reader.GetString(2)
                    };
                    doctors.Add(doctor);
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            connection.Close();

            return doctors;
        }

        public List<Appointment> GetAllAppointmentsFromDatabase()
        {
            List<Appointment> appointments = new List<Appointment>();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT AppointmentId, PatientId, DoctorId, AppointmentDate FROM Appointments";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Appointment appointment = new Appointment
                    {
                        AppointmentId = reader.GetInt32(0),
                        PatientId = reader.GetInt32(1),
                        DoctorId = reader.GetInt32(2),
                        AppointmentDate = reader.GetDateTime(3)
                    };
                    appointments.Add(appointment);
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            connection.Close();

            return appointments;
        }

        public void UpdatePatientInDatabase(Patient patient)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "UPDATE Patients SET Name = @Name, Email = @Email, Disease = @Disease WHERE PatientId = @PatientId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PatientId", patient.PatientId);
            command.Parameters.AddWithValue("@Name", patient.Name);
            command.Parameters.AddWithValue("@Email", patient.Email);
            command.Parameters.AddWithValue("@Disease", patient.Disease);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    Console.WriteLine("No patient found with the provided PatientId.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            connection.Close();
        }

        public void UpdateDoctorInDatabase(Doctor doctor)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UPDATE Doctors SET Name = @Name, Specialization = @Specialization WHERE DoctorId = @DoctorId", connection);

            command.Parameters.AddWithValue("@DoctorId", doctor.DoctorId);
            command.Parameters.AddWithValue("@Name", doctor.Name);
            command.Parameters.AddWithValue("@Specialization", doctor.Specialization);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    Console.WriteLine("No doctor found with the provided DoctorId.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            connection.Close();
        }

        public void UpdateAppointmentInDatabase(Appointment appointment)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "UPDATE Appointments SET PatientId = @PatientId, DoctorId = @DoctorId, AppointmentDate = @AppointmentDate WHERE AppointmentId = @AppointmentId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppointmentId", appointment.AppointmentId);
            command.Parameters.AddWithValue("@PatientId", appointment.PatientId);
            command.Parameters.AddWithValue("@DoctorId", appointment.DoctorId);
            command.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    Console.WriteLine("No appointment found with the provided AppointmentId.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            connection.Close();
        }

        public void DeletePatientFromDatabase(int patientId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string selectQuery = $"SELECT * FROM Patients WHERE PatientId = @PatientId";
            SqlCommand selectCommand = new SqlCommand(selectQuery, conn);
            selectCommand.Parameters.AddWithValue("@PatientId", patientId);
            try
            {
                conn.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.Read())
                {
                    Patient p = new Patient
                    {
                        PatientId = Convert.ToInt32(reader["PatientId"]),
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        Disease = reader["Disease"].ToString()
                    };
                    SaveToHistoryFile(p, "DeletedPatients.txt");
                }
                reader.Close();
                string deleteQuery = $"DELETE FROM Patients WHERE PatientId = @PatientId";
                SqlCommand deleteCommand = new SqlCommand(deleteQuery, conn);
                deleteCommand.Parameters.AddWithValue("@PatientId", patientId);
                int count = deleteCommand.ExecuteNonQuery();

                Console.WriteLine($"{count} record(s) deleted from Patients table.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            conn.Close();
        }
        public void DeleteDoctorFromDatabase(int doctorId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string selectQuery = $"SELECT * FROM Doctors WHERE DoctorId = @DoctorId";
            SqlCommand selectCommand = new SqlCommand(selectQuery, conn);
            selectCommand.Parameters.AddWithValue("@DoctorId", doctorId);
            try
            {
                conn.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.Read())
                {
                    Doctor d = new Doctor
                    {
                        DoctorId = Convert.ToInt32(reader["DoctorId"]),
                        Name = reader["Name"].ToString(),
                        Specialization = reader["Specialization"].ToString()
                    };
                    SaveToHistoryFile(d, "DeletedDoctors.txt");
                }
                reader.Close();
                string deleteQuery = $"DELETE FROM Doctors WHERE DoctorId = @DoctorId";
                SqlCommand deleteCommand = new SqlCommand(deleteQuery, conn);
                deleteCommand.Parameters.AddWithValue("@DoctorId", doctorId);
                int count = deleteCommand.ExecuteNonQuery();

                Console.WriteLine($"{count} record(s) deleted from Doctors table.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            conn.Close();
        }

        public void DeleteAppointmentFromDatabase(int appointmentId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string selectQuery = $"SELECT * FROM Appointments WHERE AppointmentId = @AppointmentId";
            SqlCommand selectCommand = new SqlCommand(selectQuery, conn);
            selectCommand.Parameters.AddWithValue("@AppointmentId", appointmentId);
            try
            {
                conn.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.Read())
                {
                    Appointment a = new Appointment
                    {
                        AppointmentId = Convert.ToInt32(reader["AppointmentId"]),
                        PatientId = Convert.ToInt32(reader["PatientId"]),
                        DoctorId = Convert.ToInt32(reader["DoctorId"]),
                        AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"])
                    };
                    SaveToHistoryFile(a, "DeletedAppointments.txt");
                }
                reader.Close();

                string deleteQuery = $"DELETE FROM Appointments WHERE AppointmentId = @AppointmentId";
                SqlCommand deleteCommand = new SqlCommand(deleteQuery, conn);
                deleteCommand.Parameters.AddWithValue("@AppointmentId", appointmentId);
                int count = deleteCommand.ExecuteNonQuery();

                Console.WriteLine($"{count} record(s) deleted from Appointments table.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            conn.Close();
        }

        private void SaveToHistoryFile<T>(T record, string fileName)
        {
            string jsonRecord = JsonSerializer.Serialize(record);
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(fileName, append: true);
                writer.WriteLine(jsonRecord);
                writer.WriteLine($"this file is Deleted on: {DateTime.Now}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"IO Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        public List<Patient> SearchPatientsInDatabase(string name)
        {
            List<Patient> patients = new List<Patient>();
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "SELECT PatientId, Name, Email, Disease FROM Patients WHERE Name LIKE @Name";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@Name", "%" + name + "%");
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Patient patient = new Patient
                    {
                        PatientId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Email = reader.GetString(2),
                        Disease = reader.GetString(3)
                    };
                    patients.Add(patient);
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            conn.Close();
            return patients;
        }
        public List<Doctor> SearchDoctorsInDatabase(string specialization)
        {
            List<Doctor> doctors = new List<Doctor>();
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "SELECT DoctorId, Name, Specialization FROM Doctors WHERE Specialization LIKE @Specialization";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@Specialization", "%" + specialization + "%");
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Doctor doctor = new Doctor
                    {
                        DoctorId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Specialization = reader.GetString(2)
                    };
                    doctors.Add(doctor);
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            conn.Close();

            return doctors;
        }

        public List<Appointment> SearchAppointmentsInDatabase(int doctorId, int patientId)
        {
            List<Appointment> appointments = new List<Appointment>();
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "SELECT AppointmentId, PatientId, DoctorId, AppointmentDate FROM Appointments WHERE 1=1";
            if (doctorId > 0)
            {
                query += " AND DoctorId = @DoctorId";
            }
            if (patientId > 0)
            {
                query += " AND PatientId = @PatientId";
            }
            SqlCommand command = new SqlCommand(query, conn);
            if (doctorId > 0)
            {
                command.Parameters.AddWithValue("@DoctorId", doctorId);
            }

            if (patientId > 0)
            {
                command.Parameters.AddWithValue("@PatientId", patientId);
            }
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Appointment appointment = new Appointment
                    {
                        AppointmentId = reader.GetInt32(0),
                        PatientId = reader.GetInt32(1),
                        DoctorId = reader.GetInt32(2),
                        AppointmentDate = reader.GetDateTime(3)
                    };
                    appointments.Add(appointment);
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            conn.Close();
            return appointments;
        }

    }
}
