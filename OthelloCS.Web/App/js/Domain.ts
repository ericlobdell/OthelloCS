
interface IGameboard {
    Positions: ICell[][];
}

interface ICell {
    Row: number,
    Column: number,
    PlayerNumber: number,
    IsTarget: boolean,
    IsHit: boolean,
    IsHighestScoring: boolean,
    Distance: number,
    PointValue: number
}

interface IPlayer {
    Name: string,
    Number: number,
    Score: number
}

interface INewMatchRequest {
    PlayerOneName: string,
    PlayerTwoName: string,
    GameMode: number
}

interface INewMatchResponse {
    CurrentPlayer: number,
    Players: IPlayer[],
    Gameboard: IGameboard,
    MatchId: string,
    GameMode: number
}

interface IMoveArguments {
    Row: number,
    Column: number
}

interface IMoveRequest {
    Row: number,
    Column: number,
    PlayerNumber: number,
    MatchId: string,
    GameMode: number,
    Gameboard: IGameboard,
    Players: IPlayer[]
}

interface IMoveResponse {
    Result: IMoveResult,
    Players: IPlayer[]
    Winner: IPlayer
}

interface IMoveResult {
    CurrentPlayer: number,
    Gameboard: IGameboard,
    MatchId: string,
    Captures: ICell[],
    IsEndOfMatch: boolean,
    ComputerMadeMove: boolean,
    ComputerMove: IMoveArguments,
    Criteria: string
}