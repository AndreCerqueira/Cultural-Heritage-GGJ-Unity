using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    static public CanvasGroup dialogWindow;
    static public CanvasGroup reviveWindow;

    // Start is called before the first frame update
    void Start()
    {
        dialogWindow = GameObject.Find("Canvas/dialogWindow").GetComponent<CanvasGroup>();
        reviveWindow = GameObject.Find("Canvas/reviveWindow").GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void revive()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void showWindow(int window)
    {
        CanvasGroup windowObject = getWindowByID(window);
        Player.openUI = true;
        StartCoroutine(DoFadeIn(windowObject));
    }


    public void closeWindow(int window)
    {
        CanvasGroup windowObject = getWindowByID(window);
        
        StartCoroutine(DoFadeOut(windowObject));
    }


    CanvasGroup getWindowByID(int id)
    {
        switch (id)
        {
            case 0:
                return dialogWindow;
            case 1:
                return reviveWindow;
            default:
                return null;
        }
    }

    static public IEnumerator DoFadeOut(CanvasGroup canvasG)
    {
        while (canvasG.alpha > 0)
        {
            canvasG.alpha -= Time.deltaTime * 4;
            yield return null;
        }

        Player.openUI = false;
        canvasG.interactable = false;
        canvasG.blocksRaycasts = false;
        StarterAssets.StarterAssetsInputs.SetCursorState(true);
        StarterAssets.ThirdPersonController.LockCameraPosition = false;
    }


    static public IEnumerator DoFadeIn(CanvasGroup canvasG)
    {
        while (canvasG.alpha < 1)
        {
            canvasG.alpha += Time.deltaTime * 4;
            yield return null;
        }

        canvasG.interactable = true;
        canvasG.blocksRaycasts = true;
        StarterAssets.StarterAssetsInputs.SetCursorState(false);
        StarterAssets.ThirdPersonController.LockCameraPosition = true;
    }
}
