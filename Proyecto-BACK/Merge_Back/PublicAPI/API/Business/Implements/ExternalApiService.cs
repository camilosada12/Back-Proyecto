using Entity.DTOs;
using System.Net.Http.Json;

namespace Business.Implements
{
    public class ExternalApiService
    {
        private readonly HttpClient _httpClient;

        public ExternalApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //public async Task<List<TouristicAttractionApiDto>> GetExternalDataAsync()
        //{
        //    await Task.Delay(10000);
        //    var response = await _httpClient.GetFromJsonAsync<List<TouristicAttractionApiDto>>(
        //        "https://api-colombia.com/api/v1/TouristicAttraction");

        //    return response ?? new List<TouristicAttractionApiDto>();
        //}


        // ✅ Asíncrono: no bloquea el hilo
        public async Task<List<TouristicAttractionApiDto>> GetExternalDataAsync()
        {
            Console.WriteLine($"[ASYNC] Inicio - Hilo #{Thread.CurrentThread.ManagedThreadId} - {DateTime.Now:HH:mm:ss.fff}");


            Console.WriteLine($"[ASYNC] Peticion - Hilo #{Thread.CurrentThread.ManagedThreadId} - {DateTime.Now:HH:mm:ss.fff}");

            await Task.Delay(5000); // simula espera sin bloquear
            var response = await _httpClient.GetFromJsonAsync<List<TouristicAttractionApiDto>>(
                "https://api-colombia.com/api/v1/TouristicAttraction");

            Console.WriteLine($"[ASYNC] Fin    - Hilo #{Thread.CurrentThread.ManagedThreadId} - {DateTime.Now:HH:mm:ss.fff}");

            return response ?? new List<TouristicAttractionApiDto>();
        }

        // ❌ Bloqueante: sí bloquea el hilo
        public List<TouristicAttractionApiDto> GetExternalData()
        {
            Console.WriteLine($"[BLOCK] Inicio - Hilo #{Thread.CurrentThread.ManagedThreadId} - {DateTime.Now:HH:mm:ss.fff}");


            Console.WriteLine($"[BLOCK] Peticion - Hilo #{Thread.CurrentThread.ManagedThreadId} - {DateTime.Now:HH:mm:ss.fff}");

            Thread.Sleep(5000); // bloquea el hilo activamente

            var response = _httpClient
                .GetFromJsonAsync<List<TouristicAttractionApiDto>>(
                    "https://api-colombia.com/api/v1/TouristicAttraction")
                .Result;


            Console.WriteLine($"[BLOCK] Fin    - Hilo #{Thread.CurrentThread.ManagedThreadId} - {DateTime.Now:HH:mm:ss.fff}");

            return response ?? new List<TouristicAttractionApiDto>();
        }

        //public List<TouristicAttractionApiDto> GetExternalData()
        //{
        //    var response = _httpClient
        //        .GetFromJsonAsync<List<TouristicAttractionApiDto>>(
        //            "https://api-colombia.com/api/v1/TouristicAttraction")
        //        .Result;
        //    Thread.Sleep(10000); // <--- BLOQUEA el hilo aquí

        //    return response ?? new List<TouristicAttractionApiDto>();
        //}
    }

}
