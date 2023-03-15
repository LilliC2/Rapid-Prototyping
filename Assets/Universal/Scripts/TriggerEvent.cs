using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : GameBehaviour
{
    public string[] triggerTag;

    public UnityEvent onTriggerEnterEvent;
    public UnityEvent onTriggerStayEvent;
    public UnityEvent onTriggerExitEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (GetComponent<Collider>() == null) return;

        if (triggerTag.Length == 0) onTriggerEnterEvent.Invoke();
        else
        {
            for (int i = 0; i < triggerTag.Length; i++)
            {
                if (other.CompareTag(triggerTag[i]) || triggerTag.Length == 0) onTriggerEnterEvent.Invoke();
            }
        }

        
        


    }

    private void OnTriggerStay(Collider other)
    {
        if (GetComponent<Collider>() == null) return;

        if (triggerTag.Length == 0) onTriggerEnterEvent.Invoke();
        else
        {
            for (int i = 0; i < triggerTag.Length; i++)
            {
                if (other.CompareTag(triggerTag[i]) || triggerTag.Length == 0) onTriggerStayEvent.Invoke();
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (GetComponent<Collider>() == null) return;

        if (triggerTag.Length == 0) onTriggerEnterEvent.Invoke();
        else
        {
            for (int i = 0; i < triggerTag.Length; i++)
            {
                if (other.CompareTag(triggerTag[i]) || triggerTag.Length == 0) onTriggerExitEvent.Invoke();
            }
        }

    }



}
