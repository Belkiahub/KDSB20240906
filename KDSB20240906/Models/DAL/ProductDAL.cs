using KDSB20240906.Models.EN;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace KDSB20240906.Models.DAL
{
    public class ProductDAL
    {
        readonly CRMContext _context;

        public ProductDAL(CRMContext CRMContext)
        {
            _context = CRMContext;
        }

        public async Task<int> Create(ProductKDSB product)
        {
            _context.Add(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<ProductKDSB> GetById(int id)
        {
            var products = await _context.ProductsKDSB.FirstOrDefaultAsync(s => s.Id == id);
            return products != null ? products : new ProductKDSB();
        }

        public async Task<List<ProductKDSB>> GetAll()
        {
            var products = await _context.ProductsKDSB.ToListAsync();
            return products;
        }

        public async Task<int> Edit(ProductKDSB product)
        {
            int result = 0;
            var productUpdate = await GetById(product.Id);
            if (productUpdate.Id != 0)
            {
                // Actualiza los datos del cliente.
                productUpdate.NombreKDSB = product.NombreKDSB;
                productUpdate.DescripcionKDSB = product.DescripcionKDSB;
                productUpdate.Precio = product.Precio;
                result = await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<int> Delete(int id)
        {
            int result = 0;
            var productDelete = await GetById(id);
            if (productDelete.Id > 0)
            {
                // Elimina el cliente de la base de datos.
                _context.ProductsKDSB.Remove(productDelete);
                result = await _context.SaveChangesAsync();
            }
            return result;
        }

        private IQueryable<ProductKDSB> Query(ProductKDSB product)
        {
            var query = _context.ProductsKDSB.AsQueryable();
            if (!string.IsNullOrWhiteSpace(product.NombreKDSB))
                query = query.Where(s => s.NombreKDSB.Contains(product.NombreKDSB));
            if (!string.IsNullOrWhiteSpace(product.DescripcionKDSB))
                query = query.Where(s => s.DescripcionKDSB.Contains(product.DescripcionKDSB));
            return query;
        }

        public async Task<int> CountSearch(ProductKDSB product)
        {
            return await Query(product).CountAsync();
        }

        public async Task<List<ProductKDSB>> Search(ProductKDSB product, int take = 10, int skip = 0)
        {
            take = take == 0 ? 10 : take;
            var query = Query(product);
            query = query.OrderByDescending(s => s.Id).Skip(skip).Take(take);
            return await query.ToListAsync();
        }
    }
}
