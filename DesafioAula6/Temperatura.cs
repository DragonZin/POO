using System;

public enum Escala
{
    Celsius,
    Fahrenheit
}

public class Temperatura
{
    private double valor;
    private Escala escala;

    public Temperatura(double valor, Escala escala)
    {
        this.valor = valor;
        this.escala = escala;
    }

    public double EmCelsius()
    {
        if (escala == Escala.Celsius)
            return valor;
        else
            return (valor - 32) * 5.0 / 9.0;
    }

    public double EmFahrenheit()
    {
        if (escala == Escala.Fahrenheit)
            return valor;
        else
            return (valor * 9.0 / 5.0) + 32;
    }

    public override string ToString()
    {
        return $"{valor:F2} {escala}";
    }
}