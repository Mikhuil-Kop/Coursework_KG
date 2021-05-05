public interface ITimeControllable
{
    void EndTimeChange();
    void GoToTime(float delta);
    void SaveTime();
    void StartTimeChange();
}