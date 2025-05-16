using Blazored.LocalStorage;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using TicketBookingWebsite.Client.Models;
using TicketBookingWebsite.Models;
using static System.Net.WebRequestMethods;

namespace TicketBookingWebsite.Client.Services
{
    public class BookingService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;

        public BookingService(HttpClient http, ILocalStorageService localStorage)
        {
            _http = http;
            _localStorage = localStorage;
        }


        public async Task<List<Booking>> GetAllAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            try
            {
                var response = await _http.GetFromJsonAsync<JsonElement>("api/bookings");

                if (response.TryGetProperty("$values", out var values))
                {
                    var bookingsList = JsonSerializer.Deserialize<List<Booking>>(values.GetRawText(), new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        ReferenceHandler = ReferenceHandler.Preserve 
                    });

                    return bookingsList ?? new List<Booking>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Booking deserialization error: {ex.Message}");
            }

            return new List<Booking>();
        }


        public async Task<Booking?> GetByIdAsync(int id)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            try
            {
                var response = await _http.GetFromJsonAsync<JsonElement>($"api/bookings/{id}");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.Preserve 
                };

                var booking = JsonSerializer.Deserialize<Booking>(response.GetRawText(), options);
                return booking;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching booking: {ex.Message}");
                return null;
            }
        }


        public async Task<bool> CreateAsync(Booking booking)
        {
            var response = await _http.PostAsJsonAsync("api/bookings", booking);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(Booking booking)
        {
            var dto = new BookingDto
            {
                Id = booking.Id,
                CustomerName = booking.CustomerName,
                CustomerEmail = booking.CustomerEmail,
                Quantity = booking.Quantity,
                EventId = booking.EventId
            };

            var response = await _http.PutAsJsonAsync($"api/bookings/{dto.Id}", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/bookings/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
