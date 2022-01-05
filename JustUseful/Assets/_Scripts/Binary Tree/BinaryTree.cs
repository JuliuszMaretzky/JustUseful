using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTree<T>
{
    public BinaryTreeNode<T> Root { get; set; }
    public int Count { get; set; }

    public List<BinaryTreeNode<T>> Traverse(TraversalEnum mode)
    {
        var nodes = new List<BinaryTreeNode<T>>();

        switch(mode)
        {
            case TraversalEnum.PREORDER:
                TraversePreOrder(Root, nodes);
                break;
            case TraversalEnum.INORDER:
                TraverseInOrder(Root, nodes);
                break;
            case TraversalEnum.POSTORDER:
                TraversePostOrder(Root, nodes);
                break;
        }

        return nodes;
    }

    public int GetHeight()
    {
        var height = 0;

        foreach(BinaryTreeNode<T> node in Traverse(TraversalEnum.PREORDER))
        {
            height = Mathf.Max(height, node.GetHeight());
        }

        return height;
    }

    private void TraversePreOrder(BinaryTreeNode<T> node, List<BinaryTreeNode<T>> result)
    {
        if (node != null)
        {
            result.Add(node);
            TraversePreOrder(node.Left, result);
            TraversePreOrder(node.Right, result);
        }
    }

    private void TraverseInOrder(BinaryTreeNode<T> node, List<BinaryTreeNode<T>> result)
    {
        if(node != null)
        {
            TraverseInOrder(node.Left, result);
            result.Add(node);
            TraverseInOrder(node.Right, result);
        }
    }

    private void TraversePostOrder (BinaryTreeNode<T> node, List<BinaryTreeNode<T>> result)
    {
        if(node != null)
        {
            TraversePostOrder(node.Left, result);
            TraversePostOrder(node.Right, result);
            result.Add(node);
        }
    }
}
