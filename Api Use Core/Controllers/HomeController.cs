using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api_Use_Core.Models;
using System.Net.Http;

namespace Api_Use_Core.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
           
            return View();
        }

        public async Task<IActionResult> About()//Si usas la bombilla para cambiarlo a asincrono fijate en el nombre porque lo cambia
        {
            HttpClient client = new HttpClient();
            List<Character> characters = new List<Character>();
            RootObject rootObject=null;//Hay que iniciarlo para que no de error en el foreach
            int i = 1;
            bool next = true;
            while (next)
            {
                string url = "https://swapi.co/api/people/?page=" + i;//Para leer las siguientes páginas
                HttpResponseMessage response = await client.GetAsync(url); //Aquí se guardará la respuesta. El await es para que la página vaya cargando mientras se consigue la respuesta

                if (response.IsSuccessStatusCode)
                {
                    rootObject = await response.Content.ReadAsAsync<RootObject>(); //Guardame en rootObject el contenido de la respuesta de forma asinrona y me lo guardas como un objeto RootObject
                    foreach (Character character in rootObject.Characters)//rootObject.Character es una lista que hemos creado en el controlador con el json2csharp.com
                    {
                        Character c = new Character
                        {
                            Name = character.Name,
                            Height = character.Height,
                            Mass = character.Mass,
                            Hair_color = character.Hair_color
                        };
                        characters.Add(c);//Creo el objeto arriba meto los datos y luego guardo en la lsita characters
                    }
                }
                if (rootObject.Next==null)
                {
                    next = false;
                }
                i++;
            }
            return View(characters);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
