using Dapper;
using LibraryAppWithDapper.API.Models;
using LibraryAppWithDapper.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LibraryAppWithDapper.API.Controllers
{
	[Route("api/customers")]
	[ApiController]
	public class CustomersController : ControllerBase
	{
		private IDbConnection db;

		public CustomersController(IConfiguration _configuration)
		{
			db = new SqlConnection(_configuration.GetConnectionString("dbConnectionString"));
		}

		[HttpGet]
		public async Task<ActionResult<List<Customer>>> GetBooksAsync()
		{
			var result = await db.QueryAsync<Customer>("SELECT * FROM Customers");
			return Ok(result);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<Customer>> GetCustomerByIdAsync(int id)
		{
			var result = await db.QuerySingleOrDefaultAsync<Customer>($"SELECT * FROM Customers WHERE Id = {id}");
			return Ok(result);
		}

		[HttpGet("{name}")]
		public async Task<ActionResult<Customer>> GetCustomerByNameAsync(string name)
		{
			var result = await db.QuerySingleOrDefaultAsync<Customer>($"SELECT * FROM Customers WHERE Name = '{name}'");
			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<int>> PostCustomerAsync(Customer customer)
		{
			int rows_affected = await db.ExecuteAsync("INSERT INTO Customers(Name,BookId,IsActive,UpdatedOn)" +
				" VALUES(@Name,@BookId,@IsActive,@UpdatedOn)", customer);

			return Ok(rows_affected);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<int>> DeleteCustomerAsync(int id)
		{
			int rows_affected = await db.ExecuteAsync($"DELETE FROM Customers WHERE Id = {id}");

			return Ok(rows_affected);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateCustomerAsync(Customer customer)
		{
			await db.ExecuteAsync("UPDATE Customers SET Name=@Name,BookId=@BookId," +
				$"IsActive=@IsActive,UpdatedOn=@UpdatedOn WHERE Id = {customer.Id}", customer);
			return Ok();
		}
	}
}
