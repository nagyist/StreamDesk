using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StreamDesk.Managed
{
    public interface IObjectDatabaseTag
    {
        List<IObjectDatabaseTag> SubItems { get; }
        string MenuTitle { get; set; }
        bool IsProvider { get; set; }
        bool IsPinned { get; set; }
        Stream StreamObject { get; set; }
        MediaType MediaType { get; set; }
        Provider ProviderObject { get; set; }
        Provider ParentProviderObject { get; set; }
        void CallSubItemsToProperArray();
    }
}
