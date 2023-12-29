using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EfeitoDigitador : MonoBehaviour
{
    private TextMeshProUGUI componenteTexto;
    private AudioSource _audioSource;
    private string mensagemoriginal;
    public bool imprimindo;
    public float tempoEntreLetras = 0.08f;


    private void Awake()
    {
        TryGetComponent(out componenteTexto);
        TryGetComponent(out _audioSource);
        mensagemoriginal = componenteTexto.text;
        componenteTexto.text = "";

    }

    private void OnEnable()
    {
        ImprimirMensagem(mensagemoriginal);
    }

    private void OnDisable()
    {
        componenteTexto.text = mensagemoriginal;
        StopAllCoroutines();
    }

    public void ImprimirMensagem(string mensagem)
    {
        if(gameObject.activeInHierarchy)
        {
            if(imprimindo) return;
            imprimindo = true;
            StartCoroutine(LetraPorLetra(mensagem));
        }
    }

    IEnumerator LetraPorLetra(string mensagem)
    {
        string msg = "";
        foreach (var letra in mensagem)
        {
            msg += letra;
            componenteTexto.text = msg;
            _audioSource.Play();
            yield return new WaitForSeconds(tempoEntreLetras);
        }

        imprimindo = false;
        StopAllCoroutines();
    }
}
