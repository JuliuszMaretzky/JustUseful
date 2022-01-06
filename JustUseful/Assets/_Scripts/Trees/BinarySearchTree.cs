using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinarySearchTree<T> : BinaryTree<T> where T : System.IComparable
{
    public bool Contains(T data)
    {
        var node = Root;

        while (node != null)
        {
            var result = data.CompareTo(node.Data);

            if (result == 0)
            {
                return true;
            }
            else if (result < 0)
            {
                node = node.Left;
            }
            else
            {
                node = node.Right;
            }
        }

        return false;
    }

    public void Add(T data)
    {
        var parent = GetParentForNewNode(data);
        var node = new BinaryTreeNode<T>() { Data = data, Parent = parent };

        if (parent == null)
        {
            Root = node;
        }
        else if (data.CompareTo(parent.Data) < 0)
        {
            parent.Left = node;
        }
        else
        {
            parent.Right = node;
        }

        Count++;
    }

    private BinaryTreeNode<T> GetParentForNewNode(T data)
    {
        var current = Root;
        BinaryTreeNode<T> parent = null;

        while(current!=null)
        {
            parent = current;
            var result = data.CompareTo(current.Data);

            if(result ==0)
            {
                throw new System.ArgumentException($"Node {data} already exists!");
            }
            else if(result<0)
            {
                current = current.Left;
            }
            else
            {
                current = current.Right;
            }
        }

        return parent;
    }

    public void Remove(T data)
    {
        Remove(Root, data);
    }

    private void Remove(BinaryTreeNode<T> node, T data)
    {
        if (node == null)
        {
            throw new System.ArgumentException($"Node {data} doesn't exist!");
        }
        else if (data.CompareTo(node.Data) < 0)
        {
            Remove(node.Left, data);
        }
        else if (data.CompareTo(node.Data) > 0)
        {
            Remove(node.Right, data);
        }
        else
        {
            if(node.Left==null && node.Right==null)
            {
                ReplaceInParent(node, null);
                Count--;
            }
            else if(node.Right==null)
            {
                ReplaceInParent(node, node.Left);
                Count--;
            }
            else if(node.Left==null)
            {
                ReplaceInParent(node, node.Right);
                Count--;
            }
            else
            {
                var successor = FindMinimumInSubtree(node.Right);
                node.Data = successor.Data;
                Remove(successor, successor.Data);
            }
        }
    }

    private void ReplaceInParent(BinaryTreeNode<T> node, BinaryTreeNode<T> newNode)
    {
        if(node.Parent!=null)
        {
            if(node.Parent.Left==node)
            {
                node.Parent.Left = newNode;
            }
            else
            {
                node.Parent.Right = newNode;
            }
        }
        else
        {
            Root = newNode;
        }

        if(newNode!=null)
        {
            newNode.Parent = node.Parent;
        }
    }

    private BinaryTreeNode<T> FindMinimumInSubtree(BinaryTreeNode<T> node)
    {
        while(node.Left!=null)
        {
            node = node.Left;
        }

        return node;
    }
}
