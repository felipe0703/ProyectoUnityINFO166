
#region Namespaces
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#endregion // Namespaces


public class UIManagerWelcome : MonoBehaviour
{
    // ########################################
    // MonoBehaviour Functions
    // ########################################

    #region MonoBehaviour

    private void Awake()
    {
        if (enabled)
        {
            // Set GUIAnimSystemFREE.Instance.m_AutoAnimation to false in Awake() will let you control all GUI Animator elements in the scene via scripts.
            GUIAnimSystemFREE.Instance.m_AutoAnimation = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowText(1.0f));
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion // MonoBehaviour

    // ########################################
    // Delay Functions
    // ########################################

    #region Delay
    IEnumerator ShowText(float Delay)
    {
        // Find game object names "Panel (Middle Center)"
        GameObject go = GameObject.Find("Panel (Middle Center)");

        // Play move-in animations
        if (go)
            GUIAnimSystemFREE.Instance.PlayInAnims(go.transform, true);

        // wait for 3 seconds
        yield return new WaitForSeconds(3);

        // Play move-out animations
        if (go)
            GUIAnimSystemFREE.Instance.PlayOutAnims(go.transform, true);

        // Wait for a while
        yield return new WaitForSeconds(Delay / 2);

        // Load next demo scene
        SceneManager.LoadScene("MainMenu");
    }
    #endregion // Delay
}
