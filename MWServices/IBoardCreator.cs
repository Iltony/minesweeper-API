using MWEntities;

namespace MWServices
{
    public interface IBoardCreator
    {
        Board GenerateBoard(Cell initialClickCell,  User user, int columns, int rows, int mines);

    }
}
