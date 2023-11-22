using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI TMP_SelectionStateTextRef;
    public static UIManager Instance { get; private set; }



    //CANVAS
    [SerializeField] private GameObject _canvasPause;
    [SerializeField] private GameObject _panelPause;
    [SerializeField] private GameObject _panelExitConfirm;


    //RELOAD SCENE
    //[SerializeField] private GameObject _panelPauseReloadConfirm;



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }
    private void Start()
    {
        HandlePause(false);
    }

    public void HandlePause(bool isPaused)
    {
        if (isPaused)
        {
            _canvasPause.SetActive(true);
            _panelPause.SetActive(true);
            Time.timeScale = 0;
        }
        else if(!isPaused)
        {
            ClearUI();
            TouchManager.Instance.UIPauseCallback();
            Time.timeScale = 1;

        }

    }
    public void HandelResume()
    {
        ClearUI();
        Time.timeScale = 1;
    }
    public void ClearUI() 
    {
        _canvasPause.SetActive(false);
        _panelPause.SetActive(false);
        _panelExitConfirm.SetActive(false);
        
    }

    #region DYNAMIC UI SELECTION
    public void Update_CurrentSelelectionState(string textToUpdate) 
    {
        TMP_SelectionStateTextRef.text = textToUpdate;
    }
    #endregion


    #region EXIT GAME MENU HANDLERS

    public void ExitGameConfirmPanel()
    {
        _panelExitConfirm.SetActive(true);
    }
    private void ExitGameConfirm(bool yes)
    {
        if (yes)
            Application.Quit();
        else
            _panelExitConfirm.SetActive(false);
    }


    #endregion

    #region EXIT LEVEL MENU HANDLERS


    public void ExitLevelConfirm(bool yes)
    {
        if (yes)
            SceneManager.LoadScene(0);
        else
            _panelExitConfirm.SetActive(false);
    }


    #endregion

    #region RELOAD SCENE MENU HANDLERS
    public void HandleReload(bool confirm)
    {
        int scene = SceneManager.GetActiveScene().buildIndex;

        if (confirm)
            SceneManager.LoadScene(scene);
        else
            return;
    }



    //private void ReloadGameConfirmPanel()
    //{
    //    _panelPauseReloadConfirm.SetActive(true);
    //}
    //private void ReloadGameConfirm(bool yes)
    //{
    //    int scene = SceneManager.GetActiveScene().buildIndex;

    //    if (yes)
    //        SceneManager.LoadScene(scene);
    //    else
    //        _panelPauseReloadConfirm.SetActive(false);
    //}


    #endregion


    
}