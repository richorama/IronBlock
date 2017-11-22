using System;
using System.Collections.Generic;

namespace IronBlock
{
    public interface IFragment
    { 
        // probably need a method like this here:
        object Evaluate(IDictionary<string,object> variables);
    }

    public class Workspace : IFragment
    {
        public Workspace()
        {
            this.Blocks = new List<IBlock>();
        }

        public IList<IBlock> Blocks {get;set;}

        public virtual object Evaluate(IDictionary<string, object> variables)
        {   
            // TODO: variables
            object returnValue = null;

            foreach (var block in this.Blocks)
            {
                returnValue = block.Evaluate(variables);
            }

            return returnValue;
        }
    }



    public abstract class IBlock : IFragment
    { 
        public IBlock()
        {
            this.Fields = new List<Field>();
            this.Values = new List<Value>();
            this.Statements = new List<Statement>();
            this.Mutations = new Dictionary<string,string>();
        }

        public string Id { get; set; }
        public IList<Field> Fields { get; set; }
        public IList<Value> Values { get; set; }
        public IList<Statement> Statements { get; set; }
        public string Type { get; set; }
        public bool Inline { get; set; }
        public IBlock Next { get; set; }
        public IDictionary<string,string> Mutations { get; set; }
        public virtual object Evaluate(IDictionary<string, object> variables)
        {
            if (null != this.Next)
            {
                return this.Next.Evaluate(variables);                
            }
            return null;
        }
    
    }

    public class Statement : IFragment
    { 
        public string Name { get; set; }
        public IBlock Block { get; set; }
        public object Evaluate(IDictionary<string, object> variables)
        {
            if (null == this.Block) return null;
            return this.Block.Evaluate(variables);
        }
    }

    public class Value : IFragment
    { 
        public string Name { get; set; }
        public IBlock Block { get; set; }
        public object Evaluate(IDictionary<string, object> variables)
        {
            if (null == this.Block) return null;
            return this.Block.Evaluate(variables);
        }

    }

    public class Field
    { 
        public string Name { get; set; }
        public string Value { get; set; }
    }



}
