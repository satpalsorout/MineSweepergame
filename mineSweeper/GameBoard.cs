using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mineSweeper
{
  public  class GameBoard
    {
        public int VLimit { get; set; }
        public int HLimit { get; set; }

        public List<CellInfo> cellList = new List<CellInfo>();
        public GameBoard(int vLimit, int hLimit)
        {
            VLimit = vLimit;
            HLimit = hLimit;
            CreateCells();
        }
        public int getNumberOfMines(int total)
        {
          return  total*35 / 100;

        }

        public List<Cell> listMine = new List<Cell>();
        public void GenerateMines(int total, int limit)
        {
            listMine = new List<Cell>();
            int nmine = getNumberOfMines(total);
            Random r = new Random();
            for (int i=1;i<nmine; i++)
            {
               
                Cell cell = new Cell();
                cell.HCordinate = r.Next(1, limit);
                cell.VCordinate = r.Next(1, limit);
                listMine.Add(cell);
            }
        }
        public void CreateCells()
        {
            GenerateMines(VLimit * HLimit, HLimit);

            for (int i = 1; i <= VLimit;i++)
            {
             
                for (int j = 1; j <= HLimit; j++)
                {
                    CellInfo cellinfo = new CellInfo();
                    cellinfo.cell.HCordinate = j;
                    cellinfo.cell.VCordinate = i;
                    if (listMine.Where(x => x.HCordinate.Equals(j) && x.VCordinate.Equals(i)).Count() > 0)
                    {
                        SetMine(cellinfo);
                    }
                    else
                    {
                        cellinfo.cell.value = "*";
                    }
                    cellList.Add(cellinfo);
                }

            }

        }

        public void startNewGame()
        {
            cellList = new List<CellInfo>();
            CreateCells();
            PrintBoard();
        }
        public CellInfo GetCell(int hc, int vc)
        {
            CellInfo cellInfo = cellList.Where(x => x.cell.VCordinate.Equals(vc) && x.cell.HCordinate.Equals(hc)).FirstOrDefault();
            return cellInfo;
        }

        public void SetMine(CellInfo cellInfo)
        {
            cellInfo.IsMine = true;
            cellInfo.cell.value = "!";
        }
        public void SetFlag(CellInfo cellInfo)
        {
            cellInfo.IsFlag = true;
        }
        public void SetOpen(CellInfo cellInfo)
        {
            cellInfo.IsOpen = true;
            var listAdjecent = GetNeighbourMineCount(cellInfo);
            int minecount = listAdjecent.Where(x => x.IsMine == true).Count();
            if (minecount > 0)
            {
                cellInfo.cell.value = minecount.ToString();
            }
            else
            {
                foreach(CellInfo cl in listAdjecent.Where(x=>x.IsOpen==false))
                {
                    int cnt= GetNeighbourMineCount(cl).Where(x=>x.IsMine==true).Count();
                    cl.IsOpen = true;
                    if (cnt > 0)
                    {
                        cl.cell.value = cnt > 0 ? cnt.ToString() : "O";
                    }
                    else
                    {
                         cl.cell.value = "O";
                        SetOpen(cl);
                        //if(cl.IsMine==false)
                        //{
                        //  foreach( var cls in GetNeighbourMineCount(cl))
                        //    {
                        //        if (cls.IsMine == false && GetNeighbourMineCount(cls).Where(x => x.IsMine == true).Count()==0)
                        //        {
                        //            int ncnt = GetNeighbourMineCount(cls).Where(x => x.IsMine == true).Count();
                        //            cls.IsOpen = true;
                        //            cls.cell.value = ncnt>0 ? ncnt.ToString() : "O";
                        //        }
                        //    }
                        //}

                    }
                }
                cellInfo.cell.value = "O";
            }
        }


       public List<CellInfo> GetNeighbourMineCount(CellInfo cellInfo)
        {
            //int count = 0;
            var listAdjecent = cellList.Where(x => x.cell.HCordinate.Equals(cellInfo.cell.HCordinate - 1) && x.cell.VCordinate.Equals(cellInfo.cell.VCordinate)
             || x.cell.HCordinate.Equals(cellInfo.cell.HCordinate + 1) && x.cell.VCordinate.Equals(cellInfo.cell.VCordinate)
             || x.cell.HCordinate.Equals(cellInfo.cell.HCordinate) && x.cell.VCordinate.Equals(cellInfo.cell.VCordinate - 1)
             || x.cell.HCordinate.Equals(cellInfo.cell.HCordinate - 1) && x.cell.VCordinate.Equals(cellInfo.cell.VCordinate - 1)
             || x.cell.HCordinate.Equals(cellInfo.cell.HCordinate) && x.cell.VCordinate.Equals(cellInfo.cell.VCordinate + 1)
             || x.cell.HCordinate.Equals(cellInfo.cell.HCordinate + 1) && x.cell.VCordinate.Equals(cellInfo.cell.VCordinate + 1)
             || x.cell.HCordinate.Equals(cellInfo.cell.HCordinate + 1) && x.cell.VCordinate.Equals(cellInfo.cell.VCordinate - 1)
             || x.cell.HCordinate.Equals(cellInfo.cell.HCordinate - 1) && x.cell.VCordinate.Equals(cellInfo.cell.VCordinate + 1)
            ).ToList();

            return listAdjecent;

            //if (cellList.Where(x => x.IsMine == true && x.cell.HCordinate.Equals(cellInfo.cell.HCordinate - 1) && x.cell.VCordinate.Equals(cellInfo.cell.VCordinate)).Count() > 0)
            //    count++;
            //if (cellList.Where(x => x.IsMine == true && x.cell.HCordinate.Equals(cellInfo.cell.HCordinate + 1) && x.cell.VCordinate.Equals(cellInfo.cell.VCordinate)).Count() > 0)
            //    count++;
            //if (cellList.Where(x => x.IsMine == true && x.cell.HCordinate.Equals(cellInfo.cell.HCordinate ) && x.cell.VCordinate.Equals(cellInfo.cell.VCordinate-1)).Count() > 0)
            //    count++;
            //if (cellList.Where(x => x.IsMine == true && x.cell.HCordinate.Equals(cellInfo.cell.HCordinate-1) && x.cell.VCordinate.Equals(cellInfo.cell.VCordinate - 1)).Count() > 0)
            //    count++;
            //if (cellList.Where(x => x.IsMine == true && x.cell.HCordinate.Equals(cellInfo.cell.HCordinate) && x.cell.VCordinate.Equals(cellInfo.cell.VCordinate + 1)).Count() > 0)
            //    count++;
            //if (cellList.Where(x => x.IsMine == true && x.cell.HCordinate.Equals(cellInfo.cell.HCordinate - 1) && x.cell.VCordinate.Equals(cellInfo.cell.VCordinate + 1)).Count() > 0)
            //    count++;
            //if (cellList.Where(x => x.IsMine == true && x.cell.HCordinate.Equals(cellInfo.cell.HCordinate + 1) && x.cell.VCordinate.Equals(cellInfo.cell.VCordinate + 1)).Count() > 0)
            //    count++;
            //if (cellList.Where(x => x.IsMine == true && x.cell.HCordinate.Equals(cellInfo.cell.HCordinate + 1) && x.cell.VCordinate.Equals(cellInfo.cell.VCordinate - 1)).Count() > 0)
            //    count++;

            //return count;
        }

        public void PrintBoard(bool isPass=false)
        {
            Console.WriteLine();
            for (int i = 1; i <= VLimit; i++)
            {
                for (int j = 1; j <= HLimit; j++)
                {
                    CellInfo cellInfo = GetCell(i,j);
                    if (cellInfo.IsOpen == false && isPass==false)
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(cellInfo.cell.value);
                    }
                }
                Console.WriteLine();
            }

  


        }

        public void NextMove()
        {
            if (cellList.Where(x => x.IsOpen.Equals(false)).Count() > 0)
            {
                if (cellList.Where(x => x.IsMine.Equals(false) && x.IsOpen.Equals(false)).Count() > 0)
                {
                   // Console.WriteLine("Enter the position to Open the cell :");
                    Console.WriteLine("Enter the Horizontal cell Position: ");
                    string Hl = Console.ReadLine();
                    Console.WriteLine("Enter the Vertical cell Position: ");
                    string Vl = Console.ReadLine();
                    if(string.IsNullOrEmpty(Hl) || string.IsNullOrEmpty(Vl) || Hl== "0" || Vl == "0")
                    {
                        Console.WriteLine("Invalid Move: ");
                        NextMove();
                    }
                        OpenCell(Convert.ToInt32(Hl), Convert.ToInt32(Vl));
                }
                else
                {
                    Console.WriteLine("You won ! ");
                    Console.WriteLine("Enter 5 to start a new game");
                    string num = Console.ReadLine();
                    if (num == "5")
                    {
                        startNewGame();
                        NextMove();
                    }
                }
            }
            else
            {
                Console.WriteLine("New game Started..");
                startNewGame();
                NextMove();
            }
        }
        public void OpenCell(int hc, int vc)
        {
          
            if (hc<=HLimit && vc<=VLimit)
            {
                CellInfo cellInfo = GetCell(hc, vc);
                if (cellInfo.IsOpen == true)
                {
                    Console.WriteLine("Not Allowed position is already open !");
                    PrintBoard();
                    NextMove();
                }
                else
                {
                    if (cellInfo.IsMine == true)
                    {
                        Console.WriteLine("you lost the game ! Try Again ...");
                        PrintBoard(true);
                        Console.WriteLine("New Game Started...");
                        startNewGame();
                        NextMove();
                    }
                    else
                    {
                        SetOpen(cellInfo);
                        PrintBoard();
                        NextMove();
                    }
                }
            }
            else
            {

                Console.WriteLine("inCorrect move !");
                PrintBoard();
                NextMove();
            }
        }

    }
}
