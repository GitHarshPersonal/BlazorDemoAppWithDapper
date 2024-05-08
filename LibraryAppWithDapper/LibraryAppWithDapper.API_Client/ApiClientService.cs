using LibraryAppWithDapper.APIClient.Models;
using LibraryAppWithDapper.APIClient.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LibraryAppWithDapper.API_Client
{
	public class ApiClientService
	{
		private readonly HttpClient _httpClient;

		public ApiClientService(ApiClientOptions apiClientOptions)
		{
			_httpClient = new HttpClient();
			_httpClient.BaseAddress = new System.Uri(apiClientOptions.ApiBaseAddress);
		}

		// get list of entities
		public async Task<List<Book>?> GetBooksAsync()
		{
			return await _httpClient.GetFromJsonAsync<List<Book>?>("/api/books");
		}
		public async Task<List<Customer>?> GetCustomersAsync()
		{
			return await _httpClient.GetFromJsonAsync<List<Customer>?>("/api/customers");
		}
		public async Task<List<Author>?> GetAuthorsAsync()
		{
			return await _httpClient.GetFromJsonAsync<List<Author>?>("/api/author");
		}

		// get by id
		public async Task<Book?> GetBookByIdAsync(int id)
		{
			return await _httpClient.GetFromJsonAsync<Book>($"/api/books/{id}");
		}
		public async Task<Customer?> GetCustomerByIdAsync(int id)
		{
			return await _httpClient.GetFromJsonAsync<Customer>($"/api/customers/{id}");
		}
		public async Task<Author?> GetAuthorByIdAsync(int id)
		{
			return await _httpClient.GetFromJsonAsync<Author>($"/api/author/{id}");
		}

		// get by name
		public async Task<Book?> GetBookByNameAsync(string name)
		{
			return await _httpClient.GetFromJsonAsync<Book>($"/api/books/{name}");
		}
		public async Task<Customer?> GetCustomerByNameAsync(string name)
		{
			return await _httpClient.GetFromJsonAsync<Customer>($"/api/customers/{name}");
		}
		public async Task<Author?> GetAuthorByNameAsync(string name)
		{
			return await _httpClient.GetFromJsonAsync<Author>($"/api/author/{name}");
		}

		// add an entity
		public async Task SaveBookAsync(Book book)
		{
			await _httpClient.PostAsJsonAsync<Book>("/api/books", book);
		}
		public async Task SaveCustomerAsync(Customer customer)
		{
			await _httpClient.PostAsJsonAsync<Customer>("/api/customers", customer);
		}
		public async Task<int> SaveAuthorAsync(Author author)
		{
			var response = await _httpClient.PostAsJsonAsync<Author>("/api/author", author);
			string responseString = await response.Content.ReadAsStringAsync();
			int result = Int32.Parse(responseString);
			return result;
		}

		// update an entity
		public async Task UpdateBookAsync(Book book)
		{
			await _httpClient.PutAsJsonAsync<Book>("/api/books", book);
		}
		public async Task UpdateCustomerAsync(Customer customer)
		{
			await _httpClient.PutAsJsonAsync<Customer>("/api/customers", customer);
		}
		public async Task<int> UpdateAuthorAsync(Author author)
		{
			var response = await _httpClient.PutAsJsonAsync<Author>("/api/author", author);
			string responseString = await response.Content.ReadAsStringAsync();
			int result = Int32.Parse(responseString);
			return result;
		}

		// delete an entity
		public async Task DeleteBookAsync(int id)
		{
			await _httpClient.DeleteAsync($"/api/books/{id}");
		}
		public async Task DeleteCustomerAsync(int id)
		{
			await _httpClient.DeleteAsync($"/api/customers/{id}");
		}
		public async Task DeleteAuthorAsync(int id)
		{
			await _httpClient.DeleteAsync($"/api/author/{id}");
		}
	}
}
