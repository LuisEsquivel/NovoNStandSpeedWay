

using System;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Web.Models;

namespace Web.Helpers
{
    public class Generals
    {

        public string CockieName = "UserIdNovoSpeedWay";
        public string key = "ABCDEFGHIJKLMÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz";
        public Services.Services services;

        public Generals()
        {
            services = new Services.Services();
        }
  


        public bool SendEmailSMTP(string CuentaDeCorreo, string CodigoVerificacion)
        {

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            try
            {

                String Body = @"<br></br>";

                Body += @"<center><div style=""background-color:#F5F5F5;""><br/><img src=""https://novosys.com.mx/Img/NOVO_logosmall-1.png"" width=""320"" height=""150""></div></center>";

                Body += @"<center>TU CÓDIGO DE VERIFICACIÓN ES<center/>";

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


        //we get the id of the current user 
        public int UserId()
        {
            return Convert.ToInt32(GetCookieValue(CockieName));
        }


        //we get the bit value of the user to know if the user is administrator
        public bool IsAdmin()
        {
            if (GetCookieValue(CockieName) == null) return false;
            int UsuarioIdInt = 0;
            int.TryParse(GetCookieValue(CockieName), out UsuarioIdInt);

            if (UsuarioIdInt > 0)
            {
                return services.Get<Usuario>("usuarios").Where(x => x.UsuarioIdInt.ToString() == GetCookieValue(CockieName)).FirstOrDefault().EsAdminBit;
            }

            return false;
        }



        //we get the value of the cookie
        public string GetCookieValue(string CookieName)
        {

            string value = null;

            if (System.Web.HttpContext.Current.Request.Cookies[CookieName] != null)
            {
                if (System.Web.HttpContext.Current.Request.Cookies[CookieName].Value.ToString().Trim().Length > 0)
                {
                    value = System.Web.HttpContext.Current.Request.Cookies[CookieName].Value.ToString();
                }
            }

            if (value == null) return null;
            return Decrypt(value);
        }




        //if exist the cookie then we delete and after that we create a new cookie for add value in the browser
        public void CreateCookie(string value)
        {

            if (System.Web.HttpContext.Current.Request.Cookies[CockieName] != null)
            {
                if (System.Web.HttpContext.Current.Request.Cookies[CockieName].Value.ToString().Trim().Length > 0)
                {
                    var c = new HttpCookie(CockieName);
                    c.Expires = DateTime.Now.AddDays(-1);
                    System.Web.HttpContext.Current.Response.Cookies.Add(c);
                }

            }



            HttpCookie cookie = new HttpCookie(CockieName);
            cookie.Value = Encrypt(value);
            cookie.Expires = DateTime.Now.AddMonths(1);
            cookie.HttpOnly = true;
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        }




        //first we encrypt the value of the coockie and finally we convert to base64 the value encrypted
        public string Encrypt(string value)
        {
            //we get one key aceptable (24 bits) for TripleDESCryptoServiceProvider
            byte[] slt = Encoding.UTF8.GetBytes(key);
            var pdb = new Rfc2898DeriveBytes(key, slt);
            var keyAceptable = pdb.GetBytes(24);

            //we use the algoritm TripleDESCryptoServiceProvider()
            var tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyAceptable;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            //we instance ICryptoTransform and we use the algoritm CreateEncryptor()
            ICryptoTransform cTransform = tdes.CreateEncryptor();

            //we encrypt the array bytes
            var BytesValue = Encoding.UTF8.GetBytes(value);
            byte[] ArrayResultado = cTransform.TransformFinalBlock(BytesValue, 0, BytesValue.Length);

            tdes.Clear();

            //we return the value of array in Base64String
            return Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
        }





        //we receive the value en base64, aftter that we convert the value to an array of bytes 
        //with Convert.FromBase64String abnd finally we decrypt that array of bytes with TransformFinalBlock
        //and we return this value as string 
        public string Decrypt(string value)
        {

            byte[] slt = Encoding.UTF8.GetBytes(key);
            var pdb = new Rfc2898DeriveBytes(key, slt);
            var keyAceptable = pdb.GetBytes(24);

            var tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyAceptable;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();

            var BytesValue = Convert.FromBase64String(value);
            byte[] resultArray = cTransform.TransformFinalBlock(BytesValue, 0, BytesValue.Length);

            tdes.Clear();

            //we return the result in string form
            return Encoding.UTF8.GetString(resultArray);
        }


    }
}
