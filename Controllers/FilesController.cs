using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Lab2.Models;

namespace Lab2.Controllers
{
    public class FilesController : Controller
    {
        private readonly IWebHostEnvironment _env;
        List<MyFile> myFiles = new List<MyFile>();

        public FilesController(IWebHostEnvironment env)
        {
            _env = env;
            string contentRootPath = _env.ContentRootPath;
            string[] files = Directory.GetFiles("TextFiles/").Select(file => Path.GetFileNameWithoutExtension(file)).ToArray();
            foreach (string fileName in files)
            {
                string content = System.IO.File.ReadAllText(_env.ContentRootPath + "/TextFiles/" + fileName + ".txt");
                MyFile myFile = new MyFile{ FileName = fileName, FileContent = content };
                myFiles.Add(myFile);
            }
        }

        public IActionResult index()
        {
            return View(myFiles);
        }

        public IActionResult content(string id)
        {
            MyFile findFile = myFiles.Where(f => f.FileName == id).FirstOrDefault();
            return View(findFile);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
