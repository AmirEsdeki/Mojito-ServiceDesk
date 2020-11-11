using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.Product.In;
using Mojito.ServiceDesk.Application.Common.DTOs.Product.Out;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.ProductService;
using Mojito.ServiceDesk.Core.Entities.Proprietary;
using Mojito.ServiceDesk.Infrastructure.Data.EF;
using Mojito.ServiceDesk.Infrastructure.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Infrastructure.Services.ProductService
{
    public class ProductService
         : BaseService<Product, PostProductDTO, PutProductDTO, GetProductDTO, ProductsFilterParams>
        , IProductService
    {
        #region ctor
        public ProductService(ApplicationDBContext db, IMapper mapper)
            : base(db, mapper)
        { }
        #endregion

        #region CRUD
        public override async Task<PaginatedList<GetProductDTO>> GetAllAsync(ProductsFilterParams arg)
        {
            try
            {
                var query = GetAllAsync();

                if (arg.Name != null)
                    query = query.Where(data => data.Name.StartsWith(arg.Name)
                        || data.Name.Contains(arg.Name));

                if (arg.CustomerId != 0)
                    query = query.Where(data => data.CustomerId == arg.CustomerId);

                var list = await new PaginatedListBuilder<Product, GetProductDTO>(mapper)
                    .CreateAsync(query, arg.PageNumber, arg.PageSize);

                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region OtherActions

        public async Task<ICollection<KeyValueDTO>> FilterAsync(string phrase)
        {
            var filteredData = await GetAllAsync(data => data.Name.StartsWith(phrase)
                || data.Name.Contains(phrase))
                .ToListAsync();
            return filteredData.Select(s => new KeyValueDTO(s.Id, s.Name)).ToList();
        }
        #endregion
    }
}
