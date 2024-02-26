using IronBlock.Blocks;
using System;
using System.Collections.Generic;

namespace IronBlock
{
  public abstract class IParser
  {

    protected IDictionary<string, Func<IBlock>> blocks = new Dictionary<string, Func<IBlock>>();

    abstract public Workspace Parse(string source, bool preserveWhitespace = false);
    
    public IParser AddBlock<T>(string type) where T : IBlock, new()
    {
      this.AddBlock(type, () => new T());
      return this;
    }

    public IParser AddBlock<T>(string type, T block) where T : IBlock
    {
      this.AddBlock(type, () => block);
      return this;
    }

    public IParser AddBlock(string type, Func<IBlock> blockFactory)
    {
      if (this.blocks.ContainsKey(type))
      {
        this.blocks[type] = blockFactory;
        return this;
      }
      this.blocks.Add(type, blockFactory);
      return this;
    }

  }
}