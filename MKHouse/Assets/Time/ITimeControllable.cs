public interface ITimeControllable
{
    void EndTimeChange();
    void GoToTime(int firstIndex, int secondIndex, float coef);
    void SaveTime(int index);
    void StartTimeChange();
}