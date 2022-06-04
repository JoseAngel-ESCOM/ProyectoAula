using System;
using System.Net;
using System.Net.Mail;

namespace ProyectoAula.Utilerias
{
    public class CorreoElectronico
    {
        private SmtpClient cliente;
        private MailMessage email;

        private string _Host = "smtp.ionos.mx;";
        private int _Puerto = 587;
        private string _Usuario = "noreply@taxcom.info";
        private string _Password = "I!0Ycj8%z";
        private bool _Ssl = true;

        public CorreoElectronico()
        {
            cliente = new SmtpClient(_Host, _Puerto)
            {
                EnableSsl = _Ssl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_Usuario, _Password)
            };
        }

        public void EnviarCorreo(string destinatario, string asunto, string mensaje, bool esHtlm = true)
        {
            email = new MailMessage(_Usuario, destinatario, asunto, mensaje);
            email.IsBodyHtml = esHtlm;
            cliente.Send(email);
        }
        public void EnviarCorreo(MailMessage message)
        {
            cliente.Send(message);
        }

        public static void Enviar(string destinatario, string asunto, string mensaje, bool esHtlm = true)
        {
            try
            {
                var correo = new CorreoElectronico();
                correo.EnviarCorreo(destinatario, asunto, mensaje, esHtlm);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
