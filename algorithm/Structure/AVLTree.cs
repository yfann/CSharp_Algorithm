using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Structure
{
    public class AVLTree<K, V> where K : IComparable
    {
        public AVLNode<K, V> Root = null;

        public void Add(K key, V value)
        {
            Root = Add(key, value, Root);
        }

        public AVLNode<K, V> Add(K key, V value, AVLNode<K, V> tree)
        {
            if (tree == null)
                tree = new AVLNode<K, V>(key, value, null, null);
            if (key.CompareTo(tree.Key) < 0)
            {
                tree.Left = Add(key, value, tree.Left);
                if (Height(tree.Left) - Height(tree.Right) == 2)
                {
                    if (key.CompareTo(tree.Left.Key) < 0)
                    {
                        tree = RotateLL(tree);
                    }
                    else
                    {
                        tree = RotateLR(tree);
                    }
                }
            }

            if (key.CompareTo(tree.Key) > 0)
            {
                tree.Right = Add(key, value, tree.Right);
                if ((Height(tree.Right) - Height(tree.Left)) == 2)
                {
                    if (key.CompareTo(tree.Right.Key) > 0)
                    {
                        tree = RotateRR(tree);
                    }
                    else
                    {
                        tree = RotateRL(tree);
                    }
                }
            }

            if (key.CompareTo(tree.Key) == 0)
            {
                tree.Attach.Add(value);
            }
            tree.Height = Math.Max(Height(tree.Left), Height(tree.Right)) + 1;

            return tree;
        }

        public AVLNode<K, V> Remove(K key, V value)
        {
            return Remove(key, value, Root, true);
        }

        public AVLNode<K, V> Remove(K key, V value, AVLNode<K, V> tree, bool isLazyRemove)
        {
            if (tree == null)
                return null;
            if (key.CompareTo(tree.Key) < 0)
            {
                tree.Left = Remove(key, value, tree.Left, isLazyRemove);
                if (Height(tree.Left) - Height(tree.Right) == 2)
                {
                    if (key.CompareTo(tree.Left.Key) < 0)
                    {
                        tree = RotateLL(tree);
                    }
                    else
                    {
                        tree = RotateLR(tree);
                    }
                }
            }
            if (key.CompareTo(tree.Key) > 0)
            {
                tree.Right = Remove(key, value, tree.Right, isLazyRemove);
                if ((Height(tree.Right) - Height(tree.Left)) == 2)
                {
                    if (key.CompareTo(tree.Right.Key) > 0)
                    {
                        tree = RotateRR(tree);
                    }
                    else
                    {
                        tree = RotateRL(tree);
                    }
                }
            }
            if (key.CompareTo(tree.Key) == 0)
            {
                if (tree.Attach.Count > 1)
                {
                    tree.Attach.Remove(value);
                }
                else
                {
                    if (tree.Left != null && tree.Right != null)
                    {
                        tree.Key = FindMin(tree.Right).Key;
                        tree.Right = Remove(tree.Key, value, tree.Right, false);
                    }
                    else//只有一个子节点或无子节点
                    {
                        tree = tree.Left == null ? tree.Right : tree.Left;
                        if (tree == null)
                            return null;
                    }
                }
            }
            tree.Height = Math.Max(Height(tree.Left), Height(tree.Right)) + 1;
            return tree;
        }

        public AVLNode<K, V> RotateLL(AVLNode<K, V> node)
        {
            var top = node.Left;
            node.Left = top.Right;
            top.Right = node;

            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
            top.Height = Math.Max(Height(top.Left), Height(top.Right)) + 1;
            return top;
        }

        public AVLNode<K, V> RotateRR(AVLNode<K, V> node)
        {
            var top = node.Right;
            node.Right = top.Left;
            top.Left = node;
            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
            top.Height = Math.Max(Height(top.Left), Height(top.Right)) + 1;
            return top;
        }

        public AVLNode<K, V> RotateLR(AVLNode<K, V> node)//左子树的右节点
        {
            node.Left = RotateRR(node.Left);
            return RotateLL(node);
        }

        public AVLNode<K, V> RotateRL(AVLNode<K, V> node)
        {
            node.Right = RotateLL(node.Right);
            return RotateRR(node);
        }

        public AVLNode<K, V> FindMin(AVLNode<K, V> tree)
        {
            if (tree == null)
                return null;
            if (tree.Left == null)
                return tree;
            return FindMin(tree.Left);
        }

        public int Height(AVLNode<K, V> node)
        {
            return node == null ? -1 : node.Height;
        }

        public void MOrder(AVLNode<K, V> node, StringBuilder sb)
        {
            if (node == null)
                return;
            MOrder(node.Left, sb);
            sb.Append(node.Key + " ");
            MOrder(node.Right, sb);
        }

        public string Print()
        {
            StringBuilder sb = new StringBuilder();
            MOrder(Root, sb);
            return sb.ToString();
        }
    }

    public class AVLNode<K, V>
    {
        public K Key;
        public int Height;
        public HashSet<V> Attach = new HashSet<V>();
        public AVLNode<K, V> Left;
        public AVLNode<K, V> Right;

        public AVLNode() { }

        public AVLNode(K key, V value, AVLNode<K, V> left, AVLNode<K, V> right)
        {
            Key = key;
            Attach.Add(value);
            Left = left;
            Right = right;
        }
    }
}