using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Cenário OK #1 ===");
        var conta = new ContaFinanceira("Teste", 100m);
        conta.Depositar(50m);
        Console.WriteLine($"Saldo final: {conta.ConsultarSaldo():C}");
        foreach (var linha in conta.GerarExtrato())
            Console.WriteLine(linha);

        Console.WriteLine("\n=== Cenário OK #2 ===");
        conta.Sacar(30m);
        Console.WriteLine($"Saldo final: {conta.ConsultarSaldo():C}");
        foreach (var linha in conta.GerarExtrato())
            Console.WriteLine(linha);

        Console.WriteLine("\n=== Cenário OK #3 (transferência por ID) ===");
        var alice = new ContaFinanceira("Alice", 200m);
        var bob = new ContaFinanceira("Bob", 50m);
        alice.TransferirParaId(bob.IdConta, 100m);
        Console.WriteLine($"Saldo Alice: {alice.ConsultarSaldo():C}");
        Console.WriteLine($"Saldo Bob (não muda, pois é só ID): {bob.ConsultarSaldo():C}");
        Console.WriteLine("Extrato Alice:");
        foreach (var linha in alice.GerarExtrato()) Console.WriteLine(linha);

        Console.WriteLine("\n=== Cenário ERRO #1 ===");
        try
        {
            conta.Sacar(1000m);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro capturado: " + ex.Message);
        }

        Console.WriteLine("\n=== Cenário ERRO #2 ===");
        try
        {
            conta.Bloquear();
            conta.Depositar(10m);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro capturado: " + ex.Message);
        }
        finally
        {
            conta.Desbloquear();
        }

        Console.WriteLine("\n=== Cenário ERRO #3 (transferência por ID) ===");
        try
        {
            alice.TransferirParaId(bob.IdConta, 500m); // Alice só tem 100
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro capturado: " + ex.Message);
        }
    }
}