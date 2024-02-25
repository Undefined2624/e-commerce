using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace BusinessLayer
{
    public static class BL_Recursos
    {
        public static string convertirSHA256(string clave)
        {
            using (System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(clave));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }   

        public static string GenerarClave()
        {
            
            string clave = Guid.NewGuid().ToString("N").Substring(0, 8);
            return clave;

        }   

        public static bool EnviarCorreo(string correo, string asunto, string mensaje)
        {

            bool resultado = false;

            try
            {
                
                MailMessage mail = new MailMessage();
                mail.To.Add(correo); //Correo destino
                mail.From = new MailAddress("priscyxdxd@gmail.com"); //Correo origen
                mail.Subject = asunto; //Asunto del correo
                mail.Body = mensaje; //Mensaje del correo

                mail.IsBodyHtml = true; //El mensaje se envía como HTML

                var smtp = new SmtpClient
                {
                    Credentials = new NetworkCredential("priscyxdxd@gmail.com", "hiidenpshdqfqqzc"),
                    Host = "smtp.gmail.com", //Servidor de correo
                    Port = 587,
                    EnableSsl = true
                };

                smtp.Send(mail); //Envío del correo
                resultado = true;

            }catch (Exception ex)
            {
                resultado = false;
                
            }

            return resultado;

        }

        public static String ConvertirBase64(string ruta, out bool conversion)
        {

            string textoBase64 = string.Empty;
            conversion = true;

            try
            {

                byte[] bytes = File.ReadAllBytes(ruta);
                textoBase64 = Convert.ToBase64String(bytes);

            }catch (Exception ex)
            {

                conversion = false;

            }

            return textoBase64;

        }
       
    }
}
