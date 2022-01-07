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
    public Dictionary<DirectionEnum, Tile> neighbors = new Dictionary<DirectionEnum, Tile>();

    public bool isSigned;

    private void Start()
    {
        neighbors[DirectionEnum.North] = neighbors[DirectionEnum.NorthEast] = neighbors[DirectionEnum.East] =
            neighbors[DirectionEnum.SouthEast] = neighbors[DirectionEnum.South] = neighbors[DirectionEnum.SouthWest] =
            neighbors[DirectionEnum.West] = neighbors[DirectionEnum.NorthWest] = null;

        directions = new List<Direction>();

        isSigned = false;
    }

    public void SetConnection(DirectionEnum direction, Tile tile)
    {
        neighbors[direction] = tile;
        directions.Add(new Direction() { direction = direction, tile = tile });
    }

    private void OnMouseDown()
    {
        isSigned = true;
        GetComponent<Renderer>().material.color = Color.red;
    }
}
