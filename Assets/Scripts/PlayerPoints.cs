using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerPoints : MonoBehaviour
{
    public int minPoints = 0;
    public int currentPoints;
    public float currentCombo;
    public float maxCombo = 3;
    public float minCombo = 0.5f;
    private float comboInput;
    public float decayDelay = 2f;
    public Canvas canvasGamePNT;
    public TextMeshProUGUI pointText;
    public TextMeshProUGUI comboText;

    // Start is called before the first frame update
    void Start()
    {
        canvasGamePNT = GetComponent<Canvas>();

        currentPoints = minPoints;
        currentCombo = minCombo;

        pointText.SetText("Points: " + currentPoints.ToString());
        comboText.SetText("Combo: " + currentCombo.ToString());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore(int Ammount)
    {
        currentPoints += (int)(100 * currentCombo);
        pointText.SetText("Points: " + currentPoints.ToString());
    }

    public void AddCombo(float Ammount)
    {
        if (currentCombo < maxCombo)
        {
            currentCombo += Ammount;
            comboInput = Ammount;
            currentCombo = Mathf.Min(currentCombo, maxCombo);

            comboText.SetText("Combo: " + currentCombo.ToString());

            StopAllCoroutines();
            StartCoroutine(ComboDecay());
        }
        else
        {
            return;
        }
    }

    IEnumerator ComboDecay()
    {
        while (currentCombo > minCombo)
        {
            yield return new WaitForSeconds(decayDelay);

            currentCombo -= comboInput;
            currentCombo = Mathf.Clamp(currentCombo, 0, maxCombo);

            comboText.SetText("Combo: " + currentCombo.ToString());
        }
    }
}
