

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ApiPlafonesWeb.Helpers
{
    public class Generals : ControllerBase
    {



        public bool SendEmailSMTP(string CuentaDeCorreo, string CodigoVerificacion)
        {

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient();

            try
            {

                String Body = @"<br></br>";

                Body += @"<center><div style=""background-color:#F5F5F5;""><br/><img src=""https://novosys.com.mx/Img/NOVO_logosmall-1.png"" width=""320"" height=""150""></div></center>";

                Body += @"<center>TU CÓDIGO DE VERIFICACION ES<center/>";

                Body += @"<br></br>";

                Body += @"<center style=""font-size:40px !important;"">"+CodigoVerificacion+"<center/>";

                mail.From = new MailAddress("informacion.novosys@gmail.com");

                mail.To.Add(CuentaDeCorreo);

                mail.Subject = "Verificación de tu cuenta Novosys :)";
                mail.IsBodyHtml = true;
                mail.Body = Body;

                SmtpServer.Port = 587;

                SmtpServer.EnableSsl = true;

                //Godaddy configuration
                //SmtpServer.Host = "relay-hosting.secureserver.net";
                //SmtpServer.Host = "smtpout.secureserver.net";
                SmtpServer.Host = "smtp.gmail.com";

                SmtpServer.UseDefaultCredentials = false;

                //Godaddy configurarion No Credentials
                SmtpServer.Credentials = new System.Net.NetworkCredential("informacion.novosys@gmail.com", "Nov2010?");

                SmtpServer.Send(mail);

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


    }
}
