using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AStar
{
    //when we want to see colors
    public static IEnumerator Search(Tile start, Tile target, bool isColoring=false)
    //when we want to use it quickly
    //public static List<Tile> Search(Tile start, Tile target, bool isColoring = false)
    {
        var open = new List<Tile>() { start };
        var closed = new List<Tile>();
        var current = start;
        ChangeTileColor(isColoring, current, Color.blue);

        var distance = DistanceToTarget(start, target);
        var costs = new Dictionary<Tile, Vector3>() { { start, new Vector3(0f, distance, distance) } };
        var cameFrom = new Dictionary<Tile, Tile>();

        while (current != target)
        {
            foreach (var n in current.directions)
            {
                //when we want to see colors
                yield return new WaitForSeconds(0.1f);
                var newTile = n.tile;

                if (newTile != null && !closed.Contains(newTile))
                {
                    var newG = costs[current].x + DistanceToTarget(current, newTile);
                    var newH = DistanceToTarget(newTile, target);

                    if (!open.Contains(newTile)) 
                    {
                        ChangeTileColor(isColoring, newTile, Color.yellow);

                        open.Add(newTile);
                        cameFrom[newTile] = current;
                        
                        costs[newTile] = new Vector3(newG, newH, newG + newH);
                    }
                    else
                    {
                        if(costs[newTile].z>newG+newH)
                        {
                            cameFrom[newTile] = current;
                            costs[newTile] = new Vector3(newG, newH, newG + newH);
                        }
                    }
                    
                }

            }
            //when we want to see colors
            yield return new WaitForSeconds(0.1f);

            closed.Add(current);
            open.Remove(current);
            current = SelectLowestFFromOpen(open, costs);

            ChangeTileColor(isColoring, current, Color.blue);
        }

        //when we want to see colors
        var path = new List<Tile>() { target };
        Tile curr = target;
        ChangeTileColor(isColoring, curr, Color.green);

        while (cameFrom.ContainsKey(curr))
        {
            yield return new WaitForSeconds(0.05f);
            curr = cameFrom[curr];
            ChangeTileColor(isColoring, curr, Color.green);
            path.Add(curr);
        }
        //end of this colorful thing

        //when we want to use it quickly
        //return Path(cameFrom, target, isColoring);
    }

    private static float DistanceToTarget(Tile first, Tile last)
    {
        return Vector3.Distance(first.transform.position, last.transform.position);
    }

    private static Tile SelectLowestFFromOpen(List<Tile> openList, Dictionary<Tile, Vector3> costsDict)
    {
        if(openList.Count<1 || costsDict.Count<1)
        {
            return null;
        }

        var lowestF = float.MaxValue;
        Tile lowestFTile = null;

        foreach(var tile in openList)
        {
            if(costsDict[tile].z<lowestF)
            {
                lowestF = costsDict[tile].z;
                lowestFTile = tile;
            }
        }

        return lowestFTile;
    }

    private static List<Tile> Path(Dictionary<Tile, Tile> cameFromDict, Tile target, bool isColoring)
    {
        var path = new List<Tile>() { target };
        Tile current = target;
        ChangeTileColor(isColoring, current, Color.green);

        while (cameFromDict.ContainsKey(current))
        {
            current = cameFromDict[current];
            ChangeTileColor(isColoring, current, Color.green);
            path.Add(current);
        }

        path.Reverse();
        return path;
    }

    private static void ChangeTileColor(bool isColoring, Tile tile, Color color)
    {
        if (isColoring)
        {
            tile.GetComponent<Renderer>().material.color = color;
        }
    }
}
