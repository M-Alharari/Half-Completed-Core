using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SchoolProjectData
{
    public static class clsStudentsAttendanceData
    {
        public static DataTable GetStudentAttendanceSummary(int enrollmentID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FullName", typeof(string));
            dt.Columns.Add("TotalDays", typeof(int));
            dt.Columns.Add("DaysPresent", typeof(int));
            dt.Columns.Add("LastDayPresent", typeof(string));
            dt.Columns.Add("AttendancePercentage", typeof(double));

            string query = @"
        SELECT 
            CONCAT(p.FirstName, ' ', 
                   ISNULL(p.SecondName, ''), ' ',
                   ISNULL(p.ThirdName, ''), ' ',
                   p.LastName) AS FullName,
            COUNT(a.AttendanceID) AS TotalDays,
            SUM(CASE WHEN a.IsPresent = 1 THEN 1 ELSE 0 END) AS DaysPresent,
            MAX(CASE WHEN a.IsPresent = 1 THEN a.AttendanceDate END) AS LastDayPresent,
            (CAST(SUM(CASE WHEN a.IsPresent = 1 THEN 1 ELSE 0 END) AS FLOAT) 
                / NULLIF(COUNT(a.AttendanceID), 0)) * 100 AS AttendancePercentage
        FROM Enrollments e
        INNER JOIN Students s ON e.StudentID = s.StudentID
        INNER JOIN People p ON s.PersonID = p.PersonID
        LEFT JOIN StudentAttendance a 
               ON e.StudentID = a.StudentID
        WHERE e.EnrollmentID = @EnrollmentID
        GROUP BY p.FirstName, p.SecondName, p.ThirdName, p.LastName;
    ";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EnrollmentID", enrollmentID);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string fullName = reader["FullName"].ToString();
                            int total = reader["TotalDays"] != DBNull.Value ? Convert.ToInt32(reader["TotalDays"]) : 0;
                            int present = reader["DaysPresent"] != DBNull.Value ? Convert.ToInt32(reader["DaysPresent"]) : 0;
                            string lastDay = reader["LastDayPresent"] != DBNull.Value ? Convert.ToDateTime(reader["LastDayPresent"]).ToShortDateString() : "-";
                            double percentage = (total > 0) ? (present * 100.0 / total) : 0;

                            dt.Rows.Add(fullName, total, present, lastDay, percentage);
                        }
                    }
                }
            }

            return dt;
        }

        public static int AddAttendance(int studentID, bool isPresent, string absenceReason, string notes, int userID, DateTime attendanceDate)
        {
            int attendanceID = -1;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                    INSERT INTO StudentAttendance 
                    (StudentID, IsPresent, AbsenceReason, Notes, AttendanceDate, CreatedByUserID, CreatedDate)
                    VALUES (@StudentID, @IsPresent, @AbsenceReason, @Notes, @AttendanceDate, @UserID, GETDATE());
                    SELECT SCOPE_IDENTITY();
                ";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                cmd.Parameters.AddWithValue("@IsPresent", isPresent);
                cmd.Parameters.AddWithValue("@AbsenceReason", (object)absenceReason ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@AttendanceDate", attendanceDate.Date);
                cmd.Parameters.AddWithValue("@UserID", userID);

                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int id))
                    attendanceID = id;
            }

            return attendanceID;
        }

        public static bool UpdateAttendance(int attendanceID, int studentID, bool isPresent, string absenceReason, string notes, int userID, DateTime attendanceDate)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                    UPDATE StudentAttendance
                    SET StudentID = @StudentID,
                        IsPresent = @IsPresent,
                        AbsenceReason = @AbsenceReason,
                        Notes = @Notes,
                        AttendanceDate = @AttendanceDate,
                        ModifiedByUserID = @UserID,
                        ModifiedDate = GETDATE()
                    WHERE AttendanceID = @AttendanceID;
                ";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AttendanceID", attendanceID);
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                cmd.Parameters.AddWithValue("@IsPresent", isPresent);
                cmd.Parameters.AddWithValue("@AbsenceReason", (object)absenceReason ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@AttendanceDate", attendanceDate.Date);
                cmd.Parameters.AddWithValue("@UserID", userID);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool GetAttendanceByID(int attendanceID, ref int studentID, ref bool isPresent, ref string absenceReason, ref string notes, ref DateTime attendanceDate)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                    SELECT StudentID, IsPresent, AbsenceReason, Notes, AttendanceDate
                    FROM StudentAttendance
                    WHERE AttendanceID = @AttendanceID
                ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AttendanceID", attendanceID);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        studentID = reader.GetInt32(0);
                        isPresent = reader.GetBoolean(1);
                        absenceReason = reader.IsDBNull(2) ? null : reader.GetString(2);
                        notes = reader.IsDBNull(3) ? null : reader.GetString(3);
                        attendanceDate = reader.GetDateTime(4);
                        return true;
                    }
                    return false;
                }
            }
        }

        public static DataTable GetAllAttendance()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                    SELECT AttendanceID, StudentID, IsPresent, AbsenceReason, Notes, AttendanceDate 
                    FROM StudentAttendance
                    ORDER BY AttendanceDate DESC
                ";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.Fill(dt);
            }
            return dt;
        }

        public static DataTable GetAttendanceByStudentMonth(int studentID, int month, int year)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                    SELECT 
                        AttendanceID,
                        StudentID,
                        CASE WHEN IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS Status,
                        AbsenceReason,
                        Notes,
                        AttendanceDate,
                        DAY(AttendanceDate) AS DayOfMonth
                    FROM StudentAttendance
                    WHERE MONTH(AttendanceDate) = @Month AND YEAR(AttendanceDate) = @Year AND StudentID = @StudentID
                    ORDER BY AttendanceDate;
                ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                cmd.Parameters.AddWithValue("@Month", month);
                cmd.Parameters.AddWithValue("@Year", year);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return dt;
        }

        public static DataTable GetAttendanceByMonth(int month, int year)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                  SELECT 
   -- e.EnrollmentID,
  sa.StudentID,
    CONCAT(p.FirstName, ' ', 
           ISNULL(p.SecondName, ''), ' ',
           ISNULL(p.ThirdName, ''), ' ',
           p.LastName) AS FullName,
   -- sa.AttendanceDate,
    DATENAME(WEEKDAY, sa.AttendanceDate) AS Day ,
    CASE WHEN sa.IsPresent = 1 THEN 'Yes' ELSE 'No' END AS  Status,
    sa.AbsenceReason,
    --DATENAME(MONTH, sa.AttendanceDate) AS MonthName,
    FORMAT(sa.AttendanceDate, 'yyyy-MM') AS YearMonth
FROM StudentAttendance sa
INNER JOIN Students s ON sa.StudentID = s.StudentID
INNER JOIN People p ON s.PersonID = p.PersonID
INNER JOIN Enrollments e ON e.StudentID = sa.StudentID
 
  AND MONTH(sa.AttendanceDate) = @Month
  AND YEAR(sa.AttendanceDate) = @Year
ORDER BY sa.AttendanceDate;


                ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Month", month);
                cmd.Parameters.AddWithValue("@Year", year);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return dt;
        }

        public static bool Exists(int studentID, DateTime date)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                    SELECT COUNT(*) 
                    FROM StudentAttendance 
                    WHERE StudentID = @StudentID 
                      AND CAST(AttendanceDate AS DATE) = @AttendanceDate";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", studentID);
                    cmd.Parameters.AddWithValue("@AttendanceDate", date.Date);

                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public static bool DeleteAttendance(int attendanceID)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "DELETE FROM StudentAttendance WHERE AttendanceID = @AttendanceID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AttendanceID", attendanceID);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool DoesAttendanceExist(int attendanceID)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM StudentAttendance WHERE AttendanceID = @AttendanceID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AttendanceID", attendanceID);
                conn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }
    }
}
