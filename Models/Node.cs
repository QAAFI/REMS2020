using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models
{    
    /// <summary>
    /// Base node in the tree, this should not be instantiated directly
    /// </summary>
    public class Node : IDisposable
    {
        public string Name { get; set; }      

        public List<Node> Children { get; set; } = new List<Node>();

        public bool IncludeInDocumentation { get; set; } = true;

        public bool Enabled { get; set; } = true;

        public bool ReadOnly { get; set; } = false;

        [JsonIgnore]
        public Node Parent { get; set; }

        [JsonIgnore]
        private bool disposed = false;

        public Node()
        {
        }
        
        /// <summary>
        /// Add a child node
        /// </summary>
        public void Add(Node child)
        {
            if (child is null) return;
            Children.Add(child);
        }

        /// <summary>
        /// Add a collection of child nodes
        /// </summary>
        public void Add(IEnumerable<Node> children)
        {
            if (children is null) return;
            foreach (Node node in children) Add(node);
        }

        /// <summary>
        /// Adds this node to the given parent
        /// </summary>
        public void AddTo(Node parent)
        {
            parent.Add(this);
        }

        /// <summary>
        /// Use a Depth-first search to find an instance of 
        /// the given node type. Returns null if none are found.
        /// </summary>
        /// <typeparam name="Node">The type of node to search for</typeparam>
        public Node SearchTree<Node>(Models.Node node) where Node : Models.Node
        {
            var result = node.Children
                .Select(n => (n.GetType() == typeof(Node)) ? n : SearchTree<Node>(n));

            return result.OfType<Node>().FirstOrDefault();
        }

        /// <summary>
        /// Iterates over the nodes ancestors until it finds
        /// the first instance of the given node type.
        /// </summary>
        /// <typeparam name="Node">The type of node to search for</typeparam>
        public Node GetAncestor<Node>() where Node : Models.Node
        {
            Models.Node ancestor = Parent;

            while (ancestor.Parent.GetType() != typeof(Node))
            {
                ancestor = ancestor.Parent;
            }

            return (Node)ancestor.Parent;
        }

        /// <summary>
        /// Implements IDisposable
        /// </summary>
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);

            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implements IDisposable
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
            }

            disposed = true;
        }
    }
}
