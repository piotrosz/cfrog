﻿using CookingFrog.Domain;
using Microsoft.AspNetCore.Components;

namespace CookingFrog.WebUI.Components;

public partial class QuantityDisplay
{
    [Parameter] 
    public required Quantity Quantity { get; set; }
}