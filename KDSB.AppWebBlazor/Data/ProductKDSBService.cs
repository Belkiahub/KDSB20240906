using KDSB.DTOs.ProducDTOs;

namespace KDSB.AppWebBlazor.Data
{
    public class ProductKDSBService
    {
        private readonly HttpClient _httpClientKDSB20240905API;

        public ProductKDSBService(IHttpClientFactory httpClientFactory)
        {
            _httpClientKDSB20240905API = httpClientFactory.CreateClient("KDSB20240906API");
        }

        public async Task<SearchResultProductDTO> Search(SearchQueryProductDTO searchQueryProductDTO)
        {
            var response = await _httpClientKDSB20240905API.PostAsJsonAsync("/product/search", searchQueryProductDTO);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<SearchResultProductDTO>();
                return result ?? new SearchResultProductDTO();
            }
            return new SearchResultProductDTO(); // Devolver un objeto vacío en caso de error o respuesta no exitosa
        }

        public async Task<GetIdResultProductDTO> GetById(int id)
        {
            var response = await _httpClientKDSB20240905API.GetAsync("/product/" + id);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GetIdResultProductDTO>();
                return result ?? new GetIdResultProductDTO();
            }
            return new GetIdResultProductDTO(); // Devolver un objeto vacío en caso de error o respuesta no exitosa
        }

        public async Task<int> Create(CreateProductDTO createProductDTO)
        {
            int result = 0;
            var response = await _httpClientKDSB20240905API.PostAsJsonAsync("/product", createProductDTO);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                if (int.TryParse(responseBody, out result) == false)
                    result = 0;
            }
            return result;
        }

        public async Task<int> Edit(EditProductDTO editProductDTO)
        {
            int result = 0;
            var response = await _httpClientKDSB20240905API.PutAsJsonAsync("/product", editProductDTO);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                if (int.TryParse(responseBody, out result) == false)
                    result = 0;
            }
            return result;
        }

        public async Task<int> Delete(int id)
        {
            int result = 0;
            var response = await _httpClientKDSB20240905API.DeleteAsync("/product/" + id);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                if (int.TryParse(responseBody, out result) == false)
                    result = 0;
            }
            return result;
        }

    }
}
