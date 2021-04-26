using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Pokemon pokemon;
    public Slider healthBar;
    public Text pokemonName;
    public Text healPoints;
    public Image pokemonIcon;

    private void Update()
    {
        if(pokemon != null)
        {
            healPoints.text = "Heal " + pokemon.healPoints.ToString();
            healthBar.value = pokemon.health;
            pokemonName.text = pokemon.pokemonName;
            healthBar.maxValue = pokemon.maxHealth;
            pokemonIcon.sprite = pokemon.pokemonIcon;

        }
        
    }

    public void Attack()
    {
        pokemon.Attack();
    }

    public void Heal()
    {
        pokemon.HealPokemon();
    }
}
