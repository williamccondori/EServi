using System;
using System.Threading.Tasks;
using EServi.Microservices.Catalog.UseCase.Models;

namespace EServi.Microservices.Catalog.UseCase.Services
{
    public interface ICatalogService
    {
        Task<PostModel> GetPostById(Guid id);

        Task<PostModel> CreatePost(PostModel postModel);

        Task<CategoryModel> CreateCategory(CategoryModel categoryModel);

        Task<SubCategoryModel> CreateSubCategory(SubCategoryModel subCategoryModel);
    }
}