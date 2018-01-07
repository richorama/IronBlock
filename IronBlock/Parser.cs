using System;
using System.Collections.Generic;
using System.Xml;

namespace IronBlock
{
    public class Parser
    {
        IDictionary<string,Func<IBlock>> blocks = new Dictionary<string,Func<IBlock>>();

        public void AddBlock<T>(string type) where T : IBlock, new()
        {
            this.AddBlock(type, () => new T());
        }

        public void AddBlock(string type, Func<IBlock> blockFactory)
        {
            if (this.blocks.ContainsKey(type))
            {
                this.blocks[type] = blockFactory;
                return;
            }
            this.blocks.Add(type, blockFactory);
        }

        public Workspace Parse(string xml)
        {
            var xdoc = new XmlDocument();
            xdoc.LoadXml(xml);

            var workspace = new Workspace();
            foreach (XmlNode node in xdoc.DocumentElement.ChildNodes)
            {
                if (node.LocalName == "block" || node.LocalName == "shadow" )
                {
                    var block = ParseBlock(node);
                    if (null != block) workspace.Blocks.Add(block);
                }
            }

            return workspace;
        }

        IBlock ParseBlock(XmlNode node)
        {
            if (bool.Parse(node.GetAttribute("disabled") ?? "false")) return null;

            var type = node.GetAttribute("type");
            if (!this.blocks.ContainsKey(type)) throw new ApplicationException($"block type not registered: '{type}'");
            var block = this.blocks[type]();
            
            block.Type = type;
            block.Id = node.GetAttribute("id");

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.LocalName)
                {
                    case "mutation":
                        ParseMutation(childNode, block);
                        break;
                    case "field":
                        ParseField(childNode, block);
                        break;
                    case "value":
                        ParseValue(childNode, block);
                        break;
                    case "statement":
                        ParseStatement(childNode, block);
                        break;
                    case "comment":
                        // comments are ignored
                        break;
                    case "next":
                        var nextBlock = ParseBlock(childNode.FirstChild);
                        if (null != nextBlock) block.Next = nextBlock;
                        break;
                    default:
                        throw new ArgumentException($"unknown xml type: {childNode.LocalName}");
                }
            }
            return block;
        }

        void ParseField(XmlNode fieldNode, IBlock block)
        {
            var field = new Field
            {
                Name = fieldNode.GetAttribute("name"),
                Value = fieldNode.InnerText
            };
            block.Fields.Add(field);
        }

        void ParseValue(XmlNode valueNode, IBlock block)
        {
            var childNode = valueNode.GetChild("block") ?? valueNode.GetChild("shadow");
            if (childNode == null) return;
            var childBlock = ParseBlock(childNode);

            var value = new Value
            {
                Name = valueNode.GetAttribute("name"),
                Block = childBlock
            };
            block.Values.Add(value);
        }


        void ParseStatement(XmlNode statementNode, IBlock block)
        {
            var childNode = statementNode.GetChild("block") ?? statementNode.GetChild("shadow");
            if (childNode == null) return;
            var childBlock = ParseBlock(childNode);

            var statement = new Statement
            {
                Name = statementNode.GetAttribute("name"),
                Block = childBlock
            };
            block.Statements.Add(statement);
        }

        void ParseMutation(XmlNode mutationNode, IBlock block)
        {
            foreach (XmlAttribute attribute in mutationNode.Attributes)
            {
                block.Mutations.Add(new Mutation("mutation", attribute.Name, attribute.Value));
            }

            foreach (XmlNode node in mutationNode.ChildNodes)
            {
                foreach (XmlAttribute attribute in node.Attributes)
                {
                    block.Mutations.Add(new Mutation(node.Name, attribute.Name, attribute.Value));
                }
            }
        }

    }

    internal static class ParserExtensions
    {
        public static XmlNode GetChild(this XmlNode node, string name)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (childNode.LocalName == name) return childNode;
            }
            return null;
        }

        public static string GetAttribute(this XmlNode node, string name)
        {
            foreach (XmlAttribute attribute in node.Attributes)
            {
                if (attribute.Name == name) return attribute.Value;
            }
            return null;

        }
    }


}