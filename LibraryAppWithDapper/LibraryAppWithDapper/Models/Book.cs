using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryAppWithDapper.Models
{
	public class Book
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public int AuthorId { get; set; }
		public bool IsAvailable { get; set; }
		public bool IsActive { get; set; }
		public DateTime UpdatedOn { get; set; }

	}
}
