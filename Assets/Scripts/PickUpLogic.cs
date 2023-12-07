using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class PickUpLogic : MonoBehaviour
{
    [SerializeField] private float healAmount;
    private PlayerUnitLogic _playerUnitLogic;



    //CURRENCY UPDATE
    [SerializeField] private int currencyAmount;


   
    [HideInInspector] public enum PICKUP_TYPE
    {
        HEAL,
        CURRENCY
    }

    [SerializeField] public PICKUP_TYPE CURRENT_TYPE;


    private void Start()
    {
        _playerUnitLogic = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUnitLogic>();
    }

    public void HealPlayer()
    {
        _playerUnitLogic.GetComponent<Health>().HealPlayer(healAmount);
        Debug.Log(healAmount + "Healed");
        Destroy(gameObject);
    }

   public void AddCurrency()
   {
        UIManager.Instance.UpdateCurrencyText(currencyAmount);
        Destroy(gameObject);

    }
}
