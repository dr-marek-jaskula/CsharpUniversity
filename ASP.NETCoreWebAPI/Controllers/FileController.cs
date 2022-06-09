using ASP.NETCoreWebAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace ASP.NETCoreWebAPI.Controllers;

//Returning files is a common thing for Web Api: for instance returning an instruction in pdf or other ReadMe.txt file
//Here the file controller will deal with both public (for non registered users) and private (registered files) files

//In order to include static files in Asp.Net Core we need to:
//1. Get NuGet Package Microsoft.AspNetCore.StaticFiles
//2. Add in Configure region in Program.cs "app.UseStaticFiles()" at the beginning of the configure region
//3. Create wwwroot folder
//4. Create z PrivateFiles folder and add to it some .txt files ("sample-file.txt" is the public one)
//5. DO NOT have static files on the same path where is api (so not in the path having with "/api/")

//6. Now we can just write:
//"https://localhost:7240/sample-file.txt" and the file is returned (without any controller) -> with header Content-Type "text/plain"
//If the path is incorrect, then the NotFount is response returned

//7. Now we will create a controller to get private files for authorized users

[Route("[controller]")]
//[Authorize]
[ApiController]
public class FileController : ControllerBase
{
    /// <summary>
    /// Return a private file stored in "PrivateFiles" folder. File name must be specified in the parameter
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    [HttpGet]
    //Enables caching with certain duration
    [ResponseCache(Duration = 1200, VaryByQueryKeys = new[] { "fileName" })]
    public ActionResult GetFile([FromQuery] string fileName)
    {
        //path to the application (to root)
        var rootPath = Directory.GetCurrentDirectory();

        //Path to the file in "PrivateFiles" where statics files are stored
        var filePath = $"{rootPath}/PrivateFiles/{fileName}";

        //We check if the file exists
        var fileExists = System.IO.File.Exists(filePath);

        if (!fileExists)
            return NotFound();

        //We get the file content
        var fileContent = System.IO.File.ReadAllBytes(filePath);

        //We dynamically (in runtime) get the file type and store in "contentType"
        var contentProvider = new FileExtensionContentTypeProvider();

        //fileName or filePath?
        contentProvider.TryGetContentType(fileName, out string? contentType);

        if (contentType is null)
            throw new NotFoundException("File type not found!");

        //To return files we use "File" method (like OK) that takes: fileContent, contentType, fileName
        return File(fileContent, contentType, fileName);
    }

    /// <summary>
    /// Upload a file to the server
    /// </summary>
    /// <param name="file">[FromForm] is used to upload file to the server. IFromFile is also a interface for the files</param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult Upload([FromForm] IFormFile file)
    {
        //Other, asynchronous way to retrieve the file:
        //var formCollection = await Request.ReadFormAsync();
        //file = formCollection.Files.First();

        //IFormFile contains information about ContentType (is it a pdf, txt or other), FileName
        if (file is not null && file.Length > 0)
        {
            var rootPath = Directory.GetCurrentDirectory();
            var fileName = file.FileName;
            var fullPath = $"{rootPath}/PrivateFiles/{fileName}";

            //We open the connection to the file and we specify that we will create the file
            using var stream = new FileStream(fullPath, FileMode.Create);

            //We copy the content to the new created file
            file.CopyTo(stream);

            return Ok();
        }
        return BadRequest();
        //POST na route file
        //in postman get to body -> "form-data" -> key: file (change key to "file") -> Value "Select Files" (select)
    }
}