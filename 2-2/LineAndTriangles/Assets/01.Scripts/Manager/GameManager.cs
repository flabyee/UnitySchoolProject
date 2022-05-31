using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

// To Do
// 타격감
// 튜토리얼
// 카메라 줌인(endSelecter와 Mover만 잡히게 cinemachinCamera이용?)

[SerializeField]
public class PlayerData
{
    public int[] stageStarCounts;
    public float volume;
}

public class GameManager : MonoBehaviour
{
    // 싱글톤
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    #region STAGE
    [Header("STAGE")]
    public GameObject[] selectStages;

    //public GameObject[,] stages = new GameObject[6, 5];

    public GameObject[] stage1 = new GameObject[5];
    public GameObject[] stage2 = new GameObject[5];
    public GameObject[] stage3 = new GameObject[5];
    public GameObject[] stage4 = new GameObject[5];
    public GameObject[] stage5 = new GameObject[5];
    public GameObject[] stage6 = new GameObject[5];
    #endregion


    [Header("PANEL")]
    public GameObject menus;            // 메인메뉴 창
    public GameObject btnPanel;         // 게임 버튼 창1
    public GameObject btn2Panel;        // 게임 버튼 창2
    public GameObject stopMenuPanel;    // 일시정지 창
    public GameObject endMovePanel;     // move 끝난후 나오는 창
    public GameObject resultPanel;      // 결과 알려주는 창
    public Text resultText;             

    [Header("OTHER")]
    private GameObject stage;           // 현재 스테이지
    private int currentStageNum;        // 현재 스테이지의 번호

    public List<Transform> moveTrList = new List<Transform>();  // Character들의 목적지 리스트
    public List<Transform> endSelecterSpawnTrList = new List<Transform>();  // EndSelecter의 생성위치 리스트

    public int[] stageStarCounts;   // 스테이지에서 얻은 별 갯수들의 배열

    private bool isEnd = false;     // EndMove가 2번 작동하지 않게함

    public Transform point;        // Mover들의 충돌위치
    public Transform endSelecter;  // EndSelecter의 위치
    private LineRenderer stageLine; // moveTrList의 목적지들의 Line
    public LineRenderer endLine;    // 충돌위치와 EndSelecter의 위치의 Line

    public ParticleSystem endEffect;    // endLine이 그려진후에 나오는 effect

    private Coroutine drawEndLineCoroutine; // endLine이 그려지는 도중에 나가면 Coroutine을 종료하기 위한 변수

    [Header("SOUNDS")]
    public AudioSource audioSource;

    public AudioClip clickSound;

    public Slider soundSlier;
    


    
    public event Action startGame;
    public event Action<Vector2, int, bool> startMove;
    public event Action startCollision;
    public event Action endMove;
    public event Action endGame;


    private void Awake()
    {
        // 싱글톤
        if(_instance != null)
        {
            Debug.Log("GameManager Singleton");
        }
        else
        {
            _instance = this;
        }

        // 초기화
        startGame = () => { };
        startMove = (x, y, z) => { };
        startCollision = () => { };
        endMove = () => { };
        endGame = () => { };    

        stageStarCounts = new int[100];

        LoadData();

    }


    private void Start()
    {
        
    }

    private void Update()
    {
        // 클릭하면 클릭사운드 재생
        if(Input.GetMouseButtonUp(0))
        {
            audioSource.clip = clickSound;
            
            audioSource.Play();
        }

        
    }
    
    // 스테이지 버튼을 누르면 해당 스테이지를 매개변수로 받아서 맵을 로딩하고 startGame
    private void StartGame(GameObject nowStage)
    {
        LoadingMap(nowStage);

        startGame();

        menus.SetActive(false);
        btnPanel.SetActive(true);
    }

    // 선택한 스테이지에 해당하는 맵 불러오고 LineRenderer로 그리기
    private void LoadingMap(GameObject nowStage)
    {
        stage = Instantiate(nowStage);

        SetMoveTrs();

        // 선 그리기
        // To Do : 교차로 구분 짓기?
        stageLine.positionCount = moveTrList.Count + 2;
        stageLine.startColor = Color.red;
        stageLine.endColor = Color.blue;
        for (int i = 0; i < moveTrList.Count + 2; i++)
        {
            stageLine.SetPosition(i, moveTrList[i % moveTrList.Count].position);
        }
    }

    // moveTrList와 EndSelecterSpawnTrList 할당
    private void SetMoveTrs()
    {
        if (stage != null)
        {
            // stage의 자식에서 tag가 MOVEPOINT인것들의 Transform을 moveTrList에 할당
            moveTrList = (from item in stage.GetComponentsInChildren<Transform>()
                          where item.gameObject.CompareTag("MOVEPOINT")
                          select item.transform).ToList();


            stageLine = stage.GetComponent<LineRenderer>();

            // stage의 자식에서 tag가 SPAWNPOINT인것들의 Transform을 moveTrList에 할당
            endSelecterSpawnTrList = (from item in stage.GetComponentsInChildren<Transform>()
                                      where item.gameObject.CompareTag("SPAWNPOINT")
                                      select item.transform).ToList();

            Debug.Log(endSelecterSpawnTrList.Count);
        }
    }

    // 가운데 버튼을 눌러서 실행
    public void StartMove(Vector2 pos, int index, bool isLeft)
    {
        startMove(pos, index, isLeft);
        btnPanel.SetActive(false);
        btn2Panel.SetActive(true);
    }

    // 가운데 버튼을 눌러서 실행
    public void StartCollision()
    {
        startCollision();

        btn2Panel.SetActive(false);
    }

    // Mover끼리 충돌이후 실행
    public void EndMove()
    {
        if (!isEnd)
        {
            isEnd = true;

            endMove();

            // To Do : 남은 거리를 보여줄때 만난 점에서부터 endSelect로 줄이 나오면서 그 줄의 색이 점점 연해진다?
            endLine.positionCount = 2;
            endLine.SetPosition(0, point.transform.position);
            endLine.SetPosition(1, point.transform.position);
            endMovePanel.SetActive(true);

            drawEndLineCoroutine = StartCoroutine(DrawEndLine());



            float distance = (point.transform.position - endSelecter.transform.position).magnitude;
            resultText.text = String.Format("남은 거리 : " + distance);


            // To Do : 이전 기록과 비교해서 더 좋아진 경우에만 다시 저장
            int result = 0;
            int stageNum = currentStageNum - 1;

            if(distance < 0.1f)         result = 3;
            else if(distance < 0.5f)    result = 2;
            else if(distance < 1f)      result = 1;
            

            if(result > stageStarCounts[stageNum])
            {
                stageStarCounts[stageNum] = result;
            }

            SaveData();
        }
    }

    // Mover들의 충돌지점과 EndSelecter사이에 Line을 그림
    private IEnumerator DrawEndLine()
    {
        float t = 0;
        Vector2 pos = Vector2.zero;

        while(t < 2f)
        {
            pos = Vector2.Lerp(point.transform.position, endSelecter.transform.position, t / 2);
            endLine.SetPosition(1, pos);

            t += Time.deltaTime;
            yield return null;
        }

        endEffect.Play();
    }

    // EndMove이후에 생긴 endMovePanel의 버튼을 눌러서 실행
    public void OnResult()
    {
        endMovePanel.SetActive(false);
        resultPanel.SetActive(true);
    }

    // OnResult이후에 생긴 resultPanel의 버튼을 눌러서 실행
    public void OnEndGame()
    {
        StopCoroutine(drawEndLineCoroutine);

        endLine.positionCount = 0;

        resultPanel.SetActive(false);
        EndGame();
    }


    private void EndGame()
    {
        isEnd = false;

        Destroy(stage);
        stage = null;

        endGame();

        menus.SetActive(true);
    }

    // Json으로 저장
    private void SaveData()
    {
        PlayerData playerData = new PlayerData();

        playerData.stageStarCounts = stageStarCounts;
        playerData.volume = audioSource.volume;

        File.WriteAllText(Application.persistentDataPath + "/PlayerData.json", JsonUtility.ToJson(playerData));
    }

    private void LoadData()
    {
        if(File.Exists(Application.persistentDataPath + "/PlayerData.json"))
        {
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(File.ReadAllText(Application.persistentDataPath + "/PlayerData.json"));

            stageStarCounts = playerData.stageStarCounts;
            audioSource.volume = playerData.volume;
        }
        else
        {
            FirstSaveData();
        }

        // 한 스테이지에서 모두 별3개를 받으면 테두리 노란색 추가
        for(int i = 0; i < 6; i++)
        {
            int count = 0;
            for(int j = 0; j < 5; j++)
            {
                count += stageStarCounts[i * 5 + j];
            }
            if(count == 15)
            {
                selectStages[i].GetComponent<Outline>().enabled = true;
            }
        }
    }

    // 처음 저장할시 배열 초기화
    private void FirstSaveData()
    {
        PlayerData playerData = new PlayerData();

        // 초기화
        for(int i = 0; i < stageStarCounts.Length; i++)
        {
            stageStarCounts[i] = 0;
        }
        audioSource.volume = 0.5f;

        playerData.stageStarCounts = stageStarCounts;
        playerData.volume = audioSource.volume;

        File.WriteAllText(Application.persistentDataPath + "/PlayerData.json", JsonUtility.ToJson(playerData));
    }

    // Option설정 이후 저장해주는 함수
    public void OnOptionSave()
    {
        audioSource.volume = soundSlier.value;

        SaveData();
    }
    // Slider의 value를 불륨으로 설정해주는 함수
    public void OnOptionLoad()
    {
        soundSlier.value = audioSource.volume;
    }

    // 일시정지
    public void OnStopMenu()
    {
        Time.timeScale = 0;
        stopMenuPanel.SetActive(true);
    }
    public void OffStopMenu()
    {
        stopMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }
    // 메인메뉴로 돌아가기
    public void ExitAtStopMenu()
    {
        SceneManager.LoadScene("GameScene");
    }    


    public void OnStage1_1()
    {
        StartGame(stage1[0]);
        currentStageNum = 1;
    }
    public void OnStage1_2()
    {
        StartGame(stage1[1]);
        currentStageNum = 2;
    }
    public void OnStage1_3()
    {
        StartGame(stage1[2]);
        currentStageNum = 3;
    }
    public void OnStage1_4()
    {
        StartGame(stage1[3]);
        currentStageNum = 4;
    }
    public void OnStage1_5()
    {
        StartGame(stage1[4]);
        currentStageNum = 5;
    }


    public void OnStage2_1()
    {
        StartGame(stage2[0]);
        currentStageNum = 6;
    }
    public void OnStage2_2()
    {
        StartGame(stage2[1]);
        currentStageNum = 7;
    }
    public void OnStage2_3()
    {
        StartGame(stage2[2]);
        currentStageNum = 8;
    }
    public void OnStage2_4()
    {
        StartGame(stage2[3]);
        currentStageNum = 9;
    }
    public void OnStage2_5()
    {
        StartGame(stage2[4]);
        currentStageNum = 10;
    }


    public void OnStage3_1()
    {
        StartGame(stage3[0]);
        currentStageNum = 11;
    }
    public void OnStage3_2()
    {
        StartGame(stage3[1]);
        currentStageNum = 12;
    }
    public void OnStage3_3()
    {
        StartGame(stage3[2]);
        currentStageNum = 13;
    }
    public void OnStage3_4()
    {
        StartGame(stage3[3]);
        currentStageNum = 14;
    }
    public void OnStage3_5()
    {
        StartGame(stage3[4]);
        currentStageNum = 15;
    }


    public void OnStage4_1()
    {
        StartGame(stage4[0]);
        currentStageNum = 16;
    }
    public void OnStage4_2()
    {
        StartGame(stage4[1]);
        currentStageNum = 17;
    }
    public void OnStage4_3()
    {
        StartGame(stage4[2]);
        currentStageNum = 18;
    }
    public void OnStage4_4()
    {
        StartGame(stage4[3]);
        currentStageNum = 19;
    }
    public void OnStage4_5()
    {
        StartGame(stage4[4]);
        currentStageNum = 20;
    }


    public void OnStage5_1()
    {
        StartGame(stage5[0]);
        currentStageNum = 21;
    }
    public void OnStage5_2()
    {
        StartGame(stage5[1]);
        currentStageNum = 22;
    }
    public void OnStage5_3()
    {
        StartGame(stage5[2]);
        currentStageNum = 23;
    }
    public void OnStage5_4()
    {
        StartGame(stage5[3]);
        currentStageNum = 24;
    }
    public void OnStage5_5()
    {
        StartGame(stage5[4]);
        currentStageNum = 25;
    }


    public void OnStage6_1()
    {
        StartGame(stage6[0]);
        currentStageNum = 26;
    }
    public void OnStage6_2()
    {
        StartGame(stage6[1]);
        currentStageNum = 27;
    }
    public void OnStage6_3()
    {
        StartGame(stage6[2]);
        currentStageNum = 28;
    }
    public void OnStage6_4()
    {
        StartGame(stage6[3]);
        currentStageNum = 29;
    }
    public void OnStage6_5()
    {
        StartGame(stage6[4]);
        currentStageNum = 30;
    }
}
