using ControleInternet.BLL;
using ControleInternet.Models;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace ControleInternet.Controllers
{
    public class AlunoController : Controller
    {
        BLL_Aluno bllAluno = new BLL_Aluno();
        BLL_ListarAluno bllListarAluno = new BLL_ListarAluno();
        BLL_CadastroAluno bllCadastrarAluno = new BLL_CadastroAluno();

        // GET: Aluno
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ControleAcesso()
        {
            return View();
        }
        public ActionResult CadastroAluno()
        {
            return View();
        }
        public ActionResult ListarAluno()
        {
            return View();
        }
        public ActionResult EditarAluno()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ObterAlunoRA(AlunoRA request)
        {
            var ret = bllAluno.ObterAlunoRA(request.RA);

            Session["ALUNOEDITAR"] = ret;
            string jsonResult = JsonConvert.SerializeObject(ret, Formatting.None);

            return Json(new
            {
                retorno = jsonResult
            });
        }

        [HttpPost]
        public ActionResult CadastrarAluno(Aluno user)
        {
            var ret = bllCadastrarAluno.CadastrarAluno(user);

            return Json(new
            {
                retorno = ret
            });
        }

        [HttpPost]
        public ActionResult ExcluirAluno(AlunoRA request)
        {
            var ret = bllAluno.ExcluirAluno(request.RA);

            return Json(new
            {
                retorno = ret
            });
        }

        [HttpPost]
        public ActionResult EdicaoAluno(Aluno user)
        {
            var ret = bllCadastrarAluno.EditarAluno(user);

            return Json(new
            {
                retorno = ret
            });
        }

        [HttpPost]
        public ActionResult GetLista()
        {
            var ret = bllListarAluno.ListarAluno();
            return Json(new
            {
                retorno = ret
            });

        }

        [HttpPost]
        public ActionResult GetControleAcesso()
        {
            var ret = bllListarAluno.ListarControleAcesso();

            return Json(new
            {
                retorno = ret
            });
        }

        [HttpPost]
        public ActionResult EntradaAluno(EntradaSaidaAluno request)
        {
            var ret = bllAluno.EntradaAluno(request);

            return Json(new
            {
                retorno = ret
            });
        }

        [HttpPost]
        public ActionResult SaidaAluno(EntradaSaidaAluno request)
        {
            var ret = bllAluno.SaidaAluno(request);

            return Json(new
            {
                retorno = ret
            });
        }
    }
}
