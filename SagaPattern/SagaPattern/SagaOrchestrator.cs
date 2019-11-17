using System.Collections.Generic;

namespace SagaPattern
{
    public abstract class SagaOrchestrator<TSagaData> where TSagaData : SagaData
    {
        protected List<SagaParticipant<TSagaData>> SagaParticipants = new List<SagaParticipant<TSagaData>>();

        protected SagaStateMachine<TSagaData> StateMachine { get; set; }

        public SagaOrchestrator<TSagaData> AddSagaParticipant(SagaParticipant<TSagaData> sagaParticipant)
        {
            this.SagaParticipants.Add(sagaParticipant);
            return this;
        }

        public void Initialize(TSagaData data)
        {
            this.StateMachine = new SagaStateMachine<TSagaData>(SagaState.State.PENDING, data);

            foreach (SagaParticipant<TSagaData> participant in SagaParticipants)
            {
                participant.Data = data;
                participant.OnSubscribe();
            }

        }

        public abstract void Execute();
    }
}
