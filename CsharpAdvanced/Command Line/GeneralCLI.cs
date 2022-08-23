namespace CsharpAdvanced.Command_Line_Dotnet;

public class GeneralCLI
{
    //tabulator autofills the file or folder name. So if we type "cd rep" and click tab we can get "cd repository"

    //cls -> cleans the console
    //exit -> exit the certain command line
    //dir -> print the directory files
    //cd -> change directory from the current or to global one

    //explorer -> open then file explorer to the location

    //dotnet sdk check -> checks the sdk version and all dotnets

    //dotnet restore -> perform a NuGet restore operation. Important to performed before the build, especially in docker containers
    //dotnet build -> build an application
    //dotnet build --no-restore -> use it to build without restoring (its good to restore before the build so this is important)

    #region Testing

    //dotnet test -> run tests for dotnet
    /*
    dotnet test[< PROJECT > | < SOLUTION > | < DIRECTORY > | < DLL > | < EXE >]
    [-a|--test-adapter-path<ADAPTER_PATH>]
    [--arch<ARCHITECTURE>]
    [--blame]
    [--blame-crash]
    [--blame-crash-dump-type<DUMP_TYPE>]
    [--blame-crash-collect-always]
    [--blame-hang]
    [--blame-hang-dump-type<DUMP_TYPE>]
    [--blame-hang-timeout<TIMESPAN>]
    [-c|--configuration<CONFIGURATION>]
    [--collect<DATA_COLLECTOR_NAME>]
    [-d|--diag<LOG_FILE>]
    [-f|--framework<FRAMEWORK>]
    [--filter<EXPRESSION>]
    [--interactive]
    [-l|--logger<LOGGER>]
    [--no-build]
    [--nologo]
    [--no-restore]
    [-o|--output<OUTPUT_DIRECTORY>]
    [--os<OS>]
    [-r|--results-directory<RESULTS_DIR>]
    [--runtime<RUNTIME_IDENTIFIER>]
    [-s|--settings<SETTINGS_FILE>]
    [-t|--list-tests]
    [-v|--verbosity<LEVEL>]
    [<args>...]
    [[--] <RunSettings arguments>]

    dotnet test -h|--help
    dotnet test ~/projects/test1/test1.csproj
    */

    //To run the specific test:
    //dotnet test --filter DisplayName=XUnitTests.OrderControllerTest.GetAll_Returns_The_Correct_Numer_Of_Orders

    #endregion Testing
}