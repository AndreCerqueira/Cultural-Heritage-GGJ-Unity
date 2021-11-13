using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    static public CanvasGroup dialogWindow;

    // Start is called before the first frame update
    void Start()
    {
        dialogWindow = GameObject.Find("Canvas/dialogWindow").GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showWindow(int window)
    {
        
        CanvasGroup windowObject = getWindowByID(window);

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
