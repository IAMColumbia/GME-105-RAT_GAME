using System.IO;
using UnityEditorInternal;
using UnityEngine;

public class NPC : MonoBehaviour
{

    private int _lineNum = 0;

    public int lineNum
    {
        get { return _lineNum; }
        set { _lineNum = value; }
    }

    public NPC_DialogueList myDialogue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string SpeakUp()
    {
        string rv = "";

        if ( lineNum < myDialogue.GetCount())
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
