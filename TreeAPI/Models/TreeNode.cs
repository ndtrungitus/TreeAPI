using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace TreeAPI.Models
{
    public class TreeNode<T> : IEnumerable<TreeNode<T>>
    {

        public T Data { get; set; }
        public TreeNode<T> Parent { get; set; }
        public ICollection<TreeNode<T>> Children { get; set; }


        public TreeNode(TreeNode<T> obj)
        {
            Data = obj.Data;
            Parent = obj.Parent;
            Children = obj.Children;
            ElementsIndex = obj.ElementsIndex;
        }

        public Boolean IsRoot
        {
            get { return Parent == null; }
        }

        public Boolean IsLeaf
        {
            get { return Children.Count == 0; }
        }

        public int Level
        {
            get
            {
                if (this.IsRoot)
                    return 0;
                return Parent.Level + 1;
            }
        }


        public TreeNode(T data)
        {
            this.Data = data;
            this.Children = new LinkedList<TreeNode<T>>();

            this.ElementsIndex = new LinkedList<TreeNode<T>>();
            this.ElementsIndex.Add(this);
        }

        public TreeNode<T> AddChild(T child)
        {
            TreeNode<T> childNode = new TreeNode<T>(child) { Parent = this };
            this.Children.Add(childNode);

            this.RegisterChildForSearch(childNode);

            return childNode;
        }

        public void UpdateNode(TreeNode<T> oldNode)
        {
            TreeNode<T> parentNode = this.Parent;
            parentNode.Children.Remove(oldNode);
            parentNode.Children.Add(this);

            this.UnregisterChildForSearch(oldNode);
            this.RegisterChildForSearch(this);
        }

        public void DeleteNode()
        {
            TreeNode<T> parentNode = this.Parent;
            parentNode.Children.Remove(this);
         
            this.RemoveNodeForSearch(this);
        }

        public override string ToString()
        {
            return Data != null ? Data.ToString() : "[data null]";
        }


        #region searching

        private ICollection<TreeNode<T>> ElementsIndex { get; set; }

        private void RegisterChildForSearch(TreeNode<T> node)
        {
            ElementsIndex.Add(node);
            if (Parent != null)
                Parent.RegisterChildForSearch(node);
        }

        private void UnregisterChildForSearch(TreeNode<T> node)
        {
            ElementsIndex.Remove(node);
            if (Parent != null)
                Parent.UnregisterChildForSearch(node);
        }


        private void RemoveNodeForSearch(TreeNode<T> node)
        {
            RemoveChildNodeForSearch(node);
            if (Parent != null)
                Parent.RemoveNodeForSearch(node);
        }

        private void RemoveChildNodeForSearch(TreeNode<T> node)
        {
            ElementsIndex.Remove(node);
            if (node.Children.Count > 0)
            {
                foreach (TreeNode<T> childNode in node.Children)
                {
                    ElementsIndex.Remove(childNode);
                }

                foreach (TreeNode<T> childNode in node.Children)
                {
                    RemoveChildNodeForSearch(childNode);
                }
            }
        }

        public TreeNode<T> FindTreeNode(Func<TreeNode<T>, bool> predicate)
        {
            return this.ElementsIndex.FirstOrDefault(predicate);
        }

        #endregion


        #region iterating

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TreeNode<T>> GetEnumerator()
        {
            yield return this;
            foreach (var directChild in this.Children)
            {
                foreach (var anyChild in directChild)
                    yield return anyChild;
            }
        }

        #endregion
    }
}
