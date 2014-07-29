using System;
using System.Net.Mail;
using System.Configuration;
using System.Net.Configuration;
using BrockAllen.MembershipReboot;

namespace AIMS.Notifications.Email
{
    public class SmtpMessageDelivery : IMessageDelivery
    {
        private readonly bool _sendAsHtml;

        public SmtpMessageDelivery(bool sendAsHtml = false)
        {
            _sendAsHtml = sendAsHtml;
        }

        public void Send(Message msg)
        {
            Tracing.Information("[SmtpMessageDelivery.Send] sending mail to " + msg.To);

            if (String.IsNullOrWhiteSpace(msg.From))
            {
                var smtp = ConfigurationManager.GetSection("system.net/mailSettings/smtp") as SmtpSection;
                msg.From = smtp.From;
            }

            using (var smtp = new SmtpClient())
            {
                smtp.Timeout = 5000;
                try
                {
                    var mailMessage = new MailMessage(msg.From, msg.To, msg.Subject, msg.Body)
                    {
                        IsBodyHtml = _sendAsHtml
                    };
                    smtp.Send(mailMessage);
                }
                catch (SmtpException e)
                {
                    Tracing.Error("[SmtpMessageDelivery.Send] SmtpException: " + e.Message);
                }
                catch (Exception e)
                {
                    Tracing.Error("[SmtpMessageDelivery.Send] Exception: " + e.Message);
                }
            }
        }
    }
}