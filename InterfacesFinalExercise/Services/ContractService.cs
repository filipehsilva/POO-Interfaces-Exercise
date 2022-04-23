using InterfacesFinalExercise.Entities;

namespace InterfacesFinalExercise.Services
{
    internal class ContractService
    {
        IOnlinePaymentService _paymentService;

        public ContractService(IOnlinePaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public void ProcessContract(Contract contract, int months)
        {
            double installmentValue = contract.TotalValue / months;

            for (int i = 1; i <= months; i++)
            {
                double interest = installmentValue + _paymentService.Interest(installmentValue, i);
                double paymentFee = interest + _paymentService.PaymentFee(interest);
                DateTime installmentDate = contract.Date.AddMonths(i);
                contract?.Installments?.Add(new Installment(installmentDate, paymentFee));
            }
        }
    }
}
