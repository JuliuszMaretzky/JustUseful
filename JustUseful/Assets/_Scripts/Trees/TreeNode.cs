using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNode<T>
{
    public T Data { get; set; }
    public List<TreeNode<T>> Children { get; set; }
}
