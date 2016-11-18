using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class GestureController : MonoBehaviour {
    public enum GestureEnumOption
    {
        moveWithRightHand,
        rotateWithRightHand,
        rotateWithTwoHand,
        rotateWithOneHandClosed,
        rotateWithOneHandClosedAndMove
    };

    public GestureEnumOption gestureOption;
    public float rotationSmooth;
    private bool closesHand;
    public GameObject handLeft;
    public GameObject handRight;
    public GameObject kinectController;
    private BodySourceManager _bodyManager;
    private Vector3 originPosition;
    private Vector3 startPosition;
    private bool isFirstStart;

    // Use this for initialization
    void Start () {
        isFirstStart = true;
        originPosition = this.transform.position;
	}

    // Update is called once per frame
    void Update()
    {

        if (isTracked())
        {
            if (gestureOption == GestureEnumOption.moveWithRightHand)
            {
                this.transform.position = handRight.transform.position;
            }
            if (gestureOption == GestureEnumOption.rotateWithRightHand)
            {
                Vector3 rotationVector = handRight.transform.position * rotationSmooth;
                this.transform.rotation = Quaternion.LookRotation(rotationVector);
            }

            if (gestureOption == GestureEnumOption.rotateWithTwoHand)
            {
                Vector3 rotationVector = (handRight.transform.position - handLeft.transform.position) * rotationSmooth;
                this.transform.rotation = Quaternion.LookRotation(rotationVector);
            }

            if (gestureOption == GestureEnumOption.rotateWithOneHandClosed && closesHand)
            {
                Vector3 rotationVector = (handRight.transform.position - handLeft.transform.position) * rotationSmooth;
                this.transform.rotation = Quaternion.LookRotation(rotationVector);
            }

            if (gestureOption == GestureEnumOption.rotateWithOneHandClosedAndMove && closesHand)
            {
                Vector3 rotationVector = (handRight.transform.position - handLeft.transform.position) * rotationSmooth;
                this.transform.rotation = Quaternion.LookRotation(rotationVector);
                if (isFirstStart)
                {
                    startPosition = (handRight.transform.position+handLeft.transform.position)/2;
                    isFirstStart = false;
                }
                Vector3 diffPosition = (handRight.transform.position + handLeft.transform.position) / 2 - startPosition;
                this.transform.position = originPosition + diffPosition;
            }
            else
            {
                isFirstStart = true;
            }
        }
    }

    public bool isTracked()
    {
        if (kinectController== null)
        {
            return false;
        }
        _bodyManager = kinectController.GetComponent<BodySourceManager>();
        if (_bodyManager == null)
        {
            return false;
        }
        Body[] data = _bodyManager.GetData();
        if (data == null)
        {
            return false;
        }
        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }
            if (body.IsTracked)
            {
                if (body.HandLeftState == HandState.Closed || body.HandRightState == HandState.Closed)
                {
                    closesHand = true;
                }
                else
                {
                    closesHand = false;
                }
                return true;
            }
        }

        return false;
    }
    
}
