using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Cofim.Common;
using Cofim.Common.Model.DataEntity;

namespace Cofim.Web.Helpers
{
    public class MailSettings
    {
        public string From { get; set; }
        public string Smtp { get; set; }

        public string Port { get; set; }
        public string Password { get; set; }
        public string HeaderBodyMail { get; set; }
        public string FooterBodyMail { get; set; }

        public MailSettings(string from, string smtp, string port, string password)
        {
            From = from;
            Smtp = smtp;
            Port = port;
            Password = password;

            HeaderBodyMail =    $"<table style = 'max-width: 600px; padding: 10px; margin:0 auto; border-collapse: collapse;'>" +
                                $"  <tr>" +
                                $"    <td style = 'background-color: #34495e; text-align: center; padding: 0'>" +
                                $"       <a href = 'https://www.facebook.com/{MessageCenter.Project_Name}/' >" +
                                $"         <img width = '20%' style = 'display:block; margin: 1.5% 3%' src= ''{MessageCenter.URL}images/line_separator.png'>" +
                                $"       </a>" +
                                $"  </td>" +
                                $"  </tr>" +
                                $"  <tr>" +
                                $"  <td style = 'padding: 0'>" +
                                $"     <img style = 'padding: 0; display: block' src = '{MessageCenter.URL}images/Logo.png' width = '100%'>" +
                                $"  </td>" +
                                $"</tr>" +
                                $"<tr>" +
                                $" <td style = 'background-color: #ecf0f1'>" +
                                $"      <div style = 'color: #34495e; margin: 4% 10% 2%; text-align: justify;font-family: sans-serif'>" +
                                $"            <h1 style = 'color: #e67e22; margin: 0 0 7px' > Buen dia, bienvenido </h1>" +
                                $"                <p style = 'margin: 2px; font-size: 15px'>" +
                                $"                GESTORÍA EN TRÁMITES VEHICULARES<br><br>" +
                                $"                Nos comprometemos a resolver cualquier trámite (simple o complejo) en el menor tiempo posible, facilitando a nuestros clientes la oportunidad de disponer a la brevedad de su equipo de transporte..<br>" +
                                $"                Entre los servicios tenemos:</p>" +
                                $"      <ul style = 'font-size: 15px;  margin: 10px 0'>" +
                                $"        <li> MOTOCICLETAS.</li>" +
                                $"        <li> AUTOS PARTICULARES.</li>" +
                                $"        <li> TRANSPORTE DE CARGA.</li>" +
                                $"        <li> TRACTOCAMIONES.</li>" +
                                $"      </ul>" +
                                $"  <div style = 'width: 100%;margin:20px 0; display: inline-block;text-align: center'>" +
                                $"    <img style = 'padding: 0; width: 200px; margin: 5px' src = '{MessageCenter.URL}images/tarjetas.png'>" +
                                $"  </div>";

            FooterBodyMail =    $"    <p style = 'color: #b3b3b3; font-size: 12px; text-align: center;margin: 30px 0 0' > Tramiauto </p>" +
                                $" </td >" +
                                $"</tr>"   +
                                $"</table>";
        }

    }

    public class MailHelper : IMailHelper
    {
        private readonly IConfiguration _configuration;
        private readonly MailSettings   _mailSettings;

        public MailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _mailSettings  = new MailSettings( _configuration["CofimSettings:SmtpUsername"]
                                             , _configuration["CofimSettings:SmtpServer"]
                                             , _configuration["CofimSettings:SmtpPort"]
                                             , _configuration["CofimSettings:SmtpPassword"]);
        }

        public void SendEmail(string to, string subject, string bodyMail)
        {          
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_mailSettings.From));
            message.To.Add  (new MailboxAddress(to));
            message.Subject = subject;
            var bodyBuilder = new BodyBuilder
            {   HtmlBody = _mailSettings.HeaderBodyMail
                         + bodyMail
                         + _mailSettings.FooterBodyMail
            };
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect     (_mailSettings.Smtp, int.Parse(_mailSettings.Port), false);
                client.Authenticate(_mailSettings.From, _mailSettings.Password);
                client.Send        (message);
                client.Disconnect  (true);
            }
        }

        public void SendEmailAccountConfirmation(string to, string tokenLink) 
        {
            string Subject = "Cofim - Confirmación de Correo electrónico";
            string Body    = $"  <div style = 'width: 100%; text-align: center'>" +
                             $"    <h2 style = 'color: #e67e22; margin: 0 0 7px'> Confirmación de Correo Electrónico </h2>" +
                             $"    <br>Para completar el registro a la aplicación y Portal Web, por favor de Click en el siguiente enlace:<br><br> " +
                             $"    <p> <a style = 'text-decoration: none; border-radius: 5px; padding: 11px 23px; color: white; background-color: #0366d6' href = \"{tokenLink}\">Confirmar cuenta de correo electrónico</a> </p>" +
                             $"  </div>"
                             ;
            SendEmail(to, Subject, Body);
        }

        public void SendEmailRecoverPwd(string to, string tokenLink)
        {
            string Subject = "Cofim - Reestrablecer Contraseña";
            string Body    = $"  <div style = 'width: 100%; text-align: center'>" +
                             $"    <h2 style = 'color: #e67e22; margin: 0 0 7px'> Reestablecimiento de Contraseña </h2>" +
                             $"    <br>Para reestrablecer la contraseña, por favor de Click en el siguiente enlace:<br><br> " +
                             $"    <p> <a style = 'text-decoration: none; border-radius: 5px; padding: 11px 23px; color: white; background-color: #0366d6' href = \"{tokenLink}\">Reestablecer Contraseña</a> </p>" +
                             $"  </div>"
                             ;
            SendEmail(to, Subject, Body);
        }

        public void SendEmailEtlLoad(string to, EtlProcessedFile etlProcessedFile)
        {
            string Subject = $"Cofim - Carga de Información {etlProcessedFile.TypeLoad}";
            string Body = $"  <div style = 'width: 100%; text-align: center'>" +
                             $"    <h2 style = 'color: #e67e22; margin: 0 0 7px'> Resumen de carga de información </h2>" +
                             $"    <p> Inicio de carga {etlProcessedFile.DateIni}. </p>" +
                             $"    <p> Fin de carga {etlProcessedFile.DateEnd}. </p>" +
                             $"    <p> Tiempo transcurrido {etlProcessedFile.ElapsedTime} segundos. </p>" +
                             $"    <p> Número de registros {etlProcessedFile.LoadedRecords}. </p>" +
                             $"    <p> Nombre del archivo {etlProcessedFile.FileName}. </p>" +
                             $"  </div>"
                             ;
            SendEmail(to, Subject, Body);
        }


    }//class

}//namespace
