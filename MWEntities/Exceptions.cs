using System;
using System.Resources;

namespace MWEntities
{
    public class DuplicatedUsernameException : Exception
    {
        public DuplicatedUsernameException(ResourceManager resourceManager) : base(resourceManager.GetString("DuplicatedUsername"))
        {
            
        }
    }

    public class InvalidUsernameException : Exception
    {
        public InvalidUsernameException(ResourceManager resourceManager) : base(resourceManager.GetString("InvalidUsername"))
        {
        }
    }

    public class InvalidBirthDateException : Exception
    {
        public InvalidBirthDateException(ResourceManager resourceManager) : base(resourceManager.GetString("InvalidBirthDate"))
        {
        }
    }

    public class InvalidBoardException : Exception
    {
        public InvalidBoardException(ResourceManager resourceManager) : base(resourceManager.GetString("InvalidBoard"))
        {
        }
    }

    public class InvalidBoardForCurrentUserException : Exception
    {
        public InvalidBoardForCurrentUserException(ResourceManager resourceManager) : base(resourceManager.GetString("InvalidBoardForCurrentUser"))
        {
        }
    }

    public class InvalidCellException : Exception
    {
        public InvalidCellException(ResourceManager resourceManager) : base(resourceManager.GetString("InvalidCell"))
        {
        }
    }

    public class GameNotStartedException : Exception
    {
        public GameNotStartedException(ResourceManager resourceManager) : base(resourceManager.GetString("GameNotStarted"))
        {
        }
    }

}
