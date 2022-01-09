using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [System.Serializable]
    public class Direction
    {
        public DirectionEnum direction;
        public Tile tile;
    }

    public List<Direction> directions;

    public bool isSigned;

    private void Start()
    {
        directions = new List<Direction>();

        isSigned = false;
    }

    public void SetConnection(DirectionEnum direction, Tile tile)
    {
        directions.Add(new Direction() { direction = direction, tile = tile });
    }

    private void OnMouseDown()
    {
        isSigned = true;
        GetComponent<Renderer>().material.color = Color.red;
    }
}
