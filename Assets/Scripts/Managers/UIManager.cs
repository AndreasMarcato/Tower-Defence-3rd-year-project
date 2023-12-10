using System.Collections;
using TMPro;
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
    [SerializeField] private TextMeshProUGUI _currencyUI;
    int _currentCurrency;


    //WIN LOSE CANVASES
    [SerializeField] private GameObject _canvasWinAndDefeat;
    [SerializeField] private GameObject _panelWin;
    [SerializeField] private GameObject _star1;
    [SerializeField] private GameObject _star2;
    [SerializeField] private GameObject _star3;

    [SerializeField] private GameObject _panelLose;
    [SerializeField] private GameObject _panelLoseExitConfirm;


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
        _currentCurrency = 0;
        _currencyUI.text = "Currency: " + _currentCurrency.ToString();
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

    #region CURRENCY REGION
    public void UpdateCurrencyText(int currencyAmount)
    {
        _currentCurrency += currencyAmount;
        _currencyUI.text = "Currency: " + _currentCurrency.ToString();
    }
    #endregion



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


    public IEnumerator StartWinHandle()
    {
        _canvasWinAndDefeat.SetActive(true);
        _panelWin.SetActive(true);
        yield return new WaitForSeconds(1);
        _star1.SetActive(true);
        yield return new WaitForSeconds(1);
        _star2.SetActive(true);
        yield return new WaitForSeconds(1);
        _star3.SetActive(true);

        GameManager.Instance.VictoryHandle();

    }

    public IEnumerator StartDefeatHandle()
    {
        _canvasWinAndDefeat.SetActive(true);
        _panelLose.SetActive(true);
        yield return new WaitForSeconds(1);

    }

    public void DefeatExitPanel(bool yes)
    {
        if(!yes)
            _panelLoseExitConfirm.SetActive(false);
        else
            _panelLoseExitConfirm.SetActive(true);
    }
    public void RecenterCamera()
    {
        Camera.main.transform.position = new Vector3(TouchManager.Instance.GetPlayerAgent().position.x, Camera.main.transform.position.y, TouchManager.Instance.GetPlayerAgent().position.z);
    }

}