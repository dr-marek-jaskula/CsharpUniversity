# Snippets

## Build-in Snippets

> prop -> normal property

> ct -> constructor

> cw -> Console.WriteLine

> foreach -> foreach loop

> class -> class without access modifier and no sealed keyword (remove this default one and add new or modify default one)

> struct -> struct

> tryf -> try-finally block

> try -> try-catch block

> for -> for loop

> forr -> for loop with decrementation

> dowhile -> do while loop

> while -> while loop

> equals -> overwrite the equal function

## Custom Snippets

> class -> change the default one to the custom one (I personally prefer to remove the class snippet and add the custom one)

```xml
<?xml version="1.0" encoding="utf-8" ?>
<CodeSnippets  xmlns="http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet">
	<CodeSnippet Format="1.0.0">
		<Header>
			<Title>Public sealed class</Title>
			<Shortcut>class</Shortcut>
			<Description>Code snippet for public sealed class</Description>
			<Author>dr Marek Jaskuła</Author>
			<SnippetTypes>
				<SnippetType>Expansion</SnippetType>
				<SnippetType>SurroundsWith</SnippetType>
			</SnippetTypes>
		</Header>
		<Snippet>
			<Declarations>
				<Literal>
					<ID>name</ID>
					<ToolTip>Class name</ToolTip>
					<Default>MyClass</Default>
				</Literal>
			</Declarations>
			<Code Language="csharp"><![CDATA[public sealed class $name$
	{
		$selected$$end$
	}]]>
			</Code>
		</Snippet>
	</CodeSnippet>
</CodeSnippets>
```

> uprop -> underscored full property

```xml
<?xml version="1.0" encoding="utf-8" ?>
<CodeSnippets  xmlns="http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet">
	<CodeSnippet Format="1.0.0">
		<Header>
			<Title>Property with underscored field</Title>
			<Shortcut>uprop</Shortcut>
			<Description>Code snippet for property with an underscored backing field</Description>
			<Author>dr Marek Jaskuła</Author>
			<SnippetTypes>
				<SnippetType>Expansion</SnippetType>
			</SnippetTypes>
		</Header>
		<Snippet>
			<Declarations>
				<Literal>
					<ID>type</ID>
					<ToolTip>Property type</ToolTip>
					<Default>int</Default>
				</Literal>
				<Literal>
					<ID>property</ID>
					<ToolTip>Property name</ToolTip>
					<Default>MyProperty</Default>
				</Literal>
				<Literal>
					<ID>field</ID>
					<ToolTip>The privet variable backing this property</ToolTip>
					<Default>myVar</Default>
				</Literal>
			</Declarations>
			<Code Language="csharp"><![CDATA[private $type$ _$field$;

	public $type$ $property$
	{
		get { return _$field$;}
		set { _$field$ = value;}
	}$end$]]>
			</Code>
		</Snippet>
	</CodeSnippet>
</CodeSnippets>
```

> fact -> xUnit overriden fact (by default there is xUnit fact)

```xml
<?xml version="1.0" encoding="utf-8" ?>
<CodeSnippets  xmlns="http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet">
	<CodeSnippet Format="1.0.0">
		<Header>
			<Title>xUnit Fact</Title>
			<Shortcut>fact</Shortcut>
			<Description>Code snippet for xUnit test method with fact attribute</Description>
			<Author>dr Marek Jaskuła</Author>
			<SnippetTypes>
				<SnippetType>Expansion</SnippetType>
			</SnippetTypes>
		</Header>
		<Snippet>
			<Code Language="CSharp">
			  <![CDATA[[Fact]
		  public void $MethodToTest$_Should$ExpectedBehavior$_When$TestScenario$()
		  {
			  //Arrange
			  
			  //Act
			  
			  //Assert
			  $exception$$end$
		  }]]>
			</Code>
			<Declarations>
			  <Literal>
				<ID>MethodToTest</ID>
				<ToolTip>Replace by the test method name</ToolTip>
				<Default>MethodToTest</Default>
			  </Literal>
			  <Literal>
				<ID>TestScenario</ID>
				<ToolTip>Replace by the test scenario</ToolTip>
				<Default>TestScenario</Default>
			  </Literal>
			  <Literal>
				<ID>ExpectedBehavior</ID>
				<ToolTip>Replace by the test expected behavior</ToolTip>
				<Default>ExpectedBehavior</Default>
			  </Literal>
			<Literal Editable="false">
				<ID>Fact</ID>
				<Function>SimpleTypeName(global::xunit)</Function>
			</Literal>
			  <Literal>
			  	<ID>exception</ID>
			  	<ToolTip>Throw exception if method body is not implemented</ToolTip>
			  	<Default>throw new NotImplementedException();</Default>
			  </Literal>
			</Declarations>
		</Snippet>
	</CodeSnippet>
</CodeSnippets>
```

> afact -> xUnit overriden afact (by default there is xUnit afact)

```xml
<?xml version="1.0" encoding="utf-8" ?>
<CodeSnippets  xmlns="http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet">
	<CodeSnippet Format="1.0.0">
		<Header>
			<Title>xUnit async Fact</Title>
			<Shortcut>afact</Shortcut>
			<Description>Code snippet for xUnit async test method with fact attribute</Description>
			<Author>dr Marek Jaskuła</Author>
			<SnippetTypes>
				<SnippetType>Expansion</SnippetType>
			</SnippetTypes>
		</Header>
		<Snippet>
			<Code Language="CSharp">
			  <![CDATA[[Fact]
		  public async Task $MethodToTest$_Should$ExpectedBehavior$_When$TestScenario$()
		  {
			  //Arrange
			  
			  //Act
			  
			  //Assert
			  $exception$$end$
		  }]]>
			</Code>
			<Declarations>
			  <Literal>
				<ID>MethodToTest</ID>
				<ToolTip>Replace by the test method name</ToolTip>
				<Default>MethodToTest</Default>
			  </Literal>
			  <Literal>
				<ID>TestScenario</ID>
				<ToolTip>Replace by the test scenario</ToolTip>
				<Default>TestScenario</Default>
			  </Literal>
			  <Literal>
				<ID>ExpectedBehavior</ID>
				<ToolTip>Replace by the test expected behavior</ToolTip>
				<Default>ExpectedBehavior</Default>
			  </Literal>
			<Literal Editable="false">
				<ID>Fact</ID>
				<Function>SimpleTypeName(global::xunit)</Function>
			</Literal>
			  <Literal>
			  	<ID>exception</ID>
			  	<ToolTip>Throw exception if method body is not implemented</ToolTip>
			  	<Default>throw new NotImplementedException();</Default>
			  </Literal>
			</Declarations>
		</Snippet>
	</CodeSnippet>
</CodeSnippets>
```

> theory -> xUnit overriden theory (by default there is xUnit theory)

```xml
<?xml version="1.0" encoding="utf-8" ?>
<CodeSnippets  xmlns="http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet">
	<CodeSnippet Format="1.0.0">
		<Header>
			<Title>xUnit Theory</Title>
			<Shortcut>theory</Shortcut>
			<Description>Code snippet for xUnit test method with theory attribute</Description>
			<Author>dr Marek Jaskuła</Author>
			<SnippetTypes>
				<SnippetType>Expansion</SnippetType>
			</SnippetTypes>
		</Header>
		<Snippet>
			<Code Language="CSharp">
			  <![CDATA[[Theory]
		  public void $MethodToTest$_Should$ExpectedBehavior$_When$TestScenario$()
		  {
			  //Arrange
			  
			  //Act
			  
			  //Assert
			  $exception$$end$
		  }]]>
			</Code>
			<Declarations>
			  <Literal>
				<ID>MethodToTest</ID>
				<ToolTip>Replace by the test method name</ToolTip>
				<Default>MethodToTest</Default>
			  </Literal>
			  <Literal>
				<ID>TestScenario</ID>
				<ToolTip>Replace by the test scenario</ToolTip>
				<Default>TestScenario</Default>
			  </Literal>
			  <Literal>
				<ID>ExpectedBehavior</ID>
				<ToolTip>Replace by the test expected behavior</ToolTip>
				<Default>ExpectedBehavior</Default>
			  </Literal>
			  <Literal Editable="false">
				<ID>Theory</ID>
				<Function>SimpleTypeName(global::xunit)</Function>
			  </Literal>
			  <Literal>
			  	<ID>exception</ID>
			  	<ToolTip>Throw exception if method body is not implemented</ToolTip>
			  	<Default>throw new NotImplementedException();</Default>
			  </Literal>
			</Declarations>
		</Snippet>
	</CodeSnippet>
</CodeSnippets>
```

> atheory -> xUnit overriden atheory (by default there is xUnit atheory)

```xml
<?xml version="1.0" encoding="utf-8" ?>
<CodeSnippets  xmlns="http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet">
	<CodeSnippet Format="1.0.0">
		<Header>
			<Title>xUnit async Theory</Title>
			<Shortcut>atheory</Shortcut>
			<Description>Code snippet for xUnit async test method with theory attribute</Description>
			<Author>dr Marek Jaskuła</Author>
			<SnippetTypes>
				<SnippetType>Expansion</SnippetType>
			</SnippetTypes>
		</Header>
		<Snippet>
			<Code Language="CSharp">
			  <![CDATA[[Theory]
		  public async Task $MethodToTest$_Should$ExpectedBehavior$_When$TestScenario$()
		  {
			  //Arrange
			  
			  //Act
			  
			  //Assert
			  $exception$$end$
		  }]]>
			</Code>
			<Declarations>
			  <Literal>
				<ID>MethodToTest</ID>
				<ToolTip>Replace by the test method name</ToolTip>
				<Default>MethodToTest</Default>
			  </Literal>
			  <Literal>
				<ID>TestScenario</ID>
				<ToolTip>Replace by the test scenario</ToolTip>
				<Default>TestScenario</Default>
			  </Literal>
			  <Literal>
				<ID>ExpectedBehavior</ID>
				<ToolTip>Replace by the test expected behavior</ToolTip>
				<Default>ExpectedBehavior</Default>
			  </Literal>
			  <Literal Editable="false">
				<ID>Theory</ID>
				<Function>SimpleTypeName(global::xunit)</Function>
			  </Literal>
			  <Literal>
			  	<ID>exception</ID>
			  	<ToolTip>Throw exception if method body is not implemented</ToolTip>
			  	<Default>throw new NotImplementedException();</Default>
			  </Literal>
			</Declarations>
		</Snippet>
	</CodeSnippet>
</CodeSnippets>
```

> aaa -> Arrange, Act, Assert sections

```xml
<?xml version="1.0" encoding="utf-8" ?>
<CodeSnippets  xmlns="http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet">
	<CodeSnippet Format="1.0.0">
		<Header>
			<Title>Arrange, Act, Assert sections</Title>
			<Shortcut>aaa</Shortcut>
			<Description>Add Arrange, Act, Assert comment section to structure tests</Description>
			<Author>dr Marek Jaskuła</Author>
			<SnippetTypes>
				<SnippetType>Expansion</SnippetType>
			</SnippetTypes>
		</Header>
		<Snippet>
			<Code Language="csharp">
				<![CDATA[//Arrange

        //Act

        //Assert
$end$]]>
			</Code>
		</Snippet>
	</CodeSnippet>
</CodeSnippets>
```
