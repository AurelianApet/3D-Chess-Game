/***************************************************************
 * File: Board.cs
 * Created By: Syed Ghulam Akbar		Date: 27 June, 2005
 * Description: The main chess board. Board contain the chess cell
 * which will contain the chess pieces. Board also contains the methods
 * to get and set the user moves.
 ***************************************************************/

using System;
using System.Collections;
using System.Xml;

namespace ChessLibrary
{
	/// <summary>
	/// he main chess board. Board contain the chess cell
	/// which will contain the chess pieces. Board also contains the methods
	/// to get and set the user moves.
	/// </summary>
    [Serializable]
	public class Board
	{
		private Side m_WhiteSide, m_BlackSide;	// Chess board site object 
		private Cells m_cells;	// collection of cells in the board
        public const int MAX_COL = 9;
        public const int MAX_ROW = 8;
        public Board()
		{
            m_WhiteSide = new Side(Side.SideType.White);	// Makde white side
            m_BlackSide = new Side(Side.SideType.Black);	// Makde white side

			m_cells = new Cells();					// Initialize the chess cells collection
		}

        // Initialize the chess board and place piece on thier initial positions
        public void Init(int mode, int formationMode)
		{
			m_cells.Clear();		// Remove any existing chess cells
            
			// Build the 64 chess board cells
			for (int row=1; row<=MAX_ROW; row++)
				for (int col=1; col<=MAX_COL; col++)
				{
					m_cells.Add(new Cell(row,col));	// Initialize and add the new chess cell
				}
            const string abc = "ABCDEFGHIJ";
            // Now setup the board for black side
            
            if (Board.MAX_COL == 8)
            {
                // Now setup the board for black side
                m_cells["a1"].piece = new Piece(Piece.PieceType.Rook, m_BlackSide, "A1");
                m_cells["h1"].piece = new Piece(Piece.PieceType.Rook, m_BlackSide, "H1");
                m_cells["b1"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide, "B1");
                m_cells["g1"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide, "G1");
                m_cells["c1"].piece = new Piece(Piece.PieceType.Bishop, m_BlackSide, "C1");
                m_cells["f1"].piece = new Piece(Piece.PieceType.Bishop, m_BlackSide, "F1");
                m_cells["e1"].piece = new Piece(Piece.PieceType.King, m_BlackSide, "E1");
                m_cells["d1"].piece = new Piece(Piece.PieceType.Queen, m_BlackSide, "D1");
                for (int col = 1; col <= MAX_COL; col++)
                    m_cells[2, col].piece = new Piece(Piece.PieceType.Pawn, m_BlackSide, abc[col - 1] + "2");

                // Now setup the board for white side
                m_cells["a" + MAX_ROW].piece = new Piece(Piece.PieceType.Rook, m_WhiteSide, "A" + MAX_ROW);
                m_cells["h" + MAX_ROW].piece = new Piece(Piece.PieceType.Rook, m_WhiteSide, "H" + MAX_ROW);
                m_cells["b" + MAX_ROW].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide, "B" + MAX_ROW);
                m_cells["g" + MAX_ROW].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide, "G" + MAX_ROW);
                m_cells["c" + MAX_ROW].piece = new Piece(Piece.PieceType.Bishop, m_WhiteSide, "C" + MAX_ROW);
                m_cells["f" + MAX_ROW].piece = new Piece(Piece.PieceType.Bishop, m_WhiteSide, "F" + MAX_ROW);
                m_cells["e" + MAX_ROW].piece = new Piece(Piece.PieceType.King, m_WhiteSide, "E" + MAX_ROW);
                m_cells["d" + MAX_ROW].piece = new Piece(Piece.PieceType.Queen, m_WhiteSide, "D" + MAX_ROW);
                for (int col = 1; col <= MAX_COL; col++)
                    m_cells[MAX_ROW - 1, col].piece = new Piece(Piece.PieceType.Pawn, m_WhiteSide, abc[col - 1] + "" + (MAX_ROW - 1));
            }
            else
            {
                if (mode == 1 || (mode == 4 && formationMode == 2) || (mode == 5 && formationMode == 0))
                {
                    m_cells["a2"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide, "A2");
                    m_cells["b2"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide, "B2");
                    m_cells["c2"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide, "C2");
                    m_cells["g2"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide, "G2");
                    m_cells["h2"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide, "H2");
                    m_cells["i2"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide, "I2");
                }
                else if (mode == 2 || (mode == 3 && formationMode == 0) || (mode == 4 && formationMode == 1))
                {
                    m_cells["c1"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide, "C1");
                    m_cells["g1"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide, "G1");
                    m_cells["c3"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide, "C3");
                    m_cells["d3"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide, "D3");
                    m_cells["f3"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide, "F3");
                    m_cells["g3"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide, "G3");
                }
                else if(mode == 0 || (mode == 3 && formationMode == 2) || (mode == 5 && formationMode == 1))
                {
                    m_cells["a1"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide, "A1");
                    m_cells["b1"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide, "B1");
                    m_cells["c1"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide, "C1");
                    m_cells["g1"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide, "G1");
                    m_cells["h1"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide, "H1");
                    m_cells["i1"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide, "I1");
                }

                m_cells["d1"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide, "D1");
                m_cells["e1"].piece = new Piece(Piece.PieceType.King, m_BlackSide, "E1");
                m_cells["f1"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide, "F1");
                for (int col = 4; col <= 6; col++)
                    m_cells[2, col].piece = new Piece(Piece.PieceType.Pawn, m_BlackSide, abc[col - 1] + "2");

                // Now setup the board for white side
                if (mode == 1 || (mode == 4 && formationMode == 1) || (mode == 5 && formationMode == 1))
                {
                    m_cells["a" + (MAX_ROW - 1)].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide, "A" + (MAX_ROW - 1));
                    m_cells["b" + (MAX_ROW - 1)].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide, "B" + (MAX_ROW - 1));
                    m_cells["c" + (MAX_ROW - 1)].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide, "C" + (MAX_ROW - 1));
                    m_cells["g" + (MAX_ROW - 1)].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide, "G" + (MAX_ROW - 1));
                    m_cells["h" + (MAX_ROW - 1)].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide, "H" + (MAX_ROW - 1));
                    m_cells["i" + (MAX_ROW - 1)].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide, "I" + (MAX_ROW - 1));
                }
                else if (mode == 2 || (mode == 3 && formationMode == 2) || (mode == 4 && formationMode == 2))
                {
                    m_cells["c" + MAX_ROW].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide, "C" + MAX_ROW);
                    m_cells["g" + MAX_ROW].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide, "G" + MAX_ROW);
                    m_cells["c" + (MAX_ROW - 2)].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide, "C" + (MAX_ROW - 2));
                    m_cells["d" + (MAX_ROW - 2)].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide, "D" + (MAX_ROW - 2));
                    m_cells["f" + (MAX_ROW - 2)].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide, "F" + (MAX_ROW - 2));
                    m_cells["g" + (MAX_ROW - 2)].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide, "G" + (MAX_ROW - 2));
                }
                else if (mode == 0 || (mode == 3 && formationMode == 0) || (mode == 5 && formationMode == 0))
                {
                    m_cells["a" + MAX_ROW].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide, "A" + MAX_ROW);
                    m_cells["b" + MAX_ROW].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide, "B" + MAX_ROW);
                    m_cells["c" + MAX_ROW].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide, "C" + MAX_ROW);
                    m_cells["g" + MAX_ROW].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide, "G" + MAX_ROW);
                    m_cells["h" + MAX_ROW].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide, "H" + MAX_ROW);
                    m_cells["i" + MAX_ROW].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide, "I" + MAX_ROW);
                }

                m_cells["d" + MAX_ROW].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide, "D" + MAX_ROW);
                m_cells["e" + MAX_ROW].piece = new Piece(Piece.PieceType.King, m_WhiteSide, "E" + MAX_ROW);
                m_cells["f" + MAX_ROW].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide, "F" + MAX_ROW);
                for (int col = 4; col <= 6; col++)
                    m_cells[MAX_ROW - 1, col].piece = new Piece(Piece.PieceType.Pawn, m_WhiteSide, abc[col - 1] + "" + (MAX_ROW - 1));
            }

            //m_cells[2, 1].piece = new Piece(Piece.PieceType.Pawn, m_WhiteSide, abc[1 - 1] + "" + 2);
            /*m_cells["f" + MAX_ROW].piece = new Piece(Piece.PieceType.Knight, m_BlackSide, "F" + MAX_ROW);
            m_cells["g" + MAX_ROW].piece = new Piece(Piece.PieceType.Knight, m_BlackSide, "G" + MAX_ROW);*/
        }

		// get the new item by rew and column
		public Cell this[int row, int col]
		{
			get
			{
				return m_cells[row, col];
			}
		}

		// get the new item by string location
		public Cell this[string strloc]
		{
			get
			{
				return m_cells[strloc];	
			}
		}

		// get the chess cell by given cell
		public Cell this[Cell cellobj]
		{
			get
			{
				return m_cells[cellobj.ToString()];	
			}
		}

        /// <summary>
        /// Serialize the Game object as XML String
        /// </summary>
        /// <returns>XML containing the Game object state XML</returns>
        public XmlNode XmlSerialize(XmlDocument xmlDoc)
        {
            XmlElement xmlBoard = xmlDoc.CreateElement("Board");

            // Append game state attributes
            xmlBoard.AppendChild(m_WhiteSide.XmlSerialize(xmlDoc));
            xmlBoard.AppendChild(m_BlackSide.XmlSerialize(xmlDoc));

            xmlBoard.AppendChild(m_cells.XmlSerialize(xmlDoc));

            // Return this as String
            return xmlBoard;
        }

        /// <summary>
        /// DeSerialize the Board object from XML String
        /// </summary>
        /// <returns>XML containing the Board object state XML</returns>
        public void XmlDeserialize(XmlNode xmlBoard)
        {
            // Deserialize the Sides XML
            XmlNode side = XMLHelper.GetFirstNodeByName(xmlBoard, "Side");

            // Deserialize the XML nodes
            m_WhiteSide.XmlDeserialize(side);
            m_BlackSide.XmlDeserialize(side.NextSibling);

            // Deserialize the Cells
            XmlNode xmlCells = XMLHelper.GetFirstNodeByName(xmlBoard, "Cells");
            m_cells.XmlDeserialize(xmlCells);
        }

		// get all the cell locations on the chess board
		public ArrayList GetAllCells()
		{
			ArrayList CellNames = new ArrayList();

			// Loop all the squars and store them in Array List
			for (int row=1; row<=MAX_ROW; row++)
				for (int col=1; col<=MAX_COL; col++)
				{
					CellNames.Add(this[row,col].ToString()); // append the cell name to list
				}

			return CellNames;
		}

		// get all the cell containg pieces of given side
        public ArrayList GetSideCell(Side.SideType PlayerSide)
		{
			ArrayList CellNames = new ArrayList();

			// Loop all the squars and store them in Array List
			for (int row=1; row<=MAX_ROW; row++)
				for (int col=1; col<=MAX_COL; col++)
				{
					// check and add the current type cell
					if (this[row,col].piece!=null && !this[row,col].IsEmpty() && this[row,col].piece.Side.type == PlayerSide)
						CellNames.Add(this[row,col].ToString()); // append the cell name to list
				}

			return CellNames;
		}

		// Returns the cell on the top of the given cell
		public Cell TopCell(Cell cell)
		{
			return this[cell.row-1, cell.col];
		}

		// Returns the cell on the left of the given cell
		public Cell LeftCell(Cell cell)
		{
			return this[cell.row, cell.col-1];
		}

		// Returns the cell on the right of the given cell
		public Cell RightCell(Cell cell)
		{
			return this[cell.row, cell.col+1];
		}

		// Returns the cell on the bottom of the given cell
		public Cell BottomCell(Cell cell)
		{
			return this[cell.row+1, cell.col];
		}

		// Returns the cell on the top-left of the current cell
		public Cell TopLeftCell(Cell cell)
		{
			return this[cell.row-1, cell.col-1];
		}

		// Returns the cell on the top-right of the current cell
		public Cell TopRightCell(Cell cell)
		{
			return this[cell.row-1, cell.col+1];
		}

		// Returns the cell on the bottom-left of the current cell
		public Cell BottomLeftCell(Cell cell)
		{
			return this[cell.row+1, cell.col-1];
		}

		// Returns the cell on the bottom-right of the current cell
		public Cell BottomRightCell(Cell cell)
		{
			return this[cell.row+1, cell.col+1];
		}
	}
}
