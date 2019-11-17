using System;

namespace SagaPattern
{
    public abstract class SagaData
    {
        public Guid CorrelationId => Guid.NewGuid();
    }
}
