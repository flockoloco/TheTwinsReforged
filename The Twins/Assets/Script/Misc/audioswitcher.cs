using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioswitcher : MonoBehaviour
{
    public GameObject boss;
    public AudioClip musicFight;
    public CameraSoundChanger sound;
    // Update is called once per frame
    private void Start()
    {
        sound = GameObject.FindWithTag("MainCamera").GetComponent<CameraSoundChanger>();
    }
    void Update()
    {
        if(boss.name == "Boss1")
        {
            if(boss.GetComponent<Boss1Ai>().triggered && boss.GetComponent<StatsHolder>().health > 0 && boss.GetComponent<Boss1Ai>().fightDuration == 0)
            {
                Debug.Log(boss.GetComponent<Boss1Ai>().fightDuration);
                sound.changeAudio(musicFight);
            }
        }
        if (boss.name == "Boss2")
        {
            if (boss.GetComponent<Boss2Ai>().triggered && boss.GetComponent<StatsHolder>().health > 0 && boss.GetComponent<Boss2Ai>().fightDuration == 0)
            {
                Debug.Log(boss.GetComponent<Boss2Ai>().fightDuration);
                sound.changeAudio(musicFight);
            }
        }
        if (boss.name == "Boss3")
        {
            if (boss.GetComponent<Boss3Ai>().triggered && boss.GetComponent<StatsHolder>().health > 0 && boss.GetComponent<Boss3Ai>().fightDuration == 0)
            {
                Debug.Log(boss.GetComponent<Boss3Ai>().fightDuration);
                sound.changeAudio(musicFight);
            }
        }
    }
}
