using System;

using UnityEngine;

[RequireComponent(typeof(PikminAudioMixer))]
public class DayNightCycle : MonoBehaviour {
  private PikminAudioMixer audioMixer_;
  private float startTime_;

  public float dayLength = 50;

  // Start is called before the first frame update
  public void Start() {
    this.audioMixer_ = this.GetComponent<PikminAudioMixer>();

    this.startTime_ = Time.time;
  }

  // Update is called once per frame
  public void Update() {
    var currentTime = Time.time - this.startTime_;

    var totalTime = this.dayLength;
    var dayToNight = Mathf.Min(currentTime / totalTime, 1);

    this.audioMixer_.DayToNight = dayToNight;
  }
}