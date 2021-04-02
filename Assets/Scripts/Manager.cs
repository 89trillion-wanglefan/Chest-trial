using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 主要控制类
/// </summary>
public class Manager : MonoBehaviour
{
    public GameObject Button;//获得游戏资源
    public GameObject Shop;//商店显示UI
    public GameObject coins;//管理金币用的空物体
    public GameObject coin;//金币的prefab
    public Text walletText;//金钱显示
    public int gain = 0;//点击增加金钱数量
    public Animator anim;//宝箱动画机
    public GameObject Effect;//宝箱粒子特效
    public GameObject Chest;//宝箱
    public GameObject JumpGround;//动画播放背景
    public GameObject TopCoin;//HUD中的金币
    private int wallet = 0;//设置钱包
    bool trig = false;//保证金币增加一次
    private static readonly int Buy = Animator.StringToHash("buy");//预存动画机状态，提高效率

    /// <summary>
    /// 购买宝箱按键
    /// </summary>
    public void Click1()
    {
        
        anim.SetBool(Buy, true);
        
        Effect.SetActive(false);//关闭粒子特效
        Chest.transform.localPosition = new Vector3(0, 368, 0);//复用资源
        Chest.transform.localScale = new Vector3(3, 3, 3);
        JumpGround.SetActive(true);//弹窗
        trig = true;
        
        

    }
    
    /// <summary>
    /// 开始界面按键
    /// </summary>
    public void ToShop()
    {
        Button.SetActive(false);//打开商店
        Shop.SetActive(true);
    }
    
    /// <summary>
    /// 数字滚动增加动画
    /// </summary>
    /// 起始值
    /// <param name="startValue"></param>
    /// 结束值
    /// <param name="targetValue"></param>
    public void IncreaseAnim(int startValue, int targetValue)
    {
        var se = DOTween.Sequence();
        se.Append(DOTween.To(delegate (float value)
        {
            var temp = Mathf.FloorToInt(value);

            walletText.text = temp + "";
            

        }, startValue, targetValue, 2f));
    }

    /// <summary>
    /// 宝箱关闭动画播放结束时执行，切换宝箱动画与状态
    /// </summary>
    public void CloseEnd()
    {
  
        gain += 5;
        IncreaseAnim(wallet, wallet + gain);
        wallet += gain;
        StartCoroutine(CreateCoin(gain));
        Effect.SetActive(true);//箱子打开，开始发光，光和下半部分位置一致

    }

    /// <summary>
    /// 购买时宝箱打开动画播放结束时执行，切换宝箱动画与状态
    /// </summary>
    public void OpenEnd()
    {
        if (trig != true) return;
        Chest.transform.localPosition = new Vector3(0, 0, 0);
        Chest.transform.localScale = new Vector3(1, 1, 1);
        JumpGround.SetActive(false);
        trig = false;
    }

    /// <summary>
    /// 协程实现金币出生动画
    /// </summary>
    /// <param name="quantity"></param>
    /// <returns></returns>
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
