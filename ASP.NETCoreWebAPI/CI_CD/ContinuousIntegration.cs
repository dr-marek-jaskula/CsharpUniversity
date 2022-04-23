namespace ASP.NETCoreWebAPI.CI_CD;

public class ContinuousIntegration
{
    //Continuous Integration (short. CI), Continuous Delivery (short. CD)
    //In order to build an app and check all test when pushing, we can manage CI - automatic process of building and testing our app when pushing to the master branch
    //Local -> Pull Request (pipeline) [restore dependencies -> build an application -> run tests] -> merge -> GitHub repository
    //Tools for CI: Jenkins, Azure DevOps, TeamCity, TravisCI, GitHub Actions
    //GitHub Actions is a free tool
    //In order to learn CI, first create a simple tests for our application

    //By GitHub Actions
    //1. Go to GitHub
    //2. Get to Actions
    //3. Select the configuration or use the custom one (we will create a custom one)
    //4. In order to create a custom configuration click -> set up a workflow yourself
    //5. We will create a file in "yml" format for the certain repository
    //6. Name the file like "ci". The base "yml" file (without comments) should look like this (i add my comment on the right hand side):

    /*
    name: CI
    on:                                                         #The first section (starting from "on") determines when the pipeline will be executed (check documentation to examine more options)
      push:                                                     #"push" -> determines that this pipeline will be executed when the code is:
        branches: [ master ]                                    #1. pushed on given branches -> Here is only master but after comma there can be more
      pull_request:                                             #"pull_request" -> determines that this pipeline will be executed when pull_request for given branches is made
        branches: [ master ]
      workflow_dispatch:                                        #enables the manual execution of this pipeline

    jobs:                                                       #determines the pipeline job (what pipeline should do). Can have more jobs (few or more). They execute async but we can make some references between them
      build:
        runs-on: ubuntu-latest                                  #determines the operating system (here is Linux, what is great)
        steps:                                                  #pipeline steps, mostly CLI commands (PowerShell commands). We can also use the predefined templates (we previously created or are accessible by default from github)
           - uses: actions/checkout@v3                          #"uses" are to use the predefined template. Here this makes that on machine that do the job we first pull the code from repo and checkout to this branch
           - name: Run a one-line script
             run: echo Hello, world!
           - name: Run a multi-line script
             run: |
               echo Add other actions to build,
               echo test, and deploy your project.
    */

    //Good to know about "yml" format is:
    //arrays are represented by [] brackets, for example -> myarray1: [ master, dev, feature1 ]
    //Also we can represent arrays element by element:
    /*
        myArray2:
            - master
            - dev
            - feature1
    */

    //7. Now we customize the presented file
    //It is very important to use the well written, popular predefined templates:
    //Setup.NET Core SDK -> good template to install SDK

    //8. The final "yml" file has the following form:
    /*

    */
}