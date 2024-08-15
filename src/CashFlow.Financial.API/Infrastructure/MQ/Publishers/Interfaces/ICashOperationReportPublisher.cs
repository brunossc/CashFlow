namespace CashFlow.Financial.API.Infrastructure.MQ.Publishers.Interfaces
{
    public interface ICashOperationReportPublisher
    {
        void SendMessage(string message);
        void EnsureCreated();
    }
}