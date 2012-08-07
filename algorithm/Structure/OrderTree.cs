using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Structure
{
    public class OrderTree<K, V> where K : IComparable
    {
        public BinaryNode<K, V> Root;

        public void Add(K key, V value)
        {
            Root = Add(key, value, Root);
        }

        public BinaryNode<K, V> Add(K key, V value, BinaryNode<K, V> tree)
        {
            if (tree == null)
                tree = new BinaryNode<K, V>(key, value, null, null);
            if (key.CompareTo(tree.Key) < 0)
                tree.Left = Add(key, value, tree.Left);
            if (key.CompareTo(tree.Key) > 0)
                tree.Right = Add(key, value, tree.Right);
            if (key.CompareTo(tree.Key) == 0)
                tree.Attach.Add(value);
            return tree;
        }

        public BinaryNode<K, V> Remove(K key, V value)
        {
            return Remove(key, value, Root, true);
        }

        public BinaryNode<K, V> Remove(K key, V value, BinaryNode<K, V> tree, bool isLazyRemove)
        {
            if (tree == null)
                return null;
            if (key.CompareTo(tree.Key) < 0)
                tree.Left = Remove(key, value, tree.Left, isLazyRemove);
            if (key.CompareTo(tree.Key) > 0)
                tree.Right = Remove(key, value, tree.Right, isLazyRemove);
            if (key.CompareTo(tree.Key) == 0)
            {
                if (tree.Attach.Count > 1 && isLazyRemove)
                {
                    tree.Attach.Remove(value);
                }
                else
                {
                    if (tree.Left != null && tree.Right != null)
                    {
                        BinaryNode<K, V> node = FindMin(tree.Right);//右侧子树找最小节点
                        tree.Key = node.Key;
                        tree.Attach = node.Attach;
                        tree.Right = Remove(tree.Key, value, tree.Right, false);//删除右侧子树最小节点，不做lazy删除value不起作用
                    }
                    else
                    {
                        tree = tree.Left == null ? tree.Right : tree.Left;
                    }
                }
            }
            return tree;
        }

        public BinaryNode<K, V> FindMin(BinaryNode<K, V> tree)
        {
            if (tree == null)
                return null;
            if (tree.Left == null)
                return tree;
            return FindMin(tree.Left);
        }

        public HashSet<V> SearchRange(K min, K max)
        {
            HashSet<V> hashSet = new HashSet<V>();
            hashSet = SearchRange(min, max, hashSet, Root);
            return hashSet;
        }

        public HashSet<V> SearchRange(K min, K max, HashSet<V> hashSet, BinaryNode<K, V> tree)
        {
            if (tree == null)
                return hashSet;
            if (min.CompareTo(tree.Key) < 0)
                SearchRange(min, max, hashSet, tree.Left);//向左遍历
            if (min.CompareTo(tree.Key) <= 0 && max.CompareTo(tree.Key) >= 0)
            {
                foreach (var item in tree.Attach)
                {
                    hashSet.Add(item);
                }
            }
            if (min.CompareTo(tree.Key) > 0 || max.CompareTo(tree.Key) > 0) //当min在右侧子树时
                SearchRange(min, max, hashSet, tree.Right);// 向右遍历
            return hashSet;
        }

        public void MiddleOrder(BinaryNode<K, V> node, StringBuilder sb)
        {
            if (node == null)
                return;
            if (node.Left != null)
                MiddleOrder(node.Left, sb);
            sb.Append(node.Key + " ");
            if (node.Right != null)
                MiddleOrder(node.Right, sb);
        }

        public string PrintList()
        {
            StringBuilder sb = new StringBuilder();
            MiddleOrder(Root, sb);
            return sb.ToString();
        }
    }

    public class BinaryNode<K, V>
    {
        public K Key;
        public HashSet<V> Attach = new HashSet<V>();
        public BinaryNode<K, V> Left;
        public BinaryNode<K, V> Right;

        public BinaryNode() { }

        public BinaryNode(K key, V value, BinaryNode<K, V> left, BinaryNode<K, V> right)
        {
            Key = key;
            Attach.Add(value);
            Left = left;
            Right = right;
        }
    }
}