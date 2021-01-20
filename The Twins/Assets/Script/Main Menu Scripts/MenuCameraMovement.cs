using UnityEngine;

public class MenuCameraMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField]
    public Transform[] wayPointArray;
    public int currentWayPoint = 0;


    private void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        GameObject.FindWithTag("GameManager").GetComponent<StartGameScript>().StartMainMenu();

    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, wayPointArray[currentWayPoint].position, 0.25f * Time.deltaTime);

        if (Vector2.Distance(transform.position, wayPointArray[currentWayPoint].position) < 0.5f)
        {
            currentWayPoint = NewWayPoint(currentWayPoint, wayPointArray.Length);
        }
    }

    public int NewWayPoint(int cWayNumber, int maxcount)
    {
        int newnumber;
        if (cWayNumber >= maxcount - 1)
        {
            newnumber = 0;
        }
        else
        {
            newnumber = cWayNumber + 1;
        }
        return newnumber;
    }
}