# Custom Templates

## Custom Class Template

Change directory to:
> C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\ItemTemplates\CSharp\Code\1033\Class

Change the class template (but first change to scoped namespaces) to:

```csharp
namespace $rootnamespace$;

public class $safeitemrootname$
{

}
```

or with "seald" keyword

```csharp
namespace $rootnamespace$;

public sealed class $safeitemrootname$
{

}
```