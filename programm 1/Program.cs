using System;
using Serilog;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            
            decimal kurs = 60.75m;
            decimal kom1 = 8.0m;
            decimal kom2 = 0.37m;
            var proverka = false;
            var vneseno = 0.0m;
            var log = new LoggerConfiguration().MinimumLevel.Information().Enrich.WithProperty("Курс доллара:", kurs).WriteTo.Seq("http://localhost:5341/", apiKey: "zf6yDO6iaMlMDehfOV97").CreateLogger();
            while (!proverka)
            {
                log.Information($"Ввод суммы для обмена");
                Console.Write("Внесите сумму в долларах для обмена: ");
                proverka = decimal.TryParse(Console.ReadLine(), out vneseno);
                if(!proverka)
                {
                    log.Error("Пользователь ввел не корректную сумму для обмена");
                }
            }
            log.Information($"Пользователь ввел корректную сумму:{vneseno}");
            var perevod = vneseno * kurs;
            decimal vidano;
            if (vneseno > 500)
            {
                Console.WriteLine("Внесенная сумма больше 500 долларов. Коммисия составит 0,37%.");
                vidano = (perevod - ((perevod * kom2) / 100));
                Console.WriteLine("Выдано: " + vidano + "рублей.");
            }
            else
            {
                Console.WriteLine("Внесенная сумма меньше 500 долларов. Коммисия составит 8 рублей.");
                vidano = (perevod - kom1);
                Console.WriteLine("Выдано: " + vidano + "рублей.");
            }
            log.Information($"Перевод в $ прошел успешно, выдано:{vidano}");
            Console.ReadKey();
        }
    }

}
