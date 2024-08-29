using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBat : MonoBehaviour
{
    private bool isRemove;
    public bool IsRemove
    {
        get
        {
            return isRemove;
        }
    }

    private IEnumerator RemoveCoroutine()
    {
        yield return null;
        Destroy(gameObject);
    }
    public void Remove()
    {
        isRemove= true;
        StartCoroutine(RemoveCoroutine());
    }
}
