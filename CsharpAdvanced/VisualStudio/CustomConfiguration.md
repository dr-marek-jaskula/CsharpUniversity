# Custom Configuration

## Underscore Prefix for Private Fields

Change default convention for naming the private fields. Use prefix underscore '_':

```Tools -> Options -> Text Editor -> C# -> Code Style -> Naming -> Manage Naming Styles -> Add naming style for Private or Internal Field```

- Naming style: _fieldName
- Required prefix: _
- Capitalization: camel Case Name

## Add Class Designer

Class designer will enable us to visually design the diagrams of DB model relations.

- Open Visual Studio Installer (for example from Tool -> Get Tools and Features -> Singletons)
- Find "Class Designer"

To get class diagram: 
```Add New Item -> Class Diagram```

To make relation just right click on certain property of field and "show as association" (for singular) and collection for multiple.

To export diagrams as images just right click and export as image

To get the class diagram view just type in search

## File Scope Namespaces

Globally change (Preffered):

```Options -> Text Editor -> Basic -> Code Style -> General```

find "Namespaces declarations" and set it to "refactor only"

Project scope change:

- Right click on project
- Add -> NewEditorConfig
- Go to -> Code Styles
- Change "Namespace declarations" to "File scoped"

## Pinned Tabs

- Search for "pinned tabs"
- Select show pinned tabs in a separate row

## Add Folder, Add class

```Tools -> Options -> General -> Keyboard -> Project.AddClass -> Global -> Alt + n```

```Tools -> Options -> General -> Keyboard -> Project.NewFolder -> Global -> Alt + f```

## Toggle Comment

```Tools -> Options -> General -> Keyboard -> Edit.ToggleComment -> Global -> ctrl + /```
