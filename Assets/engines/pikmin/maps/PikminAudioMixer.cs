using UnityEngine;

public class PikminAudioMixer : MonoBehaviour {
  public AudioClip Day;
  public AudioClip DayWithDrums;
  public AudioClip Night;
  public AudioClip NightWithDrums;

  public float DayToNight;
  public float DrumAmount;

  private AudioSource daySource_;
  private AudioSource dayDrumSource_;
  private AudioSource nightSource_;
  private AudioSource nightDrumSource_;

  // Start is called before the first frame update
  public void Start() {
    this.daySource_ = this.gameObject.AddComponent<AudioSource>();
    this.dayDrumSource_ = this.gameObject.AddComponent<AudioSource>();
    this.nightSource_ = this.gameObject.AddComponent<AudioSource>();
    this.nightDrumSource_ = this.gameObject.AddComponent<AudioSource>();

    this.UpdateVolume_();

    this.daySource_.clip = this.Day;
    this.dayDrumSource_.clip = this.DayWithDrums;
    this.nightSource_.clip = this.Night;
    this.nightDrumSource_.clip = this.NightWithDrums;

    this.daySource_.loop = true;
    this.dayDrumSource_.loop = true;
    this.nightSource_.loop = true;
    this.nightDrumSource_.loop = true;

    this.daySource_.Play();
    this.dayDrumSource_.Play();
    this.nightSource_.Play();
    this.nightDrumSource_.Play();
  }

  // Update is called once per frame
  public void Update() => this.UpdateVolume_();

  private void UpdateVolume_() {
    this.daySource_.volume = (1 - this.DayToNight) * (1 - this.DrumAmount);
    this.dayDrumSource_.volume = (1 - this.DayToNight) * this.DrumAmount;
    this.nightSource_.volume = this.DayToNight * (1 - this.DrumAmount);
    this.nightDrumSource_.volume = this.DayToNight * this.DrumAmount;
  }
}