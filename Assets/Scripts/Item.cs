using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour
{
    public bool pickedUp = false;

    public void PickedUp()
    {
        this.gameObject.SetActive(false);
        pickedUp = true;
    }
}
