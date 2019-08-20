using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    #region Audio
    AudioSource audioSource;

    public AudioClip hit;
    public AudioClip lose;
    public AudioClip victory;
    public AudioClip restart;
    public AudioClip back;
    public AudioClip continuar;
    public AudioClip plop;

    #endregion // audio

    #region Pantallas
    public GameObject pantallaVictoria;
    public GameObject pantallaDerrota;
    #endregion

    [Tooltip("Tiempo inicial en segundo")] public int tiempoInicial;
    [Tooltip("Escala del tiempo del Reloj")] [Range(-10f, 10f)] public float escalaDeTiempo = 1;
    private float tiempoDelFrameConTimeScale = 0f;
    private float tiempoAMostrarEnSegundos = 0f;
    private float escalaDeTiempoAlPausar, escalaDeTiempoInicial;
    public TextMeshProUGUI textTimer;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        audioSource = GetComponent<AudioSource>();
        intentosRestantes = jugadores.Count;
        posicionInicialJugador = jugadores[jugadorActual].transform.position + macrofago.position;      
    }

    // Start is called before the first frame update
    void Start()
    {
        //  TIMER
        escalaDeTiempoInicial = escalaDeTiempo;         //  Establecer la escala de tiempo original      
        tiempoAMostrarEnSegundos = tiempoInicial;       //  Inicializamos la variables que acumular
        ActualizarReloj(tiempoInicial);
        pausado = true;
        StartCoroutine(LlenarVidas(0.5f));
    }

    IEnumerator LlenarVidas(float time)
    {
        int cont = 0;
        do
        {
            yield return new WaitForSeconds(time);
            PlayClip(plop);
            GameObject icon = intentoIcon;
            icon.GetComponent<Image>().sprite = jugadores[cont].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            Instantiate(intentoIcon, gridIntentos);
            cont++;
        } while (cont < jugadores.Count);
        yield return new WaitForSeconds(time);
        activable.SetActive(true);
        pausado = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!pausado)
        {
            //  La siguiente variable representa el tiempo de cada frame considerando la escala de tiempo
            tiempoDelFrameConTimeScale = Time.deltaTime * escalaDeTiempo;

            //  La siguiente variable va acumulando el tiempo transcurrido para luego mostrarlo en el reloj
            tiempoAMostrarEnSegundos += tiempoDelFrameConTimeScale;
            ActualizarReloj(tiempoAMostrarEnSegundos);
        }
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
        PlayClip(hit);
        if (totalEnemigos-1 <= 0)
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
            GameObject.Find("SceneController").gameObject.SendMessage("ActivateWin");
            PlayClip(victory);
            pantallaVictoria.SetActive(true);

        }
        else
        {
            GameObject.Find("SceneController").gameObject.SendMessage("ActivateLose");
            PlayClip(lose);
            pantallaDerrota.SetActive(true);
        }
    }

    //  TIMER
    public void ActualizarReloj(float tiempoEnSegundos)
    {
        int minutos = 0;
        int segundos = 0;
        string textoDelReloj;

        //  Asegurar que el tiempo no sea negativo
        if (tiempoEnSegundos < 0)
        {
            tiempoEnSegundos = 0;
            GameOver(false);
        }

        //  Calcular minutos y segundos
        minutos = (int)tiempoEnSegundos / 60;
        segundos = (int)tiempoEnSegundos % 60;


        // Formato con el que se ve el tiempo
        //  Crear la cadena de caracteres con 2 dígitos para los minutos y segundos, separados por  ":"
        if (minutos >= 10)
        {
            textoDelReloj = minutos.ToString("00") + ":" + segundos.ToString("00");
        }
        else if (minutos >= 1)
        {
            textoDelReloj = minutos.ToString("0") + ":" + segundos.ToString("00");
        }
        else if (segundos < 10)
        {
            textoDelReloj = segundos.ToString("0");
        }
        else
        {
            textoDelReloj = segundos.ToString("00");
        }


        //  Actualizar el elemento de text de UI con la cadena de caracteres
        textTimer.text = textoDelReloj;
    }

    public void Pausar()
    {
        if (!pausado)
        {
            pausado = true;
            escalaDeTiempoAlPausar = escalaDeTiempo;
            escalaDeTiempo = 0;
        }
    }

    public void Continuar()
    {
        if (pausado)
        {
            pausado = false;
            escalaDeTiempo = escalaDeTiempoAlPausar;
        }
    }

    public void Reiniciar()
    {
        pausado = false;
        escalaDeTiempo = escalaDeTiempoInicial;
        tiempoAMostrarEnSegundos = tiempoInicial;
        ActualizarReloj(tiempoAMostrarEnSegundos);
    }

    public void PlayClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void Reintento()
    {
        PlayClip(restart);
    }
    public void Fail()
    {
        PlayClip(back);
    }
    public void Volver()
    {
        PlayClip(continuar);
    }
}
