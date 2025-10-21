using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    AsyncOperation asyncLoad;
    bool escenaLista = false;
    bool activando = false;

    public AudioClip sonidoBoton;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Debug.Log("Cargando Escena en el Fondo...");
        StartCoroutine(CargarEscenaSegundoPlano());
    }

    public void IniciarJuego()
    {
        if (escenaLista && !activando)
        {
            StartCoroutine(SonidoYActivarEscena());
        }
        else
        {
            Debug.Log("La escena aún no está lista para activarse");
        }
    }

    public void Salir()
    {
        StartCoroutine(SonidoYSalir());
    }

    IEnumerator SonidoYActivarEscena()
    {
        ReproducirSonidoBoton();
        yield return new WaitForSeconds(sonidoBoton.length);
        StartCoroutine(ActivarEscena());
    }

    IEnumerator SonidoYSalir()
    {
        ReproducirSonidoBoton();
        yield return new WaitForSeconds(sonidoBoton.length);
        Application.Quit();
        Debug.Log("Salir del Juego");
    }

    void ReproducirSonidoBoton()
    {
        if (audioSource != null && sonidoBoton != null)
            audioSource.PlayOneShot(sonidoBoton);
    }

    IEnumerator CargarEscenaSegundoPlano()
    {
        // Carga en 2do plano
        asyncLoad = SceneManager.LoadSceneAsync("Scenes/Escena_1");
        // Desactiva la carga automática
        asyncLoad.allowSceneActivation = false;

        // %
        while (!asyncLoad.isDone)
        {
            //Debug.Log($"Progreso de carga: {asyncLoad.progress * 100f}%");
            if (asyncLoad.progress >= 0.9f)
            {
                Debug.Log("Escena lista. Pulsa JUGAR para cargarla");
                escenaLista = true;
                break;
            }
            yield return null;
        }
    }

    IEnumerator ActivarEscena()
    {
        activando = true;
        Debug.Log("Activando Escena...");

        // Transición negro
        yield return new WaitForSeconds(0.5f);

        asyncLoad.allowSceneActivation = true;
    }
}
