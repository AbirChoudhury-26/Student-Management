using Microsoft.AspNetCore.Mvc;
using Student_Management.Models;
using Student_Management.Service;

namespace Student_Management.Controllers
{

	
	public class StudentController : Controller
	{

		private readonly IConfiguration _configuration;
		private readonly IStudentServices _studentServices;

		public StudentController(IConfiguration configuration, IStudentServices studentServices)
		{
			_configuration = configuration;
			_studentServices = studentServices;
		}

		public IActionResult Index()
		{
			StudentVM model= new StudentVM();
            model.StudentsList = _studentServices.GetStudentDetails().ToList();
			return View(model);
		}

		[HttpPost]

		public IActionResult AddUpdateStudent(Student student)
		{
			StudentVM model =new StudentVM();
			student.CreatedBy = 1;
			string url = Request.Headers["Referer"].ToString();

			string result= string.Empty;
			 if(student.Id>0)
			{
				result= _studentServices.UpdateStudent(student);
			}
			else
			{
				result= _studentServices.InsertStudent(student);

                if (result == "Save Successfully")
                {
                    TempData["SuccessMessage"] = "Data Insert Hua hai";
                  
                }
               /* else
                {
                    TempData["ErrorMessage"] = result;
                    return Redirect(url);
                }*/

            }

            /*	 if(result=="Save Successfully")
                {
                    TempData["SuccessMessage"] = "Save Successfully";
                    return Redirect(url);

                }*/
            /*else
			{
				TempData["ErrorMessage"] = result;*/
            //return Redirect(url);
            /**/
            return Redirect(url);

        }

		[HttpPost]

		public IActionResult DeleteStudent(Student StudentId)
		{
            string url = Request.Headers["Referer"].ToString();
			string result = _studentServices.DeleteStudent(StudentId);
			 if(result== "Deleted Successfully")
			{
				return Json(new { message= "Deleted Successfully" });
			}
			 else
			{
				return Json(new { message = "Error Occured" });
			}
        }

    }
}
