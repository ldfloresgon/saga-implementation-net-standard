namespace SagaPattern
{
    public class SagaState
    {
        public enum State
        {
            INIT = 0,
            PENDING = 1,
            OK = 2,
            NO_OK = 3
        }
    }
}
