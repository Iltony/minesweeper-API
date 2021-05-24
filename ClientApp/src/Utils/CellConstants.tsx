export const GameStatus = {
    Active: 0,
    GameOver: 1,
    Resolved: 2
};

export const CellStatus = {
    Clear: 0,
    Flagged:1,
    Suspicious:2,
    Revealed:3,
};

const GameConstats = { GameStatus:GameStatus, CellStatus:CellStatus }
export default GameConstats;