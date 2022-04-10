using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pikachuControlScript : MonoBehaviour {

    public static pikachuControlScript Instance { set; get; }
    public bool pikachuMove;
    public ParticleSystem Sparks;
    public GameObject Lightnings;
    public float quickAttackValue = 10f;
    public float thunderShockValue = 30f;
    float pikachuHealth = 100f;
    Image HPBar;

	// Use this for initialization
	void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {

        if (GameObject.FindGameObjectWithTag("CharizardGO") != null)
        {
            transform.LookAt(GameObject.FindGameObjectWithTag("CharizardGO").transform.position);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        if(pikachuMove)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 3f);
        }

        if (pikachuHealth <= 0)
        {
            StartCoroutine(waitThenDead());
        }
	}


    public void Growl()
    {
        charizardControlScript.Instance.quickAttackFromPikachu(quickAttackValue, false);
        gameObject.GetComponent<Animator>().Play("Attack");
        pikachuMove = true;
        GameControllerScript.GameStatus = "pikachuUsedGrowl";
        GameControllerScript.Instance.gameStatusInfoBar();
    }

    public void Thundershock()
    {
        GameControllerScript.GameStatus = "pikachuUsedThundershock";
        GameControllerScript.Instance.gameStatusInfoBar();
        StartCoroutine(ThundershockCoroutine());
    }

    IEnumerator ThundershockCoroutine()
    {
        gameObject.GetComponent<Animator>().Play("electro");
        yield return new WaitForSeconds(0.5f);
        Sparks.Play();
        yield return new WaitForSeconds(0.5f);
        // sound play()
        Lightnings.gameObject.SetActive(true);
        charizardControlScript.Instance.thunderShockFromPikachu(thunderShockValue);
        GameObject.FindGameObjectWithTag("CharizardGO").GetComponent<Animator>().Play("collisionWithPikashu");
        yield return new WaitForSeconds(1f);
        Lightnings.gameObject.SetActive(false);
        Sparks.Stop();
        GameObject.Find("Pikachu").transform.position = GameObject.Find("MyPokemons").transform.position;
        GameControllerScript.Instance.ConfirmButton.SetActive(true);
    }

    public void flyAttackFromCharizard(float flyValue, bool collisionWithCharizard)
    {
        HPBar = GameObject.Find("HPBarBackground").GetComponent<Image>();
        pikachuHealth -= flyValue;

        if (collisionWithCharizard)
        {
            HPBar.fillAmount = pikachuHealth / 100f;
        }
    }

    public void flamethrowerFromCharizard(float flameThrowerValue)
    {
        HPBar = GameObject.Find("HPBarBackground").GetComponent<Image>();
        pikachuHealth -= flameThrowerValue;
        HPBar.fillAmount = pikachuHealth / 100f;
    }


    IEnumerator waitThenDead()
    {
        yield return new WaitForSeconds(2.5f);
        GameControllerScript.GameStatus = "pikachuIsDead";
        GameControllerScript.Instance.gameStatusInfoBar();
        gameObject.SetActive(false);
    }

    
}
