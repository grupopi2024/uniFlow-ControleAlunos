using ControleInternet.BLL;
using ControleInternet.Models;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace ControleInternet.Controllers
{
    public class PrincipalController : Controller
    {
        BLL_LoginUsuario _bllLoginUsuario = new BLL_LoginUsuario();
        BLL_TrocaSenhaUsuario _bllTrocaSenhaUsuario = new BLL_TrocaSenhaUsuario();
        BLL_InformacoesPessoais _bllInformacoesPessoais = new BLL_InformacoesPessoais();

        // GET: Principal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Principal()
        {
            ViewBag.User = Session["USUARIO"];
            return View();
        }

        public ActionResult TrocaSenhaUsuario()
        {
            return View();
        }

        public ActionResult Frequencia()
        {
            return View();
        }

        public ActionResult ListarAluno()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario user)
        {
            var ret = _bllLoginUsuario.LoginUsuario(user);
            Session["USUARIOLOGADO"] = ret;

            string jsonResult = JsonConvert.SerializeObject(ret, Formatting.None);

            return Json(new
            {
                retorno = jsonResult
            });
        }

        [HttpPost]
        public ActionResult TrocaSenha(TrocaSenhaUsuario user)
        {
            var ret = _bllTrocaSenhaUsuario.TrocaSenhaUsuario(user);

            return Json(new
            {
                retorno = ret
            });
        }

        [HttpPost]
        public ActionResult AtualizaDadosUsuario(Usuario user)
        {
            UsuarioId userId = new UsuarioId();
            userId._id = Convert.ToString(user._id);
            var retornoUsuarioEmail = _bllInformacoesPessoais.BuscarDadosPessoaisByEmail(user.Email);

            if (retornoUsuarioEmail == null)
            {
                return Json(new
                {
                    retorno = "Erro ao atualizar usuário"
                });
            }

            Session["USUARIO"] = null;
            Session["USUARIO"] = retornoUsuarioEmail;

            user._id = retornoUsuarioEmail._id;
            user.Senha = retornoUsuarioEmail.Senha;
            user.Email = retornoUsuarioEmail.Email;

            var ret = _bllInformacoesPessoais.AlteraDadosPessoais(user);

            return Json(new
            {
                retorno = ret
            });
        }

        [HttpPost]
        public ActionResult BuscarDadosPessoaisById(UsuarioId user)
        {
            var ret = _bllInformacoesPessoais.BuscarDadosPessoaisById(user);

            Session["USUARIO"] = null;
            Session["USUARIO"] = ret;

            return Json(new
            {
                retorno = ret
            });
        }
    }
}