using UnityEngine;

public class RuntimeDataManager : MonoBehaviour
{
    public static RuntimeDataManager Instance;

    public int SelectedSwordIndex = 0;
    public int GoodLeafCount = 0;
    public int CurrentHouseIndex = 0;
    public int Money = 0;
    public int Log = 0;
    public int Logs = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
