namespace ASP.NETCoreWebAPI.Dockerize;

public class DockerPatterns
{
    //'#' is used for comment

    //"*/" like "*/temp" says "any 'temp' folder in the root or its direct subfolders" -> example "/somefolder/temp.txt"

    //"*" like "temp*" says "any file that stars from "temp" -> example "temporary.txt"

    //'*' means 0 or more characters

    //"*/*/temp*" -> example "/somefolder/subfolder/temporary.txt" (two lvls deep)

    //'?' meas 1 character

    //"temp?" -> example "/tempa"

}
