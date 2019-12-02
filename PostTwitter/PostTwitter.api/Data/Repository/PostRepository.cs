using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostTwitter.api.Domain.Models;
using PostTwitter.api.Domain.Repositories;
using PostTwitter.api.Data;

namespace PostTwitter.api.Data.Repositories
{
    public class PostRepository :BaseRepository, IPostRepository
    {
        public PostRepository(PostTwitterContext context) : base(context){}
        public List<Post> GetAll()
        {
            return _context.Set<Post>().ToList();
        }

        public List<Usuario> GetUsuarioMaxSeguidores()
        {
            return _context.Usuarios.OrderByDescending(mr => mr.QtdeSeguidores).Take(5).Distinct().ToList();
        }

        public IEnumerable<object> GetTotalPostPorData()
        {
            var ret = _context.Posts.GroupBy(p => p.Data)
                        .Select(group => new
                        {
                            Data = group.Key,
                            Count = group.Count()
                        }).ToList();

            return ret;
        }

        public IEnumerable<object> GetTotalTagsPorLocal()
        {
            var ret = _context.Posts.GroupBy( p => new { p.Tag, p.Local })
                  .Select(p => new
                  {
                      Tag = p.Key.Tag,
                      Local = p.Key.Local,
                      Count = p.Count()
                  }).ToList();

            return ret;
        }

        public Post Add(Post post)
        {
            var usuario = AddUsuario(post.Usuario);

            post.Usuario = usuario;

            _context.Set<Post>().Add(post);
            _context.SaveChanges();

            return post;
        }

        public Usuario AddUsuario(Usuario usuario)
        {
            var existeUsuario = _context.Set<Usuario>().Where(p => p.Cd_Twitter == usuario.Cd_Twitter).AsEnumerable().FirstOrDefault();

            if (existeUsuario != null)
                usuario = existeUsuario;
            else
            {
                _context.Set<Usuario>().Add(usuario);
                _context.SaveChanges();
            }

            return usuario;
        }
    }
}
