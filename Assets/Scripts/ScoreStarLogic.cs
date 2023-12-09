using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreStarLogic : MonoBehaviour
{
    [HideInInspector]
    public enum StarType
    {
        STAR_1,
        STAR_2,
        STAR_3
    }
    
    public StarType starType;

    Vector3 scaleVector = Vector3.zero;
    float elapsedTime = 0;
   


    private void Update()
    {
        bool isActive = gameObject.activeInHierarchy;

        if (isActive)
            switch (starType)
            {
                case StarType.STAR_1:
                    if (transform.localScale.magnitude <= 1)
                        transform.localScale += (Vector3.one / 0.1f) * Time.deltaTime;
                    break;
                case StarType.STAR_2:
                    if (transform.localScale.magnitude <= 1)
                        transform.localScale += (Vector3.one / 0.1f) * Time.deltaTime;
                    break;
                case StarType.STAR_3:
                    if (transform.localScale.magnitude <= 1)
                        transform.localScale += (Vector3.one / 0.1f) * Time.deltaTime;
                    break;
            }
        else return;

        if (transform.localScale.magnitude >= 1)
        {
            if (starType == StarType.STAR_3)
                SceneManager.LoadScene(0);
            Destroy(this);
        }
    }

}
