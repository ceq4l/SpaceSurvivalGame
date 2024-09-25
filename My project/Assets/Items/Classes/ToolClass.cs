using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tool", menuName ="Items/Tool")]
public class ToolClass : ItemClass
{
    public float MaxDurability;

    public override ToolClass GetTool() { return this; }
}