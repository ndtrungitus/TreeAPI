using System;
using TreeAPI.TreeData;

namespace TreeAPI.Models
{
    public class MockTreeData : ITreeData
    {
        private TreeNode<NodeData> _root;
        public MockTreeData() : base()
        {
            _root = new TreeNode<NodeData>(new NodeData(0, "nodeA"));
            TreeNode<NodeData> nodeB = _root.AddChild(new NodeData(1, "nodeB"));
            TreeNode<NodeData> nodeC = _root.AddChild(new NodeData(2, "nodeC"));
            TreeNode<NodeData> nodeD = _root.AddChild(new NodeData(3, "nodeD"));

            TreeNode<NodeData> nodeE = nodeB.AddChild(new NodeData(4, "nodeE"));
            TreeNode<NodeData> nodeF = nodeB.AddChild(new NodeData(5, "nodeF"));
            TreeNode<NodeData> nodeG = nodeB.AddChild(new NodeData(6, "nodeG"));

            TreeNode<NodeData> nodeH = nodeC.AddChild(new NodeData(7, "nodeH"));

            TreeNode<NodeData> nodeI = nodeD.AddChild(new NodeData(8, "nodeI"));
            TreeNode<NodeData> nodeJ = nodeD.AddChild(new NodeData(9, "nodeJ"));

            TreeNode<NodeData> nodeK = nodeI.AddChild(new NodeData(10, "nodeK"));
            TreeNode<NodeData> nodeL = nodeI.AddChild(new NodeData(11, "nodeL"));
            TreeNode<NodeData> nodeM = nodeI.AddChild(new NodeData(12, "nodeM"));
        }

        public TreeNode<NodeData> GetRootNode()
        {
            return _root;
        }

        public TreeNode<NodeData> GetNodeById(int id)
        {
            TreeNode<NodeData> node = _root.FindTreeNode(node => node.Data != null && node.Data.Id == id);
            return node;
        }

        public TreeNode<NodeData> GetNodeByName(string name)
        {
            TreeNode<NodeData> node = _root.FindTreeNode(node => node.Data != null && node.Data.Name.Equals(name));
            return node;
        }

        public TreeNode<NodeData> AddNode(NodeData data, int parent, int index)
        {
            TreeNode<NodeData> parentNode = _root.FindTreeNode(node => node.Data != null && node.Data.Id == parent);
            if (parentNode != null)
            {
                TreeNode<NodeData> childNode = parentNode.AddChild(data);

                return childNode;
            }

            return null;
        }

        public TreeNode<NodeData> EditNode(NodeData data)
        {
            TreeNode<NodeData> oldNode = _root.FindTreeNode(node => node.Data != null && node.Data.Id == data.Id);
            TreeNode<NodeData> node = new TreeNode<NodeData>(oldNode);
            if (node != null)
            {
                node.Data = data;
                node.UpdateNode(oldNode);

                return node;
            }

            return null;
        }

        public bool DeleteNode(int id)
        {
            TreeNode<NodeData> node = _root.FindTreeNode(node => node.Data != null && node.Data.Id == id);
            if (node != null)
            {
                node.DeleteNode();
                return true;
            }

            return false;
        }
    }
}
