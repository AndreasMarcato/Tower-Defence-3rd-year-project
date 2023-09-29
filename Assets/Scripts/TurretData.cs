using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretData : MonoBehaviour
{
    [SerializeField] private SO_TurretData data;
    private float _attackRange;
    private float _attackPower;
    private float _attackSpeed;
    public float AttackRange { get => _attackRange; set => _attackRange = value; }
    public float AttackPower { get => _attackPower; set => _attackPower = value; }
    public float AttackSpeed { get => _attackSpeed; set => _attackSpeed = value; }

    private void Awake()
    {
        AttackRange = data.attackRange; AttackPower = data.attackPower; AttackSpeed = data.attackSpeed;
    }
}
