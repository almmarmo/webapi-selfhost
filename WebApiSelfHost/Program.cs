using Microsoft.Owin.Hosting;
using System;

namespace WebApiSelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:8080";
            MongoRepository rep = new MongoRepository("test");
            //long count = rep.CountAsync("restaurants").Result;
            
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Web Server está rodando em " + url);
                Console.WriteLine("Pressione qualquer tecla para sair.");
                
                Console.ReadLine();
            }
        }
    }
}
