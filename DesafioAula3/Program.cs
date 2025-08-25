using System;

class Program
{
    static void Main()
    {
        Console.Write("Digite a senha: ");
        string senha = Console.ReadLine();

        if (senha == "1234")
        {
            Console.WriteLine("Acesso liberado.");
        }
        else
        {
            Console.WriteLine("Senha incorreta.");
        }
    }
}