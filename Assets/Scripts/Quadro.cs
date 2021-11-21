using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;

public class Quadro : MonoBehaviour
{
    StarterAssetsInputs input;
    bool contact;

    // Start is called before the first frame update
    void Start()
    {
        input = FindObjectOfType<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (input.interact && contact)
        {
            SceneManager.LoadScene("Egypt");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(GameManager.DoFadeIn(GameManager.tipWindow));
            contact = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(GameManager.DoFadeOut(GameManager.tipWindow));
            contact = false;
        }
    }
}
