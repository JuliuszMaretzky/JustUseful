using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node<T>
{
    public int Index { get; set; }
    public T Data { get; set; }
    public List<Node<T>> Neighbors { get; set; } = new List<Node<T>>();
    public List<int> Weights { get; set; } = new List<int>();

    public override string ToString()
    {
        return $"Node with index {Index}: {Data}, neighbors count: {Neighbors.Count}";
    }
}
