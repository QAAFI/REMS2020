using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public interface INode
    {
        public List<INode> Children { get; set; }

        public INode Parent { get; set; }

        public void Add(INode child);

        public void Add(IEnumerable<INode> children);

        public void AddTo(INode parent);
        
        public T SearchTree<T>(INode node) where T : INode;

        public T GetAncestor<T>() where T : INode;
    }
}
