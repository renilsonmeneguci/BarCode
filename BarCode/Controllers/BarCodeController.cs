using System.Web.Mvc;
using System.IO;
using System.Drawing;
using BarCode.Helpers;
using BarcodeLib;
using System.Drawing.Imaging;

namespace BarCode.Controllers
{
    public class BarCodeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Invalido()
        {
            return View();
        }

        public ActionResult GerarCodigo(string texto, int? largura, int? altura)
        {
            texto = texto ?? "0000000000000000000000000000000000000000000000";
            largura = largura ?? 800;
            altura = altura ?? 100;

            try
            {
                Barcode b = new Barcode();
                Image img = b.Encode(TYPE.Interleaved2of5, texto, Color.Black, Color.White, (int)largura, (int)altura);

                using (var streak = new MemoryStream())
                {
                    img.Save(streak, ImageFormat.Png);
                    return File(streak.ToArray(), "image/png");
                }
            }
            catch (System.Exception)
            {

                return RedirectToAction("Invalido");
            }
        }
    }
}