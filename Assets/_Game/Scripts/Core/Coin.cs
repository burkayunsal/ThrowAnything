using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
  public void CoinMovement()
  {
      Vector3 coinJumpPosition = new Vector3(Random.Range(0,2.1f), 0.5f, Random.Range(0,2.1f));
      transform.DOJump(transform.position + coinJumpPosition,3f,1,.3f).SetEase(Ease.Linear).OnComplete(() =>
      {
          transform.parent = GameObject.FindWithTag("PlayerParent").transform;
          
          transform.DOLocalMove(new Vector3(0,.5f,0),.3f).SetDelay(.05f).SetEase(Ease.InBounce).OnComplete(() =>
          {
              Destroy(gameObject);
              SaveLoadManager.AddCoin(1);
          });
      });
  }
}
