using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorSample
{
    public static class Interop
    {
        internal static ValueTask<object> DemoWorkspace(
            IJSRuntime jsRuntime,
            ElementReference blocklyDiv,
            ElementReference toolbox,
            ElementReference startBlocks)
        {
            return jsRuntime.InvokeAsync<object>(
                "BlocklyFunctions.createDemoWorkspace",
                blocklyDiv,
                toolbox,
                startBlocks);
        }
    }
}