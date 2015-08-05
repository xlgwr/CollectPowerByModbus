using System;

namespace EPMCS.Model
{
    public interface IRowVersion
    {
        DateTime RowVersion { get; set; }
    }
}