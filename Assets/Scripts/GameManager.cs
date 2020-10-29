using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public NormalBall nBall;
    public int nBallQuant;
    public SuperBall sBall;
    public int sBallQuant;
    public UltraBall uBall;
    public int uBallQuant;
    public Pointer pointer;
    public GameObject spawn;
    public int selectedBall;
    public GameObject spawnedBall;
    public GameObject ballToSpawn;
    public bool canSpawn;
    public Trees tree;
    public GameObject uiNBall;
    public GameObject uiSBall;
    public GameObject uiUBall;
    public float points;
    public Text tNBall;
    public Text tSBall;
    public Text tUBall;
    public Text uiPoints;
    public bool canChange;
    private int _totalBalls;
    public int _totalEnemies;
    public GameObject container;
    public Poke pok;
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject notBadText;
    public GameObject goodText;
    public GameObject excellentText;
    public GameObject endPointsObj;
    public Text endPoints;
    // Start is called before the first frame update
    void Start()
    {
        tNBall.text = "x" + nBallQuant.ToString();
        tSBall.text = "x" + sBallQuant.ToString();
        tUBall.text = "x" + uBallQuant.ToString();
        selectedBall = 1;
        canSpawn = true;
        canChange = true;
        uiPoints.text = points.ToString();
        _totalBalls = nBallQuant + sBallQuant + uBallQuant;
        CountEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        CheckKeys();
        SelectBall(selectedBall);

        if (_totalBalls == 0 && _totalEnemies > 0 && spawnedBall == null)
            Lose();

        if (_totalEnemies == 0)
            Win();
    }

    public void CanClick(bool state)
    {
        pointer.ClickState(state);
        if (state)
            canSpawn = true;
    }

    private void CheckKeys()
    {
        if (canChange)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                selectedBall = 1;

            if (Input.GetKeyDown(KeyCode.Alpha2))
                selectedBall = 2;

            if (Input.GetKeyDown(KeyCode.Alpha3))
                selectedBall = 3;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (canSpawn)
                {
                    SpawnBall(ballToSpawn);
                }
            }
        }
        
    }

    public void Damage(GameObject collision, float damage)
    {
        if (collision.layer == LayerMask.NameToLayer("Object"))
        {
            tree = collision.GetComponent<Trees>();
            tree.Damage(damage);
        }

        if (collision.layer == LayerMask.NameToLayer("Poke"))
        {
            pok = collision.GetComponent<Poke>();
            pok.Damage(damage);
            _totalEnemies--;
        }


    }

    private void SelectBall(int ball)
    {
        switch (ball)
        {
            case 1:
                
                uiNBall.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                uiSBall.transform.localScale = Vector3.one;
                uiUBall.transform.localScale = Vector3.one;
                if (nBallQuant != 0)
                {
                    ballToSpawn = nBall.gameObject;
                    canSpawn = true;
                }
                else canSpawn = false;
                break;

            case 2:
                
                uiSBall.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                uiNBall.transform.localScale = Vector3.one;
                uiUBall.transform.localScale = Vector3.one;
                if (sBallQuant != 0)
                {
                    ballToSpawn = sBall.gameObject;
                    canSpawn = true;
                }
                else canSpawn = false;
                
                break;

            case 3:
                
                uiUBall.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                uiNBall.transform.localScale = Vector3.one;
                uiSBall.transform.localScale = Vector3.one;
                if (uBallQuant != 0)
                {
                    ballToSpawn = uBall.gameObject;
                    canSpawn = true;
                }
                else canSpawn = false;
                break;
        }
    }

    private void SpawnBall(GameObject ballToSpawn)
    {
        if (spawnedBall != null)
        {
            Destroy(spawnedBall);
        }
        GameObject ball = Instantiate(ballToSpawn, spawn.transform, false);
        ball.transform.localPosition = Vector3.zero;
        spawnedBall = ball;
    }

    public void AddPoints(float addedPoints)
    {
        points += addedPoints;
        uiPoints.text = points.ToString();
    }

    public void ReduceQuantity()
    {
        switch (selectedBall)
        {
            case 1:
                nBallQuant--;
                tNBall.text = "x" + nBallQuant.ToString();
                break;

            case 2:
                sBallQuant--;
                tSBall.text = "x" + sBallQuant.ToString();
                break;

            case 3:
                uBallQuant--;
                tUBall.text = "x" + uBallQuant.ToString();
                break;
        }
        _totalBalls--;
    }

    public void Win()
    {
        winScreen.SetActive(true);
        endPointsObj.SetActive(true);
        endPoints.text = "Points" + "\n" + points.ToString();

        if (points <= 500)
            notBadText.SetActive(true);

        else if (points < 1000)
            goodText.SetActive(true);

        else if (points >= 1000)
            excellentText.SetActive(true);

        
    }

    public void Lose()
    {
        loseScreen.SetActive(true);
        endPointsObj.SetActive(true);
        endPoints.text = "Points" + "\n" + points.ToString();
    }

    private void CountEnemies()
    {
        foreach (Transform poke in container.transform)
        {
            _totalEnemies++;
        }
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
