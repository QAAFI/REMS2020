using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Models
{
    using Core;
    using Functions;
    using Functions.DemandFunctions;
    using Functions.SupplyFunctions;
    using PMF;
    using PMF.Library;
    using PMF.Organs;
    using PMF.Phen;
    using PMF.Struct;
    using PostSimulationTools;
    using Report;
    using Soils;
    using Storage;
    using Surface;

    /// <summary>
    /// Base node in the tree, this should not be instantiated directly
    /// </summary>
    public abstract class ApsimNode : INode, IDisposable
    {
        // TODO: Make class abstract?

        public virtual string Name { get; set; }      

        public virtual List<INode> Children { get; set; } = new List<INode>();

        public virtual bool IncludeInDocumentation { get; set; } = true;

        public virtual bool Enabled { get; set; } = true;

        public virtual bool ReadOnly { get; set; } = false;

        [JsonIgnore]
        public virtual INode Parent { get; set; }

        [JsonIgnore]
        private bool disposed = false;

        public ApsimNode()
        {
        }
        
        /// <summary>
        /// Add a child node
        /// </summary>
        public void Add(INode child)
        {
            if (child is null) return;
            Children.Add(child);
        }

        /// <summary>
        /// Add a collection of child nodes
        /// </summary>
        public void Add(IEnumerable<INode> children)
        {
            if (children is null) return;
            foreach (INode node in children) Add(node);
        }

        /// <summary>
        /// Adds this node to the given parent
        /// </summary>
        public void AddTo(INode parent)
        {
            parent.Add(this);
        }

        /// <summary>
        /// Use a Depth-first search to find an instance of 
        /// the given node type. Returns null if none are found.
        /// </summary>
        /// <typeparam name="T">The type of node to search for</typeparam>
        public T SearchTree<T>(INode node) where T : INode
        {
            var result = node.Children
                .Select(n => (n.GetType() == typeof(T)) ? n : SearchTree<T>(n));

            return result.OfType<T>().FirstOrDefault();
        }

        /// <summary>
        /// Iterates over the nodes ancestors until it finds
        /// the first instance of the given node type.
        /// </summary>
        /// <typeparam name="T">The type of node to search for</typeparam>
        public T GetAncestor<T>() where T : INode
        {
            INode ancestor = Parent;

            while (ancestor.Parent.GetType() != typeof(T))
            {
                ancestor = ancestor.Parent;
            }

            return (T)ancestor.Parent;
        }

        public void WriteToFile(string file)
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

        public static T ReadFromFile<T>(string file) where T : INode
        {
            using (StreamReader stream = new StreamReader(file))
            using (JsonReader reader = new JsonTextReader(stream))
            {
                JsonSerializer serializer = new JsonSerializer()
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Objects
                };                

                var node = serializer.Deserialize<INode>(reader);
                return (T)node;
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
