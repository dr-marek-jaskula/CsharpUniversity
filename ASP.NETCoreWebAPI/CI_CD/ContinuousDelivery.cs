namespace ASP.NETCoreWebAPI.CI_CD;

public class ContinuousDelivery
{
    //Continuous Delivery - set the code to the production state with manual acceptation
    //This will be also named pipeline

    //GitHub repository is being triggered (for example merge to the master branch)
    //-> publish application
    //-> deploy developer environment (can be also test environment and other)
    //-> deploy production environment (with manual approval)

    //1. Remove from CI pipeline the "on" "push" section, because now on push should the second pipeline should be used (dev and production environment)

    //2. We create a new "yml" file in the .github/workflows named "deploy.yml":
    //THIS WILL BE NOT FULL FILE!!! Because it all to the moment of selecting the publish server
    /*

name: deploy
on:
  push:
    branches: [ master ]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      - name: Setup .NET Core SDK
        uses: actions/setup-dotnet@v2.0.0
        with:
          dotnet-version: 6.0.x
      - name: Restore dependencies
        run: dotnet restore
      - name: Build
        run: dotnet build --no-restore
      - name: Publish
        run: dotnet publish ./ASP.NETCoreWebAPI/ASP.NETCoreWebAPI.csproj -c Release -o ${{ env.DOTNET_ROOT }}/api #-o is output folder. ${{ env.DOTNET_ROOT }} is to use here an environmental variable that is defined in Setup .NET Core SDK and aim at root folder (despite the operating system). Moreover, use just one project not all
      - name: upload artifact
        uses: actions/upload-artifact@v3.0.0
        with:
          name: api-artifact    #custom name for artifact. Artifacts are data that can be share between different steps, therefore we need to upload id (use marketplace template)
          path: ${{ env.DOTNET_ROOT }}/api   #our artifact path to the published api

  deploy-dev:     #new job
    runs-on: ubuntu-latest
    needs: build   #we say that this job need previous job to be done
    steps:
      - name: Download a Build Artifact
        uses: actions/download-artifact@v3.0.0
        with:
          name: api-artifact       #same name as in build step

    */

    //No test because they are for pull requests.
    //No pull request because they are for CI
    //Artifacts are for sharing data between different steps -> they need to be uploaded and downloaded in different steps (use templates)
    //ABOVE FILE DOES NOT HAVE THE PLACING THE API TO THE DEV OR PROD ENVICRONMENT (because I do not upload to the Azure server but docker locally is planned)
}