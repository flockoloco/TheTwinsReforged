using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatScript : MonoBehaviour
{
    public PlayerStats playerstas;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            playerstas = gameObject.GetComponent<PlayerStats>();
            playerstas.gold += 10000;
            playerstas.bars += 10000;
            playerstas.nuggets += 10000;
        }    
        if (Input.GetKeyDown(KeyCode.B))
        {
            Vector3 fountain = GameObject.FindWithTag("Fountain").transform.position;
            GameObject.FindWithTag("Player").transform.position = new Vector3(fountain.x, fountain.y - 2, 0);
        }
    }
}
