class Program
{
    static void Main()
    {
        Temperatura t1 = new Temperatura(0, Escala.Celsius);
        Console.WriteLine($"0°C em Fahrenheit = {t1.EmFahrenheit():F2}°F");

        Temperatura t2 = new Temperatura(32, Escala.Fahrenheit);
        Console.WriteLine($"32°F em Celsius = {t2.EmCelsius():F2}°C");
    }
}