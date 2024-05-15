using ControleInternet.BLL;
using ControleInternet.Models;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace ControleInternet.Controllers
{
    public class UsuarioController : Controller
    {
        BLL_Usuario bllUsuario = new BLL_Usuario();
        BLL_ListarUsuario bllListarUsuario = new BLL_ListarUsuario();
        BLL_CadastroUsuario bllCadastrarUsuario = new BLL_CadastroUsuario();

        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CadastroUsuario()
        {
            return View();
        }

        public ActionResult EditarUsuario()
        {
            return View();
        }

        public ActionResult GerenciarUsuario()
        {
            ViewBag.UsuariosCadastrados = Session["UsuariosCadastrados"];
            return View();
        }

        public ActionResult ListarUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ObterUsuarios()
        {
            var ret = bllUsuario.ObterUsuario();

            Session["UsuariosCadastrados"] = ret;
            ViewBag.UsuariosCadastrados = ret;


            string jsonResult = JsonConvert.SerializeObject(ret, Formatting.None);

            return Json(new
            {
                retorno = jsonResult
            });
        }

        [HttpPost]
        public ActionResult ObterUsuarioCPF(UsuarioCPF request)
        {
            var ret = bllUsuario.ObterUsuarioCPF(request.CPF);

            Session["USUARIOEDITAR"] = ret;
            string jsonResult = JsonConvert.SerializeObject(ret, Formatting.None);

            return Json(new
            {
                retorno = jsonResult
            });
        }

        [HttpPost]
        public ActionResult ExcluirUsuario(UsuarioCPF request)
        {
            var ret = bllUsuario.ExcluirUsuario(request);

            return Json(new
            {
                retorno = ret
            });
        }

        [HttpPost]
        public ActionResult GetLista()
        {
            var ret = bllListarUsuario.ListarUsuario();
            return Json(new
            {
                retorno = ret
            });

        }

        [HttpPost]
        public ActionResult CadastrarUsuario(Usuario user)
        {
            var ret = bllCadastrarUsuario.CadastrarUsuario(user);

            return Json(new
            {
                retorno = ret
            });
        }

        [HttpPost]
        public ActionResult EdicaoUsuario(Usuario user)
        {
            var ret = bllCadastrarUsuario.EditarUsuario(user);

            return Json(new
            {
                retorno = ret
            });
        }
    }
}