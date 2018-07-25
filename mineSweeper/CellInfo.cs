using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mineSweeper
{
  public  class CellInfo
    {
        public Cell cell { get; set; }
        public Boolean IsOpen { get; set; }
        public Boolean IsFlag { get; set; }
        public Boolean IsMine { get; set; }

        public CellInfo()
        {
            cell = new Cell();

        }
    }
}
