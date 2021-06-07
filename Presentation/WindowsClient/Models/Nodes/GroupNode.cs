using System;

namespace WindowsClient.Models
{
    public class GroupNode : ImportNode<NullValidator>
    {
        public override string Key { get; }

        public GroupNode(string name) : base(name)
        {
            
        }

        protected override void OnMenuOpening(object sender, EventArgs args)
        {
            // No current implementation
        }

        /// <summary>
        /// Adds a trait to the database for every invalid child node
        /// </summary>
        public async void AddTraits(object sender, EventArgs args)
        {
            foreach (ColumnNode n in Nodes)
                if (n.Excel.State["Valid"] is false)
                    await n.AddTrait(sender, args);
        }

        /// <summary>
        /// Sets the ignored state of all child nodes
        /// </summary>
        private void IgnoreAll(object sender, EventArgs args)
        {
            foreach (ColumnNode n in Nodes)
                if (n.Excel.State["Valid"] is false)
                    n.ToggleIgnore(null, args);
        }

        public override void Validate()
        {
            foreach (ColumnNode node in Nodes)
                node.Validate();
        }
    }
}
