using System;

namespace LibraryAppWithDapper.APIClient.Models.ApiModels
{
	public class Author
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public bool IsActive { get; set; }
		public DateTime UpdatedOn { get; set; }
	}
}
