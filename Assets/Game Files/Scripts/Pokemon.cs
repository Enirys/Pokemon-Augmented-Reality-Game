using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pokemon : MonoBehaviour
{
    public string pokemonName;
    public float health;
    public float maxHealth;
    private float attack;
    private float heal;
    public int healPoints;
    public Sprite pokemonIcon;

    public bool IsPlaying { get; set; }

    [SerializeField] private GameObject attackParticle;
    [SerializeField] private GameObject healParticle;

    private Animator pokemonAnim;
    private AudioSource pokemonAudioSrc;

    public Pokemon opponent;

    [SerializeField] private Text pokemonTurn;
    //public Slider healthBar;
    //public Text pokemonNameText;
    //[SerializeField] private Text healPointsText;
    [SerializeField] private GameObject pokemonTurns;

    [SerializeField] private AudioClip[] audios;

    private void Start()
    {
        pokemonAnim = GetComponent<Animator>();
        pokemonAudioSrc = GetComponent<AudioSource>();
        //healthBar.maxValue = maxHealth;
    }

    private void Update()
    {


    }

    public void Attack()
    {
        if(IsPlaying)
        {
            attack = Random.Range(5f, maxHealth / 2);
            if(pokemonAnim != null)
            {
                pokemonAnim.SetTrigger("Attack");
            }
            
            IsPlaying = !IsPlaying;
            opponent.IsPlaying = !opponent.IsPlaying;
            opponent.StartCoroutine(opponent.Hurt());
            StartCoroutine(AttackPokemon());
            int rand = Random.Range(0, audios.Length);
            pokemonAudioSrc.clip = audios[rand];
            pokemonAudioSrc.Play();
            

        } else
        {
            pokemonTurns.SetActive(true);
            pokemonTurn.text = opponent.pokemonName + " turn!";
            StartCoroutine(Deactivate(pokemonTurns));
        }
    }

    private IEnumerator AttackPokemon()
    {
        yield return new WaitForSeconds(1f);
        opponent.health -= attack;
        pokemonAudioSrc.clip = null;
    }

    private IEnumerator Deactivate(GameObject gameobject)
    {
        yield return new WaitForSeconds(1f);
        gameobject.SetActive(false);
    }

    public void HealPokemon()
    {
        if(IsPlaying)
        {
            StartCoroutine(Heal());
            IsPlaying = !IsPlaying;
            opponent.IsPlaying = !opponent.IsPlaying;
        } else
        {
            pokemonTurns.SetActive(true);
            pokemonTurn.text = opponent.pokemonName + " turn!";
            StartCoroutine(Deactivate(pokemonTurns));
        }
    }

    private IEnumerator Heal()
    {
        if (pokemonAnim != null)
        {
            pokemonAnim.SetTrigger("Heal");
        }
            
        yield return new WaitForSeconds(0.5f);
        Instantiate(healParticle, transform.position, Quaternion.identity);

        if (healPoints > 0)
        {
            heal = Random.Range(10f, maxHealth / 2);
            healPoints--;
            health += heal;
            if(health > maxHealth)
            {
                health = maxHealth;
            }
        }
    }

    private IEnumerator Hurt()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(opponent.attackParticle, transform.position, Quaternion.identity);
        if (pokemonAnim != null)
        {
            pokemonAnim.SetTrigger("Hurt");
        }
            
    }

    public void Dead()
    {
        if (pokemonAnim != null)
        {
            pokemonAnim.SetTrigger("Dead");
        }
            
    }

    public void Win()
    {
        if (pokemonAnim != null)
        {
            pokemonAnim.SetTrigger("Win");
        }
            
    }
}
