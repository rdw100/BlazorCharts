﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using Blazor.Charts.ChartJs.Entities;

namespace Blazor.Charts.ChartJs.Components
{
    /// <summary>
    /// Represents a Blazor Chart component that calls JavaScript 
    /// from C# by injecting the IJSRuntime service.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    public class LineChartBase : ComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public ChartTypes Types { get; set; }

        [Parameter]
        public string[] Data { get; set; }

        [Parameter]
        public string[] BackgroundColor { get; set; }

        [Parameter]
        public string[] BorderColor { get; set; } = new[] { "rgb(255, 99, 132)" };

        [Parameter]
        public int BorderWidthNum { get; set; } = 0;

        [Parameter]
        public string[] Labels { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string DatasetTitle { get; set; } = "My First Dataset";

        /// <summary>
        /// Instantiates the Chart class with all configurable options.
        /// Method invoked after each time the component has been rendered. Note that the component does
        /// not automatically re-render after the completion of any returned <see cref="T:System.Threading.Tasks.Task" />, because
        /// that would cause an infinite render loop.
        /// </summary>
        /// <param name="firstRender">Set to <c>true</c> if this is the first time <see cref="M:Microsoft.AspNetCore.Components.ComponentBase.OnAfterRender(System.Boolean)" /> has been invoked
        /// on this component instance; otherwise <c>false</c>.</param>
        /// <remarks>
        /// The <see cref="M:Microsoft.AspNetCore.Components.ComponentBase.OnAfterRender(System.Boolean)" /> and <see cref="M:Microsoft.AspNetCore.Components.ComponentBase.OnAfterRenderAsync(System.Boolean)" /> lifecycle methods
        /// are useful for performing interop, or interacting with values received from <c>@ref</c>.
        /// Use the <paramref name="firstRender" /> parameter to ensure that initialization work is only performed
        /// once.
        /// </remarks>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var config = new
            {
                Type = Types.ToString().ToLower(),
                Options = new
                {
                    Responsive = true,
                    //Title = new
                    //{
                    //    Display = true,
                    //    Text = Title
                    //},
                    Scales = new
                    {
                        YAxes = new[]
                        {
                        new { Ticks = new {
                            BeginAtZero=true
                        } }
                    }
                    }
                },
                Data = new
                {
                    Datasets = new[]
                    {
                    new {
                            Data = Data,
                            BackgroundColor = BackgroundColor,
                            BorderColor = BorderColor,
                            BorderWidth = BorderWidthNum,
                            Label = DatasetTitle
                    }
                },
                    Labels = Labels
                }
            };

            // Inject the IJSRuntime abstraction into a class (.cs)
            await JSRuntime.InvokeVoidAsync("setup", Id, config);
        }
    }
}
