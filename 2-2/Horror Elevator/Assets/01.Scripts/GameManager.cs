using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Obstacle")]
    public GameObject obstacle_Bottom_Prefab;
    public GameObject obstacle_Middle_Prefab;
    public GameObject obstacle_Top_Prefab;
    //public Sprite[] obstacleSprites;
    private List<Obstacle> obstacle_Bottom_List = new List<Obstacle>();
    private List<Obstacle> obstacle_Middle_List = new List<Obstacle>();
    private List<Obstacle> obstacle_Top_List = new List<Obstacle>();

    public Image hpImage;
    public Sprite[] hpSprites;

    public Text timeText;
    public Button endBtn;

    
    [SerializeField]
    private float gameSpeedAcceleration = 0;

    public Light2D light;


    public MeshRenderer backgroundMesh;
    public float backgroundSpeed = 0.1f;
    float offsetX;
    float offsetY;

    public Transform playerTrm;

    private bool isStarted = true;
    public float time = 0f;

    private void Awake()
    {
        
    }

    void Start()
    {
        Time.timeScale = 1.5f;

        StartCoroutine(GenerationObstacleBottom());

        isStarted = false;

        offsetX = backgroundMesh.material.mainTextureOffset.x;
    }

    void Update()
    {
        if (isStarted)
        {
            offsetY -= backgroundSpeed * Time.deltaTime;
            backgroundMesh.material.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));

            Time.timeScale += (gameSpeedAcceleration / (Time.timeScale * Time.timeScale)) * Time.deltaTime;

            time += Time.deltaTime;

            timeText.text = $"버틴 시간 : {Mathf.Floor(time)}";
        }
    }

    private GameObject CreateObstacle(GameObject prefab)
    {
        GameObject obstacle = Instantiate(prefab, transform.position, Quaternion.identity, transform);
        obstacle.SetActive(false);
        return obstacle;
    }
    IEnumerator GenerationObstacleBottom()
    {
        Vector2[] pos1 = new Vector2[3] { new Vector2(0, -2.41f), new Vector2(1, -2.41f), new Vector2(2, -2.41f) }; // 3
        Vector2[] pos2 = new Vector2[3] { new Vector2(0, -2.41f), new Vector2(1, -2.41f), new Vector2(3, -2.41f) }; // 2
        Vector2[] pos3 = new Vector2[3] { new Vector2(0, -2.41f), new Vector2(2, -2.41f), new Vector2(3, -2.41f) }; // 1
        Vector2[] pos4 = new Vector2[3] { new Vector2(1, -2.41f), new Vector2(2, -2.41f), new Vector2(3, -2.41f) }; // 0

        Vector2[] randomPos = null;

        yield return new WaitForSeconds(3f);

        isStarted = true;

        while (isStarted)
        {
            switch(Random.Range(0, 4))
            {
                case 0: randomPos = pos1; break;
                case 1: randomPos = pos2; GenerationObstacleMiddle();  break;
                case 2: randomPos = pos3; GenerationObstacleMiddle();  break;
                case 3: randomPos = pos4; break;
                default:
                    Debug.Log("Random.Range Error");
                    break;
            }

            GenerationObstacleTop();

            for (int i = 0; i < 3; i++)
            {
                Obstacle obstacle = obstacle_Bottom_List.Find(x => !x.gameObject.activeSelf);
                if (obstacle == null)
                {
                    obstacle = CreateObstacle(obstacle_Bottom_Prefab).GetComponent<Obstacle>();
                    obstacle_Bottom_List.Add(obstacle);
                }

                obstacle.transform.position = randomPos[i];

                obstacle.gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(Random.Range(2.5f, 3f));
        }
    }
    /*IEnumerator GenerationObstacleMiddle()
    {
        Vector2 pos;

        while (true)
        {
            // 공중에 오래있으면 trigger on
            //if(playerTrm.position.y >= -1.5f)
            //{
            //    t += Time.deltaTime;
            //    if(t > 0.5f)
            //    {
            //        middleTrigger = true;
            //        t = 0;
            //        yield return new WaitForSeconds(2f);
            //    }
            //}
            //else
            //{
            //    t = 0;
            //}

            
            if(middleTrigger)
            {
                middleTrigger = false;

                Obstacle obstacle = obstacle_Middle_List.Find(x => !x.gameObject.activeSelf);
                if (obstacle == null)
                {
                    obstacle = CreateObstacle(obstacle_Middle_Prefab).GetComponent<Obstacle>();
                    obstacle_Middle_List.Add(obstacle);
                }

                pos = new Vector2(1.45f, playerTrm.position.y);
                obstacle.transform.position = pos;

                obstacle.gameObject.SetActive(true);
            }

            yield return null;
        }
    }*/
    private void GenerationObstacleMiddle()
    {
        Vector2 pos;

        Obstacle obstacle = obstacle_Middle_List.Find(x => !x.gameObject.activeSelf);
        if (obstacle == null)
        {
            obstacle = CreateObstacle(obstacle_Middle_Prefab).GetComponent<Obstacle>();
            obstacle_Middle_List.Add(obstacle);
        }

        pos = new Vector2(1.45f, playerTrm.position.y);
        obstacle.transform.position = pos;

        obstacle.gameObject.SetActive(true);
    }
    private void GenerationObstacleTop()
    {
        Vector2 pos;
        //float t = 0;

        if (Random.Range(0, 3) == 0)
        {
            Obstacle obstacle = obstacle_Top_List.Find(x => !x.gameObject.activeSelf);
            if (obstacle == null)
            {
                obstacle = CreateObstacle(obstacle_Top_Prefab).GetComponent<Obstacle>();
                obstacle_Top_List.Add(obstacle);
            }

            pos = new Vector2(playerTrm.position.x, 4);
            obstacle.transform.position = pos;

            obstacle.gameObject.SetActive(true);
        }
    }

    public void SetHpUI(int hp)
    {
        if(hp <= 0)
        {
            hpImage.color = new Color(0, 0, 0, 0);
        }
        else
        {
            hpImage.sprite = hpSprites[hp - 1];

        }
    }

    public void EndGame()
    {
        isStarted = false;
        Time.timeScale = 0;
        endBtn.gameObject.SetActive(true);
    }
}
