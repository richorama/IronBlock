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

You'll need to pass this XML to you server using an Ajax call or similar.

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


## License

MIT