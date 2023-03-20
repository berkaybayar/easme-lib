using System.Net;
using System.Net.Mail;
using EasMe.Exceptions;

namespace EasMe;

/// <summary>
///     Simple mail sender
/// </summary>
public class EasMail
{
    /// <summary>
    ///     Mail sender class, uses SMTP protocol.
    /// </summary>
    /// <param name="Host"></param>
    /// <param name="MailAddress"></param>
    /// <param name="Password"></param>
    /// <param name="Port"></param>
    /// <param name="isSSL"></param>
    public EasMail(string host,
        string mailAddress,
        string password,
        int port,
        bool enableSSL = false)
    {
        Host = host;
        MailAddress = mailAddress;
        Password = password;
        Port = port;
        EnableSSL = enableSSL;
    }

    private string Host { get; }
    private string MailAddress { get; }
    private string Password { get; }
    private int Port { get; }
    private bool EnableSSL { get; }

    /// <summary>
    ///     Sends mail, better to create thread for this function.
    /// </summary>
    /// <param name="Body"></param>
    /// <param name="SendTo"></param>
    /// <param name="Subject"></param>
    public void SendMail(string Subject, string Body, string SendTo, bool isBodyHtml = false)
    {
        try
        {
            var fromAddress = new MailAddress(MailAddress);
            var toAddress = new MailAddress(SendTo);
            using var smtp = new SmtpClient
            {
                Host = Host,
                Port = Port,
                EnableSsl = EnableSSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, Password)
            };
            using var message = new MailMessage(fromAddress, toAddress) { Subject = Subject, Body = Body };
            message.IsBodyHtml = isBodyHtml;
            smtp.Send(message);
        }
        catch (Exception ex)
        {
            throw new EmailSendFailedException("Failed to send email.", ex);
        }
    }
}