namespace Application.Common.Exceptions
{
    public class AppException : Exception
    {
        public AppException(string Message) : base(Message)
        { }
    }

    public class DbAccessException : AppException
    {
        public DbAccessException(string Message) : base(Message)
        { }
    }

    public class NotFoundException: AppException
    {
        public NotFoundException(string Message) : base(Message)
        {}
    }

    // Ownership Verification Exception
    public class OwnershipVerificationException : AppException
    {
        public OwnershipVerificationException(string message) : base(message)
        { }
    }

    // Insufficient Funds Exception
    public class InsufficientFundsException : AppException
    {
        public InsufficientFundsException(string message) : base(message)
        { }
    }

    // Transaction Failure Exception
    public class TransactionFailureException : AppException
    {
        public TransactionFailureException(string message) : base(message)
        { }
    }

    // Blockchain Connectivity Exception
    public class BlockchainConnectivityException : AppException
    {
        public BlockchainConnectivityException(string message) : base(message)
        { }
    }

    // Contract Deployment Exception
    public class ContractDeploymentException : AppException
    {
        public ContractDeploymentException(string message) : base(message)
        { }
    }

    public class MetadataValidationException : AppException
    {
        public MetadataValidationException(string message) : base(message)
        { }
    }

    public class EthereumVerificationException: AppException
    {
        public EthereumVerificationException(string Message) : base(Message)
        {}
    }

    public class DuplicateResourceException: AppException
    {
        public DuplicateResourceException(string Message) : base(Message)
        {}
    }
}
