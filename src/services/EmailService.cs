using System;
using System.IO;
using System.Text.Json;
using System.Net.Mail;
using System.Net;

/*
Arquivo para lidar com a tarefa de enviar emails
A classe EmailSender tem uma classe Smtp de outro arquivo que contém todas as informações necessárias para se enviar emails
*/
class EmailSender
{
    private Smtp config;

    //Construtor bobo mas para deixar tudo bem explicitado
    public EmailSender(Smtp smtp)
    {
        config = smtp;
    }

    //Método que será usado no programa principal para enviar emails
    public void SendEmail(string subject, string body)
    {
        
        try
        {
            //Cria um client smtp
            SmtpClient client = new SmtpClient(config.SmtpServer, config.SmtpPort);
            client.Credentials = new NetworkCredential(config.SmtpUser, config.SmtpPassword);
            client.EnableSsl = true;
            //Cria a mensagem a ser enviada a partir do assunto e corpo de email do programa principal
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(config.SmtpUser);
            mail.To.Add(config.EmailReceiver);
            mail.Subject = subject;
            mail.Body = body;
            client.Send(mail);
            //Console.WriteLine("email enviado");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao enviar email: {ex.Message}");
        }
    }
}
