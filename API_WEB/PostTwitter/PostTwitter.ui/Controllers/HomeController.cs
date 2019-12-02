using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PostTwitter.ui.Models;
using PostTwitter.api.Domain.Models;
using PostTwitter.api.Domain.Services;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PostTwitter.ui.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;

        public HomeController(IPostService postService)
        {
            _postService = postService;
        }

        public IActionResult Index()
        {
            string query = "#itau";

            Twitter t = new Twitter();
            var listPost = t.returnPostTwitter(query);

            return View(listPost);
        }

        [HttpPost]
        public IActionResult Index(string tag)
        {
            Twitter t = new Twitter();
            var listPost = t.returnPostTwitter(tag);

            foreach(var post in listPost)
            {
                _postService.Add(post);
            }
              

            return View(listPost);
        }


        
        //public async Task UpdateProdutoAsync(Produto produto)
        //{
        //    string url = "http://www.macwebapi.somee.com/api/produtos/{0}";
        //    var uri = new Uri(string.Format(url, produto.Id));
        //    var data = JsonConvert.SerializeObject(produto);
        //    var content = new StringContent(data, Encoding.UTF8, "application/json");
        //    HttpResponseMessage response = null;
        //    response = await client.PutAsync(uri, content);

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        throw new Exception("Erro ao atualizar produto");
        //    }
        //}

    }
}
