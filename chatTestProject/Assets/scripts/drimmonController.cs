using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drimmonController : MonoBehaviour
{

    int curState = 2;
    Vector3 transformValue = Vector2.left * 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transformValue, Space.World);//平移角色
    }
    public void setState(int currState)
    {
        float speed = 20.0f;
        Vector3 transformValue = new Vector2();//定义平移向量
        switch (currState)
        {
            case 0://角色状态向右时，角色不断向右缓慢移动
                Debug.Log("向右");
                this.curState = 0;
                this.transformValue = Vector2.right * Time.deltaTime * speed;
                break;
            case 1://角色状态向左时，角色不断向左缓慢移动
                Debug.Log("向左");
                this.curState = 1;
                this.transformValue = Vector2.left * Time.deltaTime * speed;
                break;
            case 2:
                this.transformValue = Vector2.left * Time.deltaTime * 0;
                break;
        }
    }

}
