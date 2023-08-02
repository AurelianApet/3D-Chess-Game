using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialScreen : MonoBehaviour
{
    public static TutorialScreen ins;
    public GameObject Parent;
    public int _curPage = 0;
    public Text _pageNo;
    public GameObject cancelBtn, backBtn, nextBtn;
    void Awake()
    {
        if (ins == null)
        {
            ins = this;
        }
    }

    void Start()
    {
//		_curPage = 0;
        // Reset();
    }

    void OnEnable()
    {
//		_curPage = 0;

        Reset();

        if (PlayerPrefs.GetInt("IsFirst") != 1)
        {
            PlayerPrefs.SetInt("IsFirst", 1);	
//			OnClickBtn (true);
        }
    }

    void Reset()
    {
        backBtn.SetActive(false);
        nextBtn.SetActive(true);
        _curPage = 0;

        // We don't want to turn Of First Page nad Last Gameobject as it is Page Number
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(0).gameObject.SetActive(true);
//        Parent.SetActive(false);
    }

    public void OnClickBtn(bool _isNext)
    {
        transform.GetChild(_curPage).gameObject.SetActive(false);
       
        if (_isNext)
        {
            _curPage++;
        }
        else
        {
            _curPage--;
        }

        if (_curPage == 7)
        {
            nextBtn.SetActive(false);
        }else{
            nextBtn.SetActive(true);
        }
        if(_curPage == 0){
            backBtn.SetActive(false);
        }else
        {
            backBtn.SetActive(true);
        }
        //_curPage = _curPage % 9;
        
        transform.GetChild(_curPage).gameObject.SetActive(true);
        _pageNo.text = (_curPage + 1).ToString();
    }

    public void OnCancelBtn()
    {
        Reset();
    }
}
