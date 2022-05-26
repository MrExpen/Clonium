using GameLib.Exceptions;

namespace GameLib;

public class Board
{
    private readonly Cell[,] _map;
    private byte _playerTurn;
    public byte PlayersCount { get; }
    private byte _winner;

    public Cell this[int x, int y]
    {
        get
        {
            if (x >= 0 && x < LengthX &&
                y >= 0 && y < LengthY)
            {
                return _map[x, y];
            }

            return new Cell(CellState.Wall);
        }
    }

    public int LengthX => _map.GetLength(0);
    public int LengthY => _map.GetLength(1);

    public byte? PlayerTurn => _playerTurn switch
    {
        byte.MaxValue => null,
        _ => _playerTurn
    };
    
    public byte? Winner => _winner switch
    {
        byte.MaxValue => null,
        _ => _winner
    };

    private void _ChangeTurn()
    {
        byte nextTurn = (byte)((_playerTurn + 1) % PlayersCount);
        int alivePlayersBit = 0;
        for (int x = 0; x < LengthX; x++)
        {
            for (int y = 0; y < LengthY; y++)
            {
                if (_map[x, y].HasOwner)
                {
                    alivePlayersBit |= 1 << _map[x, y].Player;
                    if ((alivePlayersBit & 1 << nextTurn) != 0)
                    {
                        _playerTurn = nextTurn;
                        return;
                    }
                }
            }
        }

        do
        {
            nextTurn = (byte)((nextTurn + 1) % PlayersCount);
        } while ((alivePlayersBit & 1 << nextTurn) == 0);

        if (nextTurn == _playerTurn)
        {
            _winner = _playerTurn;
            _playerTurn = (byte)((_playerTurn + 1) % PlayersCount);
        }
        else
        {
            _playerTurn = nextTurn;
        }
    }

    public Cell[,] GetMapCopy() => (Cell[,])_map.Clone();
    public void MakeTurn(int x, int y)
    {
        if (Winner.HasValue || x < 0 || x >= LengthX || y < 0 || y >= LengthY ||
            !_map[x, y].HasOwner || PlayerTurn != _map[x, y].Player)
        {
            throw new InvalidTurnException();
        }
        
        _makeTurn(x, y);
        _ChangeTurn();
    }

    public void MakeTurn(Point point) => MakeTurn(point.X, point.Y);
    private void _makeTurn(int x, int y)
    {
        if (x < 0 || x >= LengthX || y < 0 || y >= LengthY || _map[x, y].State == CellState.Wall)
        {
            return;
        }

        _map[x, y].Player = _playerTurn;

        if (++_map[x, y].State != CellState.Four) return;
        _map[x, y].State = CellState.Zero;
        
        _makeTurn(x - 1, y);
        _makeTurn(x + 1, y);
        _makeTurn(x, y + 1);
        _makeTurn(x, y - 1);
    }

    public Board(string pattern="........\n.0....2.\n........\n........\n........\n........\n.3....1.\n........", byte playersCount = 2)
    {
        PlayersCount = playersCount;
        _winner = byte.MaxValue;
        
        var tmp = pattern.Split();

        if (tmp.Any(x => x.Length != tmp[0].Length) || playersCount is < 2 or > 4 || !pattern.Contains('0') ||
            !pattern.Contains('1')) 
        {
            throw new Exception();
        }

        int sizeY = tmp.Length;
        int sizeX = tmp[0].Length;

        _map = new Cell[sizeX, sizeY];
        for (var y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                switch (tmp[y][x])
                {
                    case '#':
                        _map[x, y] = new Cell(CellState.Wall);
                        break;
                    case '.':
                        _map[x, y] = new Cell(CellState.Zero);
                        break;
                    case <= '3' and >= '0':
                        var playerNumber = (byte)(tmp[y][x] - '0');
                        if (playerNumber < PlayersCount)
                        {
                            _map[x, y] = new Cell(CellState.Three, (byte)(tmp[y][x] - '0'));
                        }
                        else
                        {
                            _map[x, y] = new Cell(CellState.Zero);
                        }
                        break;
                    default:
                        throw new Exception();
                }
            }
        }
    }

    public Board(Board board)
    {
        PlayersCount = board.PlayersCount;
        _playerTurn = board._playerTurn;
        _winner = board._winner;
        _map = board.GetMapCopy();
    }
}

