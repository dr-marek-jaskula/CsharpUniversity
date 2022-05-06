In order to make the "Program" class (which is internal) visible to this project we need to:
1. Go to program calls and at the top of it (after usings) write:
[assembly: InternalsVisibleTo("xUnitTestsForWebApi")]