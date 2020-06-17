using System;

namespace ConsoleApplication1.Enity
{
    public class Transaction
    {
        public string TransactionCode { get; set; }
        public string SenderAccountNumber { get; set; }
        public string ReceiverAccountNumber { get; set; }
        public TransactionType Type { get; set; }
        public double Amount { get; set; }
        public double Fee { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TransactionStatus TransactionStatus { get; set; }

        public override string ToString()
        {
            string return_string = TransactionCode + " | " + SenderAccountNumber + " | " + ReceiverAccountNumber +
                                   " | " + Amount + " | " + Fee + " | " + Message + " | ";
            string final_return_string = return_string + CreatedAt + " | " + UpdatedAt + " | " +
                                         (TransactionStatus) TransactionStatus;
            return final_return_string;
        }
    }


    public enum TransactionType
    {
        WITHDRAW = 1,
        DEDPOSIT = 2,
        TRANSFER = 3
    }

    public enum TransactionStatus
    {
        PENDING = 1,
        DONE = 2,
        FAILS = 0
    }
}