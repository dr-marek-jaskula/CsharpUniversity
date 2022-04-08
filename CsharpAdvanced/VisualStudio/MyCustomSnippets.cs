namespace CsharpAdvanced.VisualStudio;

public class MyCustomSnippets
{
    //Create uprop snippet to make fullprop with privete field underscore prefix (_)
    //Set this code into a .snippet file (best way is just to search in visual studio for snippet manager, open one snipet, save as your snippet and paste your code there)
    //Code for uprop snippet:

    /*
<?xml version="1.0" encoding="utf-8" ?>
<CodeSnippets  xmlns="http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet">
 <CodeSnippet Format="1.0.0">
  <Header>
   <Title>Full property with underscored field</Title>
   <Shortcut>uprop</Shortcut>
   <Description>Code snippet for property and private backing field with undescore prefix</Description>
   <Author>Eltin</Author>
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
        <ToolTip>The private field with underscore</ToolTip>
        <Default>myField</Default>
      </Literal>
    </Declarations>
    <Code Language="csharp">
      <![CDATA[private $type$ _$field$;
    public $type$ $property$	
    {	
	get { return _$field$;}
	set { _$field$ = value;}
    }	
    $end$]]>	
    </Code>
  </Snippet>
</CodeSnippet>
</CodeSnippets>

    */
}

