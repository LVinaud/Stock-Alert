using System;
using System.IO;
using System.Text.Json;

/*
Arquivo para ler o arquivo de configuração,
A classe Smtp tem todos os dados necessários para o envio de emails que será feito por outro arquivo
*/

class Smtp
{
    public string EmailReceiver { get; set; }
    public string SmtpServer { get; set; }
    public int SmtpPort{ get; set; }
    public string SmtpUser { get; set; }
    public string SmtpPassword { get; set; }

    public void ReadConfig()
    {
        //Nome e local do arquivo de configuração
        string configPath = "/home/eduds/Desktop/stock-alert/src/config.json";
        //Busca o arquivo e lida com possíveis erros
        if (!File.Exists(configPath))
        {
            Console.WriteLine("Erro na leitura do arquivo de configuração.");
            return;
        }
        //Pega o json e separa ele na classe que foi definida Smtp
        string jsonContent = File.ReadAllText(configPath);
        Smtp config = JsonSerializer.Deserialize<Smtp>(jsonContent);
        
        //Checa se há algum erro novamente
        if (config == null)
        {
            Console.WriteLine("Erro ao carregar configurações.");
            return;
        }
        
        //Salva na classe Smtp 
        EmailReceiver = config.EmailReceiver;
        SmtpPassword = config.SmtpPassword;
        SmtpPort = config.SmtpPort;
        SmtpServer = config.SmtpServer;
        SmtpUser = config.SmtpUser;
    }
}