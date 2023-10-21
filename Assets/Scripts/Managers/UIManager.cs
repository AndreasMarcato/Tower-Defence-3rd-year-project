using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI TMP_SelectionStateTextRef;
    public static UIManager Instance { get; private set; }

    

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


    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        
    }

    public void Update_CurrentSelelectionState(string textToUpdate) 
    {
        TMP_SelectionStateTextRef.text = textToUpdate;
    }
}