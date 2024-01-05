using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarregarCena : MonoBehaviour
{
    public string proxCena;

    void Start()
    {
        SceneManager.LoadScene(proxCena);
    }
}
