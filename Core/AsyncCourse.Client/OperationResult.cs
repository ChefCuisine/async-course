using Vostok.Clusterclient.Core.Model;

namespace AsyncCourse.Client;

public class OperationResult<TResult>
{
    private readonly TResult result;
    
    protected OperationResult(TResult result, ResponseCode responseCode)
    {
        ResponseCode = responseCode;
        this.result = result;
    }

    public ResponseCode ResponseCode { get; }
    
    public virtual bool IsSuccessful => ResponseCode.IsSuccessful();

    public TResult Result
    {
        get
        {
            EnsureSuccess();
            return result;
        }
    }

    public virtual void EnsureSuccess()
    {
        if (!ResponseCode.IsSuccessful() && !ResponseCode.IsRedirection())
        {
            throw new Exception($"Api call failed with code: {ResponseCode}");
        }
    }
    
    internal static OperationResult<TResult> Error(ResponseCode responseCode)
    {
        return new OperationResult<TResult>(default, responseCode);
    }
    
    internal static OperationResult<TResult> Success(TResult result, ResponseCode responseCode = ResponseCode.Ok)
    {
        if (!responseCode.IsSuccessful() && !responseCode.IsRedirection())
        {
            throw new InvalidOperationException(
                $"Couldn't create successful {nameof(OperationResult<TResult>)} " +
                $"with unsuccessful response code {responseCode}");
        }

        return new OperationResult<TResult>(result, responseCode);
    }
}