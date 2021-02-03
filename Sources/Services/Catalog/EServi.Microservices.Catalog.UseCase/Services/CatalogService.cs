using System;
using System.Threading.Tasks;
using EServi.Microservices.Catalog.UseCase.Models;

namespace EServi.Microservices.Catalog.UseCase.Services
{
    public class CatalogService : ICatalogService
    {
        public Task<PostModel> CreatePost(PostModel postModel)
        {
            throw new NotImplementedException();
        }
        
        public Task<CategoryModel> CreateCategory(CategoryModel categoryModel)
        {
            throw new NotImplementedException();
        }
        
        public Task<SubCategoryModel> CreateSubCategory(SubCategoryModel subCategoryModel)
        {
            throw new NotImplementedException();
        }
    }
}