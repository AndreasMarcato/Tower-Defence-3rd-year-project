using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Credits : MonoBehaviour
{
    bool isHidden = true;
    private void Awake()
    {
        isHidden = true;
    }
    public void HidePanel()
    {
        isHidden = !isHidden;
        if (isHidden)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }

}
