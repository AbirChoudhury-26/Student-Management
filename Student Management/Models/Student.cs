namespace Student_Management.Models
{
	public class Student
	{
		public int? Id { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string? City { get; set; }
		public DateTime Creation_at { get; set; }
		public int? CreatedBy { get; set; }
		public string? Response { get; set; }
	}
}
