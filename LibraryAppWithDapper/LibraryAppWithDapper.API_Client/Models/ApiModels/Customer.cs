using System;

namespace LibraryAppWithDapper.APIClient.Models.ApiModels
{
	public class Customer
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public int BookId { get; set; }
		public bool IsActive { get; set; }
		public DateTime UpdatedOn { get; set; }
	}
}
