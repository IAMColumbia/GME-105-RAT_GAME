using UnityEngine;
using UnityEngine.UI;

public class ShowThatBox : MonoBehaviour
{
    [SerializeField]
    public GameObject UI_Textbox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayText(string ourLine)
    {
        GameObject helpy = GameObject.Find("UI_Dialogue(Clone)");


        if (helpy == null)
        {
            helpy = Instantiate(UI_Textbox);
            
        }

        Text helper = helpy.GetComponentInChildren<Text>();

        helper.text = ourLine;

    }

    public void DestroyText()
    {
        GameObject helpy = GameObject.Find("UI_Dialogue(Clone)");

        Destroy(helpy);
    }
}
