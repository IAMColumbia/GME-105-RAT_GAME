using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(fileName = "NPC_DialogueList", menuName = "Scriptable Objects/NPC_DialogueList")]
public class NPC_DialogueList : ScriptableObject
{

    [System.Serializable]
    public class DialogueEntry
    {
        public string line;
        public MonoBehaviour methodScript;
        public string methodName;
    }

    public List<DialogueEntry> entries = new List<DialogueEntry>();

    public string GetLine(int num)
    {
        return entries[num].line;
    }

    public int GetCount()
    {
        return entries.Count;
    }

}
