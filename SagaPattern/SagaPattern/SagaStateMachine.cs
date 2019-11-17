namespace SagaPattern
{
    public class SagaStateMachine<TSagaData>
        where TSagaData : SagaData
    {
        public readonly TSagaData Data;
        public SagaState.State State { get; private set; }

        public SagaStateMachine(SagaState.State state, TSagaData data)
        {
            this.State = state;
            this.Data = data;
        }
    }
}
