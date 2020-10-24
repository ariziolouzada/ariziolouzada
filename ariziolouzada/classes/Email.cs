using System;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;

namespace ariziolouzada.classes
{
    public class Email
    {
        ///CONFIGURAÇÃO LOCAWEB 
        ///https://ajuda.locaweb.com.br/wiki/configurar-o-smtp-locaweb-no-outlook-smtp-locaweb/
        ///



        /// <summary>
        /// Método para enviar email sem cópia e sem anexo da classe Email.
        /// </summary>
        /// <param name="nomeRemetente"></param>
        /// <param name="emailRemetente"></param>
        /// <param name="emailDestinatario"></param>
        /// <param name="assuntoMensagem"></param>
        /// <param name="conteudoMensagem"></param>
        /// <returns>Verdadeiro se o email for enviado com sucesso.</returns>
        public static bool Enviar(
                                          string nomeRemetente
                                        , string emailRemetente
                                        , string emailDestinatario
                                        , string assuntoMensagem
                                        , string conteudoMensagem
                                       )
        {

            //Cria objeto com dados do e-mail.
            MailMessage objEmail = new MailMessage();

            //Define o Campo From e ReplyTo do e-mail.
            objEmail.From = new MailAddress(nomeRemetente + "<" + emailRemetente + ">");

            //Define os destinatários do e-mail.
            objEmail.To.Add(emailDestinatario);

            //Enviar cópia para.
            //objEmail.CC.Add(emailComCopia);

            //Enviar cópia oculta para.
            //objEmail.Bcc.Add(emailComCopiaOculta);

            //Define a prioridade do e-mail.
            objEmail.Priority = MailPriority.Normal;

            //Define o formato do e-mail HTML (caso não queira HTML alocar valor false)
            objEmail.IsBodyHtml = true;

            //Define título do e-mail.
            objEmail.Subject = assuntoMensagem;

            //Define o corpo do e-mail.
            objEmail.Body = conteudoMensagem;


            //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");


            // Caso queira enviar um arquivo anexo
            //Caminho do arquivo a ser enviado como anexo
            //string arquivo = Server.MapPath("arquivo.jpg");

            // Ou especifique o caminho manualmente
            //string arquivo = @"e:\home\LoginFTP\Web\arquivo.jpg";

            // Cria o anexo para o e-mail
            //Attachment anexo = new Attachment(arquivo, System.Net.Mime.MediaTypeNames.Application.Octet);

            // Anexa o arquivo a mensagem
            //objEmail.Attachments.Add(anexo);

            //Cria objeto com os dados do SMTP

            var objSmtp = new SmtpClient();

            //Alocamos o endereço do host para enviar os e-mails 
            objSmtp.Host = "localhost";
            //objSmtp.Host = "smtp2.locaweb.com.br";

            objSmtp.Port = 25;

            //Enviamos o e-mail através do método .send()
            try
            {
                objSmtp.Send(objEmail);
                //MessageBoxScript("E-mail enviado com sucesso !");
                return true;
            }
            catch (Exception ex)
            {
                //MessageBoxScript("Ocorreram problemas no envio do e-mail. Erro = " + ex.Message);
                return false;
            }
            finally
            {
                //excluímos o objeto de e-mail da memória
                objEmail.Dispose();
                //anexo.Dispose();
            }



        }
        
        public static string EnviarLocaWeb(
                                          string nomeRemetente
                                        , string emailRemetente
                                        , string emailDestinatario
                                        , string assuntoMensagem
                                        , string conteudoMensagem
                                       )
        {

            //Cria objeto com dados do e-mail.
            MailMessage objEmail = new MailMessage();

            //Define o Campo From e ReplyTo do e-mail.
            objEmail.From = new MailAddress(nomeRemetente + "<" + emailRemetente + ">");

            //Define os destinatários do e-mail.
            objEmail.To.Add(emailDestinatario);

            //Enviar cópia para.
            //objEmail.CC.Add(emailComCopia);

            //Enviar cópia oculta para.
            //objEmail.Bcc.Add(emailComCopiaOculta);

            //Define a prioridade do e-mail.
            objEmail.Priority = MailPriority.Normal;

            //Define o formato do e-mail HTML (caso não queira HTML alocar valor false)
            objEmail.IsBodyHtml = true;

            //Define título do e-mail.
            objEmail.Subject = assuntoMensagem;

            //Define o corpo do e-mail.
            objEmail.Body = conteudoMensagem;


            //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");


            // Caso queira enviar um arquivo anexo
            //Caminho do arquivo a ser enviado como anexo
            //string arquivo = Server.MapPath("arquivo.jpg");

            // Ou especifique o caminho manualmente
            //string arquivo = @"e:\home\LoginFTP\Web\arquivo.jpg";

            // Cria o anexo para o e-mail
            //Attachment anexo = new Attachment(arquivo, System.Net.Mime.MediaTypeNames.Application.Octet);

            // Anexa o arquivo a mensagem
            //objEmail.Attachments.Add(anexo);

            //Cria objeto com os dados do SMTP

            var objSmtp = new SmtpClient();

            //Alocamos o endereço do host para enviar os e-mails 
            objSmtp.Host = "smtp.ariziolouzada.com.br";
            //objSmtp.Host = "email-ssl.com.br";
            //objSmtp.Host = "smtplw.com.br";
            //objSmtp.Host = "smtp2.locaweb.com.br";

            //objSmtp.Port = 465;
            objSmtp.Port = 587;

            objSmtp.EnableSsl = false;

            objSmtp.Credentials = new NetworkCredential("contato@ariziolouzada.com.br", "Manu2019**");
            

            //Enviamos o e-mail através do método .send()
            try
            {
                objSmtp.Send(objEmail);
                //MessageBoxScript("E-mail enviado com sucesso !");
                return "true";
            }
            catch (Exception ex)
            {
                //MessageBoxScript(");
                return "Erro no envio do e-mail: " + ex.Message;
            }
            finally
            {
                //excluímos o objeto de e-mail da memória
                objEmail.Dispose();
                //anexo.Dispose();
            }



        }


        public static string EnviarGmail(
                                          string nomeRemetente
                                        , string emailRemetente
                                        , string emailDestinatario
                                        , string assuntoMensagem
                                        , string conteudoMensagem
                                       )
        {

            //Cria objeto com dados do e-mail.
            MailMessage objEmail = new MailMessage();

            //Define o Campo From e ReplyTo do e-mail.
            objEmail.From = new MailAddress(nomeRemetente + "<" + emailRemetente + ">");

            //Define os destinatários do e-mail.
            objEmail.To.Add(emailDestinatario);

            //Enviar cópia para.
            //objEmail.CC.Add(emailComCopia);

            //Enviar cópia oculta para.
            //objEmail.Bcc.Add(emailComCopiaOculta);

            //Define a prioridade do e-mail.
            objEmail.Priority = MailPriority.High;

            //Define o formato do e-mail HTML (caso não queira HTML alocar valor false)
            objEmail.IsBodyHtml = true;

            //Define título do e-mail.
            objEmail.Subject = assuntoMensagem;

            //Define o corpo do e-mail.
            objEmail.Body = conteudoMensagem;


            //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
            objEmail.SubjectEncoding = Encoding.UTF8;// GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.UTF8; //GetEncoding("ISO-8859-1");


            // Caso queira enviar um arquivo anexo
            //Caminho do arquivo a ser enviado como anexo
            //string arquivo = Server.MapPath("arquivo.jpg");

            // Ou especifique o caminho manualmente
            //string arquivo = @"e:\home\LoginFTP\Web\arquivo.jpg";

            // Cria o anexo para o e-mail
            //Attachment anexo = new Attachment(arquivo, System.Net.Mime.MediaTypeNames.Application.Octet);

            // Anexa o arquivo a mensagem
            //objEmail.Attachments.Add(anexo);

            //Cria objeto com os dados do SMTP

            var objSmtp = new SmtpClient();

            //Alocamos o endereço do host para enviar os e-mails 
            objSmtp.Host = "smtp.gmail.com";
            //objSmtp.Host = "smtp2.locaweb.com.br";

            objSmtp.Port = 587;
            objSmtp.EnableSsl = true;
            objSmtp.UseDefaultCredentials = false;
                
            objSmtp.Credentials = new NetworkCredential("ariziolouzada@gmail.com", "Aaol45**");
            //objSmtp.Credentials = new NetworkCredential("trial", "lAfSrWpd5461");

            //Enviamos o e-mail através do método .send()
            try
            {
                objSmtp.Send(objEmail);
                //MessageBoxScript("E-mail enviado com sucesso !");
                return "true";
            }
            catch (Exception ex)
            {
                //MessageBoxScript("Ocorreram problemas no envio do e-mail. Erro = " + ex.Message);
                //return "false";
                return "Ocorreram problemas no envio do e-mail. Erro: " + ex.Message;
            }
            finally
            {
                //excluímos o objeto de e-mail da memória
                objEmail.Dispose();
                //anexo.Dispose();
            }



        }


        /// <summary>
        /// Método para enviar email com cópia e sem anexo da classe Email.
        /// </summary>
        /// <param name="nomeRemetente"></param>
        /// <param name="emailRemetente"></param>
        /// <param name="emailDestinatario"></param>
        /// <param name="emailComCopia"></param>
        /// <param name="assuntoMensagem"></param>
        /// <param name="conteudoMensagem"></param>
        /// <returns>Verdadeiro se o email for enviado com sucesso.</returns>
        public static bool EnviarComCopia(
                                                  string nomeRemetente
                                                , string emailRemetente
                                                , string emailDestinatario
                                                , string emailComCopia
                                                , string assuntoMensagem
                                                , string conteudoMensagem
                                       )
        {

            //Cria objeto com dados do e-mail.
            MailMessage objEmail = new MailMessage();

            //Define o Campo From e ReplyTo do e-mail.
            objEmail.From = new MailAddress(nomeRemetente + "<" + emailRemetente + ">");

            //Define os destinatários do e-mail.
            objEmail.To.Add(emailDestinatario);

            //Enviar cópia para.
            objEmail.CC.Add(emailComCopia);

            //Enviar cópia oculta para.
            //objEmail.Bcc.Add(emailComCopiaOculta);

            //Define a prioridade do e-mail.
            objEmail.Priority = MailPriority.Normal;

            //Define o formato do e-mail HTML (caso não queira HTML alocar valor false)
            objEmail.IsBodyHtml = true;

            //Define título do e-mail.
            objEmail.Subject = assuntoMensagem;

            //Define o corpo do e-mail.
            objEmail.Body = conteudoMensagem;


            //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");


            // Caso queira enviar um arquivo anexo
            //Caminho do arquivo a ser enviado como anexo
            //string arquivo = Server.MapPath("arquivo.jpg");

            // Ou especifique o caminho manualmente
            //string arquivo = @"e:\home\LoginFTP\Web\arquivo.jpg";

            // Cria o anexo para o e-mail
            //Attachment anexo = new Attachment(arquivo, System.Net.Mime.MediaTypeNames.Application.Octet);

            // Anexa o arquivo a mensagem
            //objEmail.Attachments.Add(anexo);

            //Cria objeto com os dados do SMTP
            var objSmtp = new SmtpClient();

            //Alocamos o endereço do host para enviar os e-mails  
            objSmtp.Host = "localhost";
            objSmtp.Port = 25;

            //Enviamos o e-mail através do método .send()
            try
            {
                objSmtp.Send(objEmail);
                //MessageBoxScript("E-mail enviado com sucesso !");
                return true;
            }
            catch (Exception ex)
            {
                //MessageBoxScript("Ocorreram problemas no envio do e-mail. Erro = " + ex.Message);
                return false;
            }
            finally
            {
                //excluímos o objeto de e-mail da memória
                objEmail.Dispose();
                //anexo.Dispose();
            }



        }

        /// <summary>
        /// Método para enviar email sem cópia e com anexo da classe Email.
        /// </summary>
        /// <param name="nomeRemetente"></param>
        /// <param name="emailRemetente"></param>
        /// <param name="emailDestinatario"></param>
        /// <param name="arquivoAnexo">Caminho completo do arquivo.</param>
        /// <param name="assuntoMensagem"></param>
        /// <param name="conteudoMensagem"></param>
        /// <returns>Verdadeiro se o email for enviado com sucesso.</returns>
        public static bool EnviarComAnexo(
                                                  string nomeRemetente
                                                , string emailRemetente
                                                , string emailDestinatario
                                                , string arquivoAnexo
                                                , string assuntoMensagem
                                                , string conteudoMensagem
                                       )
        {

            //Cria objeto com dados do e-mail.
            MailMessage objEmail = new MailMessage();

            //Define o Campo From e ReplyTo do e-mail.
            objEmail.From = new MailAddress(nomeRemetente + "<" + emailRemetente + ">");

            //Define os destinatários do e-mail.
            objEmail.To.Add(emailDestinatario);

            //Enviar cópia para.
            //objEmail.CC.Add(emailComCopia);

            //Enviar cópia oculta para.
            //objEmail.Bcc.Add(emailComCopiaOculta);

            //Define a prioridade do e-mail.
            objEmail.Priority = MailPriority.Normal;

            //Define o formato do e-mail HTML (caso não queira HTML alocar valor false)
            objEmail.IsBodyHtml = true;

            //Define título do e-mail.
            objEmail.Subject = assuntoMensagem;

            //Define o corpo do e-mail.
            objEmail.Body = conteudoMensagem;


            //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");


            // Caso queira enviar um arquivo anexo
            //Caminho do arquivo a ser enviado como anexo
            //string arquivo = Server.MapPath("arquivo.jpg");

            // Ou especifique o caminho manualmente
            //string arquivo = @"e:\home\LoginFTP\Web\arquivo.jpg";

            // Cria o anexo para o e-mail
            Attachment anexo = new Attachment(arquivoAnexo, System.Net.Mime.MediaTypeNames.Application.Octet);

            // Anexa o arquivo a mensagem
            objEmail.Attachments.Add(anexo);

            //Cria objeto com os dados do SMTP
            var objSmtp = new SmtpClient();

            //Alocamos o endereço do host para enviar os e-mails  
            objSmtp.Host = "localhost";
            objSmtp.Port = 25;

            //Enviamos o e-mail através do método .send()
            try
            {
                objSmtp.Send(objEmail);
                //MessageBoxScript("E-mail enviado com sucesso !");
                return true;
            }
            catch (Exception ex)
            {
                //MessageBoxScript("Ocorreram problemas no envio do e-mail. Erro = " + ex.Message);
                return false;
            }
            finally
            {
                //excluímos o objeto de e-mail da memória
                objEmail.Dispose();
                //anexo.Dispose();
            }



        }


        public static bool EnviarPeloGmail(
                                                  string nomeRemetente
                                                , string emailRemetente
                                                , string emailDestinatario
                                                , string assuntoMensagem
                                                , string conteudoMensagem)
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(nomeRemetente + "<" + emailRemetente + ">");
            mail.To.Add(emailDestinatario); // para
            mail.Subject = assuntoMensagem; // assunto
            mail.Body = conteudoMensagem; // mensagem
            //mail.BodyEncoding = System.Text.Encoding.UTF8;
            //mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High; //Prioridade do E-Mail

            using (var smtp = new SmtpClient("smtp.gmail.com"))
            {
                smtp.Port = 465;       // porta para SSL
                smtp.UseDefaultCredentials = false;
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network; // modo de envio
                smtp.UseDefaultCredentials = false; // vamos utilizar credencias especificas

                // seu usuário e senha para autenticação
                smtp.Credentials = new NetworkCredential("ariziolouzada@gmail.com", "Aaol40**");
                smtp.EnableSsl = true; // GMail requer SSL

                try
                {
                    // envia o e-mail
                    smtp.Send(mail);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                    //throw;
                }
                finally
                {
                    //excluímos o objeto de e-mail da memória
                    mail.Dispose();
                    //anexo.Dispose();
                }
            }

        }

        public static bool IsEmailValido(string email)
        {
            var rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
            return (rg.IsMatch(email));
        }

    }

}
