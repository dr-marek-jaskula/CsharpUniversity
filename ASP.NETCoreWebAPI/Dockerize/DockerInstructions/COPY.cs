using System;

namespace ASP.NETCoreWebAPI.Dockerize.DockerInstructions;

public class COPY
{
    //COPY instruction:
    //COPY <src> <dest>
    //Copies files from the source path to the destination path

    //Example:
    //COPY /source/file/path  /destination/path

    //Good practice: use COPY if possible, use ADD only for its additional use cases (like urls)

    //Other examples:
    //To copy all files starting with “hom”:
    //COPY hom* /mydir/

    //In the example below, ? is replaced with any single character, e.g., “home.txt”.
    //COPY hom?.txt /mydir/
}