using BookStore.Order.Entity;
using BookStore.Order.Interface;
using BookStore.Order.Model;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace BookStore.Order.Service
{
    public class BookRepo : IBookRepo
    {
        private readonly IConfiguration configuration;

        public BookRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<BookEntity> GetBookDetails(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://localhost:7115/api/Book/GetbookById?bookID={id}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                ResponseModel apiResponseModel = JsonConvert.DeserializeObject<ResponseModel>(content);


                if (response.IsSuccessStatusCode)
                {
                    BookEntity bookEntity = JsonConvert.DeserializeObject<BookEntity>(apiResponseModel.Data.ToString());
                    return bookEntity;
                }
            }
            return null;
        }
    }
}
