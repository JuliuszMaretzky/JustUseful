using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph<T>
{
    private bool _isDirected = false;
    private bool _isWeighted = false;
    public List<Node<T>> Nodes { get; set; } = new List<Node<T>>();

    public Graph(bool isDirected, bool isWeighted)
    {
        _isDirected = isDirected;
        _isWeighted = isWeighted;
    }

    public Edge<T> this[int from, int to]
    {
        get
        {
            var nodeFrom = Nodes[from];
            var nodeTo = Nodes[to];
            var i = nodeFrom.Neighbors.IndexOf(nodeTo);

            if(i>=0)
            {
                var edge = new Edge<T>()
                {
                    From = nodeFrom,
                    To = nodeTo,
                    Weight = i < nodeFrom.Weights.Count ? nodeFrom.Weights[i] : 0
                };

                return edge;
            }
            return null;
        }
    }

    public Node<T> AddNode(T value)
    {
        var node = new Node<T>() { Data = value };
        Nodes.Add(node);
        UpdateIndices();
        return node;
    }

    public void RemoveNode(Node<T> nodeToRemove)
    {

        Nodes.Remove(nodeToRemove);
        UpdateIndices();

        foreach (var node in Nodes)
        {
            RemoveEdge(node, nodeToRemove);
        }
    }

    public void AddEdge(Node<T> from, Node<T> to, int weight = 0)
    {
        from.Neighbors.Add(to);

        if (_isWeighted)
        {
            from.Weights.Add(weight);
        }
        if(!_isDirected)
        {
            to.Neighbors.Add(from);
            if (_isWeighted)
            {
                to.Weights.Add(weight);
            }
        }
    }

    public void RemoveEdge(Node<T> from, Node<T> to)
    {
        var index = from.Neighbors.FindIndex(n => n == to);

        if (index >= 0)
        {
            from.Neighbors.RemoveAt(index);
            if (_isWeighted)
            {
                from.Weights.RemoveAt(index);
            }
        }
    }

    public List<Edge<T>> GetEdges()
    {
        var edges = new List<Edge<T>>();

        foreach (var from in Nodes)
        {
            for (int i = 0; i < from.Neighbors.Count; i++)
            {
                var edge = new Edge<T>()
                {
                    From = from,
                    To = from.Neighbors[i],
                    Weight = i < from.Weights.Count ? from.Weights[i] : 0
                };

                edges.Add(edge);
            }
        }
        return edges;
    }

    private void UpdateIndices()
    {
        var i = 0;
        Nodes.ForEach(n => n.Index = i++);
    }
}
