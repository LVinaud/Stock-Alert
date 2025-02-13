using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Net.Mail;
using System.Runtime.InteropServices;
class Program
{
    static async Task Main(string[] args) 
    
    //primeiro passo é ler os argumentos, sendo eles o nome do ativo, o valor de venda e o valor de compra.
    //isso é passado pelo args que lê os inputs no terminal
    
    {
        //checagem se há 3 argumentos, pois caso contrário está havendo um comportamento inesperado
        if (args.Length != 3)
        {
            Console.WriteLine("Uso: StockQuoteAlert.exe <ATIVO> <PRECO_VENDA> <PRECO_COMPRA>");
            return;
        }

        string stock = args[0]; 
        float sellValue = float.Parse(args[1]);
        float buyValue = float.Parse(args[2]);
        //Console.WriteLine($"Ativo: {ativo}\nPreço de venda: {precoVenda}\nPreço de compra: {precoCompra}");

        /*Agora, devo começar a monitorar o valor do ativo
        para isso crio um http client que vou usar para enviar requisitos GET para a brapi, 
        escolhida pela sua gratuidade e simplicidade,
        agora, pego a url que farei requisição, e uso um token necessário para as requisições,
        de acordo com a documentação da brapi*/

        string apiKey = "token-exemplo";
        string url = $"https://brapi.dev/api/quote/{stock}?token={apiKey}";

        StockOperations operation = new StockOperations(url);
        //Pronto, agora basta comparar com os valores de venda e compra e mandar os emails
        //Configura os emaisl de acordo com o arquivo de config.json

        Smtp smtpConfig = new Smtp();
        smtpConfig.ReadConfig();

        //Crio a classe responsável por mandar os emails

        EmailSender sender = new EmailSender(smtpConfig);

        //Finalmente, o loop que verifica o valor do ativo periódicamente e manda os emails
        
        //Frequência de verificações em segundos, por exemplo a cada 60 segundos uma verificação é feita.
        float frequency = 10;
        //Tempo máximo de verificação em minutos, por exemplo 120 vai deixar o programa rodando e verificando o ativo por duas horas. -1 para rodar indefinidamente
        float maxTime = 1;
        int numOfRequests = (int)((maxTime * 60) / frequency);

        while(numOfRequests > 0) {
            System.Threading.Thread.Sleep((int) (frequency * 1000));
            float stockMarketValue = await operation.ReadStockValue();
            //Se o valor é maior ou igual que o de venda ou menor ou igual que o de compra, manda email, caso contrário printa o valor no terminal.
            if(stockMarketValue >= sellValue) 
            {
                sender.SendEmail($"O ativo {stock} atingiu o valor de venda!", $"Venda o ativo {stock}, ele está atualmente cotado a {stockMarketValue}, acima do valor que especificou como limite para venda.");
            } else if (stockMarketValue <= buyValue) 
            {
                sender.SendEmail($"O ativo {stock} atingiu o valor de compra!", $"Compre o ativo {stock}, ele está atualmente cotado a {stockMarketValue}, abaixo do valor que especificou como limite para compra.");
            } else 
            {
                Console.WriteLine(stockMarketValue);
            }
            numOfRequests -= 1;
        }
    }
}
