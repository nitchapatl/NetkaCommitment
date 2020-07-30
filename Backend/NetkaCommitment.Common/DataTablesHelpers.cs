using NetkaCommitment.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetkaCommitment.Common
{
    public class DataTablesHelpers
    {
        public static object ConvertToServerSidePagination<T>(DataTableAjaxPostViewModel model, IEnumerable<T> collection)
        {
            return new DataTablesServerSideResult<T>
            {
                draw = model.draw,
                recordsTotal = model.recordsTotal,
                recordsFiltered = model.recordsFiltered,
                data = collection
            };
        }

    }
}
