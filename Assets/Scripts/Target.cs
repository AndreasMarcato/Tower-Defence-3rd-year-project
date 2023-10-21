using UnityEngine;
using TMPro;

public class Target : MonoBehaviour, IE_Target
{
    public delegate void TouchSelectedGameObject(GameObject go);
    public event TouchSelectedGameObject OnTouchSelectedGameObject;


    public enum TypeOfTarget
    {
        None,
        PLAYER,
        ENEMY,
        BUILDING
    }

    public TypeOfTarget currentTarget;
    private UIManager _UIManager;
    private GameManager _GameManager;
    private void Start()
    {
        _UIManager = UIManager.Instance;
        _GameManager = GameManager.Instance;
    }

    public void TargetInteract() 
    {
        Debug.Log(gameObject.name.ToString());
        _UIManager.Update_CurrentSelelectionState(transform.name.ToString());
        switch (currentTarget)
        {
            case TypeOfTarget.PLAYER:
                _GameManager.SELECTION_NEXT = GameManager.SelectedObject.PLAYER_UNIT;
                break;
            case TypeOfTarget.ENEMY:
                _GameManager.SELECTION_NEXT = GameManager.SelectedObject.ENEMY_UNIT;
                break;
            case TypeOfTarget.BUILDING:
                _GameManager.SELECTION_NEXT = GameManager.SelectedObject.BUILDING;
                break;

        }
    }

}
