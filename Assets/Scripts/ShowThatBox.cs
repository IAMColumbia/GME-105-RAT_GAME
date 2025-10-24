using System;
using UnityEngine;
using UnityEngine.UI;

public class ShowThatBox : MonoBehaviour
{
    [SerializeField]
    public GameObject UI_Textbox;

    public GameObject My_Textbox = null;

    public Text My_Text = null;

    public float charDelay = 0f;

    public float charDelayMax = 0.5f;

    public bool displayLoop = false;

    public string textToDisplay;

    public int butHowMuch = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (displayLoop)
        {
            if (charDelay <= 0)
            {
                char[] trueText = textToDisplay.ToCharArray();

                Array.Resize(ref trueText, butHowMuch);

                My_Text.text = "";


                foreach (char let in trueText)
                {

                    My_Text.text += let.ToString();
                }

                butHowMuch++;

                Debug.Log(butHowMuch + "_" + textToDisplay.Length);

                if (butHowMuch > textToDisplay.Length)
                {
                    displayLoop = false;
                    butHowMuch = 1;
                }
                else
                {
                    charDelay = charDelayMax;
                }
            }
            else
            {
                charDelay -= Time.deltaTime;
            }
        }
    }

    public void DisplayText(string ourLine)
    {

        if (My_Textbox == null)
        {
            My_Textbox = Instantiate(UI_Textbox);



        }

        if (displayLoop)
        {
            butHowMuch = textToDisplay.Length;

        }
        else
        {
            My_Text = My_Textbox.GetComponentInChildren<Text>();
            textToDisplay = ourLine;

            displayLoop = true;
        }

    }

    public void DestroyText()
    {
        GameObject helpy = GameObject.Find("UI_Dialogue(Clone)");

        Destroy(helpy);
    }

}