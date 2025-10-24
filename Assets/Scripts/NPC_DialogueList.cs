using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(fileName = "NPC_DialogueList", menuName = "Scriptable Objects/NPC_DialogueList")]
public class NPC_DialogueList : ScriptableObject
{

    public List<string> lines = new List<string>();

    public string GetLine(int num)
    {
        return lines[num];
    }

    public int GetCount()
    {
        return lines.Count;
    }

}
