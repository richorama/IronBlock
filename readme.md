[![Build status](https://ci.appveyor.com/api/projects/status/yk44w5v19lvq65lc/branch/master?svg=true)](https://ci.appveyor.com/project/richorama/ironblock/branch/master)

# IronBlock

A .net core interpreter for [blockly](https://developers.google.com/blockly) programs. 

## Installation

Via [nuget](https://www.nuget.org/packages/IronBlock):

```
PM> Install-Package IronBlock
```

or

```
> dotnet add package IronBlock
```

## Basic Usage

Firstly, in JavaScript save your blockly workspace as an XML file:

```js
var xml = Blockly.Xml.workspaceToDom(workspace);
```

The blockly [code demo](https://blockly-demo.appspot.com/static/demos/code/index.html) allows you to view the XML of your workspace.

The XML will look something like this:

```xml
<xml>
  <block type="text_print">
    <value name="TEXT">
      <shadow type="text">
        <field name="TEXT">Hello World</field>
      </shadow>
    </value>
  </block>
</xml>
```

You'll need to pass this XML to your .NET server using an Ajax call or similar.

You can then parse the XML, and execute the Blockly program from .NET.

```cs
// create a parser
var parser = new Parser();

// add the standard blocks to the parser
parser.AddStandardBlocks();

// parse the xml file to create a workspace
var workspace = parser.Parse(xml);

// run the workspace
var output = workspace.Evaluate();

// "Hello World"
```

## Custom Blocks

Blockly has a [block designer](https://blockly-demo.appspot.com/static/demos/blockfactory/index.html) allowing you to create your own blocks very easily.

Custom blocks can be implemented in C# by inheriting `IBlock`:

```cs
public class MyCustomBlock : IBlock
{
    public override object Evaluate(Context context)
    {
        // read a field
        var myField = this.Fields.Get("MY_FIELD");
        
        // evaluate a value
        var myValue = this.Values.Evaluate("MY_VALUE", context);
        
        // evaluate a statement
        var myStatement = this.Statements.Get($"MY_STATEMENT");
        myStatement.Evaluate(context); // evaluate your statement

        // if your block returns a value, simply `return myValue`

        // if your block is part of a statment, and another block runs after it, call
        base.Evaluate(context);
        return null;
    }
}
```

You can then register your block and run it:

```cs
var parser = new Parser();
parser.AddBlock<MyCustomBlock>("my_custom_block");
var workspace = parser.Parse(xml);
workspace.Evaluate();
```

## License

MIT
