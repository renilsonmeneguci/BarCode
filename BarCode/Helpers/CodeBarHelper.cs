using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Web;

namespace BarCode.Helpers
{
    public class CodeBarHelper
    {
        public static byte[] GenerarCodigo(string code, string format, int width, int height, int size)
        {
            var resultado = new byte[] { };
            width = (width == 0) ? 200 : width;
            height = (width == 0) ? 60 : height;
            size = (width == 0) ? 60 : size;

            if (!string.IsNullOrEmpty(code))
            {
                using (var stream = new MemoryStream())
                {
                    var bitmap = new Bitmap(width, height);
                    var grafic = Graphics.FromImage(bitmap);
                    var fuente = CargarFuente(format, size);
                    var point = new Point();
                    var brush = new SolidBrush(Color.Black);

                    grafic.FillRectangle(new SolidBrush(Color.White), 0, 0, width, height);
                    grafic.DrawString(FormatBarCode(code.ToUpper()), fuente, brush, point);
                    bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    stream.Seek(0, SeekOrigin.Begin);
                    resultado = stream.ToArray();
                }
            }

            return resultado;
        }


        /// <summary>
        /// Formato con los caracteres de escape establecidos en la fuente que utilizamos.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private static string FormatBarCode(string code)
        {
            return string.Format("*{0}*", code);
        }
        /// <summary>
        /// Generamos la nueva fuente para cargar en la imagen
        /// </summary>
        /// <param name="fuente"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private static Font CargarFuente(string fuente, int size)
        {
            var pfc = new PrivateFontCollection();
            var f = "BARCOD39.TTF";

            switch (fuente)
            {
                case "ITF":
                    f = "ITF.TTF";
                    break;
                case "E39":
                    f = "BARCOD39.TTF";
                    break;
                case "E13":
                    f = "EAN-13.TTF";
                    break;
                case "E9":
                    f = "FRE3OF9X.TTF";
                    break;
            }

            var path = System.Web.Hosting.HostingEnvironment.MapPath("~/fonts/");
            pfc.AddFontFile(path + f);
            return new Font(pfc.Families[0], size);
        }
    }
}