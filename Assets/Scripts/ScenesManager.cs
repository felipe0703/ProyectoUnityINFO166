using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenesManager : MonoBehaviour
{
    public void CambiarEscena(string escena)
    {
        //SceneManager.LoadScene(escena);

        // Disable all buttons
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);

        // Waits 1.5 secs for Moving Out animation then load next level
        GUIAnimSystemFREE.Instance.LoadLevel(escena, 1.5f);

        GameObject.Find("SceneController").gameObject.SendMessage("HideAllGUIs");
    }
}
