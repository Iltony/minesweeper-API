using AutoMapper;
using MWEntities;
using MWPersistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWServices
{
    public class BoardService : IBoardService
    {
        private IBoardRepository _boardRepository;
        private IUserRepository _userRepository;
        private IServicesResourceManager _serviceResourceManager;
        private IMapper _iMapper;
        private IGameProcessor _gameProcessor;

        public BoardService(
            IBoardRepository boardRepository,
            IUserRepository userRepository,
            IServicesResourceManager serviceResourceManager,
            IMapper iMapper,
            IGameProcessor gameProcessor)
        {
            _boardRepository = boardRepository;
            _userRepository = userRepository;
            _serviceResourceManager = serviceResourceManager;
            _iMapper = iMapper;
            _gameProcessor = gameProcessor;
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
            newBoard = await _gameProcessor.CheckAsync(newBoard, initialClickCell.Columns, initialClickCell.Rows);

            return newBoard;
        }

        private async Task<User> ValidateBeforeInitialize(Cell initialClickCell, string username, int columns, int rows)
        {
            if (((initialClickCell?.Columns ?? 0) >= columns) || ((initialClickCell?.Rows ?? 0) >= rows))
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
    }
}
