using UnityEngine;

[CreateAssetMenu]
public class PlayerSettings : ScriptableObject
{
    public float speed = 10f;
    public float maxVel = 10f;
    public float groundDist = 0.6f;
    public float jumpForce = 5f;
    public float passSpeed = 5f;
    public float passHeight = 5f;
    public float spikeForce = 5f;
}
