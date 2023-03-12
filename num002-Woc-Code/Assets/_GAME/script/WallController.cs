using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WallController : MonoBehaviour
{
    public GameObject[] item;
    private List<Vector3> itemPositionList = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        InitMap();
    }

    // Update is called once per frame
    private void InitMap()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if(index==1)
        {
            for (int i = -5; i < 9; i++)
            {
                CreateItem(item[0], new Vector3(-15, i, 0), Quaternion.identity);
            }
            for (int i = -5; i < 9; i++)
            {
                CreateItem(item[0], new Vector3(15, i, 0), Quaternion.identity);
            }
        }
        else if(index==2)
        {
            for (int i = -5; i < 9; i++)
            {
                CreateItem(item[0], new Vector3(-29, i, 0), Quaternion.identity);
            }
            for (int i = -5; i < 9; i++)
            {
                CreateItem(item[0], new Vector3(38, i, 0), Quaternion.identity);
            }
        }
      
    }

    void Update()
    {

    }
    private void CreateItem(GameObject createCameObject, Vector3 createPosition, Quaternion createRotation)//优化
    {
        GameObject itemGo = Instantiate(createCameObject, createPosition, createRotation);
        itemGo.transform.SetParent(gameObject.transform);
        itemPositionList.Add(createPosition);
    }
}
