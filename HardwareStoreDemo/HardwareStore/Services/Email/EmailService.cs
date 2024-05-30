using HardwareStore.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace HardwareStore.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendEmail(Dictionary<string, string> data, List<SaleDetail> detailsList)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(
                    _configuration["Mailtrap:EmailUsername"],
                    _configuration["Mailtrap:EmailFrom"]
                   ));

                message.To.Add(new MailboxAddress(data["RecepientName"], data["EmailTo"]));

                message.Subject = data["Subject"];

                var builder = new BodyBuilder();

                var templatePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "EmailTemplates", "SaleEmail.html");

                var templateContent = File.ReadAllText(templatePath);

                var saleDate = DateTime.Now;

                templateContent = templateContent.Replace("@SaleDate", saleDate.ToString("dd/MM/yyyy HH:mm:ss"));
                templateContent = templateContent.Replace("@CustomerName", data["RecepientName"]);
                templateContent = templateContent.Replace("@Email", data["EmailTo"]);
                templateContent = templateContent.Replace("@Address", data["Address"]);
                templateContent = templateContent.Replace("@City", data["City"]);


                // aqui se guardaran los detalles de venta
                string detailRows = string.Empty;

                decimal total = 0;

                foreach (var detail in detailsList)
                {
                    detailRows += $"<tr><td>{detail.ProductName}</td><td>$ {detail.UnitPrice}</td><td>{detail.Quantity}</td><td>$ {detail.UnitPrice * detail.Quantity}</td></tr>";
                    total += detail.UnitPrice * detail.Quantity;
                }

                templateContent = templateContent.Replace("@DetailRows", detailRows);

                templateContent = templateContent.Replace("@Total", total.ToString());

                builder.HtmlBody = templateContent;

                message.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect(_configuration["Mailtrap:Host"],
                        int.Parse(_configuration["Mailtrap:Port"]),
                        false);

                    client.Authenticate(
                        _configuration["Mailtrap:Username"],
                        _configuration["Mailtrap:Password"]
                       );

                    client.Send(message);

                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                Console.WriteLine(message);
                throw;
            }
        }
    }
}
