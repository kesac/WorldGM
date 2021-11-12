using System;
using System.Collections.Generic;
using System.Text;

namespace WorldGM.Entities
{
    public interface Entity
    {
        uint Id { get; }
        string Name { get; }
    }
}
