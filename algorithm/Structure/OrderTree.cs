using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Structure
{
    public class OrderTree<K,V> where K:IComparable
    {
        public BinaryNode<K, V> Root;

        public void Add(K key, V value)
        {
            Root = Add(key,value,Root);
        }

        public BinaryNode<K, V> Add(K key, V value, BinaryNode<K, V> tree)
        {
            if (tree == null)
                tree = new BinaryNode<K, V>(key,value,null,null);
            if (key.CompareTo(tree.Key) < 0)
                tree.Left = Add(key,value,tree.Left);
            if (key.CompareTo(tree.Key) > 0)
                tree.Right = Add(key,value,tree.Right);
            if (key.CompareTo(tree.Key) == 0)
                tree.Attach.Add(value);
            return tree;
        }

        public HashSet<V> SearchRange(K min, K max)
        {
            HashSet<V> hashSet = new HashSet<V>();
            hashSet = SearchRange(min,max,hashSet,Root);
            return hashSet;
        }

        public HashSet<V> SearchRange(K min, K max, HashSet<V> hashSet, BinaryNode<K, V> tree)
        {
            if (tree == null)
                return hashSet;
            if (min.CompareTo(tree.Key) < 0)
                SearchRange(min, max, hashSet, tree.Left);
            if (min.CompareTo(tree.Key) <= 0 && max.CompareTo(tree.Key) >= 0)
            {
                foreach (var item in tree.Attach)
                {
                    hashSet.Add(item);
                }
            }
            if (min.CompareTo(tree.Key) > 0 || max.CompareTo(tree.Key) > 0)
                SearchRange(min,max,hashSet,tree.Right);
            return hashSet;
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
