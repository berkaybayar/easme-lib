using System;
using System.Net;
using System.Net.Mail;

namespace EasMe.dll
{

    public class EasMail
    {
        readonly string _host;
        readonly string _mailaddress;
        readonly string _password;
        readonly int _port;
        readonly bool _isSSL;
        EasMail(string Host, string MailAddress, string Password, int Port, bool isSSL = false)
        {
            _host = Host;
            _mailaddress = MailAddress;
            _password = Password;
            _port = Port;
            _isSSL = isSSL;

        }
        public void MailSender(string body, string sendto, string subject)
        {
            try
            {
                var fromAddress = new MailAddress(_mailaddress);
                var toAddress = new MailAddress(sendto);
                using (var smtp = new SmtpClient
                {
                    Host = _host,
                    Port = _port,
                    EnableSsl = _isSSL,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, _password)

                })
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
                {
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
    }
}
