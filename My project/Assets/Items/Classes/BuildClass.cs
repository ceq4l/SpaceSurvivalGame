using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/BuildItem", fileName = "BuildItem")]
public class BuildClass : ItemClass
{
    public GameObject BuildPreview;
    public GameObject BuildPrefab;

    public BuildType buildType;

    public enum BuildType
    {
        BasePart,
        Machine
    }

    public override BuildClass GetBuild() { return this; }
}
