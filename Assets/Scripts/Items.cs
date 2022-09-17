using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public bool goUp;
    public double speed = 0.3;
    public float rotspeed = 20;
    public int points;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SwitchDir());
    }

    // Update is called once per frame
    void Update()
    {
        if (goUp)
        {
            transform.position = transform.position + new Vector3(0, (float) speed * Time.deltaTime, 0);
        }
        else
        {
            transform.position = transform.position - new Vector3(0, (float) speed * Time.deltaTime, 0);
        }

        transform.Rotate(Vector3.up * (rotspeed * 10 * Time.deltaTime), Space.World);
    }

    IEnumerator SwitchDir()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(0.75f);
            goUp = !goUp;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPicked(other);
        }
    }

    protected virtual void OnPicked(Collider other)
    {
        other.GetComponent<ScoreManager>().scoring(points);
        Destroy(gameObject);
    }
}
