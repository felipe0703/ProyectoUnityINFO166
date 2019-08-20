#region Namespaces
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#endregion // Namespaces


public class UIManagerLevels : MonoBehaviour
{
    // ########################################
    // Variables
    // ########################################

    #region Variables

    // Canvas
    public Canvas canvas;
    
    // GUIAnimFREE objects of title text
    public GUIAnimFREE title1;
    public GUIAnimFREE title2;

    // GUIAnimFREE objects of buttons
    public GUIAnimFREE btn_Win;
    public GUIAnimFREE btn_Lose1;
    public GUIAnimFREE btn_Lose2;

    #endregion  // Variables


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
        
        // Disable all scene switch buttons
        GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(canvas, false);
    }

    // Update is called once per frame
    void Update()
    {
    }
    #endregion // MonoBehaviour

    // ########################################
    // MoveIn/MoveOut functions
    // ########################################


    #region MoveIn/MoveOut

    public void ActivateWin()
    {
        /// MoveIn title
        StartCoroutine(MoveInTitleGameObjectsWin());
    }

    public void ActivateLose()
    {
        /// MoveIn title
        StartCoroutine(MoveInTitleGameObjectsLose());
    }
    // MoveIn title 
    IEnumerator MoveInTitleGameObjectsWin()
    {
        yield return new WaitForSeconds(0.4f);

        // MoveIn title
        title1.PlayInAnims(GUIAnimSystemFREE.eGUIMove.Self);

        // MoveIn buttons
        StartCoroutine(MoveInPrimaryButtonsWin());
    }
    IEnumerator MoveInTitleGameObjectsLose()
    {
        yield return new WaitForSeconds(0.4f);

        // MoveIn title
        title2.PlayInAnims(GUIAnimSystemFREE.eGUIMove.Self);

        // MoveIn buttons
        StartCoroutine(MoveInPrimaryButtonsLose());
    }

    // MoveIn m_Dialog
    IEnumerator MoveInPrimaryButtonsWin()
    {
        yield return new WaitForSeconds(0.5f);

        // MoveIn dialogs
        btn_Win.PlayInAnims(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

        // Enable all scene switch buttons
        StartCoroutine(EnableAllDemoButtons());
    } 
    
    // MoveIn m_Dialog
    IEnumerator MoveInPrimaryButtonsLose()
    {
        yield return new WaitForSeconds(0.5f);

        // MoveIn dialogs
        btn_Lose1.PlayInAnims(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
        btn_Lose2.PlayInAnims(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

        // Enable all scene switch buttons
        StartCoroutine(EnableAllDemoButtons());
    }

    // Enable/Disable all scene switch Coroutine
    IEnumerator EnableAllDemoButtons()
    {
        yield return new WaitForSeconds(0.5f);

        // Enable all scene switch buttons
        GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(canvas, true);
    }

    // MoveOut all primary buttons
    public void HideAllGUIs()
    {
        // MoveOut buttons
        btn_Win.PlayOutAnims(GUIAnimSystemFREE.eGUIMove.Self);
        btn_Lose1.PlayOutAnims(GUIAnimSystemFREE.eGUIMove.Self);
        btn_Lose2.PlayOutAnims(GUIAnimSystemFREE.eGUIMove.Self);


        // MoveOut title
        StartCoroutine(HideTitleText());
    }

    // MoveOut m_Title1 and m_Title2
    IEnumerator HideTitleText()
    {
        yield return new WaitForSeconds(1.0f);

        // MoveOut m_Title1 and m_Title2
        title1.PlayOutAnims(GUIAnimSystemFREE.eGUIMove.Self);
        title2.PlayOutAnims(GUIAnimSystemFREE.eGUIMove.Self);
    }

    #endregion  // MoveIn/MoveOut
}
