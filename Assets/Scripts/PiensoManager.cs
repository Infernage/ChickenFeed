using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiensoManager : MonoBehaviour {

    // Handler declaration
    public event EventHandler<FeedKillEventArgs> FeedKilled;

    List<GameObject> FeedList;
    List<GameObject> FeedToDelete;

    const int MaximumFeed = 3;
    int FeedAmount;

    

	// Use this for initialization
	void Start () {
        FeedList = new List<GameObject>();
        FeedToDelete = new List<GameObject>();
        FeedAmount = 0;
	}

    public GameObject AddPienso(Vector2 InitialLocation)
    {
        if (FeedAmount < MaximumFeed)
        {
            GameObject pruebaPrefab = (GameObject)Resources.Load("Prefabs/PruebaFeed");

            //Pienso FeedPointer = gameObject.AddComponent<Pienso>();

           GameObject FeedPointer =  Instantiate(pruebaPrefab,InitialLocation,Quaternion.identity);
            FeedPointer.GetComponent<Pienso>().Location = InitialLocation;
            FeedPointer.GetComponent<Pienso>().Start();
            
            //pruebaPrefab.Location = InitialLocation;
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
        foreach(GameObject item in FeedList)
        {
            if(item.GetComponent<Pienso>().RemainingTime<=0.0f)
                FeedToDelete.Add(item);

            //Debug.Log(item.GetComponent<Pienso>().RemainingTime);
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

            Debug.Log("Algo para borrar");


            GameObject PS_1 = (GameObject)Resources.Load("Prefabs/PS_Feather_1");
            GameObject PS_2 = (GameObject)Resources.Load("Prefabs/PS_Feather_2");

            //PS_1.GetComponent<ParticleSystem>().Play();
            //PS_2.GetComponent<ParticleSystem>().Play();

            GameObject PS_Pointer = Instantiate(PS_1);
            GameObject PS_Pointer_2 = Instantiate(PS_2);
            PS_Pointer.GetComponent<ParticleSystem>().Play();
            PS_Pointer_2.GetComponent<ParticleSystem>().Play();

        }

	}

    protected virtual void OnFeedKilled(FeedKillEventArgs e)
    {
        FeedKilled?.Invoke(this, e);
    }
}

public class FeedKillEventArgs : EventArgs
{
    public int FeedAmount { get; set; }
}
