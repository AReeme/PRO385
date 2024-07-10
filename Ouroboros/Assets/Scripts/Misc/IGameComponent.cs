public interface IGameComponent
{
    void Initialize();
    void UpdateComponent();
    void Pause();
    void Resume();
    void End();
}