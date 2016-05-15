using System;


[Serializable]
public class PlayerData
{

    private static PlayerData instance;

    private PlayerData() { }

    public static PlayerData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerData();
            }
            return instance;
        }
    }

    public string SceneName = "";
    public int CheckPointID = -1;

    public float m_maxLife = 0f;
    public float m_maxLightEnergy = 0f;
    public float m_maxJumpEnergy = 0;
    public int m_coins = 0;
}