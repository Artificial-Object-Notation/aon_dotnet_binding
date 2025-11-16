using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AonDotNet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var data = new
            {
                id = 1,
                nome = "Alice",
                profile = new
                {
                    enderecos = new[]
                    {
                        new { cep = "06114020", rua = "Rua das Dores" },
                        new { cep = "06114021", rua = "Rua Azul" }
                    },
                    idade = 30
                }
            };

            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            });

            Console.WriteLine("==== JSON → AON ====");
            string aon = Aon.JsonToAon(json, "users");
            Console.WriteLine(aon);

            Console.WriteLine("\n==== AON → JSON ====");
            string back = Aon.AonToJson(aon);
            Console.WriteLine(back);
        }
    }
}
