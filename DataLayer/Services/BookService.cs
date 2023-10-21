using DataLayer.IServices;
using DataLayer.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace DataLayer.Services
{
    public class BookService : IBookService
    {
        private static HttpClient client = new()
        {
            BaseAddress = new Uri("https://fakerestapi.azurewebsites.net"),
        };
        public async Task<Book> Create(Book book)
        {
            try
            {
                using StringContent jsonContent = new(
                JsonSerializer.Serialize(book),
                Encoding.UTF8,
                "application/json");

                using HttpResponseMessage response = await client.PostAsync(
                    "api/v1/Books",
                    jsonContent);

                response.EnsureSuccessStatusCode();
                var newBook = await response.Content.ReadFromJsonAsync<Book>();
                return newBook ?? new();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                using HttpResponseMessage response = await client.DeleteAsync($"api/v1/Books/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            try
            {
                var books = await client.GetFromJsonAsync<List<Book>>("api/v1/Books/");
                return books?.OrderBy(x => x.Id).ToList() ?? new List<Book>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Book?> GetById(int id)
        {
            try
            {
                var book = await client.GetFromJsonAsync<Book>($"api/v1/Books/{id}");
                return book;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Book> Update(Book book)
        {
            try
            {
                using StringContent jsonContent = new(
                JsonSerializer.Serialize(book),
                Encoding.UTF8,
                "application/json");

                using HttpResponseMessage response = await client.PutAsync(
                    $"api/v1/Books/{book.Id}",
                    jsonContent);

                response.EnsureSuccessStatusCode();

                var bookUpdated = await response.Content.ReadFromJsonAsync<Book>();
                return bookUpdated ?? new();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
