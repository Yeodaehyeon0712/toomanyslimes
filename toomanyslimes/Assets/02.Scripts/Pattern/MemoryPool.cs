using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MemoryPool<T> where T : Object
{
    Queue<T> m_pool;
    public MemoryPool(int capacity)
    {
        m_pool = new Queue<T>(capacity);
    }
    public void Register(T item)
    {
        m_pool.Enqueue(item);
    }
    public T GetItem()
    {
        return m_pool.Count > 0 ? m_pool.Dequeue() : null;
    }
    public void Clear()
    {
        while (m_pool.Count > 0)
        {
            var pop = m_pool.Dequeue();
            if (pop != null)
            {
                MonoBehaviour.DestroyImmediate(pop);
            }
        }
    }
}
