using Dapper;
using LibraryAppWithDapper.API.Models;
using LibraryAppWithDapper.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LibraryAppWithDapper.API.Controllers
{
	[Route("api/author")]
	[ApiController]
	public class AuthorController : ControllerBase
	{
		private IDbConnection db;

		public AuthorController(IConfiguration _configuration)
		{
			db = new SqlConnection(_configuration.GetConnectionString("dbConnectionString"));
		}

		[HttpGet]
		public async Task<ActionResult<List<Author>>> GetAuthorsAsync()
		{
			var result = await db.QueryAsync<Author>("SELECT * FROM Author");
			return Ok(result);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<Author>> GetAuthorByIdAsync(int id)
		{
			var result = await db.QuerySingleOrDefaultAsync<Author>($"SELECT * FROM Author WHERE Id = {id}");
			return Ok(result);
		}

		[HttpGet("{name}")]
		public async Task<ActionResult<Author>> GetAuthorByNameAsync(string name)
		{
			var result = await db.QuerySingleOrDefaultAsync<Author>($"SELECT * FROM Author WHERE Name = '{name}'");
			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<int>> PostAuthorAsync(Author author)
		{
			int id = await db.QuerySingleAsync<int>("INSERT INTO Author(Name,Description,IsActive,UpdatedOn)" +
				" VALUES(@Name,@Description,@IsActive,@UpdatedOn);SELECT CAST(SCOPE_IDENTITY() AS INT)", author);

			return Ok(id);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<int>> DeleteAuthorAsync(int id)
		{
			int rows_affected = await db.ExecuteAsync($"DELETE FROM Author WHERE Id = {id}");

			return Ok(rows_affected);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateAuthorAsync(Author author)
		{
			int id = await db.QuerySingleAsync<int>("UPDATE Author SET Name=@Name,Description=@Description," +
				$"IsActive=@IsActive,UpdatedOn=@UpdatedOn WHERE Id = {author.Id}", author);
			return Ok(id);
		}
	}
}
