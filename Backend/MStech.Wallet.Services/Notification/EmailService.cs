using DataBase.Repository;
using Implementation.BaseService;
using Mstech.ViewModel.DTO;
using ViewModel.Infrastructure;
using MStech.Wallet.DataBase.Etity.Client;
using System.Net.Mail;
using System.Net;
using Common.Models;
using Microsoft.Extensions.Options;

namespace Implementation.Notification
{
    public class EmailService : BaseService<WalletClient>
    {
        private readonly SmtpOption _smtpOption;

        public EmailService(IUnitOfWork _unitOfWork, IRepository<WalletClient> _repository, IOptions<SmtpOption> smtpOptions) : base(_unitOfWork)
        {
            _smtpOption = smtpOptions.Value;
        }

        public async Task<Result<EmailViewModel>> SendEmail(EmailViewModel model)
        {
            var result = new Result<EmailViewModel>();
            // Set up the SMTP client using the Gmail SMTP configuration
            var smtpClient = new SmtpClient
            {
                Host = _smtpOption.HostUrl,
                Port = _smtpOption.HostPort,
                EnableSsl = _smtpOption.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtpOption.SenderEmail, _smtpOption.Password)
            };

            try
            {
                // Prepare the email message
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("mahdins75@gmail.com"),
                    Subject = model.EmailTitle,
                    Body = model.EmailMessage,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(model.Destination);

                await smtpClient.SendMailAsync(mailMessage);

                return new Result<EmailViewModel>() { Entity = new EmailViewModel() };
            }
            catch (Exception ex)
            {

                return new Result<EmailViewModel>() { Entity = new EmailViewModel() };
            }
        }



    }

}