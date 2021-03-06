using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pikachuCollisionScript : MonoBehaviour {

    public bool collisionWithPikachu = true;

	void OnCollisionEnter(Collision Col)
	{
        if (Col.gameObject.tag == "charizardCollision")
        {
            pikachuControlScript.Instance.pikachuMove = false;
            GameObject.Find("Pikachu").transform.position = GameObject.Find("MyPokemons").transform.position;
            GameObject.Find("Pikachu").GetComponent<Animator>().Play("pikashuIDLE");
            GameObject.FindGameObjectWithTag("CharizardGO").GetComponent<Animator>().Play("collisionWithPikashu");
            //tackle Sound play 
            charizardControlScript.Instance.quickAttackFromPikachu(0f, collisionWithPikachu);
            GameControllerScript.Instance.ConfirmButton.SetActive(true);
        }
	}
}
