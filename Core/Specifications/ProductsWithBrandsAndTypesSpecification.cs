using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithBrandsAndTypesSpecification : BaseSpecification<Product>
    {
        public ProductsWithBrandsAndTypesSpecification(ProductSpecParams productparams)
        : base( x =>
                (!productparams.BrandId.HasValue || x.ProductBrandId == productparams.BrandId) &&
                (!productparams.TypeId.HasValue || x.ProductTypeId == productparams.TypeId)
        )
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
            AddOrderBy(x => x.Name);
            ApplyPaging(productparams.PageSize * (productparams.PageIndex -1), productparams.PageSize);

            if(!string.IsNullOrEmpty(productparams.Sort))
            {
                switch(productparams.Sort)
                {
                    case "priceAsc":
                       AddOrderBy(p => p.Price);
                       break;
                    case "priceDesc":
                       AddOrderByDescending(p => p.Price);
                       break;
                    default:
                       AddOrderBy(n => n.Name);
                       break;
                }
            }
        }

        public ProductsWithBrandsAndTypesSpecification(int id) : base(x => x.Id == id)
        {
             AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }
    }



}