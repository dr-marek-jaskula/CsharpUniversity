using System.Diagnostics;

namespace ASP.NETCoreWebAPI.Dockerize;

public class DockerIgnore
{
    //file .dockerignore is the file containing the files that should be omitted by docker.
    //It is very important not to include folder like "obj" or "bin".

    //Details: ("https://docs.docker.com/engine/reference/builder/#dockerignore-file")
    //Before the docker CLI sends the context to the docker daemon, it looks for a file named .dockerignore in the root directory of the context. 
    //If this file exists, the CLI modifies the context to exclude files and directories that match patterns in it. (look DockerPatterns)
    //This helps to avoid unnecessarily sending large or sensitive files and directories to the daemon and potentially adding them to images using ADD or COPY.

    //The CLI interprets the .dockerignore file as a newline-separated list of patterns similar to the file globs of Unix shells

    //To add files/folders ignored by the docker, just add .dockerignore test file to the root directory
    //However, in most cases the auto generated .dockerignore is present in the root directory with following text:

    #region .dockerignore
    //          **/.classpath
    //          **/.dockerignore
    //          **/.env
    //          **/.git
    //          **/.gitignore
    //          **/.project
    //          **/.settings
    //          **/.toolstarget
    //          **/.vs
    //          **/.vscode
    //          **/*.*proj.user
    //          **/*.dbmdl
    //          **/*.jfm
    //          **/azds.yaml
    //          **/bin
    //          **/charts
    //          **/docker-compose*
    //          **/Dockerfile*
    //          **/node_modules
    //          **/npm-debug.log
    //          **/obj
    //          **/secrets.dev.yaml
    //          **/values.dev.yaml
    //          LICENSE
    //          README.md
    #endregion .dockerignore
}
