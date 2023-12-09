using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{

    //SELECTION HANDLER
    public enum SelectedObject
    {
        PLAYER_UNIT,
        ENEMY_UNIT,
        BUILDING

    }
    private SelectedObject SELECTION_CURRENT;
    public SelectedObject SELECTION_NEXT;
    private Camera _camera;

    //Input Events Reference
    private Vector3 storedVector;
    //event raycast layermask
    private LayerMask GROUND = 9;

    //private void OnEnable()
    //{
    //    inputManager.OnTouchPosition += InputManager_OnTouchPosition;

    //}
    //private void OnDisable()
    //{
    //    inputManager.OnTouchPosition -= InputManager_OnTouchPosition;

    //}

    public static GameManager Instance { get; private set; }

    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        _camera = Camera.main;

        
    }
    private void Start()
    {
     
    }

    private void InputManager_OnTouchPosition(Vector3 position, GameObject raycastHitGameObject)
    {
        Debug.Log(raycastHitGameObject.name);
    }

    

    private void Update()
    {

        switch (SELECTION_CURRENT)
        {
            case SelectedObject.PLAYER_UNIT:
                //UI UPDATE
                return;
            case SelectedObject.ENEMY_UNIT:
                //UI UPDATE
                return;
            case SelectedObject.BUILDING:
                //UI UPDATE
                return;
        }

    }
    private void LateUpdate()
    {
        if (SELECTION_CURRENT != SELECTION_NEXT)
        {
            switch (SELECTION_NEXT)
            {
                case SelectedObject.PLAYER_UNIT:
                    if (SELECTION_CURRENT == SelectedObject.PLAYER_UNIT)
                        StateToPlayer_Unit();
                    else if (SELECTION_CURRENT == SelectedObject.ENEMY_UNIT)
                        StatetoEnemy_Unit();
                    else if (SELECTION_CURRENT == SelectedObject.BUILDING)
                        StateToBuilding_Unit();
                    return;
                case SelectedObject.ENEMY_UNIT:
                    if (SELECTION_CURRENT == SelectedObject.PLAYER_UNIT)
                        StateToPlayer_Unit();
                    else if (SELECTION_CURRENT == SelectedObject.ENEMY_UNIT)
                        StatetoEnemy_Unit();
                    else if (SELECTION_CURRENT == SelectedObject.BUILDING)
                        StateToBuilding_Unit();
                    return;
                case SelectedObject.BUILDING:
                    if (SELECTION_CURRENT == SelectedObject.PLAYER_UNIT)
                        StateToPlayer_Unit();
                    else if (SELECTION_CURRENT == SelectedObject.ENEMY_UNIT)
                        StatetoEnemy_Unit();
                    else if (SELECTION_CURRENT == SelectedObject.BUILDING)
                        StateToBuilding_Unit();
                    return;
            }

        }
    }



    #region State Transitions
    private void StateToPlayer_Unit()
    {

    }
    private void StatetoEnemy_Unit()
    {

    }
    private void StateToBuilding_Unit()
    {

    }
    #endregion


    #region State Machines
    private void SM_PLAYER_UNIT()
    {

    }
    private void SM_ENEMY_UNIT()
    {

    }
    private void SM_BUILDING_UNIT()
    {

    }
    #endregion



    public IEnumerator VictoryHandle()
    {
        UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();
        int nextSceneID = currentScene.buildIndex - 1;

        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneID);
        yield return new WaitForSeconds(1);
    }


    public IEnumerator LoseHandle()
    {
        UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();
        int currentSceneID = currentScene.buildIndex;

        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(currentSceneID);

        yield return new WaitForSeconds(1);
    }

}
