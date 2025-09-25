using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform Door;
    
    public Vector3 closedPos = new Vector3(.51f, 3.63f, 0);
    public Vector3 openedPos = new Vector3( .51f, 7, 0);
    
    public float openSpeed = 5;
    
    private bool open = false;


    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            Door.position = Vector3.Lerp(Door.position, openedPos, Time.deltaTime * openSpeed);
        }
        else
        {
            Door.position = Vector3.Lerp(Door.position, closedPos, Time.deltaTime * openSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            openedDoor();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            closedDoor();
        }
    }
    public void closedDoor()
    {
        open = false;
    }
    
    public void openedDoor()
    {
        open = true;
    }
}
