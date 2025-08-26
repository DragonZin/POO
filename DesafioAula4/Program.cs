using System;

class Program
{
    static void Main()
    {
        double soma = 0;

        for (int i = 1; i <= 4; i++)
        {
            Console.Write($"Digite a nota da prova {i}: ");
            double nota = double.Parse(Console.ReadLine());
            soma += nota;
        }

        double media = soma / 4;

        Console.WriteLine($"\nMédia: {media:F2}");

        if (media >= 6)
            Console.WriteLine("Aluno aprovado!");
        else
            Console.WriteLine("Aluno reprovado!");
    }
}
