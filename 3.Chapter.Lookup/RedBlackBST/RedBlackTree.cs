using System;
using Newtonsoft.Json;

namespace RedBlackBST
{
    public class RedBlackTree<TKey, TValue> where TKey : IComparable where TValue : IComparable
    {
        private TreeNode<TKey, TValue> _root;

        public TreeNode<TKey, TValue> Get(TKey key)
        {
            var tmpNode = _root;
            return Get(tmpNode, key);
        }

        private TreeNode<TKey, TValue> Get(TreeNode<TKey, TValue> node, TKey key)
        {
            if (node == null) return null;
            switch (key.CompareTo(node.Key))
            {
                case 1:
                    return Get(node.Right, key);
                case 0:
                    return node;
                case -1:
                    return Get(node.Left, key);
            }
            return null;
        }

        public void Put(TKey key, TValue value)
        {
            var root = _root;
            _root = Put(root, key, value);
            _root.Color = NodeColor.BLACK;
        }

        private TreeNode<TKey, TValue> Put(TreeNode<TKey, TValue> hNode, TKey key, TValue value)
        {

            if (hNode == null) return new TreeNode<TKey, TValue>(key, value, NodeColor.RED);
            switch (key.CompareTo(hNode.Key))
            {
                case 1:
                    hNode.Right = Put(hNode.Right, key, value);
                    break;
                case 0:
                    hNode.Value = value;
                    break;
                case -1:
                    hNode.Left = Put(hNode.Left, key, value);
                    break;
            }
            // 注意顺序：都是以h节点来操作
            if (!IsRed(hNode.Left) && IsRed(hNode.Right)) hNode = RotateLeft(hNode);
            if (IsRed(hNode.Left) && IsRed(hNode.Left?.Left)) hNode = RotateRight(hNode);
            if (IsRed(hNode.Left) && IsRed(hNode.Right)) FilpColors(hNode);
            hNode.N = 1 + Size(hNode.Left) + Size(hNode.Right);
            return hNode;
        }

        public void DeleteMin()
        {
            if (!IsRed(_root.Left) && !IsRed(_root.Right))
            {
                _root.Color = NodeColor.RED;
            }
            _root = DeleteMin(_root);
            _root.Color = NodeColor.BLACK;
        }

        private TreeNode<TKey, TValue> DeleteMin(TreeNode<TKey, TValue> hNode)
        {
            if (hNode.Left == null) return null;
            if (!IsRed(hNode.Left) && !IsRed(hNode.Left.Left))
            {
                hNode = MoveRedLeft(hNode);
            }
            hNode.Left = DeleteMin(hNode.Left);

            if (IsRed(hNode.Right))
            {
                hNode = RotateLeft(hNode);
            }

            if (!IsRed(hNode.Left) && IsRed(hNode.Right)) hNode = RotateLeft(hNode);
            if (IsRed(hNode.Left) && IsRed(hNode.Left?.Left)) hNode = RotateRight(hNode);
            if (IsRed(hNode.Left) && IsRed(hNode.Right)) FilpColors(hNode);

            hNode.N = Size(hNode.Left) + Size(hNode.Right) + 1;
            return hNode;
        }

        private TreeNode<TKey, TValue> MoveRedLeft(TreeNode<TKey, TValue> h)
        {
            // h.Color = NodeColor.BLACK;
            // h.Left.Color = NodeColor.RED;
            ColorsFlip(h);
            if (IsRed(h.Right.Left))
            {
                h.Right = RotateRight(h.Right);
                h = RotateLeft(h);
                //FilpColors(h);
            }
            // else{
            //     h.Right.Color = NodeColor.RED;
            // }
            return h;
        }

        private int Size(TreeNode<TKey, TValue> node)
        {
            if (node == null) return 0;
            else return node.N;
        }

        // 左旋操作
        private TreeNode<TKey, TValue> RotateLeft(TreeNode<TKey, TValue> hNode)
        {
            var rightNode = hNode.Right;
            hNode.Right = rightNode.Left;
            rightNode.Left = hNode;
            // 注意颜色变换
            rightNode.Color = hNode.Color;
            hNode.Color = NodeColor.RED;
            rightNode.N = hNode.N;
            hNode.N = 1 + Size(hNode.Left) + Size(hNode.Right);
            return rightNode;
        }

        //右旋操作
        private TreeNode<TKey, TValue> RotateRight(TreeNode<TKey, TValue> hNode)
        {
            var leftNode = hNode.Left;
            hNode.Left = leftNode.Right;
            leftNode.Right = hNode;
            // 注意颜色变换
            leftNode.Color = hNode.Color;
            hNode.Color = NodeColor.RED;
            leftNode.N = hNode.N;
            hNode.N = Size(hNode.Left) + Size(hNode.Right) + 1;
            return leftNode;
        }

        private void FilpColors(TreeNode<TKey, TValue> node)
        {
            node.Color = NodeColor.RED;
            node.Left.Color = NodeColor.BLACK;
            node.Right.Color = NodeColor.BLACK;
        }

        private void ColorsFlip(TreeNode<TKey, TValue> node)
        {
            node.Color = NodeColor.BLACK;
            node.Left.Color = NodeColor.RED;
            node.Right.Color = NodeColor.RED;
        }

        private bool IsRed(TreeNode<TKey, TValue> node)
        {
            if (node == null) return false;
            return node.Color == NodeColor.RED;
        }

        public string Visualize()
        {
            TreeVisualizer vis = new TreeVisualizer();
            Visualize(_root, vis);
            return JsonConvert.SerializeObject(vis);
        }
        private void Visualize(TreeNode<TKey, TValue> node, TreeVisualizer vis)
        {
            if (node == null) return;
            vis.nodes.Add(new TreeVisualizer.Node() { id = node.Key.ToString(), label = node.Key.ToString() });
            if (node.Left != null)
            {
                vis.edges.Add(new TreeVisualizer.Edge()
                {
                    from = node.Key.ToString(),
                    to = node.Left.Key.ToString()
                    ,
                    label = node.Left.Color == NodeColor.RED ? "RED" : "BLACK"
                    //,label = "left"
                });
                Visualize(node.Left, vis);
            }
            else
            {
                vis.nodes.Add(new TreeVisualizer.Node()
                {
                    id = $"{node.Key.ToString()}-left",
                    label = $"null"
                });
                vis.edges.Add(new TreeVisualizer.Edge()
                {
                    from = node.Key.ToString(),
                    to = $"{node.Key.ToString()}-left"
                    //,label = "left"
                });
            }
            if (node.Right != null)
            {
                vis.edges.Add(new TreeVisualizer.Edge()
                {
                    from = node.Key.ToString(),
                    to = node.Right.Key.ToString()
                    ,
                    label = node.Right.Color == NodeColor.RED ? "RED" : "BLACK"
                    //,label = "right"
                });
                Visualize(node.Right, vis);
            }
            else
            {
                vis.nodes.Add(new TreeVisualizer.Node() { id = $"{node.Key.ToString()}-right", label = $"null" });
                vis.edges.Add(new TreeVisualizer.Edge()
                {
                    from = node.Key.ToString(),
                    to = $"{node.Key.ToString()}-right"
                    //,label = "right"
                });
            }



        }

    }
}
