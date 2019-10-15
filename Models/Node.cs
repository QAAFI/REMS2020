using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Models
{    
    /// <summary>
    /// Base node in the tree, this should not be instantiated directly
    /// </summary>
    public abstract class Node : IDisposable
    {
        public virtual string Name { get; set; }      

        public virtual List<Node> Children { get; set; } = new List<Node>();

        public virtual bool IncludeInDocumentation { get; set; } = true;

        public virtual bool Enabled { get; set; } = true;

        public virtual bool ReadOnly { get; set; } = false;

        [JsonIgnore]
        public virtual Node Parent { get; set; }

        [JsonIgnore]
        private bool disposed = false;

        public Node()
        {
        }
        
        /// <summary>
        /// Add a child node
        /// </summary>
        public virtual void Add(Node child)
        {
            if (child is null) return;
            Children.Add(child);
        }

        /// <summary>
        /// Add a collection of child nodes
        /// </summary>
        public virtual void Add(IEnumerable<Node> children)
        {
            if (children is null) return;
            foreach (Node node in children) Add(node);
        }

        /// <summary>
        /// Adds this node to the given parent
        /// </summary>
        public virtual void AddTo(Node parent)
        {
            parent.Add(this);
        }

        /// <summary>
        /// Use a Depth-first search to find an instance of 
        /// the given node type. Returns null if none are found.
        /// </summary>
        /// <typeparam name="Node">The type of node to search for</typeparam>
        public virtual Node SearchTree<Node>(Models.Node node) where Node : Models.Node
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
        public virtual Node GetAncestor<Node>() where Node : Models.Node
        {
            Models.Node ancestor = Parent;

            while (ancestor.Parent.GetType() != typeof(Node))
            {
                ancestor = ancestor.Parent;
            }

            return (Node)ancestor.Parent;
        }

        public virtual void WriteToFile(string file)
        {
            using (StreamWriter stream = new StreamWriter(file))
            using (JsonWriter writer = new JsonTextWriter(stream))
            {
                writer.CloseOutput = true;
                writer.AutoCompleteOnClose = true;

                JsonSerializer serializer = new JsonSerializer()
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Objects
                };
                serializer.Serialize(writer, this);

                serializer = null;
                Dispose();
            }
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
