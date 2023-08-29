using BookStore.Order.Entity;
using BookStore.Order.Interface;
using BookStore.Order.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace BookStore.Order.Service
{
    public class UserRepo : IUserRepo
    {
        private readonly IConfiguration configuration;

        public UserRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<UserEntity> GetUserDetails(string token)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync($"https://localhost:7099/api/User/Display");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                ResponseModel apiResponseModel = JsonConvert.DeserializeObject<ResponseModel>(content);
                if (response.IsSuccessStatusCode)
                {
                    UserEntity UserEntity = JsonConvert.DeserializeObject<UserEntity>(apiResponseModel.Data.ToString());
                    return UserEntity;
                }
            }
            return null;
        }
    }
}
