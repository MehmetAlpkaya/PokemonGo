using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour {

    public static GameControllerScript Instance { set; get; }
    public static string GameStatus;
    public Text InfoText;
    public GameObject SelectBar;
    public GameObject ConfirmButton;
    public GameObject FightButtons;
    public GameObject StartButtons;
    public GameObject PokemonEnemyUI;
	// Use this for initialization
	void Start () {
        Instance = this;
	}
	
	public void gameStatusInfoBar()
    {
        switch (GameStatus)
        {

            case "loadLocationScene":
                //SceneManager.LoadScene("LocationBasedGame");
                Debug.Log("load new scene");
                break;

            case "enemyIsDead":
                InfoText.text = "CHARIZARD is dead!";
                GameStatus = "loadLocationScene";
                ConfirmButton.SetActive(true);
                break;

            case "pikachuIsDead":
                InfoText.text = "Your POKEMON is dead!";
                GameStatus = "loadLocationScene";
                ConfirmButton.SetActive(true);
                break;

            case "selectOption":

                InfoText.text = "";
                ConfirmButton.SetActive(false);
                SelectBar.gameObject.SetActive(true);
                StartButtons.gameObject.SetActive(true);
                FightButtons.gameObject.SetActive(false);
                break;
            
            case "caughtCharizard":

                InfoText.text = "CHARIZARD was caught!";
                break;

            case "ashUsedPokeball":

                InfoText.text = "ASH uses POKEBALL!";
                break;

            case "enemyAttacks":

                ConfirmButton.SetActive(false);
                int RandomAttack = Random.Range(0, 2);
                if (RandomAttack == 0)
                {
                    InfoText.text = "CHARIZARD used FLY!";
                    charizardControlScript.Instance.Fly();
                }
                else if(RandomAttack == 1)
                {
                    InfoText.text = "CHARIZARD used FLAMETHROWER!";
                    charizardControlScript.Instance.Flamethrower();
                }
                GameStatus = "selectOption";
                break;

            case "pikachuUsedThundershock":

                InfoText.text = "PIKACHU used THUNDERSHOCK!";
                SelectBar.gameObject.SetActive(false);
                ConfirmButton.SetActive(false);
                GameStatus = "enemyAttacks";
                break;

            case "pikachuUsedGrowl":

                InfoText.text = "PIKACHU used QUICKATTACK!";
                SelectBar.gameObject.SetActive(false);
                ConfirmButton.SetActive(false);
                GameStatus = "enemyAttacks";
                break;

            case "pokemonIsOut":

                InfoText.text = "GO! PIKACHU!";
                break;

            case "enemyAppeared":

                InfoText.text = "Choose your Pokemon";
                break;

            case "fightHasStarted":

                InfoText.text = "Wild CHARIZARD appeared!";
                PokemonEnemyUI.gameObject.SetActive(true);
                ConfirmButton.SetActive(true);
                GameStatus = "enemyAppeared";
                break;

            default:
                InfoText.text = "";
                break;
        }
    }

}
