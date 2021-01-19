using UnityEngine; 
using UnityEngine.UI;
using System.Threading.Tasks;
using System;

public class moveActiveCardTo : MonoBehaviour
{
	public static moveActiveCardTo Ins;//make copy of class for calling from other class 
    [SerializeField] float speed = 20f;
    [SerializeField] private Text numberOfTrueAnswersText;
	GameObject DestObj;
	public AudioSource wrongAudio;
	public Color wrongColor;
	 public  persist_state state;
	CardMaker cardMaker;
	[HideInInspector]public int  numberOfCorrectAnswers;

	public void Awake()
	{
		Ins = this;
	}

	public void setAsDest(GameObject Obj) {
		if (state.activeCard.GetComponent<category>().cat == Obj.GetComponent<category>().cat)//this is how you get your cat;
		{
           DestObj = Obj;
           numberOfCorrectAnswers++;
           numberOfTrueAnswersText.text = numberOfCorrectAnswers.ToString() + "/8";
           Debug.Log("start moving");
          // state.SaveCardThatTheyUsed();
           //if number of Correct answers is equal to ten then go to play secne
		}
		else//when make rong move this is going to be shown
		{
			wrongAudio.Play();
			var buttonImg = Obj.GetComponent<Image>();
			buttonImg.color = wrongColor;
			state.remainedSortTime -= 0.5f;

			((Action)(()=> {
				buttonImg.color = new Color(255,255,255);
			})).runAfter(200);
		}
	}

    void Start()
    {
	    numberOfCorrectAnswers = 0;
		state = GameObject.Find("persist_State").GetComponent<persist_state>();
		cardMaker = GameObject.Find("sort").GetComponent<CardMaker>();


    }

    // Update is called once per frame
    void Update () {
        if (DestObj){
            float step = speed * Time.deltaTime;
			Transform source = state.activeCard.transform;
            source.position = Vector2.MoveTowards(source.position, DestObj.transform.position, step);  
            source.localScale = Vector2.MoveTowards(source.localScale,new Vector2(0.13f,0.13f),step/5);  
            if(Vector2.Distance( source.position,DestObj.transform.position) <= 0 ){
                DestObj = null;
                Debug.Log("stop moving");
				cardMaker.nextActiveCard();
				
                //gameObject.transform.position = new  Vector2(0,0);
                //gameObject.transform.localScale = new  Vector2(0.32f,0.32f);
            }
        }
    }
	
	

	
}


public static class ActionExtensions
{
    public static async void runAfter(this Action act, double delay)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(delay));
        act();
    }
}
