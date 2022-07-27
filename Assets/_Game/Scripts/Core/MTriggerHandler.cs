using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MTriggerHandler : MonoBehaviour
{
    Dictionary<string, UnityEvent> dicEvts = new Dictionary<string, UnityEvent>();

    [SerializeField] CollisionRules[] rules;

    private void Start()
    {
        foreach (CollisionRules cr in rules)
        {
            dicEvts.Add(cr._tag, cr.evt);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (dicEvts.ContainsKey(collision.gameObject.tag))
        {
            if (dicEvts.TryGetValue(collision.tag, out UnityEvent evt))
            {
                evt?.Invoke();
            }
        }
    }


    [System.Serializable]
    public class CollisionRules
    {
        public string _tag;
        public UnityEvent evt;
    }
}
