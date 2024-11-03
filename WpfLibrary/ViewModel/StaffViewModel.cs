using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WpfLibrary.ViewModel
{
    public class StaffViewModel : BaseViewModel
    {
        private readonly HttpClient _httpClient;
        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        public void SetPassword(string password)
        {
            Password = password;
        }

        public StaffViewModel()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7143/api/Staff/checkLoginz") }; // Đặt URL API của bạn ở đây
        }

        public async Task<string> Login()
        {
            var parameters = new Dictionary<string, string>
{
    { "username", Username },
    { "password", Password }
};
            var content = new FormUrlEncodedContent(parameters);

            var response = await _httpClient.PostAsync($"https://localhost:7143/api/Staff/checkLogin?username={Username}&password={Password}", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ResponseModel>();
                return result.Token; // hoặc xử lý token ở đây nếu cần
            }

            return null; // hoặc thông báo lỗi
        }


    }

    public class ResponseModel
    {
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
