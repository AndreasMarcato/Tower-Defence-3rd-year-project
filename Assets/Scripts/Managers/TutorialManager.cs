using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour
{

    public GameObject[] popUps;
    private int popUpIndex;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }


    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
                popUps[popUpIndex].SetActive(true);
            else
                popUps[popUpIndex].SetActive(false);
        }


        switch(popUpIndex)
        {
            case 0:
                {
                    Tutorial_0_Start();
                }
                break;
            case 1:
                {
                    Tutorial_1_Start();
                }
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
        }
    }

    private void Tutorial_0_Start()
    {
        if (_playerInput.actions["TouchPress"].triggered)
            return;
    }

    private void Tutorial_1_Start()
    {

    }
}
