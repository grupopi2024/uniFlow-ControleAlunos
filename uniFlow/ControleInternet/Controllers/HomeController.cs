using ControleInternet.BLL;
using System.Web.Mvc;

namespace ControleInternet.Controllers
{

    public class HomeController : Controller
    {
        BLL_CadastroUsuario bllUsuario = new BLL_CadastroUsuario();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Teste()
        {
            return View();
        }
    }
}