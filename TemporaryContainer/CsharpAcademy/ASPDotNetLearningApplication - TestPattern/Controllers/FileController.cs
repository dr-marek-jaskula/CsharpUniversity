using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASPDotNetLearningApplication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace ASPDotNetLearningApplication
{
    [Route("file")]
    //[Authorize]
    public class FileController : ControllerBase
    {
        //To co sprawia ze cachuje wszystkie pliki!
        [HttpGet]
        [ResponseCache(Duration = 1200, VaryByQueryKeys = new[] { "fileName" })] //dzieki temu jak sie raz sciagnie z breakpointem, a potem drugi raz to potem breakpoint nie zatrzyma!
        public ActionResult GetFile([FromQuery] string fileName)
        {
            //jako, że ścieżka jest uzywana do naszej aplikacji tj u mnie "C:\Users\Marek\source\repos\ASPDotNetLearningApplication - TestPattern"
            var rootPath = Directory.GetCurrentDirectory();

            //robimy scieżkę do PrivateFiles
            var filePath = $"{rootPath}/PrivateFiles/{fileName}";

            //sprawzamy czy plik istnieje

            //pełna scieżka aby nie mylił z kontrolerem
            var fileExists = System.IO.File.Exists(filePath);

            if (!fileExists)
                return NotFound();

            //zawartość pliku
            var fileContents = System.IO.File.ReadAllBytes(filePath);

            //musimy dynamicznie zczytać rodzaj pliku
            var contentProvider = new FileExtensionContentTypeProvider();

            //pod zmienną "contentType" będzie krył się typ pliku
            contentProvider.TryGetContentType(fileName, out string contentType);

            //do zwracanai plików jest metoda File. przyjmuje: content, typ i nazwę
            return File(fileContents, contentType, fileName); 
        }

        [HttpPost]
        public ActionResult Upload([FromForm] IFormFile file)
        {
            //w interface'sie IFormFile jest pare fajnych info

            if (file is not null && file.Length > 0)
            {
                var rootPath = Directory.GetCurrentDirectory();
                var fileName = file.FileName;
                var fullPath = $"{rootPath}/PrivateFiles/{fileName}";

                //otwieramy połączenie do pliku, dajemy enum, że będziemy tworzyć plik
                using var stream = new FileStream(fullPath, FileMode.Create);
                //zapisujemy plik pod konkretną ścieżką
                file.CopyTo(stream);

                return Ok();
            }
            return BadRequest();
        }
    }
}
