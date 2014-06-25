using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using Polla.Web.Controllers;
using log4net;

namespace Polla.Web.Utils
{
    public class EmailMessage
    {
        private List<string> _toList;
        private List<string> _ccList;
        private List<string> _bccList;
        private List<EmailAttachment> _attachments;
        private MessageSettings _settings;
        private ILog _logger = LogManager.GetLogger(typeof(EmailMessage));

        public List<string> To { get { return _toList ?? (_toList = new List<string>()); } set { _toList = value; } }
        public List<string> Cc { get { return _ccList ?? (_ccList = new List<string>()); } set { _ccList = value; } }
        public List<string> Bcc { get { return _bccList ?? (_bccList = new List<string>()); } set { _bccList = value; } }
        public List<EmailAttachment> Attachments { get { return _attachments ?? (_attachments = new List<EmailAttachment>()); } set { _attachments = value; } }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ContentType { get { return (Settings.IsBodyHtml ? "text/html" : "text/plain"); } }
        public string EmailServer { get; set; }

        public bool Send()
        {
            try
            {
                _logger.Info("START - EmailMessage.Send");
                var emailSmtpClient = new SmtpClient
                {
                    Host = EmailServer,
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(From, "polla123")
                };
                var mail = new MailMessage();

                foreach (var toAddress in To)
                {
                    mail.To.Add(toAddress);
                }

                foreach (var ccAddress in Cc)
                {
                    mail.CC.Add(ccAddress);
                }

                foreach (var bccAddress in Bcc)
                {
                    mail.Bcc.Add(bccAddress);
                }

                mail.Subject = Subject;
                mail.Body = Body;
                mail.BodyEncoding = Settings.BodyEncoding;
                mail.IsBodyHtml = Settings.IsBodyHtml;
                mail.From = new MailAddress(From);

                foreach (var attachment in Attachments)
                {
                    mail.Attachments.Add(new Attachment(attachment.FilePath));
                }
                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                emailSmtpClient.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("EXCEPTION : ", ex);
                throw;
            }
            finally
            {
                _logger.Info("FINISH - EmailMessage.Send");
            }
        }

        public MessageSettings Settings
        {
            get
            {
                return _settings ?? (_settings = new MessageSettings
                {
                    SubjectEncoding = Encoding.UTF8,
                    BodyEncoding = Encoding.UTF8,
                    IsBodyHtml = true
                });
            }
            set
            {
                _settings = value;
            }
        }
    }

    public class MessageSettings
    {
        public Encoding SubjectEncoding { get; set; }
        public Encoding BodyEncoding { get; set; }
        public bool IsBodyHtml { get; set; }
    }
    public class EmailAttachment
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }



}