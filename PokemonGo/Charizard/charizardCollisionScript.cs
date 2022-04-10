using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charizardCollisionScript : MonoBehaviour {

    public bool collisionWithCharizard = true;

    void OnCollisionEnter(Collision Col)
    {
        if (Col.gameObject.tag == "pikachuCollision" && charizardControlScript.Instance.goCharizard)
        {
            charizardControlScript.Instance.goCharizard = false;
            GameObject.FindGameObjectWithTag("CharizardGO").transform.position = GameObject.Find("EnemyPokemon").transform.position;
            GameObject.FindGameObjectWithTag("CharizardGO").GetComponent<Animator>().Play("CharizardIDLE");
            GameObject.Find("Pikachu").GetComponent<Animator>().Play("attackFromCharizard");
            //tackle Sound play 
            pikachuControlScript.Instance.flyAttackFromCharizard(0f, collisionWithCharizard);
            GameControllerScript.Instance.ConfirmButton.SetActive(true);
        }
    }
}
