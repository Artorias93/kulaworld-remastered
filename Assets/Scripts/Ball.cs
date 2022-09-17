using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    [SerializeField]
    public Rigidbody rb;
    bool isGrounded = true;
    public float forwardForce = 30f;
    public float backwardForce = 30f;
    public float maxJump = 1;
    public GameObject pnlDeath;
    public Button btnRetry;
    Color endCol;
    string msg;


    private void Start()
    {
        pnlDeath.SetActive(false);
        GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Text>().enabled = true;
        GetComponent<Ball>().enabled = true;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = Camera.main.transform.forward;
        Vector3 forwardDir = new Vector3(forward.x, 0, forward.z).normalized;

        if (transform.position.y < -20)
        {
            ColorUtility.TryParseHtmlString("#FF0E00", out endCol);
            endMsg("YOU LOSE!", endCol);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(forwardDir * (forwardForce * Time.deltaTime), ForceMode.Impulse);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(forwardDir * (-backwardForce * Time.deltaTime), ForceMode.Impulse);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        if (Input.GetKeyDown("space") && isGrounded && maxJump == 0)
        {
            maxJump += 1;
            Vector3 jump = new Vector3(0.0f, 5.0f, 0.0f);
            GetComponent<Rigidbody>().AddForce(jump, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        isGrounded = true;
        maxJump = 0;
        if (other.gameObject.name=="Ending")
        {
            rb.Sleep();
            ColorUtility.TryParseHtmlString("#00E017", out endCol);
            endMsg("LEVEL COMPLETED!", endCol);
        }
    }

    void endMsg(string msg, Color endCol)
    {

        GetComponent<Ball>().enabled = false;
        GameObject.Find("Camera Motor").GetComponent<CameraRotation>().stopRot();
        float score = GetComponent<ScoreManager>().score;
        GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Text>().enabled = false;
        pnlDeath.transform.GetChild(0).GetComponent<Text>().text = msg;
        pnlDeath.transform.GetChild(0).GetComponent<Text>().color = endCol;
        pnlDeath.transform.GetChild(1).GetComponent<Text>().text = "Your Score: " + score;
        pnlDeath.SetActive(true);
    }
}
