## Continuous Integration (CI) guide

CI is done to build an app and check all automated test while pushing.

By GitHub Actions
1. Go to GitHub
2. Get to Actions
3. Select the configuration or use the custom one (we will create a custom one)
4. In order to create a custom configuration click -> set up a workflow yourself
5. Create a file in "yml" (or "yaml") format for the certain repository
6. Name the file like "ci". 

Base "yml" file (without comments) should look like this (I add my comment on the right hand side):

```yaml
name: CI
on:                                                         #The first section (starting from "on") determines when the pipeline will be executed (check documentation to examine more options)
    push:                                                   #"push" -> determines that this pipeline will be executed when the code is:
    branches: [ master ]                                    #1. pushed on given branches -> Here is only master but after comma there can be more
    pull_request:                                           #"pull_request" -> determines that this pipeline will be executed when pull_request for given branches is made
    branches: [ master ]
    workflow_dispatch:                                      #enables the manual execution of this pipeline

jobs:                                                       #determines the pipeline job (what pipeline should do). Can have more jobs (few or more). They execute async but we can make some references between them
    build:
    runs-on: ubuntu-latest                                  #determines the operating system (here is Linux, what is great)
    steps:                                                  #pipeline steps, mostly CLI commands (PowerShell commands). We can also use the predefined templates (we previously created or are accessible by default from github)
        - uses: actions/checkout@v3                         #"uses" are to use the predefined template. Here this makes that on machine that do the job we first pull the code from repo and checkout to this branch
        - name: Run a one-line script
            run: echo Hello, world!
        - name: Run a multi-line script
            run: |
            echo Add other actions to build,
            echo test, and deploy your project.
```

Basic info about yaml files:
arrays are represented by [] brackets. For instance: 
```yaml
myArray1: [ master, dev, feature1 ]
```

Also we can represent arrays element by element:
```yaml
myArray2:
    - master
    - dev
    - feature1
```

7. Customize the presented file.
It is very important to use the well written, popular predefined templates:
Setup.NET Core SDK -> good template to install SDK

8. The final "yml" file has the following form:
```yaml
name: CI
on:
  push:
    branches: [ master ]
  pull_request:
    branches: [ master ]
  workflow_dispatch:

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      - name: Setup .NET Core SDK
        uses: actions/setup-dotnet@v2.0.0                       #predefined template to install sdk if needed
        with:
          dotnet-version: 6.0.x                                 #specified the version
      - name: Restore dependencies
        run: dotnet restore
      - name: Build
        run: dotnet build --no-restore
      - name: Test
        run: dotnet test --filter DisplayName=XUnitTests.OrderControllerTest.GetAll_Returns_The_Correct_Numer_Of_Orders             #run just the single specified test
```

This file is in ".github/workflows" directory that is one folder above the solution

9. Change branch politics: 
- do not commit the push if something in the pipeline fails (for example code does not build)
    - Go to GitHub -> Settings -> Branches -> Add rule: (Protected branches are available to Pro, Team, and Enterprise users)
    - fill branch name (for example "master")
    - Check the options of branch protection like:
        - "Require a pull request before merging" -> "Require approvals" (at least one person besides author needs to approve the merge)
        - "Require status checks to pass before merging" -> search for "build" and use it (this is important, what we need)
        - "Include administration" -> to prevent overriding the new rule