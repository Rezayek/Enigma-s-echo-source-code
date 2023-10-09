using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
public class ThreadedDataRequester : MonoBehaviour
{
    static ThreadedDataRequester instance;
    Queue<ThreadInfo> DataQueue = new Queue<ThreadInfo>();


    private void Awake()
    {
        instance = FindObjectOfType<ThreadedDataRequester>();
    }

    public static void RequestData(Func<object> genrateData, Action<object> callBack)
    {

        ThreadStart threadStart = delegate
        {
            instance.DataThread(genrateData, callBack);
        };

        new Thread(threadStart).Start();

    }



    void DataThread(Func<object> generateData, Action<object> callBack)
    {

        object data = generateData();
        lock (DataQueue)
        {
            DataQueue.Enqueue(new ThreadInfo(callBack, data));
        }

    }

    


    private void Update()
    {

        if (DataQueue.Count > 0)
        {
            for (int i = 0; i < DataQueue.Count; i++)
            {

                ThreadInfo threadInfo = DataQueue.Dequeue();

                threadInfo.callBack(threadInfo.parameter);
            }
        }

        
    }

    struct ThreadInfo
    {
        public readonly Action<object> callBack;
        public readonly object parameter;

        public ThreadInfo(Action<object> callBack, object parameter)
        {
            this.callBack = callBack;
            this.parameter = parameter;
        }
    }
}
