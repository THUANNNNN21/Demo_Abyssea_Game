using UnityEngine;

[CreateAssetMenu(fileName = "SkillSO", menuName = "Scriptable Objects/SkillSO")]
public class SkillSO : ScriptableObject
{
    public string skillName = "Skill";
    public float cooldownTime;
    public PrefabToSpawnName prefabToSpawnName = PrefabToSpawnName.None;
}
public enum PrefabToSpawnName
{
    None = 0,
    Meteorite1 = 1,
    Meteorite2 = 2,
    NormalEnemy1 = 3,
}