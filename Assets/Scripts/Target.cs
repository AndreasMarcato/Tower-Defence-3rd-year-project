using UnityEngine;
using TMPro;

public class Target : MonoBehaviour, IE_Target
{
    public enum TypeOfTarget
    {
        None,
        PLAYER,
        ENEMY,
        BUILDING
    }

    public TypeOfTarget currentTarget;
    private UIManager _UIManager;
    private void Start()
    {
        _UIManager = UIManager.Instance;
    }

    public void TargetInteract() 
    {
        Debug.Log(gameObject.name.ToString());
        _UIManager.Update_CurrentSelelectionState(transform.name.ToString());

    }

}
