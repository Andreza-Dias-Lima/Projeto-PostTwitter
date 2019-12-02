using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostTwitter.api.Domain.Models;

namespace PostTwitter.api.Domain.Services
{
    public interface IPostService
    {
        List<Post> GetAll();

        List<Usuario> GetUsuarioMaxSeguidores();

        IEnumerable<object> GetTotalPostPorData();

        IEnumerable<object> GetTotalTagsPorLocal();

        Post Add(Post post);
    }
}
