using KDSB.DTOs.ProducDTOs;
using KDSB20240906.Models.DAL;
using KDSB20240906.Models.EN;

namespace KDSB20240906.Endpoints
{
    public static class ProductEndpoint
    {
        public static void AddProductEndpoints(this WebApplication app)
        {
            app.MapPost("/product/search", async (SearchQueryProductDTO productDTO, ProductDAL productDAL) =>
            {
                // Crear un objeto 'Customer' a partir de los datos proporcionados
                var products = new ProductKDSB
                {
                    NombreKDSB = productDTO.NombreKDSB_Like != null ? productDTO.NombreKDSB_Like : string.Empty,
                    DescripcionKDSB = productDTO.DescripcionKDSB_Like != null ? productDTO.DescripcionKDSB_Like : string.Empty
                };
                // Inicializar una lista de clientes y una variable para contar las filas
                var productos = new List<ProductKDSB>();
                int countRow = 0;
                // Verificar si se debe enviar la cantidad de filas
                if (productDTO.SendRowCount == 2)
                {
                    // Realizar una búsqueda de clientes y contar las filas
                    productos = await productDAL.Search(products, skip: productDTO.Skip, take: productDTO.Take);
                    if (productos.Count > 0)
                        countRow = await productDAL.CountSearch(products);
                }
                else
                {
                    // Realizar una búsqueda de clientes sin contar las filas
                    productos = await productDAL.Search(products, skip: productDTO.Skip, take: productDTO.Take);
                }
                // ...
                var productResult = new SearchResultProductDTO
                {
                    Data = new List<SearchResultProductDTO.ProducDTOs>(),
                    CountRow = countRow
                };
                // Mapear los resultados a objetos 'CustomerDTO' y agregarlos al resultado
                productos.ForEach(s => {
                    productResult.Data.Add(new SearchResultProductDTO.ProducDTOs
                    {
                        Id = s.Id,
                        NombreKDSB = s.NombreKDSB,
                        DescripcionKDSB = s.DescripcionKDSB,
                        Precio = s.Precio
                    });
                });
                // Devolver los resultados
                return productResult;
            });

            app.MapGet("/products", async (ProductDAL productDAL) =>
            {
                // Obtener todos los productos
                var products = await productDAL.GetAll();

                // Mapeo de los productos a un DTO si es necesario
                var productResults = products.Select(product => new GetIdResultProductDTO
                {
                    Id = product.Id,
                    NombreKDSB = product.NombreKDSB,
                    DescripcionKDSB = product.DescripcionKDSB,
                    Precio = product.Precio
                }).ToList();

                // Verificar si se encontraron productos y devolver la respuesta correspondiente
                if (productResults.Any())
                    return Results.Ok(productResults);
                else
                    return Results.NotFound("No products found.");
            });


            app.MapGet("/product/{id}", async (int id, ProductDAL productDAL) =>
            {
                // Obtener un cliente por ID
                var product = await productDAL.GetById(id);

                
                var customerResult = new GetIdResultProductDTO
                {
                    Id = product.Id,
                    NombreKDSB = product.NombreKDSB,
                    DescripcionKDSB = product.DescripcionKDSB,
                    Precio = product.Precio
                };

                // Verificar si se encontró el cliente y devolver la respuesta correspondiente
                if (customerResult.Id > 0)
                    return Results.Ok(customerResult);
                else
                    return Results.NotFound(customerResult);
            });

            app.MapPost("/product", async (CreateProductDTO createProductDTO, ProductDAL productDAL) =>
            {
                // Crear un objeto 'Customer' a partir de los datos proporcionados
                var product = new ProductKDSB
                {
                    NombreKDSB = createProductDTO.NombreKDSB,
                    DescripcionKDSB = createProductDTO.DescripcionKDSB,
                    Precio = createProductDTO.Precio
                };

                // Intentar crear el cliente y devolver el resultado correspondiente
                int result = await productDAL.Create(product);
                if (result != 0)
                    return Results.Ok(result);
                else
                    return Results.StatusCode(500);
            });

            app.MapPut("/product", async (EditProductDTO editProductDTO, ProductDAL productDAL) =>
            {
                // Crear un objeto 'Customer' a partir de los datos proporcionados
                var product = new ProductKDSB
                {
                    Id = editProductDTO.Id,
                    NombreKDSB = editProductDTO.NombreKDSB,
                    DescripcionKDSB = editProductDTO.DescripcionKDSB,
                    Precio = editProductDTO.Precio
                };

                int result = await productDAL.Edit(product);
                if (result != 0)
                    return Results.Ok(result);
                else
                    return Results.StatusCode(500);
            });

            app.MapDelete("/product/{id}", async (int id, ProductDAL productDAL) =>
            {
                // Intentar eliminar el cliente y devolver el resultado correspondiente
                int result = await productDAL.Delete(id);
                if (result != 0)
                    return Results.Ok(result);
                else
                    return Results.StatusCode(500);
            });

        }
    }
}
