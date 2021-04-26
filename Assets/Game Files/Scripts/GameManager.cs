using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Pokemon[] pokemons;
    public Pokemon pokemon1;
    public Pokemon pokemon2;
    [SerializeField] private Text pokemonStart;
    [SerializeField] private Text pokemonWin;

    [SerializeField] private Player player1;
    [SerializeField] private Player player2;

    [SerializeField] private GameObject pokemonStarts;

    public static GameManager instance;

    private AudioSource audioSrc;
    [SerializeField] private AudioClip[] gameAudios;

    [SerializeField] private GameObject gameOverPanel;

    public bool gameStarted = false;
    public bool gameOver = false;
    public bool pokemonSelected = false;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }

        audioSrc = GetComponent<AudioSource>();
        audioSrc.clip = gameAudios[0];
        audioSrc.Play();

    }

    private void Update()
    {
        if(!gameOver)
        {
            if (pokemon1 != null && pokemon2 != null && !gameStarted)
            {
                pokemons = new Pokemon[2];
                pokemons[0] = pokemon1;
                pokemons[1] = pokemon2;
                pokemons[0].opponent = pokemons[1];
                pokemons[1].opponent = pokemons[0];
                player1.pokemon = pokemons[0];
                player2.pokemon = pokemons[1];
                gameStarted = true;
            }

            if (gameStarted && !pokemonSelected)
            {
                int random = Random.Range(0, 2);
                pokemons[random].IsPlaying = true;
                pokemonStarts.SetActive(true);
                pokemonStart.text = pokemons[random].pokemonName + " Starts!";
                pokemonSelected = true;
                StartCoroutine(Deactivate(pokemonStarts));
            }

            if (gameStarted && pokemonSelected)
            {
                if (pokemons[0].health < 0)
                {
                    pokemons[0].Dead();
                    pokemons[1].Win();
                    pokemonWin.text = pokemons[1].pokemonName + " Won!";
                    gameOver = true;

                }
                else if (pokemons[1].health < 0)
                {
                    pokemons[1].Dead();
                    pokemons[0].Win();
                    pokemonWin.text = pokemons[0].pokemonName + " Won!";
                    gameOver = true;
                }
            }

        } else
        {
            gameOverPanel.SetActive(true);
            player1.gameObject.SetActive(false);
            player2.gameObject.SetActive(false);
        }

    }

    public IEnumerator Deactivate(GameObject gameobject)
    {
        yield return new WaitForSeconds(2.5f);
        gameobject.SetActive(false);
    }
}
