using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks;
using ProceduralLevelGenerator.Unity.Generators.Common;

[CreateAssetMenu(menuName = "Edgar/Examples/Docs/My custom post process", fileName = "MyCustomPostProcess")]
public class PostProcess : DungeonGeneratorPostProcessBase
{
    public GameObject lvl0;
    
    public override void Run(GeneratedLevel level, LevelDescription levelDescription)
    {
        /*
        lvl0 = GameObject.Find("Generated Level");
        foreach (Transform child in lvl0.transform)
        {
            if (child.gameObject.name == "Rooms")
            {
                foreach (Transform childRoom in child.transform)
                {
                    if (childRoom.gameObject.name.Contains("Corridor"))
                    {
                        foreach (Transform childDoor in childRoom.transform)
                        {
                            if(childDoor.gameObject.tag == "Door")
                            {
                                childDoor.GetComponent<DoorScript>().CheckContacts();
                            }
                        }
                    }
                }
            }
        }*/
    }
}
