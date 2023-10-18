using Dapper;
using Student_Management.Models;
using System.Data;
using System.Data.SqlClient;


namespace Student_Management.Service
{
	// Here we are going to Inherit our Interface
	public class StudentServices : IStudentServices
	{


		private readonly IConfiguration _configuration;

		public StudentServices(IConfiguration configuration)   // We have created a constructor for our class ,which is meant to server for configuration
		{
			_configuration = configuration;
			ConnectionString = _configuration.GetConnectionString("DBConnection");
			providerName = " System.Data.SqlClient";
		}

		public string ConnectionString { get; }
		public string providerName { get; }

		//  Retrieving the connection string from the configuration services using IConfiguration. You use this connection string to create an IDbConnection instance, which is responsible for connecting to the database.
		
		// IDbConnection is responsible for connecting to the database and executing SQL queries,
		public IDbConnection Connection
		{
			get { return new SqlConnection(ConnectionString); }
		}
		public string DeleteStudent(Student Id)
		{
            string result = "";
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();

                    // Logic of Update STUDENT in DB and  using of Update Stored procedure is used .
                    // Also we have Parameters ,so we need to add them also.


                    var std = dbConnection.Query<Student>("deleteStudents",commandType: CommandType.StoredProcedure);
                    if (std != null && std.FirstOrDefault().Response == "Deleted Successfully")
                    {
                        result = "Deleted Successfully";

                    }

                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                return errorMessage;
            }
        }

        public string DeleteStudent(int id)
        {
            throw new NotImplementedException();
        }

        public List<Student> GetStudentDetails()
		{
			List<Student> students = new List<Student>();

			try
			{
				using (IDbConnection dbConnection = Connection)	
				{
					dbConnection.Open();

					students = dbConnection.Query<Student>("spGetDetails",commandType:CommandType.StoredProcedure).ToList();  // To mention the query list that we are using Stored procedure for our student details
					dbConnection.Close();
					return students;
				}
			}
			catch(Exception ex)
			{
				string errorMessage = ex.Message;
				return students;
			}
		}

		public string InsertStudent(Student student)
		{
			string result = "";
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();

					// Logic of INSERTING STUDENT in DB and  using of INSERT Stored procedure is used .
					// Also we have Parameters ,so we need to add them also.


					var std = dbConnection.Query<Student>("insertStudents",new { StudentName =student.Name, email=student.Email, city=student.City,createdBy=student.CreatedBy } ,commandType: CommandType.StoredProcedure);
                     if(std!=null && std.FirstOrDefault().Response== "Save Successfully")
					{
						result = "Save Successfully";

                        Console.WriteLine(student.Name);
                        Console.WriteLine(student.Email);
                        Console.WriteLine(student.City);
                        Console.WriteLine("hello");
						Console.WriteLine("Abir");
						Console.WriteLine("Choudhury");

                    }
					
					dbConnection.Close();
					return result;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
				return errorMessage;
            }
        }

		public string UpdateStudent(Student student)
		{
            string result = "";
			try
			{
				using (IDbConnection dbConnection = Connection)
				{
					dbConnection.Open();

					// Logic of Update STUDENT in DB and  using of Update Stored procedure is used .
					// Also we have Parameters ,so we need to add them also.


					var std = dbConnection.Query<Student>("updateStudents", new { StudentName = student.Name, email = student.Email, city = student.City, createdBy = student.CreatedBy, StudentId = student.Id }, commandType: CommandType.StoredProcedure);
					if (std != null && std.FirstOrDefault().Response == "UPDATED SUCCESSFULLY")
					{
						result = "Save Successfully";

					}

					dbConnection.Close();
					return result;
				}
			}
			catch (Exception ex)
			{
				string errorMessage = ex.Message;
				return errorMessage;
			}
        }
	}
}
