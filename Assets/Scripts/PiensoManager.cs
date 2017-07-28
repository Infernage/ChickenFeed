using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiensoManager : MonoBehaviour {

    // Handler declaration
    public event EventHandler<FeedKillEventArgs> FeedKilled;

    List<Feed> FeedList;
    List<Feed> FeedToDelete;

    const int MaximumFeed = 3;
    int FeedAmount;

    

	// Use this for initialization
	void Start () {
        FeedList = new List<Feed>();
        FeedToDelete = new List<Feed>();
        FeedAmount = 0;
	}

    public Feed AddPienso(Vector2 InitialLocation)
    {
        if (FeedAmount < MaximumFeed)
        {
            Feed FeedPointer = new Feed(InitialLocation);
            FeedList.Add(FeedPointer);
            FeedAmount++;
            return FeedPointer;
        }
        else
            return null;

    }
	
	// Update is called once per frame
	void Update () {

        //Check if feed has no time remaining
        foreach(Feed item in FeedList)
        {
            if(item.RemainingTime<=0.0f)
                FeedToDelete.Add(item);
        }

        for(int i = FeedToDelete.Count-1;i >=0; i--)
        {
            FeedList.Remove(FeedToDelete[i]);
            Destroy(FeedToDelete[i]);
            FeedToDelete.RemoveAt(i);
            FeedAmount--;

            //Launch event OnFeedKilled
            FeedKillEventArgs args = new FeedKillEventArgs();
            args.FeedAmount = FeedAmount;
            OnFeedKilled(args);

        }

	}

    protected virtual void OnFeedKilled(FeedKillEventArgs e)
    {
        EventHandler<FeedKillEventArgs> handler = FeedKilled;
        if (handler != null)
        {
            handler(this, e);
        }
    }
}

public class FeedKillEventArgs : EventArgs
{
    public int FeedAmount { get; set; }
}
