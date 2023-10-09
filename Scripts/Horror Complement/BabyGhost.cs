using UnityEngine;

public class BabyGhost : HorrorComplementsAbstract
{

    // Start is called before the first frame update
    void Start()
    {
        audioCaller = AudioEventCaller.Instance;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        isUsed = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerIsNear();
    }
}
