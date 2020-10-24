using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Web.UI;

namespace ariziolouzada.sbessencial.sistema
{
    public partial class gerar_captcha : Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            // prepara os componentes de saída para desenho
            Bitmap bmp = new Bitmap(150, 25);
            Graphics grap = Graphics.FromImage(bmp);

            grap.Clear(Color.SteelBlue);// cor de fundo da imagem
            grap.TextRenderingHint = TextRenderingHint.SystemDefault;//modo de redenrização da imagem

            // gera randomicamente letras e números
            string senhaCaptcha = "", senhaTemp = "";
            Random rand = new Random();
            while (senhaCaptcha.Length < 6)
            {
                //senhaTemp += (char)rand.Next(48, 122); // 48 = zero...  122 = z
                senhaTemp += (char)rand.Next(65, 90); // Somente letras maiúsculas
                senhaCaptcha += RetiraCaracterEspecial(senhaTemp);//elimina caracteres especiais
                senhaTemp = "";
            }

            // grava na variável de sessão
            Session["captchaSbEssencial"] = senhaCaptcha;

            // formata a fonte
            Font fonte = new Font("Arial", 18, FontStyle.Italic);

            //centraliza o texto dentro da imagem
            StringFormat formatter = new StringFormat();
            formatter.LineAlignment = StringAlignment.Center;
            formatter.Alignment = StringAlignment.Center;

            RectangleF rectangle = new RectangleF(0, 0, bmp.Width, bmp.Height);

            //grap.DrawString(senhaCaptcha, fonte, Brushes.White, 1, 1);
            grap.DrawString(senhaCaptcha, fonte, Brushes.White, rectangle, formatter);

            // manda para o response
            Response.ContentType = "image/gif";
            bmp.Save(Response.OutputStream, ImageFormat.Gif);

            // limpa variáveis
            grap.Dispose();
            fonte.Dispose();
            bmp.Dispose();
        }


        protected string RetiraCaracterEspecial(string valor)
        {
            string valorSemCaracteresEpeciais = valor.Replace(":", "")
                                                     .Replace(";", "")
                                                     .Replace("<", "")
                                                     .Replace("=", "")
                                                     .Replace(">", "")
                                                     .Replace("?", "")
                                                     .Replace("@", "")
                                                     .Replace("[", "")
                                                     .Replace("\\", "")
                                                     .Replace("]", "")
                                                     .Replace("^", "")
                                                     .Replace("_", "")
                                                     .Replace("`", "");
            return valorSemCaracteresEpeciais;
        }

    }
}