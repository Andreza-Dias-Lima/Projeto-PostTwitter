using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostTwitter.api.Domain.Models;
using PostTwitter.api.Domain.Repositories;
using PostTwitter.api.Domain.Services;

namespace PostTwitter.api.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public List<Post> GetAll()
        {
            return _postRepository.GetAll();
        }

        public List<Usuario> GetUsuarioMaxSeguidores()
        {
            return _postRepository.GetUsuarioMaxSeguidores();
        }

        public IEnumerable<object> GetTotalPostPorData()
        {
            return _postRepository.GetTotalPostPorData();
        }

        public IEnumerable<object> GetTotalTagsPorLocal()
        {
            return _postRepository.GetTotalTagsPorLocal();
        }

        public Post Add(Post post)
        {
            return _postRepository.Add(post);
        }

    }
}
