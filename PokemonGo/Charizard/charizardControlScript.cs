using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class charizardControlScript : MonoBehaviour {

    public static charizardControlScript Instance { set; get; }
    bool flyCharizard;
    public bool goCharizard;
    public ParticleSystem FlameThrowerGO;
    float charizardHealth = 100f;
    public float flyValue = 20f;
    public float flameThrowerValue = 30;
    Image HPBar;


	// Use this for initialization
	void Start () {

        Instance = this;
      
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectWithTag("PikachuGO") !=null)
        {
            transform.LookAt(GameObject.FindGameObjectWithTag("PikachuGO").transform.position);
        }

        if(flyCharizard)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 2f);
        }
        if(goCharizard)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 5f);
        }

        if (charizardHealth <=0)
        {
            StartCoroutine(waitThenDead());
        }
	}

    public void charizardAppeared()
    {
        //sound charizard play
    }

    public void Fly()
    {
        flyCharizard = true;
        StartCoroutine(waitForFlyAttack());
        pikachuControlScript.Instance.flyAttackFromCharizard(flyValue, false);
    }

    IEnumerator waitForFlyAttack()
    {
        yield return new WaitForSeconds(1.5f);
        flyCharizard = false;
        yield return new WaitForSeconds(0.5f);
        goCharizard = true;
        gameObject.GetComponent<Animator>().Play("flyAttack");
    }

    public void Flamethrower()
    {
        StartCoroutine(waitForFlameThrower());
    }

    IEnumerator waitForFlameThrower()
    {
        gameObject.GetComponent<Animator>().Play("flamethrower");
        yield return new WaitForSeconds(1f);
        FlameThrowerGO.Play();
        // play sound
        yield return new WaitForSeconds(0.5f);
        pikachuControlScript.Instance.flamethrowerFromCharizard(flameThrowerValue);
        GameObject.Find("Pikachu").GetComponent<Animator>().Play("attackFromCharizard");
        yield return new WaitForSeconds(1.2f);
        FlameThrowerGO.Stop();
        GameControllerScript.Instance.ConfirmButton.SetActive(true);
    }


    public void quickAttackFromPikachu(float quickAttackValue, bool collisionWithPikachu)
    {
        HPBar = GameObject.Find("HPBarBackgroundEnemy").GetComponent<Image>();
        charizardHealth -= quickAttackValue;

        if(collisionWithPikachu)
        {
            HPBar.fillAmount = charizardHealth / 100f;
        }
    }

    public void thunderShockFromPikachu(float thunderShockValue)
    {
        HPBar = GameObject.Find("HPBarBackgroundEnemy").GetComponent<Image>();
        charizardHealth -= thunderShockValue;
        HPBar.fillAmount = charizardHealth / 100f;
    }

    IEnumerator waitThenDead()
    {
        yield return new WaitForSeconds(2.5f);
        GameControllerScript.GameStatus = "enemyIsDead";
        GameControllerScript.Instance.gameStatusInfoBar();
        gameObject.SetActive(false);
    }
}
