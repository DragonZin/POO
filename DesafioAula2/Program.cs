using System;
class Program
{
    static void Main()
    {
        string nomeProduto = "Notebook Gamer";
        int quantidade = 15;
        double precoUnitario = 4999.90;
        bool ativoParaVenda = true;

        Console.WriteLine("Produto: " + nomeProduto);
        Console.WriteLine("Quantidade em estoque: " + quantidade);
        Console.WriteLine("Preço unitário: R$ " + precoUnitario);
        Console.WriteLine("Ativo para venda: " + ativoParaVenda);
    }
}