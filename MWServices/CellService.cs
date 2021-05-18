using AutoMapper;
using MWEntities;
using MWPersistence;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MWServices
{
    public class CellService : ICellService
    {
        private IBoardRepository _boardRepository;
        private IUserRepository _userRepository;
        private IServicesResourceManager _serviceResourceManager;
        private IMapper _iMapper;
        private IBoardCreator _gameProcessor;
        private ICellResolver _cellResolver;
        private IGameStatusResolver _gameStatusResolver;
        public CellService(
            IBoardRepository boardRepository,
            IUserRepository userRepository,
            IServicesResourceManager serviceResourceManager,
            IMapper iMapper,
            IBoardCreator gameProcessor,
            ICellResolver cellResolver,
            IGameStatusResolver gameStatusResolver)
        {
            _boardRepository = boardRepository;
            _userRepository = userRepository;
            _serviceResourceManager = serviceResourceManager;
            _iMapper = iMapper;
            _gameProcessor = gameProcessor;
            _cellResolver = cellResolver;
            _gameStatusResolver = gameStatusResolver;
        }

        public async Task<Board> SaveBoardAsync(Board board)
        {
            if ((board?.GameStatus != GameStatus.Active) || (board?.Owner?.Username == null))
            {
                throw new InvalidBoardException(_serviceResourceManager.ResourceManager);
            }

            var persistibleBoard = _iMapper.Map<Board, PersistibleBoard>(board);
            var result = await _boardRepository.SaveBoardAsync(persistibleBoard);

            return _iMapper.Map<PersistibleBoard, Board> (result);
        }

        public async Task<Board> ResumeAsync(Guid boardId, string currentUserName)
        {
            var persistibleBoard = await _boardRepository.GetBoardAsync(currentUserName, boardId);
            if ((persistibleBoard == null))
            {
                throw new InvalidBoardException(_serviceResourceManager.ResourceManager);
            }

            var board = _iMapper.Map<PersistibleBoard, Board>(persistibleBoard);
            if (board?.Owner.Username!= currentUserName)
            {
                throw new InvalidBoardForCurrentUserException(_serviceResourceManager.ResourceManager);
            }

            return board;
        }

        public async Task<Board> InitializeAsync(Cell initialClickCell, string username, int columns, int rows, int mines)
        {
            User user = await ValidateBeforeInitialize(initialClickCell, username, columns, rows);

            // Generates the board          
            var newBoard = _gameProcessor.GenerateBoard(initialClickCell, user, columns, rows, mines);

            // Process the first click
            newBoard = await CheckAsync(newBoard, initialClickCell.Column, initialClickCell.Row);

            return newBoard;
        }

        private async Task<User> ValidateBeforeInitialize(Cell initialClickCell, string username, int columns, int rows)
        {
            if (((initialClickCell?.Column ?? 0) >= columns) || ((initialClickCell?.Row ?? 0) >= rows))
            {
                throw new InvalidCellException(_serviceResourceManager.ResourceManager);
            }

            User user = null;

            // if the user is guest is not validated, otherwise it must exist in the database
            if (username != null)
            {
                user = await _userRepository.GetUserAsync(username);

                if (user == null)
                {
                    throw new InvalidUsernameException(_serviceResourceManager.ResourceManager);
                }
            }

            return user;
        }


        public async Task<Board> CheckAsync(Board board, int cellColumn, int cellRow)
        {
            if ((cellColumn < 1 || cellColumn > board.Columns)
                || (cellRow < 1 || cellRow > board.Rows))
            {
                throw new InvalidCellException(_serviceResourceManager.ResourceManager);
            }

            var cell = board.Cells.Where(c => c.Column == cellColumn && c.Row == cellRow).FirstOrDefault();

            if (cell.ItIsAMine)
            {
                board.GameStatus = GameStatus.GameOver;
            }
            else { 
                 //Resolve the cell and adjacents
                _cellResolver.ResolveCell(board.Cells, cell, board.Columns, board.Rows);
            }
                                
            await _gameStatusResolver.EvaluateGameStatus(board);

            return board;
        }

        public async Task<Board> FlagAsync(Board board, int cellColumn, int cellRow)
        {
            if ((cellColumn < 1 || cellColumn > board.Columns)
                  || (cellRow < 1 || cellRow > board.Rows))
            {
                throw new InvalidCellException(_serviceResourceManager.ResourceManager);
            }

            var affectedCell = board.Cells.Where(c => c.Column == cellColumn && c.Row == c.Row).FirstOrDefault();
            CellStatus? newStatus = null;
            
            switch (affectedCell.Status)
            {
                case CellStatus.Clear:
                    newStatus = CellStatus.Flagged;
                    break;
                case CellStatus.Flagged:
                    newStatus = CellStatus.Suspicious;
                    break;
                case CellStatus.Suspicious:
                    newStatus = CellStatus.Clear;
                    break;
                case CellStatus.Revealed:
                default:
                    //otherwise does nothing
                    break;
            }

            if (newStatus.HasValue)
            {
                board.Cells.Where(c => c.Column == cellColumn && c.Row == c.Row).FirstOrDefault().Status = newStatus.Value;
            }

            await _gameStatusResolver.EvaluateGameStatus(board);

            return board;
        }
    }
}
