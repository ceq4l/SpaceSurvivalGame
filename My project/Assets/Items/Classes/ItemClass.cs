using UnityEngine;

public class ItemClass : ScriptableObject
{
    public int ItemId;
    public Sprite ItemIcon;
    public float MaxStackSize;

    public virtual MiscClass GetMisc() { return null; }
    public virtual ToolClass GetTool() { return null; }
    public virtual BuildClass GetBuild() { return null; }
}
