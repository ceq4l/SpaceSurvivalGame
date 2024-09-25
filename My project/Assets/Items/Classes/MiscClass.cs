using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/MiscItem", fileName = "MiscItem")]
public class MiscClass : ItemClass
{
    public override MiscClass GetMisc() { return this; }
}