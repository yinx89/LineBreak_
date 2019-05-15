using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LineBreak_.Models;

namespace LineBreak_.Controllers
{
    public class LineBreakController : Controller
    {
        // GET: LineBreak
        public ActionResult Index()
        {
            var LineBreak = new LineBreak();
            return View(LineBreak);
        }
        [HttpPost]
        public ActionResult Index(LineBreak LineBreak)
        {
            LineBreak.Resultado = LineBreak_.Models.Program.Separa(LineBreak.Texto,LineBreak.Numero);
            return Json(LineBreak);
            // Json por View
        }
    }
}