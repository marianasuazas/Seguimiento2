using System.Reflection;
using System.Runtime.ExceptionServices;

namespace SGCAE.Application.Utilities.Mediator;

// Mediador propio: recibe una petición, busca el caso de uso registrado que la resuelve y lo ejecuta.
public class SimpleMediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public SimpleMediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
    {
        // Arma el tipo del caso de uso (IRequestHandler<petición, respuesta>) y lo busca registrado.
        Type handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        object? handler = _serviceProvider.GetService(handlerType);

        if (handler is null)
        {
            throw new MediatorException($"No hay un caso de uso registrado para {request.GetType().Name}.");
        }

        try
        {
            // Ejecuta su método Handle (por reflexión, porque el tipo se conoce al ejecutar).
            MethodInfo method = handlerType.GetMethod("Handle")!;
            return await (Task<TResponse>)method.Invoke(handler, new object[] { request })!;
        }
        catch (TargetInvocationException ex) when (ex.InnerException is not null)
        {
            // La reflexión envuelve los errores; se desenvuelven para que llegue la excepción original.
            ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
            throw;
        }
    }
}
