﻿using System;
using System.Collections.Generic;

using BetterCms.Module.Root.Models;
using BetterCms.Module.Root.Mvc.Grids.GridOptions;

namespace BetterCms.Module.Pages.ViewModels.Filter
{
    public class PagesFilter : SearchableGridOptions
    {
        public List<LookupKeyValue> Tags { get; set; }
        public Guid? CategoryId { get; set; }
        public bool IncludeArchived { get; set; }
    }
}