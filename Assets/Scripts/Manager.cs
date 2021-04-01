using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Manager : MonoBehaviour
{
    public GameObject Button;//获得游戏资源
    public GameObject Shop;
    public GameObject coins;
    public GameObject coin;
    public Text text;
    public int gain = 0;
    public Animator anim;
    public GameObject Effect;
    public GameObject Chest;
    public GameObject Jump;
    public GameObject TopCoin;
    private int wallet = 0;//设置钱包
    bool trig = false;//保证跳回一次
    private static readonly int Buy = Animator.StringToHash("buy");

    public void Click1()
    {
        
        anim.SetBool(Buy, true);
        
        Effect.SetActive(false);//关闭粒子特效
        Chest.transform.localPosition = new Vector3(0, 368, 0);//复用资源
        Chest.transform.localScale = new Vector3(3, 3, 3);
        Jump.SetActive(true);//弹窗
        trig = true;
        
        

    }
    
    public void ToShop()
    {
        Button.SetActive(false);//打开商店
        Shop.SetActive(true);
    }
    
    public void IncreaseAnim(int startValue, int targetValue)//数字滚动增加
    {
        var se = DOTween.Sequence();
        se.Append(DOTween.To(delegate (float value)
        {
            var temp = Mathf.FloorToInt(value);

            text.text = temp + "";
            

        }, startValue, targetValue, 2f));
    }

    public void OpenEnd()
    {
  
        gain += 5;
        IncreaseAnim(wallet, wallet + gain);
        wallet += gain;
        StartCoroutine(CreateCoin(gain));
        Effect.SetActive(true);//箱子打开，开始发光，光和下半部分位置一致

    }

    public void CloseEnd()
    {
        if (trig != true) return;
        Chest.transform.localPosition = new Vector3(0, 0, 0);
        Chest.transform.localScale = new Vector3(1, 1, 1);
        Jump.SetActive(false);
        trig = false;
    }

    private IEnumerator CreateCoin(int quantity)
    {
        anim.SetBool(Buy, false);
        
        for (var temp = Mathf.Min(quantity,15); temp > 0; temp--)
        {
            GameObject mov=GameObject.Instantiate(coin, coins.transform, false);
            
            mov.transform.localScale = new Vector3 ( 0f, 0f, 0f );
            mov.transform.DOScale(new Vector3(0.2f/3, 0.2f/3, 0.2f/3), 1f).OnComplete(()=>//箱子大了三倍，金币生成后先放大
            {
                mov.transform.DOMove(TopCoin.transform.position, 1).OnComplete(()=>//放大完成，移动
                {
                    GameObject.Destroy(mov);//到位置后销毁
                });
            });
            
            yield return new WaitForSeconds(0.1f);//在生成下一个硬币前等待0.1秒
        }
        
    }
}
