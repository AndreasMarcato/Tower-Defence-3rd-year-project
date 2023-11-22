using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI TMP_SelectionStateTextRef;
    public static UIManager Instance { get; private set; }

    //CANVAS
    private GameObject _canvas;
    private GameObject _panelPause;

    //EXIT GAME
    private GameObject _panelPauseExitConfirm;


    //RELOAD SCENE
    private GameObject _panelPauseReloadConfirm;

    bool isPaused = false;


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

        //GET ALL CANVAS AND PANEL
        //CANVAS
        _canvas = GameObject.FindGameObjectWithTag("_canvas").gameObject;
        //PAUSE
        _panelPause = GameObject.FindGameObjectWithTag("_panelPause").gameObject;
        //EXIT PANEL AND CONFIRM PANEL
        _panelPauseExitConfirm = GameObject.FindGameObjectWithTag("_panelPauseExitConfirm").gameObject;
        //RELOAD PANEL AND CONFIRM PANEL
        _panelPauseReloadConfirm = GameObject.FindGameObjectWithTag("_panelPauseReloadConfirm").gameObject;
        
        //_panelPause = GameObject.FindGameObjectWithTag().gameObject;
        //_panelPause = GameObject.FindGameObjectWithTag().gameObject;
        //_panelPause = GameObject.FindGameObjectWithTag().gameObject;
        //_panelPause = GameObject.FindGameObjectWithTag().gameObject;
        //_panelPause = GameObject.FindGameObjectWithTag().gameObject;
    }
    private void OnEnable()
    {
        _canvas.SetActive(false);
        _panelPause.SetActive(false);
        _panelPauseReloadConfirm.SetActive(false);
        _panelPauseExitConfirm.SetActive(false);
    }
    private void OnDisable()
    {
        
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
        }

    }










    public void Update_CurrentSelelectionState(string textToUpdate) 
    {
        TMP_SelectionStateTextRef.text = textToUpdate;
    }



    #region EXIT GAME MENU HANDLERS

    public void ExitGameConfirmPanel()
    {
        _panelPauseExitConfirm.SetActive(true);
    }
    private void ExitGameConfirm(bool yes)
    {
        if (yes)
            Application.Quit();
        else
            _panelPauseExitConfirm.SetActive(false);
    }


    #endregion

    #region RELOAD SCENE MENU HANDLERS

    private void ReloadGameConfirmPanel()
    {
        _panelPauseReloadConfirm.SetActive(true);
    }
    private void ReloadGameConfirm(bool yes)
    {
        int scene = SceneManager.GetActiveScene().buildIndex;

        if (yes)
            SceneManager.LoadScene(scene);
        else
            _panelPauseReloadConfirm.SetActive(false);
    }


    #endregion


}