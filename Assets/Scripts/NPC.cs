using UnityEngine;

public class NPC : MonoBehaviour
{

    public int lineNum = 0;

    public NPC_DialogueList myDialogue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpeakUp()
    {
        string thisLine = myDialogue.GetLine(lineNum);
        ShowThatBox help = GameObject.Find("Main Camera").GetComponent<ShowThatBox>();

        help.DisplayText(thisLine);
    }
}
