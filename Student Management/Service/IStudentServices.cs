using Student_Management.Models;

namespace Student_Management.Service
{
	public interface IStudentServices
	{
		// This Interface is Taking the Model in form of StudentDetails List.
		// The Interface is a common set of method  and hence we declare only abstract method.

		// different Class taht can be created for diff purposer like details,update,delete etc  can inherit from this Interface class only
		public List<Student> GetStudentDetails(); 

		// These all methods will be defined in our StudentSercice class which is inheriting the Interface " Istudent",hence definition will be given to abstract methods 
		public string InsertStudent(Student student);
		public string UpdateStudent(Student student);
		public string DeleteStudent(Student Id);
        string DeleteStudent(int id);
    }
}
