using BethanysPieShop2.Models;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShop2.App.Pages
{
    public partial class PieCard
    {
        [Parameter]
        public Pie? Pie { get; set; }
    }
}
