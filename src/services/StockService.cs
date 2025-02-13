using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http;
using System.Text.Json.Nodes;

/*
Arquivo para leitura do valor do ativo
Poderia ter criado uma classe para o ativo com mais dados, como a alta do dia, etc,
mas como esse programa apenas avalia o valor de mercado atual do ativo,
preferi apenas ler essa variável
*/

class StockOperations {
    private HttpClient client;

    private string url;
    //Crio o client para as requsições da api
    public StockOperations(string url1) {
        client = new HttpClient();
        url = url1;
    }
    
    public async Task<float> ReadStockValue() {
        HttpResponseMessage response = await client.GetAsync(url);
        
        //Leio a resposta da requisição

        string jsonResponse = await response.Content.ReadAsStringAsync();  // Lendo a resposta como string

        //A resposta é uma string de json, agora preciso capturar o valor de "regularMarketPrice" e comparar com os valores de venda e compra
        
        //Transformo o json em jsonNode e pego o primeiro elemento, que é o que interessa: "results"
        
        JsonNode jsonNode = JsonNode.Parse(jsonResponse)["results"];

        //Esse jsonNode é um array de um item só, item esse que é os resultados, portanto busco no primeiro item dele o "regularMarketPrice" e converto em float.

        float regularMarketPrice = jsonNode[0]["regularMarketPrice"].GetValue<float>();
        
        return regularMarketPrice;
    }

}