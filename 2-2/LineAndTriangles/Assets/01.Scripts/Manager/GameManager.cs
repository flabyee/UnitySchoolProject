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
// Ÿ�ݰ�
// Ʃ�丮��
// ī�޶� ����(endSelecter�� Mover�� ������ cinemachinCamera�̿�?)

[SerializeField]
public class PlayerData
{
    public int[] stageStarCounts;
    public float volume;
}

public class GameManager : MonoBehaviour
{
    // �̱���
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
    public GameObject menus;            // ���θ޴� â
    public GameObject btnPanel;         // ���� ��ư â1
    public GameObject btn2Panel;        // ���� ��ư â2
    public GameObject stopMenuPanel;    // �Ͻ����� â
    public GameObject endMovePanel;     // move ������ ������ â
    public GameObject resultPanel;      // ��� �˷��ִ� â
    public Text resultText;             

    [Header("OTHER")]
    private GameObject stage;           // ���� ��������
    private int currentStageNum;        // ���� ���������� ��ȣ

    public List<Transform> moveTrList = new List<Transform>();  // Character���� ������ ����Ʈ
    public List<Transform> endSelecterSpawnTrList = new List<Transform>();  // EndSelecter�� ������ġ ����Ʈ

    public int[] stageStarCounts;   // ������������ ���� �� �������� �迭

    private bool isEnd = false;     // EndMove�� 2�� �۵����� �ʰ���

    public Transform point;        // Mover���� �浹��ġ
    public Transform endSelecter;  // EndSelecter�� ��ġ
    private LineRenderer stageLine; // moveTrList�� ���������� Line
    public LineRenderer endLine;    // �浹��ġ�� EndSelecter�� ��ġ�� Line

    public ParticleSystem endEffect;    // endLine�� �׷����Ŀ� ������ effect

    private Coroutine drawEndLineCoroutine; // endLine�� �׷����� ���߿� ������ Coroutine�� �����ϱ� ���� ����

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
        // �̱���
        if(_instance != null)
        {
            Debug.Log("GameManager Singleton");
        }
        else
        {
            _instance = this;
        }

        // �ʱ�ȭ
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
        // Ŭ���ϸ� Ŭ������ ���
        if(Input.GetMouseButtonUp(0))
        {
            audioSource.clip = clickSound;
            
            audioSource.Play();
        }

        
    }
    
    // �������� ��ư�� ������ �ش� ���������� �Ű������� �޾Ƽ� ���� �ε��ϰ� startGame
    private void StartGame(GameObject nowStage)
    {
        LoadingMap(nowStage);

        startGame();

        menus.SetActive(false);
        btnPanel.SetActive(true);
    }

    // ������ ���������� �ش��ϴ� �� �ҷ����� LineRenderer�� �׸���
    private void LoadingMap(GameObject nowStage)
    {
        stage = Instantiate(nowStage);

        SetMoveTrs();

        // �� �׸���
        // To Do : ������ ���� ����?
        stageLine.positionCount = moveTrList.Count + 2;
        stageLine.startColor = Color.red;
        stageLine.endColor = Color.blue;
        for (int i = 0; i < moveTrList.Count + 2; i++)
        {
            stageLine.SetPosition(i, moveTrList[i % moveTrList.Count].position);
        }
    }

    // moveTrList�� EndSelecterSpawnTrList �Ҵ�
    private void SetMoveTrs()
    {
        if (stage != null)
        {
            // stage�� �ڽĿ��� tag�� MOVEPOINT�ΰ͵��� Transform�� moveTrList�� �Ҵ�
            moveTrList = (from item in stage.GetComponentsInChildren<Transform>()
                          where item.gameObject.CompareTag("MOVEPOINT")
                          select item.transform).ToList();


            stageLine = stage.GetComponent<LineRenderer>();

            // stage�� �ڽĿ��� tag�� SPAWNPOINT�ΰ͵��� Transform�� moveTrList�� �Ҵ�
            endSelecterSpawnTrList = (from item in stage.GetComponentsInChildren<Transform>()
                                      where item.gameObject.CompareTag("SPAWNPOINT")
                                      select item.transform).ToList();

            Debug.Log(endSelecterSpawnTrList.Count);
        }
    }

    // ��� ��ư�� ������ ����
    public void StartMove(Vector2 pos, int index, bool isLeft)
    {
        startMove(pos, index, isLeft);
        btnPanel.SetActive(false);
        btn2Panel.SetActive(true);
    }

    // ��� ��ư�� ������ ����
    public void StartCollision()
    {
        startCollision();

        btn2Panel.SetActive(false);
    }

    // Mover���� �浹���� ����
    public void EndMove()
    {
        if (!isEnd)
        {
            isEnd = true;

            endMove();

            // To Do : ���� �Ÿ��� �����ٶ� ���� ���������� endSelect�� ���� �����鼭 �� ���� ���� ���� ��������?
            endLine.positionCount = 2;
            endLine.SetPosition(0, point.transform.position);
            endLine.SetPosition(1, point.transform.position);
            endMovePanel.SetActive(true);

            drawEndLineCoroutine = StartCoroutine(DrawEndLine());



            float distance = (point.transform.position - endSelecter.transform.position).magnitude;
            resultText.text = String.Format("���� �Ÿ� : " + distance);


            // To Do : ���� ��ϰ� ���ؼ� �� ������ ��쿡�� �ٽ� ����
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

    // Mover���� �浹������ EndSelecter���̿� Line�� �׸�
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

    // EndMove���Ŀ� ���� endMovePanel�� ��ư�� ������ ����
    public void OnResult()
    {
        endMovePanel.SetActive(false);
        resultPanel.SetActive(true);
    }

    // OnResult���Ŀ� ���� resultPanel�� ��ư�� ������ ����
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

    // Json���� ����
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

        // �� ������������ ��� ��3���� ������ �׵θ� ����� �߰�
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

    // ó�� �����ҽ� �迭 �ʱ�ȭ
    private void FirstSaveData()
    {
        PlayerData playerData = new PlayerData();

        // �ʱ�ȭ
        for(int i = 0; i < stageStarCounts.Length; i++)
        {
            stageStarCounts[i] = 0;
        }
        audioSource.volume = 0.5f;

        playerData.stageStarCounts = stageStarCounts;
        playerData.volume = audioSource.volume;

        File.WriteAllText(Application.persistentDataPath + "/PlayerData.json", JsonUtility.ToJson(playerData));
    }

    // Option���� ���� �������ִ� �Լ�
    public void OnOptionSave()
    {
        audioSource.volume = soundSlier.value;

        SaveData();
    }
    // Slider�� value�� �ҷ����� �������ִ� �Լ�
    public void OnOptionLoad()
    {
        soundSlier.value = audioSource.volume;
    }

    // �Ͻ�����
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
    // ���θ޴��� ���ư���
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
