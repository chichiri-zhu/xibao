using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;
using System.Linq;
using Excel;
using Newtonsoft.Json;

public class ExcelToLevelData : MonoBehaviour
{
    [SerializeField] private Transform SendManagerTransform;
    [SerializeField] private List<LevelEnemyAmount> levelEnemyAmountList;
    public List<SendPoint> sendPointList;

    void Start()
    {
        sendPointList = SendManagerTransform.GetComponentsInChildren<SendPoint>().ToList();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SavePlayComponent();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ReadExcel("/Data/test.xlsx");
        }
    }

    public void SavePlayComponent()
    {
        Player player = GameManager.Instance.GetPlayer();
        Component[] components = player.gameObject.GetComponents<Component>();

        Dictionary<string, string> componentData = new Dictionary<string, string>();
        foreach (Component component in components)
        {
            Debug.Log(component);
            // 将组件转换为JSON字符串
            string json = JsonConvert.SerializeObject(component);

            // 将组件类型和JSON数据添加到字典中
            componentData.Add(component.GetType().ToString(), json);
        }
        string jsonData = JsonConvert.SerializeObject(componentData);
        Debug.Log(jsonData);
    }

    public void ReadExcel(string xmlName)
    {
        FileStream stream = File.Open(Application.dataPath + xmlName, FileMode.Open, FileAccess.Read, FileShare.Read);
        //IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);//读取 Excel 1997-2003版本
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);//读取 2007及以后的版本

        DataSet result = excelReader.AsDataSet();

        if (stream != null)
        {
            stream.Close();
        }

        int[] counts = GetCount(result.Tables[0]);
        int rows = counts[0];
        int columns = counts[1];
        //Debug.LogError("row:" + rows + "...col:" + columns);
        levelEnemyAmountList = new List<LevelEnemyAmount>();
        SoldierListSO soldierListSO = AssetManager.Instance.soldierListSO;
        for (int i = 0; i < rows; i++)
        {
            int level = int.Parse(result.Tables[0].Rows[i][0].ToString());
            string name = result.Tables[0].Rows[i][1].ToString();
            int num = int.Parse(result.Tables[0].Rows[i][2].ToString());
            float yanchiTime = float.Parse(result.Tables[0].Rows[i][3].ToString());
            int pointNum = int.Parse(result.Tables[0].Rows[i][4].ToString());

            if(pointNum != 1)
            {
                continue;
            }

            LevelEnemyAmount levelEnemyAmount = levelEnemyAmountList.FirstOrDefault(obj => obj.level == level);
            if(levelEnemyAmount == null)
            {
                levelEnemyAmount = new LevelEnemyAmount();
                levelEnemyAmount.level = level;
                levelEnemyAmount.enemySetoutList = new List<EnemySetoutTimer>();
                levelEnemyAmountList.Add(levelEnemyAmount);
            }
            EnemySetoutTimer addEnemySetoutTimer = new EnemySetoutTimer();
            addEnemySetoutTimer.setoutTimer = yanchiTime;

            ArmsSO arm = soldierListSO.soldierList.FirstOrDefault(obj => obj.nameString == name);
            if(arm != null)
            {
                SoldierAmount soldierAmount = new SoldierAmount();
                soldierAmount.soldier = arm;
                soldierAmount.amount = num;
                addEnemySetoutTimer.soldierAmount = soldierAmount;
                levelEnemyAmount.enemySetoutList.Add(addEnemySetoutTimer);

                Debug.Log(level + "," + name + "," + num + "," + yanchiTime + "," + pointNum);
            }
            
            //for (int j = 0; j < columns; j++)
            //{
            //    Debug.LogError(result.Tables[0].Rows[i][j].ToString());
            //}
        }
    }

    private int[] GetCount(DataTable dt)
    {
        int i = dt.Rows.Count;
        for (int m = 0; m < dt.Rows.Count; m++)
        {
            if (string.IsNullOrEmpty(dt.Rows[m][0].ToString()))
            {
                i = m;
                break;
            }
        }

        int j = dt.Columns.Count;
        for (int n = 0; n < dt.Columns.Count; n++)
        {
            if (string.IsNullOrEmpty(dt.Rows[0][n].ToString()))
            {
                j = n;
                break;
            }
        }
        return new int[] { i, j };
    }
}
