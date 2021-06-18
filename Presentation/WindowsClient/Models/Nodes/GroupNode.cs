using System;

namespace WindowsClient.Models
{
    public class GroupNode : ImportNode
    {
        public override string Key { get; }

        public GroupNode(string name) : base(name)
        { }

        protected override void OnMenuOpening(object sender, EventArgs args)
        {
            // No current implementation
        }

        public override void Refresh()
        {
            foreach (ColumnNode node in Nodes)
                node.Refresh();
        }
    }
}
