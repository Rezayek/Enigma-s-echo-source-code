public class PuzzelController : AbstractInteractor
{
    private void Start()
    {
        action = EnvActions.PuzzelAction;
    }
    public override EnvActions GetEvent()
    {
        return action;
    }
}
