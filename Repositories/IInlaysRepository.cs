using System;
using System.Collections.Generic;
using System.Text;
using Repositories.Models;

namespace Repositories
{
    public interface IInlaysRepository : IRepository<Inlays>
    {
        public void SetInlay(DateTime date);
        public Inlays[] GetArrayInlaysTospesificArivalDate(DateTime date);
    }
}
