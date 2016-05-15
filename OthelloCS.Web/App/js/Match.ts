class Match {
    CurrentPlayer: number;
    MatchId: string;
    GameMode: number;
    Gameboard: IGameboard;
    Players: IPlayer[];

    constructor() {
        this.Players = [];
    }
}