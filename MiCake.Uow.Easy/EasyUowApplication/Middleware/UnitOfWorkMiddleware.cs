using MiCake.Uow.Easy;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace EasyUowApplication.Middleware
{
    public class UnitOfWorkMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public UnitOfWorkMiddleware(RequestDelegate next, IUnitOfWorkManager unitOfWorkManager)
        {
            _next = next;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await _next(httpContext);
            var currentUow = _unitOfWorkManager.GetCurrentUnitOfWork();
            currentUow?.SaveChanges();
            currentUow?.Dispose();
        }
    }
}
