using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BallMachineManager : MonoBehaviour
{
    public GameObject ballMatchineUI;
    public Transform[] allUI;

    public Slider pointSlider;//31
    public Slider speedSlider;//37
    public Slider rotationDirectionXSlider;//43
    public Slider rotationDirectionYSlider;//49
    public Slider rotationDirectionZSlider;//55
    public Slider rotationSpeedSlider;//61
    public Slider timeIntervalSlider;//67
    public Slider cycleNumberSlider;//73 



    public Text pointText;//13
    public Text speedText;//14
    public Text rotationalXText;//15
    public Text rotationalYText;//16
    public Text rotationalZText;//17
    public Text rotationalSpeedText;//18
    public Text fireTimeText;//19
    public Text cycleNumbetText;//20


    public Button topSpinButton;//22
    public Button bottomSpinButton;//24
    public Button begainButton;//26
    public Button endButton;//28

    public int point;
    public int speed;
    public Vector3 rotaionVector;
    public int rotaionSpeed;
    public float timeInterval;
    public int cyclesNumber;
    public int currentNumber;

    public bool start;
    public bool paused;
    public bool end;
    public void RotateHead(float angle)
    {
        transform.Rotate(new Vector3(0,0,angle), Space.Self);
    }
    public void Booting()
    {
        if (start)
        {
            
            for (int i = currentNumber; i < cyclesNumber; i++)
            {
                StartCoroutine(CreatBollC());
                if (paused)
                {
                    currentNumber = i;
                    break;
                }
                if (end)
                {
                    currentNumber = 0;
                    break;
                }
                
            }
        }
        
        
    }

    public IEnumerator CreatBollC()
    {
        yield return new WaitForSeconds(timeInterval);
        BallMachine.Instance.CreatBall();
    }

    private void Awake()
    {
        ballMatchineUI = GameObject.Find("Canvas/FaBg");
        allUI = ballMatchineUI.gameObject.GetComponentsInChildren<Transform>();
        for (int i = 0;i < allUI.Length; i++)
        {
            //Debug.Log(i+"-->"+allUI[i].name);
        }

        pointText = allUI[13].gameObject.GetComponent<Text>();//13
        speedText = allUI[14].gameObject.GetComponent<Text>();//14
        rotationalXText = allUI[15].gameObject.GetComponent<Text>();//15
        rotationalYText = allUI[16].gameObject.GetComponent<Text>();//16
        rotationalZText = allUI[17].gameObject.GetComponent<Text>();//17
        rotationalSpeedText = allUI[18].gameObject.GetComponent<Text>();//18
        fireTimeText = allUI[19].gameObject.GetComponent<Text>();//19
        cycleNumbetText = allUI[20].gameObject.GetComponent<Text>();//20

        pointSlider = allUI[31].gameObject.GetComponent<Slider>();//31
        speedSlider = allUI[37].gameObject.GetComponent<Slider>();//37
        rotationDirectionXSlider = allUI[43].gameObject.GetComponent<Slider>();//43
        rotationDirectionYSlider = allUI[49].gameObject.GetComponent<Slider>();//49
        rotationDirectionZSlider = allUI[55].gameObject.GetComponent<Slider>();//55
        rotationSpeedSlider = allUI[61].gameObject.GetComponent<Slider>();//61
        timeIntervalSlider = allUI[67].gameObject.GetComponent<Slider>();//67
        cycleNumberSlider = allUI[73].gameObject.GetComponent<Slider>();//73

        topSpinButton = allUI[22].gameObject.GetComponent<Button>();//22
        bottomSpinButton=allUI[24].gameObject.GetComponent<Button>();//24
        begainButton = allUI[26].gameObject.GetComponent<Button>();//26
        endButton= allUI[28].gameObject.GetComponent<Button>();//28
    }
    private void Start()
    {
        
        pointSlider.onValueChanged.AddListener(OnPoint);
        speedSlider.onValueChanged.AddListener(OnSpeed);
        rotationDirectionXSlider.onValueChanged.AddListener(OnRotationDirectionX);
        rotationDirectionYSlider.onValueChanged.AddListener(OnRotationDirectionY);
        rotationDirectionZSlider.onValueChanged.AddListener(OnRotationDirectionZ);
        rotationSpeedSlider.onValueChanged.AddListener(OnRotationSpeed);
        timeIntervalSlider.onValueChanged.AddListener(OnTimeInterval);
        cycleNumberSlider.onValueChanged.AddListener(OnCycleNumber);

        topSpinButton.onClick.AddListener(OnTopSpin);
        bottomSpinButton.onClick.AddListener(OnBottomSpin);
        begainButton.onClick.AddListener(OnStart);
        endButton.onClick.AddListener(OnEnd);
    }
    private void Update()
    {
        Booting();
    }


    public void OnPoint(float value)
    {
        pointText.text = value.ToString();
        point = (int)value;
    }
    public void OnSpeed(float value)
    {
        speedText.text = value.ToString();
        speed = (int)value;
    }
    public void OnRotationDirectionX(float value)
    {
        rotationalXText.text = value.ToString();
        rotaionVector.x = value;
    }
    public void OnRotationDirectionY(float value)
    {
        rotationalYText.text = value.ToString();
        rotaionVector.y = value;
    }
    public void OnRotationDirectionZ(float value)
    {
        rotationalZText.text = value.ToString();
        rotaionVector.z = value;
    }
    public void OnRotationSpeed(float value)
    {
        rotationalSpeedText.text = value.ToString();
        rotaionSpeed = (int)value;
    }
    public void OnTimeInterval(float value)
    {
        fireTimeText.text = value.ToString();
        timeInterval=value;
    }
    public void OnCycleNumber(float value)
    {
        cycleNumbetText.text = value.ToString();
        cyclesNumber =(int) value;
    }
    public void OnTopSpin()
    {

    }
    public void OnBottomSpin()
    {

    }
    public void OnStart()
    {
        start=true;
    }
    public void OnEnd()
    {
        end=true;
        start=false;
    }
}
