#region Namespaces
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#endregion // Namespaces

public class UIManagerChooseLevel : MonoBehaviour
{

    // ########################################
    // Variables
    // ########################################

    #region Variables
    
    // Canvas
    public Canvas canvas;

    // GUIAnimFREE objects of title text
    public GUIAnimFREE title;

    // GUIAnimFREE objects of buttons
    public GUIAnimFREE btn_Level1;
    public GUIAnimFREE btn_Level2;
    public GUIAnimFREE btn_Level3;
    public GUIAnimFREE btn_Level4;
    public GUIAnimFREE btn_Level5;
    public GUIAnimFREE btn_Level6;

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
        /// MoveIn title
        StartCoroutine(MoveInTitleGameObjects());
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

    // MoveIn title 
    IEnumerator MoveInTitleGameObjects()
    {
        yield return new WaitForSeconds(1.0f);

        // MoveIn title
        title.PlayInAnims(GUIAnimSystemFREE.eGUIMove.Self);

        // MoveIn buttons
        StartCoroutine(MoveInPrimaryButtons());
    }

    // MoveIn m_Dialog
    IEnumerator MoveInPrimaryButtons()
    {
        yield return new WaitForSeconds(1.0f);

        // MoveIn dialogs
        btn_Level1.PlayInAnims(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
        btn_Level2.PlayInAnims(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
        btn_Level3.PlayInAnims(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
        btn_Level4.PlayInAnims(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
        btn_Level5.PlayInAnims(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
        btn_Level6.PlayInAnims(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

        // Enable all scene switch buttons
        StartCoroutine(EnableAllDemoButtons());
    }

    // Enable/Disable all scene switch Coroutine
    IEnumerator EnableAllDemoButtons()
    {
        yield return new WaitForSeconds(0.8f);

        // Enable all scene switch buttons
        GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(canvas, true);
    }

    // MoveOut all primary buttons
    public void HideAllGUIs()
    {
        // MoveOut buttons
        btn_Level1.PlayOutAnims(GUIAnimSystemFREE.eGUIMove.Self);
        btn_Level2.PlayOutAnims(GUIAnimSystemFREE.eGUIMove.Self);
        btn_Level3.PlayOutAnims(GUIAnimSystemFREE.eGUIMove.Self);
        btn_Level4.PlayOutAnims(GUIAnimSystemFREE.eGUIMove.Self);
        btn_Level5.PlayOutAnims(GUIAnimSystemFREE.eGUIMove.Self);
        btn_Level6.PlayOutAnims(GUIAnimSystemFREE.eGUIMove.Self);
        

        // MoveOut title
        StartCoroutine(HideTitleText());
    }

    // MoveOut m_Title1 and m_Title2
    IEnumerator HideTitleText()
    {
        yield return new WaitForSeconds(1.0f);

        // MoveOut m_Title1 and m_Title2
        title.PlayOutAnims(GUIAnimSystemFREE.eGUIMove.Self);
    }

    #endregion  // MoveIn/MoveOut


}
