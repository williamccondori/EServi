using System;
using System.Threading.Tasks;
using EServi.Microservices.Catalog.Domain.Repositories;
using EServi.Microservices.Catalog.UseCase.Models;

namespace EServi.Microservices.Catalog.UseCase.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IPostRepository _postRepository;

        public CatalogService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<PostModel> GetPostById(Guid id)
        {
            var post = await _postRepository.GetById(id);

            return new PostModel
            {
            };
        }

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