using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordscript : MonoBehaviour
{
    private Vector2 mousePos;
    private GameObject player;
    private Vector2 playerPos;
    private Camera cam;
    private Vector2 finalvector;
    private Vector2 mouseDir;
    private float swordrotatorotato;

    public bool rotatoFrezeto;

    private Animator swordAnimator;

    private float swordTimer;
    void Awake()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindWithTag("Player");
        swordAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        swordTimer += Time.deltaTime;
        if (player.GetComponent<PlayerStats>().shopOpen == false) 
        { 
            if (Input.GetKey(KeyCode.Mouse0) && (swordTimer > player.GetComponent<PlayerStats>().equippedSword.atkSpeed))
            {
                swordAnimator.SetBool("Attack", true);
                rotatoFrezeto = true;
            }
        }
    }
    void FixedUpdate()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        playerPos = player.transform.position;
        mouseDir = new Vector2(mousePos.x - playerPos.x, mousePos.y - playerPos.y).normalized;
        finalvector = mouseDir * 1.25f;
        if (swordAnimator.GetBool("Attack") == true)
        {
            GetComponentInChildren<BoxCollider2D>().enabled = true;
        }
        else
        {
            GetComponentInChildren<BoxCollider2D>().enabled = false;
        }
        if (!rotatoFrezeto)
        {
            swordrotatorotato = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
            gameObject.transform.parent.rotation = Quaternion.Euler(0, 0, swordrotatorotato);
            gameObject.transform.parent.position = new Vector3( playerPos.x, playerPos.y - 0.2f, 0);
        }
    }
    void stopSwordAttackAnimation()
    {
        swordTimer = 0;
        swordAnimator.SetBool("Attack", false);
        rotatoFrezeto = false;
    }
}
