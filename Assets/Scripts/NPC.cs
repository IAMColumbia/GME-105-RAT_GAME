using System.IO;
using UnityEditorInternal;
using UnityEngine;

public class NPC : MonoBehaviour
{

    public NPC_DialogueList myDialogue;

    private int _lineNum = 0;

    public int lineNum
    {
        get { return _lineNum; }
        set { _lineNum = value; }
    }


    public string SpeakUp()
    {
        string rv = "";

        if (lineNum < myDialogue.GetCount())
        {
            rv = myDialogue.GetLine(lineNum);
            Debug.Log(rv + "_" + lineNum + "_" + myDialogue.GetCount());
        }

        return rv;
    }

    public void NextLine()
    {
        lineNum++;
    }

    public void LineReset()
    {
        lineNum = 0;
    }
}
