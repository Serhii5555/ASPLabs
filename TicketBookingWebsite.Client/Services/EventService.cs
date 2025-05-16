using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using TicketBookingWebsite.Models;

namespace TicketBookingWebsite.Client.Services
{
    public class EventService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;

        public EventService(HttpClient http, ILocalStorageService localStorage)
        {
            _http = http;
            _localStorage = localStorage;
        }

        public async Task<List<Event>> GetAllAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _http.GetFromJsonAsync<JsonElement>("api/events");

            if (response.TryGetProperty("$values", out var values))
            {
                try
                {
                    var eventsList = JsonSerializer.Deserialize<List<Event>>(values.GetRawText(), new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return eventsList ?? new List<Event>();
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Deserialization error: {ex.Message}");
                }
            }

            return new List<Event>();
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<Event>($"api/events/{id}");
        }

        public async Task<bool> CreateAsync(Event ev)
        {
            var response = await _http.PostAsJsonAsync("api/events", ev);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(Event ev)
        {
            var response = await _http.PutAsJsonAsync($"api/events/{ev.Id}", ev);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/events/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
