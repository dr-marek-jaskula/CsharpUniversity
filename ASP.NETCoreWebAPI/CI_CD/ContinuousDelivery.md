## Continuous Delivery (CD) guide

Continuous Delivery - set the code to the production state with manual acceptation.

GitHub repository is being triggered (for example merge with master branch)
- publish application
- deploy developer environment (can be also test environment and other)
- deploy production environment (with manual approval)

1. Remove from CI pipeline the "on" "push" section, because now on push the second pipeline should be used (dev and production environment)

2. We create a new "yml" file in the .github/workflows named "deploy.yml":
```yaml
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
```

Above file does not result in placing the api to the dev or prod environment due to the fact that i do not upload it to the Microsoft Azure.

### Notes

- No tests, because they are combined with pull requests.
- No pull request, because they are in CI pipeline
- artifacts are for sharing data between different steps 
    - they need to be uploaded and downloaded in different steps (use templates)
- To allow multiple appsetting.json in publishing we PROBABLY!! (not sure, but looks like it is working) we just need to include to the project file:
```
<ItemGroup>
<Content Remove="appsettings.json"/>
    <None Include="appsettings.json">
	    <ExcludeFromSingleFile>true</ExcludeFromSingleFile>
	    <CopyToPublishDirectory>Never</CopyToPublishDirectory>
	    <ErrorOnDuplicatePublishOutputFiles>false</ErrorOnDuplicatePublishOutputFiles>
    </None>
</ItemGroup>
```

> Check: "https://github.com/dotnet/sdk/issues/22716", "https://github.com/dotnet/sdk/issues/22716", "https://docs.microsoft.com/en-us/dotnet/core/compatibility/sdk/6.0/duplicate-files-in-output"

### Secrets

Jobs are by default independent, but using artifact we can create dependencies.

In order not to hard code the connection string in the GitHub Actions, we can use GitHub Secrets:
> Settings -> Secrets -> Actions -> New Secret

Then, we cant see the secrets. We can only override them or remove them.

We can use the secrets like a variable. Therefore, (for secret named "CUSTOM_PUBLISH_PROFILE_DEV"):
> #{{ secrets.CUSTOM_PUBLISH_PROFILE_DEV }}

We can add pipeline environment, for clarity and for environmental politics:
```yaml
environment:    #environmental variables for managing pipelines
   name: 'Dev'   #strings are in single quotes
```

Environments will be visible in GutHub -> Settings -> Environments

### Continuous Deployment yaml file

Hence, the yaml file will be of the following form

```yaml
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
    environment:    #environmental variables for managing pipielines
      name: 'Dev'   #strings are in single quotes. Name of the environment
      url: ${{ steps.deploy-to-somewhere.outputs.webapp-url }}  #In order to get the url from the further defined step, we do like this (need to refer id)
    steps:
      - name: Download a Build Artifact
        uses: actions/download-artifact@v3.0.0
        with:
          name: api-artifact       #same name as in build step
      - name: Somewhere WebApp deploy   #name of the way for we deploy the app. It can be Azure WebApp or other
        id: deploy-to-somewhere       #In order to refer this step we need to add id to it
        uses: Azure/webapps-deploy@v2   #This is about to deploy on Azure Service
        with:
          app-name: 'CSharpUniversity-dev'
          publish-profile: ${{ secrets.CUSTOM_PUBLISH_PROFILE_DEV }}
```

Nevertheless, this project is not uploaded to the Azure Cloud. 

The production 'yml' file would be like this:

```yaml
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
    environment:    #environmental variables for managing pipielines
      name: 'Dev'   #strings are in single quotes. Name of the environment
      url: ${{ steps.deploy-to-somewhere.outputs.webapp-url }}  #In order to get the url from the further defined step, we do like this (need to refer id)
    steps:
      - name: Download a Build Artifact
        uses: actions/download-artifact@v3.0.0
        with:
          name: api-artifact       #same name as in build step
      - name: Somewhere WebApp deploy   #name of the way for we deploy the app. It can be Azure WebApp or other
        id: deploy-to-somewhere       #In order to refer this step we need to add id to it
        uses: Azure/webapps-deploy@v2   #This is about to deploy on Azure Service
        with:
          app-name: 'CSharpUniversity-dev'
          publish-profile: ${{ secrets.CUSTOM_PUBLISH_PROFILE_DEV }}

  deploy-prod:
    runs-on: ubuntu-latest
    needs: deploy-dev   #we say that this job need previous job to be done
    environment:
      name: 'Prod'
      url: ${{ steps.deploy-to-somewhere.outputs.webapp-url }}  #In order to get the url from the further defined step, we do like this (need to refer id)
    steps:
      - name: Download a Build Artifact
        uses: actions/download-artifact@v3.0.0
        with:
          name: api-artifact
      - name: Somewhere WebApp deploy
        id: deploy-to-somewhere
        uses: Azure/webapps-deploy@v2   #This is about to deploy on Azure Service
        with:
          app-name: 'CSharpUniversity-prod'
          publish-profile: ${{ secrets.CUSTOM_PUBLISH_PROFILE_PROD }}
```

Until now we have Continuous Deployment (we do not have any manual acceptation form the developer)

### Continuous Delivery 

In order to obtain Continuous Delivery:

- Select the proper Environment (Environments will be visible in GutHub -> Settings -> Environments)
- Apply manual acceptation -> "Required reviewers" and we can select who can approve the change
- Before the certain deploy is done, the targeted reviewer gets a email and need to "Review deployments" at GutHub Actions

Continuous Delivery is obtained.

Summarizing:
1. After a pull request, a CI.yml file is executed
2. CI.yml makes a push
3. Therefore, deploy.yml is executed up to the certain deploy with the manual review required
4. Main developer reviews the deploy and the last production deploy step is realized