using System.Net;
using System.Net.Mail;

namespace EasMe;

/// <summary>
///   Simple mail sender
/// </summary>
public class EasMail
{
  /// <summary>
  ///   Mail sender class, uses SMTP protocol.
  /// </summary>
  /// <param name="host"></param>
  /// <param name="mailAddress"></param>
  /// <param name="password"></param>
  /// <param name="port"></param>
  /// <param name="enableSsl"></param>
  public EasMail(string host,
                 string mailAddress,
                 string password,
                 int port,
                 bool enableSsl = false) {
    Host = host;
    MailAddress = mailAddress;
    Password = password;
    Port = port;
    EnableSSL = enableSsl;
  }

  private string Host { get; }
  private string MailAddress { get; }
  private string Password { get; }
  private int Port { get; }
  private bool EnableSSL { get; }

  /// <summary>
  ///   Sends mail, better to create thread for this function.
  /// </summary>
  /// <param name="body"></param>
  /// <param name="sendTo"></param>
  /// <param name="subject"></param>
  /// <param name="isBodyHtml"></param>
  public void SendMail(string subject, string body, string sendTo, bool isBodyHtml = false) {
    try {
      var fromAddress = new MailAddress(MailAddress);
      var toAddress = new MailAddress(sendTo);
      using var smtp = new SmtpClient {
        Host = Host,
        Port = Port,
        EnableSsl = EnableSSL,
        DeliveryMethod = SmtpDeliveryMethod.Network,
        UseDefaultCredentials = false,
        Credentials = new NetworkCredential(fromAddress.Address, Password)
      };
      using var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body };
      message.IsBodyHtml = isBodyHtml;
      smtp.Send(message);
    }
    catch (Exception ex) {
      throw new Exception("Failed to send email.", ex);
    }
  }
}