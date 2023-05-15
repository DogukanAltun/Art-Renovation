using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level")]
public class Level : ScriptableObject
{
    public int level;

    public Material art;

    public int[] WallLength;

    public int CounterLimit;

    public int[] nodes;
}
