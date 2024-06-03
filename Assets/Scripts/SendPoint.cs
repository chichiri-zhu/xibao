using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SendPoint : MonoBehaviour
{
    public List<LevelEnemyAmount> levelEnemyAmountList;
    private List<Transform> enemyList;
    public bool isEnemyInitFinish;
    public bool isClear;
    public bool isBattle;
    [SerializeField] private Transform tipListTransform;

    private List<EnemySetoutTimer> currentEnemySetoutList;

    private void Start()
    {
        InitSendEnemyData();
        GameManager.Instance.OnBattleStart += Instance_OnBattleStart;
        GameManager.Instance.OnBattleEnd += Instance_OnBattleEnd;
        GameManager.Instance.OnPrepareStart += Instance_OnPrepareStart;
    }

    private void Instance_OnPrepareStart(object sender, System.EventArgs e)
    {
        InitSendEnemyData();
    }

    private void Update()
    {
        if (isBattle && isEnemyInitFinish)
        {
            isClear = enemyList.FindAll(obj => obj != null).Count() <= 0;
        }
    }

    private void Instance_OnBattleStart(object sender, System.EventArgs e)
    {
        isClear = false;
        isEnemyInitFinish = false;
        isBattle = true;
        InitEnemys();
    }

    private void Instance_OnBattleEnd(object sender, System.EventArgs e)
    {
        ClearEnemys();
        isBattle = false;
    }

    private void InitSendEnemyData()
    {
        int level = LevelManager.Instance.GetLevel();
        Debug.Log(level);
        LevelEnemyAmount levelEnemyAmount = levelEnemyAmountList.FirstOrDefault(obj => obj.level == level);
        if(levelEnemyAmount != null)
        {
            currentEnemySetoutList = levelEnemyAmount.enemySetoutList;
        }
        else
        {
            currentEnemySetoutList = new List<EnemySetoutTimer>();
        }

        for (int i = 0; i < tipListTransform.childCount; i++)
        {
            Destroy(tipListTransform.GetChild(i).gameObject);
        }

        Dictionary<ArmsSO, int> armsAmountDic = new Dictionary<ArmsSO, int>();
        foreach (EnemySetoutTimer item in currentEnemySetoutList)
        {
            SoldierAmount soldierAmount = item.soldierAmount;
            if (armsAmountDic.ContainsKey(soldierAmount.soldier))
            {
                armsAmountDic[soldierAmount.soldier] += soldierAmount.amount;
            }
            else
            {
                armsAmountDic.Add(soldierAmount.soldier, soldierAmount.amount);
            }
        }

        if(armsAmountDic.Count > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            foreach (var item in armsAmountDic)
            {
                SendPointTipItem.Create(item.Key, item.Value, tipListTransform);
            }
        }
        else
        {
            transform.localScale = new Vector3(0, 0, 0);
        }
    }

    private List<Coroutine> enemySetoutCoroutines = new List<Coroutine>();
    private List<EnemySetoutTimer> isFinishFlagList = new List<EnemySetoutTimer>();
    private void InitEnemys()
    {
        isEnemyInitFinish = false;
        enemyList = new List<Transform>();
        foreach (EnemySetoutTimer enemySetoutTimer in currentEnemySetoutList)
        {
            //StartCoroutine(_EnemySetOut(enemySetoutTimer.soldierAmount, enemySetoutTimer.setoutTimer));
            // 保存协程引用
            Coroutine coroutine = StartCoroutine(_EnemySetOut(enemySetoutTimer));
            enemySetoutCoroutines.Add(coroutine);

        }
        //isEnemyInitFinish = true;
        // 检查所有协程是否执行完成
        StartCoroutine(CheckEnemySetoutCoroutines());
    }

    IEnumerator _EnemySetOut(EnemySetoutTimer enemySetoutTimer)
    {
        float setoutTimer = enemySetoutTimer.setoutTimer;
        isFinishFlagList.Add(enemySetoutTimer);
        yield return new WaitForSeconds(setoutTimer);
        SoldierAmount enemyAmount = enemySetoutTimer.soldierAmount;
        for (int i = 1; i <= enemyAmount.amount; i++)
        {
            Vector3 enemyPosition = transform.position + UtilsClass.GetRandomDir() * UnityEngine.Random.Range(0f, 2f);

            //Enemy enemy = Enemy.Create(enemyAmount.soldier, enemyPosition);
            //enemy.transform.SetParent(transform, true);

            //enemyList.Add(enemy.transform);
            SetOutEnemy(enemyAmount.soldier, enemyPosition);
        }

        if (isFinishFlagList.Contains(enemySetoutTimer))
        {
            isFinishFlagList.Remove(enemySetoutTimer);
        }
        // 返回协程引用
        //yield return StartCoroutine(EnemySetoutCoroutine());
        //yield return null;
    }

    public Enemy SetOutEnemy(ArmsSO enemyArms, Vector3 spawnPosition)
    {
        Enemy enemy = Enemy.Create(enemyArms, spawnPosition);
        enemy.transform.SetParent(transform, true);
        enemyList.Add(enemy.transform);
        //OnEnemySetOut?.Invoke(this, new OnSoldierSetOutArgs() { soldierTransform = enemy.transform });
        return enemy;
    }

    private IEnumerator CheckEnemySetoutCoroutines()
    {
        while (isFinishFlagList.Count > 0)
        {
            yield return null;
        }
        isEnemyInitFinish = true;
    }

    private void ClearEnemys()
    {
        if(enemyList == null)
        {
            return;
        }
        foreach (Transform enemy in enemyList)
        {
            if (enemy != null)
            {
                Destroy(enemy.gameObject);
            }
        }
        enemyList.Clear();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnBattleStart -= Instance_OnBattleStart;
        GameManager.Instance.OnBattleEnd -= Instance_OnBattleEnd;
    }
}
