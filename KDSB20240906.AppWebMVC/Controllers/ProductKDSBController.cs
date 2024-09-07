using KDSB.DTOs.ProducDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KDSB20240906.AppWebMVC.Controllers
{
    public class ProductKDSBController : Controller
    {
        private readonly HttpClient _httpClientKDSB20240905API;

        public ProductKDSBController(IHttpClientFactory httpClientFactory)
        {
            _httpClientKDSB20240905API = httpClientFactory.CreateClient("KDSB20240906API");
        }

        // GET: ProductKDSBController
        public async Task<ActionResult> Index(SearchQueryProductDTO searchQueryProductDTO, int CountRow = 0)
        {
            // Configuración de valores por defecto para la búsqueda
            if (searchQueryProductDTO.SendRowCount == 0)
                searchQueryProductDTO.SendRowCount = 2;
            if (searchQueryProductDTO.Take == 0)
                searchQueryProductDTO.Take = 10;

            var result = new SearchResultProductDTO();

            // Realizar una solicitud HTTP POST para buscar clientes en el servicio web
            var response = await _httpClientKDSB20240905API.PostAsJsonAsync("/product/search", searchQueryProductDTO);

            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<SearchResultProductDTO>();

            result = result != null ? result : new SearchResultProductDTO();

            // Configuración de valores para la vista+
            if (result.CountRow == 0 && searchQueryProductDTO.SendRowCount == 1)
                result.CountRow = CountRow;

            ViewBag.CountRow = result.CountRow;
            searchQueryProductDTO.SendRowCount = 0;
            ViewBag.SearchQuery = searchQueryProductDTO;

            return View(result);
        }

        // GET: ProductKDSBController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var result = new GetIdResultProductDTO();

            // Realizar una solicitud HTTP GET para obtener los detalles del cliente por ID
            var response = await _httpClientKDSB20240905API.GetAsync("/product/" + id);

            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<GetIdResultProductDTO>();

            return View(result ?? new GetIdResultProductDTO());
        }

        // GET: ProductKDSBController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductKDSBController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductDTO createProductDTO)
        {
            try
            {
                // Realizar una solicitud HTTP POST para crear un nuevo cliente
                var response = await _httpClientKDSB20240905API.PostAsJsonAsync("/product", createProductDTO);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Error = "Error al intentar guardar el registro";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: ProductKDSBController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var result = new GetIdResultProductDTO();
            var response = await _httpClientKDSB20240905API.GetAsync("/product/" + id);

            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<GetIdResultProductDTO>();

            return View(new EditProductDTO(result ?? new GetIdResultProductDTO()));
        }

        // POST: ProductKDSBController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditProductDTO editProductDTO)
        {
            try
            {
                // Realizar una solicitud HTTP PUT para editar el cliente
                var response = await _httpClientKDSB20240905API.PutAsJsonAsync("/product", editProductDTO);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Error = "Error al intentar editar el registro";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

       
        public async Task<IActionResult> Delete(int id)
        {
            var result = new GetIdResultProductDTO();
            var response = await _httpClientKDSB20240905API.GetAsync("/product/" + id);

            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<GetIdResultProductDTO>();

            return View(result ?? new GetIdResultProductDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, GetIdResultProductDTO getIdResultProductDTO)
        {
            try
            {
                // Realizar una solicitud HTTP DELETE para eliminar el cliente por ID
                var response = await _httpClientKDSB20240905API.DeleteAsync("/product/" + id);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Error = "Error al intentar eliminar el registro";
                return View(getIdResultProductDTO);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(getIdResultProductDTO);
            }
        }
    }
}
