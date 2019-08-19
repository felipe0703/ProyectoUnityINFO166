using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int totalEnemigos;
    public int intentosRestantes;
    public Vector3 posicionInicialJugador;
    public List<GameObject> jugadores;
    private int jugadorActual = 0;
    private bool pausado = false;
    public GameObject activable;
    public Transform gridIntentos;
    public GameObject intentoIcon;
    public Transform macrofago;

    #region Pantallas
    public GameObject pantallaVictoria;
    public GameObject pantallaDerrota;
    #endregion

    

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        intentosRestantes = jugadores.Count;
        posicionInicialJugador = jugadores[jugadorActual].transform.position + macrofago.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LlenarVidas(0.5f));
    }

    IEnumerator LlenarVidas(float time)
    {
        int cont = 0;
        do
        {
            yield return new WaitForSeconds(time);
            GameObject icon = intentoIcon;
            icon.GetComponent<Image>().sprite = jugadores[cont].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            Instantiate(intentoIcon, gridIntentos);
            cont++;
        } while (cont < jugadores.Count);
        yield return new WaitForSeconds(time);
        activable.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void IntentoMenos()
    {
        if(!pausado && intentosRestantes <= 0 && totalEnemigos > 0)
        {
            GameOver(false);
        }
        else if(!pausado)
        {
            jugadorActual++;
            Instantiate(jugadores[jugadorActual], posicionInicialJugador, jugadores[jugadorActual].transform.rotation,macrofago);  
        }
    }

    public void Lanzado()
    {
        Destroy(gridIntentos.GetChild(0).gameObject);
        intentosRestantes--;
    }

    public void EnemigoMuerto()
    {
        if(totalEnemigos-1 <= 0)
        {
            GameOver(true);
        }
        else
        {
            totalEnemigos--;
        }
    }

    private void GameOver(bool win)
    {
        pausado = true;
        if(win)
        {
            pantallaVictoria.SetActive(true);
        }
        else
        {
            pantallaDerrota.SetActive(true);
        }
    }
}
