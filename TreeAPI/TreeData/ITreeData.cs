using TreeAPI.Models;

namespace TreeAPI.TreeData
{
    public interface ITreeData
    {
        TreeNode<NodeData> GetRootNode();

        TreeNode<NodeData> GetNodeById(int id);

        TreeNode<NodeData> GetNodeByName(string name);

        TreeNode<NodeData> AddNode(NodeData data, int parent, int index);

        TreeNode<NodeData> EditNode(NodeData data);

        bool DeleteNode(int id);
    }
}
