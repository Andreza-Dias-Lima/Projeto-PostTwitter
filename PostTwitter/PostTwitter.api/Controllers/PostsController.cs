using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PostTwitter.api.Domain.Models;
using PostTwitter.api.Domain.Services;

namespace PostTwitter.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ILogger _logger;

        public PostsController(ILogger<PostsController> logger, IPostService postService)
        {
            _postService = postService;
            _logger = logger;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<List<Post>> GetAll()
        {
            _logger.LogInformation("--------- Executando método getAll Posts ----------");
            _logger.LogDebug("Acessando serviço _postService....");

            var result = new List<Post>();

            try
            {
                result = _postService.GetAll();

                if (result == null)
                    _logger.LogWarning("Não houve retorno na busca!");
                else
                    _logger.LogDebug("Dados retornado com sucesso..." + result.ToString());

            }
            catch (Exception ex)
            {
                _logger.LogError("Logging error = Ocorreu um erro na busca dos Posts...");
                _logger.LogError("Erro= "+ ex.Message);
            }

            _logger.LogInformation("--------- Fim metodo getAll Posts ----------");

            return result;
        }

        [HttpGet(Name = "getUsuarioMaxSeguidores")]
        public ActionResult<List<Usuario>> GetUsuarioMaxSeguidores()
        {
            _logger.LogInformation("--------- Executando método getUsuarioMaxSeguidores ----------");
            _logger.LogDebug("Acessando serviço _postService....");

            var result = new List<Usuario>();

            try
            {
                result = _postService.GetUsuarioMaxSeguidores();

                if (result == null)
                    _logger.LogWarning("Não houve retorno na busca!");
                else
                    _logger.LogDebug("Dados retornado com sucesso...");

            }
            catch (Exception ex)
            {
                _logger.LogError("Logging error = Ocorreu um erro na busca do método getUsuarioMaxSeguidores...");
                _logger.LogError("Erro= " + ex.Message);
            }

            _logger.LogInformation("--------- Fim metodo getUsuarioMaxSeguidores ----------");

            return result;
        }

        [HttpGet(Name = "getTotalPostPorData")]
        public ActionResult<IEnumerable<object>> GetTotalPostPorData()
        {
            _logger.LogInformation("--------- Executando método getTotalPostPorData ----------");
            _logger.LogDebug("Acessando serviço _postService....");

            var result = new object();

            try
            {
                result = _postService.GetTotalPostPorData();

                if (result == null)
                    _logger.LogWarning("Não houve retorno na busca!");
                else
                    _logger.LogDebug("Dados retornado com sucesso...");

            }
            catch (Exception ex)
            {
                _logger.LogError("Logging error = Ocorreu um erro na busca do método getTotalPostPorData...");
                _logger.LogError("Erro= " + ex.Message);
            }

            _logger.LogInformation("--------- Fim metodo getTotalPostPorData ----------");

            return new ObjectResult(result);
        }

        [HttpGet(Name = "getTotalTagsPorLocal")]
        public ActionResult<IEnumerable<object>> GetTotalTagsPorLocal()
        {
            _logger.LogInformation("--------- Executando método getTotalTagsPorLocal ----------");
            _logger.LogDebug("Acessando serviço _postService....");

            var result = new object();

            try
            {
                result = _postService.GetTotalTagsPorLocal();

                if (result == null)
                    _logger.LogWarning("Não houve retorno na busca!");
                else
                    _logger.LogDebug("Dados retornado com sucesso...");

            }
            catch (Exception ex)
            {
                _logger.LogError("Logging error = Ocorreu um erro na busca do método getTotalPostPorData...");
                _logger.LogError("Erro= " + ex.Message);
            }

            _logger.LogInformation("--------- Fim metodo getTotalTagsPorLocal ----------");

            return new ObjectResult(result);
        }

        [HttpPost]
        public ActionResult<Post> Add(Post post)
        {
            _logger.LogInformation("--------- Executando método Add Post ----------");
            _logger.LogDebug("Acessando serviço Add Post....");

            var result = new object();

            try
            {
                if (post == null)
                {
                    _logger.LogError("Logging error = post null");
                    return BadRequest();
                }
                
                result = _postService.Add(post);

                if (result == null)
                    _logger.LogWarning("Não houve retorno do objeto inserido!");
                else
                    _logger.LogDebug("Dados inseridos com sucesso...");

            }
            catch (Exception ex)
            {
                _logger.LogError("Logging error = Ocorreu um erro na inserção...");
                _logger.LogError("Erro= " + ex.Message);
            }

            _logger.LogInformation("--------- Fim metodo Add Post ----------");

            return new ObjectResult(result);
        }
    }
}
