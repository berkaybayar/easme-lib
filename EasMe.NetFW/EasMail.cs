using System;
using System.Net;
using System.Net.Mail;

namespace EasMe
{

    public class EasMail
    {
        readonly string _host;
        readonly string _mailaddress;
        readonly string _password;
        readonly int _port;
        readonly bool _isSSL;
        /// <summary>
        /// Mail sender helper, uses SMTP protocol.
        /// </summary>
        /// <param name="Host"></param>
        /// <param name="MailAddress"></param>
        /// <param name="Password"></param>
        /// <param name="Port"></param>
        /// <param name="isSSL"></param>
        public EasMail(string Host, string MailAddress, string Password, int Port, bool isSSL = false)
        {
            _host = Host;
            _mailaddress = MailAddress;
            _password = Password;
            _port = Port;
            _isSSL = isSSL;

        }
        /// <summary>
        /// Sends mail, better to create thread for this function.
        /// </summary>
        /// <param name="Body"></param>
        /// <param name="SendTo"></param>
        /// <param name="Subject"></param>
        public void SendMail(string Body, string SendTo, string Subject)
        {
            try
            {
                var fromAddress = new MailAddress(_mailaddress);
                var toAddress = new MailAddress(SendTo);
                var smtp = new SmtpClient
                {
                    Host = _host,
                    Port = _port,
                    EnableSsl = _isSSL,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, _password)

                };
                var message = new MailMessage(fromAddress, toAddress) { Subject = Subject, Body = Body };
                message.IsBodyHtml = true;
                smtp.Send(message);

            }
            catch (Exception) { throw; }


        }
    }
}
