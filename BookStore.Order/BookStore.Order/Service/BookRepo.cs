using BookStore.Order.Entity;
using BookStore.Order.Interface;
using BookStore.Order.Model;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace BookStore.Order.Service
{
    public class BookRepo : IBookRepo
    {
        private readonly IHttpClientFactory httpClientFactory;

        public BookRepo( IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory; 
        }

        // this method uses normal HTTP client 

        //public async Task<BookEntity> GetBookDetails(int id)
        //{
        //    HttpClient client = new HttpClient();
        //    HttpResponseMessage response = await client.GetAsync($"https://localhost:7115/api/Book/GetbookById?bookID={id}");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        string content = await response.Content.ReadAsStringAsync();
        //        ResponseModel apiResponseModel = JsonConvert.DeserializeObject<ResponseModel>(content);


        //        if (response.IsSuccessStatusCode)
        //        {
        //            BookEntity bookEntity = JsonConvert.DeserializeObject<BookEntity>(apiResponseModel.Data.ToString());
        //            return bookEntity;
        //        }
        //    }
        //    return null;
        //}

        // this method uses IHTTPFACTORY interface 
        public async Task<BookEntity> GetBookDetails(int id)
        {
            var client = httpClientFactory.CreateClient("MyApi");
            var response = await client.GetAsync($"Book/GetbookById?bookID={id}");
            if(response.IsSuccessStatusCode)
            {
                var apiResponseModel = await response.Content.ReadFromJsonAsync<ResponseModel>();
                if(apiResponseModel != null)
                {
                    var bookEntity = JsonConvert.DeserializeObject<BookEntity>(apiResponseModel.Data.ToString());
                    return bookEntity;
                }
            }
            return null;
        }
    }
}
