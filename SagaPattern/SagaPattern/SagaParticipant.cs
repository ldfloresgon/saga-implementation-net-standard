namespace SagaPattern
{
    public abstract class SagaParticipant<TSaga> where TSaga : SagaData
    {
        public TSaga Data { get; set; }
        public abstract void OnSubscribe();
    }
}
