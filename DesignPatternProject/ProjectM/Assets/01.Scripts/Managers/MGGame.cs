using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class MGGame : MonoBehaviour
{
    // public MGTeam _gTeamManager;
    // public MGStage _gStageManager;
    // public MGMinion _gMinionManager;
    // public MGHero.MGHero _gHeroManager;


    // hero
    List<CONEntity> heroConList = new List<CONEntity>();

    // archer
    List<CONEntity> archerConList = new List<CONEntity>();
    public int archerCount = 24;
    public int rowArcherCount = 3; // 한줄에 배치할 궁수의 갯수
    public Transform archerSpawnTrm;
    Coroutine shootArrowCor;
    public float rateOfShoot = 1f;

    // enemy
    List<CONEntity> enemyConList = new List<CONEntity>();
    public Transform enemySpawnPos;

    // castle
    public Transform castleTrm;
    public int maxCastleHp = 100;
    private int curCastleHp;


    public EventSO stageStart;
    public EventSO stageEnd;
    public EventSO shootArrow;


    void Awake()
    {
        GameSceneClass.gMGGame = this;

        // GameSceneClass._gColManager = new MGUCCollider2D();

        // _gTeamManager = new MGTeam();
        // _gStageManager = new MGStage();
        // _gMinionManager = new MGMinion();
        // _gHeroManager = new MGHero.MGHero();

        // Global._gameStat = eGameStatus.Playing;

        GameObject.Instantiate(Global.prefabsDic[ePrefabs.MainCamera]);

        heroConList.Clear();

        curCastleHp = maxCastleHp;

        SpawnArcher();
    }

    void OnEnable()
    {
        // GamePlayData.init();
        // MGGameStatistics.instance.initData();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CONEntity heroCon = GameSceneClass.gMGPool.CreateObj(ePrefabs.HeroGirl, UnityEngine.Random.insideUnitCircle);
            heroConList.Add(heroCon);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (heroConList.Count > 0)
            {
                heroConList[heroConList.Count - 1].SetActive(false);
                heroConList.RemoveAt(heroConList.Count - 1);
            }

        }

    }

    void LateUpdate()
    {
        
    }

    void SpawnArcher()
    {
        Vector2 pos = new Vector2();

        int yCount = archerCount / rowArcherCount;  
        int remainCount = archerCount % rowArcherCount;  

        for(int y = 0; y < yCount; y++)
        {
            for(int x = 0; x < rowArcherCount; x++)
            {
                CONEntity archer = GameSceneClass.gMGPool.CreateObj(ePrefabs.GoblinArcher, archerSpawnTrm.position + new Vector3(x * 2f, y * 2f, 0));
                archerConList.Add(archer);
            }
        }
    }

    void SpawnEnemy()
    {
        Vector2 pos = enemySpawnPos.position + new Vector3(0, UnityEngine.Random.Range(-7f, 3f), 0);

        CONEntity enemy = GameSceneClass.gMGPool.CreateObj(ePrefabs.OrcWarrior, pos);
        enemyConList.Add(enemy);
    }

    public void DestoryEnemy(CONEntity conEntity)
    {
        enemyConList.Remove(conEntity);
        conEntity.SetActive(false);
    }

    public void StageStart()
    {
        stageStart.Occurred();

        if(shootArrowCor == null)
        {
            shootArrowCor = StartCoroutine(ShootArrowCor());
        }
        StartCoroutine(SpawnEnemyCor());

        curCastleHp = maxCastleHp;
    }

    public void StageEnd()
    {
        stageEnd.Occurred();

        StopCoroutine(shootArrowCor);

        shootArrowCor = null;


        foreach(CONEntity item in enemyConList)
        {
            item.SetActive(false);
        }
        enemyConList.Clear();

    }

    IEnumerator ShootArrowCor()
    {
        while(true)
        {
            yield return new WaitForSeconds(rateOfShoot);

            // 제일 앞에 있는 적 찾아서 보내주기
            if(enemyConList.Count > 0)
            {
                shootArrow.Occurred(enemyConList.OrderBy(x => x.transform.position.x).First().gameObject);
            }
        }
    }

    IEnumerator SpawnEnemyCor()
    {
        for(int i = 0; i < 15; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void OnCastleDamaged(int damage)
    {
        curCastleHp -= damage;
        if(curCastleHp <= 0)
        {
            StageEnd();
        }

        GameSceneClass.gUiRootGame.SetHpBar((float)curCastleHp / (float)maxCastleHp);
    }
}
