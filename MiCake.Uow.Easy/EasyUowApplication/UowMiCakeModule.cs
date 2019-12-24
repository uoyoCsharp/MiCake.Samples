using MiCake.Core.Abstractions.Modularity;
using MiCake.Uow.Easy;

namespace EasyUowApplication
{
    [DependOn(typeof(MiCakeUowEasyModule))]
    public class UowMiCakeModule : MiCakeModule
    {
    }
}
