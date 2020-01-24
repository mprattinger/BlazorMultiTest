using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp2.Data
{
    public class DataState
    {
        public event Func<string, DataModel, Task> Notify;

        public async Task Update(string id, DataModel data)
        {
            if (Notify != null)
            {
                await Notify.Invoke(id, data);
            }
        }
    }
}
