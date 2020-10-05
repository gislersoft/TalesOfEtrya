using UnityEngine;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

public class _Environment : MonoBehaviour {

    public static int GlobalCounter;
    public static int MaxSteps;

    public int threadStep;
    public int maxThreadStep;

    public _Agent aradis;
    public Queue<ActionThreadInfo> threadInfoQueue;

    public double reward;
    public List<float> accumRewards;
    public double value;
    public bool done;

    public double gamma;

    public void Start()
    {
        SetUp();
        //RequestStep();
    }

    private void Update()
    {
        //if (threadInfoQueue.Count > 0)
        //{
        //    ActionThreadInfo actionThreadInfo = threadInfoQueue.Dequeue();
        //    actionThreadInfo.callback(actionThreadInfo.selectedAction);

        //    RequestStep();
        //}
        if(Input.GetKeyDown(KeyCode.A))
            StartCoroutine(RequestStepCo());
    }

    public void SetUp()
    {
        GlobalCounter = 0;
        threadStep = 0;
        accumRewards = new List<float>();
        threadInfoQueue = new Queue<ActionThreadInfo>();
        aradis.SetUp();
        //player.SetUp();
    }

    public void ResetEnvironment()
    {
        threadStep = 0;
        accumRewards.Clear();
        aradis.SetUp();
        StartCoroutine(RequestStepCo());
    }

    public IEnumerator RequestStepCo()
    {
        int action = aradis.GetAction();

        yield return null;

        PerformAction(action);

        yield return new WaitForSeconds(1f);
    }

    public void RequestStep()
    {
        Debug.Log("Requesting action");
        ThreadStart threadStart = delegate
        {
            var action = aradis.GetAction();

            lock (threadInfoQueue)
            {
                threadInfoQueue.Enqueue(new ActionThreadInfo(action, PerformAction));
            }
        };

        new Thread(threadStart).Start();
    }

    public void PerformAction(int selectedAction)
    {
        Debug.Log("Performing action: " + selectedAction);
        GlobalCounter++;
        threadStep++;
        float episodeReward = 0;
        if(selectedAction == (int)_AradisActions.MOVE_LEFT)
        {
            episodeReward = aradis.MoveForward();
        }
        else if(selectedAction == (int)_AradisActions.MOVE_RIGHT)
        {
            episodeReward = aradis.MoveBackward();
        }
        else if (selectedAction == (int)_AradisActions.JUMP)
        {
            episodeReward = aradis.Jump();
        }
        else if (selectedAction == (int)_AradisActions.ATTACK)
        {
            episodeReward = aradis.Attack();
        }

        if (aradis.KilledEnemy())
        {
            episodeReward = aradis.rewardsDict["Won"];
            done = true;
        }

        if (aradis.Died())
        {
            episodeReward = aradis.rewardsDict["Lose"];
            done = true;
        }

        accumRewards.Add(episodeReward);

        if(!done && threadStep < maxThreadStep)
        {
            StartCoroutine(RequestStepCo());
        }
        else
        {
            EndEpisode();
        }
            
    }

    public void EndEpisode()
    {
        aradis.UpdateNetworks(accumRewards);
        ResetEnvironment();
    }
}

public struct ActionThreadInfo
{
    public int selectedAction;
    public Action<int> callback;

    public ActionThreadInfo(int selectedAction, Action<int> callback)
    {
        this.selectedAction = selectedAction;
        this.callback = callback;
    }
}

