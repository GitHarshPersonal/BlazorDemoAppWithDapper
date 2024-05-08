using Dapper;
using LibraryAppWithDapper.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LibraryAppWithDapper.Controllers
{
	[Route("api/books")]
	[ApiController]
	public class BookController : ControllerBase
	{
		private IDbConnection db;

		public BookController(IConfiguration _configuration)
        {
			db = new SqlConnection(_configuration.GetConnectionString("dbConnectionString"));
        }

		[HttpGet]
		public async Task<ActionResult<List<Book>>> GetBooksAsync()
		{
			var result = await db.QueryAsync<Book>("SELECT * FROM Books");
			return Ok(result);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<Book>> GetBookByIdAsync(int id)
		{
			var result = await db.QuerySingleOrDefaultAsync<Book>($"SELECT * FROM Books WHERE Id = {id}");
			return Ok(result);
		}

		[HttpGet("{name}")]
		public async Task<ActionResult<Book>> GetBookByNameAsync(string name)
		{
			var result = await db.QuerySingleOrDefaultAsync<Book>($"SELECT * FROM Books WHERE Title = '{name}'");
			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<int>> PostBookAsync(Book book)
		{
			int rows_affected = await db.ExecuteAsync("INSERT INTO Books(Title,Description,AuthorId,IsAvailable,IsActive,UpdatedOn)" +
				" VALUES(@Title,@Description,@AuthorId,@IsAvailable,@IsActive,@UpdatedOn)", book);

			return Ok(rows_affected);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<int>> DeleteBookAsync(int id)
		{
			int rows_affected = await db.ExecuteAsync($"DELETE FROM Books WHERE Id = {id}");

			return Ok(rows_affected);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateBookAsync(Book book)
		{
			await db.ExecuteAsync("UPDATE Books SET Title=@Title,Description=@Description,AuthorId=@AuthorId," +
				$"IsActive=@IsActive,UpdatedOn=@UpdatedOn, IsAvailable=@IsAvailable WHERE Id = {book.Id}", book);
			return Ok();
		}
    }
}
