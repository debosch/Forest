using UnityEngine;

public class GreenMonster : Character
{
    [SerializeField]
    private Transform pivot;

    public Vector3 startMarker;
    public Vector3 endMarker;
    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;
    private bool reverse = false;


    override protected void Start()
    {
        base.Start();
        startMarker = pivot.position + Vector3.left * 10;
        endMarker = pivot.position + Vector3.right * 10;
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker, endMarker);
    }

    private void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        if(reverse)
            transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
        else
            transform.position = Vector3.Lerp(endMarker, startMarker, fracJourney);
            

        if (Vector3.Distance(transform.position, startMarker) == 0f || Vector3.Distance(transform.position, endMarker) == 0f)
        {
            if (reverse)
            {
                Debug.Log(reverse);
                reverse = false;
                ChangeDirection();
            }   
            else
            {
                Debug.Log(reverse);
                reverse = true;
                ChangeDirection();
            }
                

            startTime = Time.deltaTime;
        }
    }

   
}
